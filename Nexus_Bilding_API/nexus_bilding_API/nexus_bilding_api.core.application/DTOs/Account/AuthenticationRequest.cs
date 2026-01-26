using System.Text.Json.Serialization;

namespace nexus_bilding_api.core.application.DTOs.Account;

public class AuthenticationRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthenticationResponse
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new();
    public bool IsVerified { get; set; }
    public string JWToken { get; set; } = string.Empty;
    [JsonIgnore]
    public string RefreshToken { get; set; } = string.Empty;
}
