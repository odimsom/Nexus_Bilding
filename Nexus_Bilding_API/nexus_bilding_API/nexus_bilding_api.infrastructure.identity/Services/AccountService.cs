using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using nexus_bilding_api.core.application.DTOs.Account;
using nexus_bilding_api.core.application.Exceptions;
using nexus_bilding_api.core.application.Interfaces;
using nexus_bilding_api.core.domain.Base;
using nexus_bilding_api.core.domain.Settings;
using nexus_bilding_api.infrastructure.identity.Entities;

namespace nexus_bilding_api.infrastructure.identity.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly JWTSettings _jwtSettings;
    private readonly IEmailService _emailService;

    public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JWTSettings> jwtSettings, IEmailService emailService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
        _emailService = emailService;
    }

    public async Task<Result<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<AuthenticationResponse>.Fail($"No Account Registered with {request.Email}");
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password, false, false);
        if (!result.Succeeded)
        {
            return Result<AuthenticationResponse>.Fail($"Error: Incorrect Password for {request.Email}");
        }

        if (!user.IsActive)
        {
            return Result<AuthenticationResponse>.Fail($"Account Deactivated for {request.Email}");
        }

        var response = await GenerateJwtToken(user);
        return Result<AuthenticationResponse>.Ok(response);
    }

    public async Task<Result<string>> RegisterAsync(RegisterRequest request, string origin)
    {
        var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
        if (userWithSameUserName != null)
        {
            return Result<string>.Fail($"Username '{request.UserName}' is already taken.");
        }

        var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
        if (userWithSameEmail != null)
        {
            return Result<string>.Fail($"Email '{request.Email}' is already registered.");
        }

        var user = new AppUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            IsActive = false, // User is inactive until email is confirmed
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            // Default Role assignment can go here
            // await _userManager.AddToRoleAsync(user, "Basic");

            // Send Verification Email
            var verificationUri = await SendVerificationEmail(user, origin);

            // For now, in a real scenario you just return "Please check your email"
            // But if we want to debug, we can log it or return it. 
            // We will stick to the requirement: Send email.

            return Result<string>.Ok(user.Id);
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<string>.Fail($"Error registering user: {errors}");
        }
    }

    public async Task<Result<string>> ConfirmEmailAsync(string userId, string code)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result<string>.Fail("User not found.");
        }

        code = Encoding.UTF8.GetString(Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlDecode(code));
        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (result.Succeeded)
        {
            user.IsActive = true;
            await _userManager.UpdateAsync(user);
            return Result<string>.Ok(user.Id);
        }
        else
        {
            return Result<string>.Fail("Error confirming email.");
        }
    }

    private async Task<string> SendVerificationEmail(AppUser user, string origin)
    {
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = Microsoft.AspNetCore.WebUtilities.WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        // Construct the URL. Assuming the API has an endpoint or the Frontend handles it.
        // Usually, the email link points to the Frontend, which then calls the API.
        // Let's assume origin comes from the header 'origin'.

        var route = "confirm-email";
        var endpointUri = new Uri(string.Concat($"{origin}/", route));
        var verificationUri = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(endpointUri.ToString(), "userId", user.Id);
        verificationUri = Microsoft.AspNetCore.WebUtilities.QueryHelpers.AddQueryString(verificationUri, "code", code);

        var emailRequest = new nexus_bilding_api.core.application.DTOs.Email.EmailRequestDTO
        {
            To = user.Email,
            Body = $"Please confirm your account by visiting this URL: {verificationUri}",
            Subject = "Confirm Registration"
        };

        try
        {
            await _emailService.SendAsync(emailRequest);
        }
        catch (Exception ex)
        {
            // Log but don't fail registration? Or fail? 
            // Better to not fail but maybe user has to resend.
        }

        return verificationUri;
    }

    private async Task<AuthenticationResponse> GenerateJwtToken(AppUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim("roles", role));
        }

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("uid", user.Id)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);

        return new AuthenticationResponse
        {
            Id = user.Id,
            JWToken = jwtToken,
            Email = user.Email!,
            UserName = user.UserName!,
            Roles = roles.ToList(),
            IsVerified = user.EmailConfirmed
        };
    }
}
