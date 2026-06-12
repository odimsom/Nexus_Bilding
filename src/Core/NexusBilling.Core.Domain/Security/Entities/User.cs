using NexusBilling.Core.Domain.Common;
using NexusBilling.Core.Domain.Common.Errors;
using NexusBilling.Core.Domain.Common.Result;

namespace NexusBilling.Core.Domain.Security.Entities;

public class User : Entity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public TenantIdentifier? TenantId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string EmployeeNo { get; set; } = string.Empty;

    private User() { }

    public static OperationResult<User, DomainError> Create(
        string username,
        string email,
        string passwordHash,
        TenantIdentifier tenantId,
        string fullName = "",
        string employeeNo = "")
    {
        if (string.IsNullOrWhiteSpace(username))
            return OperationResult<User, DomainError>.Fail(
                DomainError.Validation("User.UsernameRequired", "Username is required."));

        if (string.IsNullOrWhiteSpace(email))
            return OperationResult<User, DomainError>.Fail(
                DomainError.Validation("User.EmailRequired", "Email is required."));

        if (string.IsNullOrWhiteSpace(passwordHash))
            return OperationResult<User, DomainError>.Fail(
                DomainError.Validation("User.PasswordRequired", "Password hash is required."));

        var user = new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            TenantId = tenantId,
            IsActive = true,
            FullName = fullName,
            EmployeeNo = employeeNo
        };

        return OperationResult<User, DomainError>.Ok(user);
    }

    public OperationResult<User, DomainError> ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            return OperationResult<User, DomainError>.Fail(
                DomainError.Validation("User.PasswordRequired", "New password hash is required."));

        PasswordHash = newPasswordHash;
        UpdatedAt = DateTime.UtcNow;

        return OperationResult<User, DomainError>.Ok(this);
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
    public void UpdateProfile(string fullName, string email, string employeeNo)
    {
        FullName = fullName;
        Email = email;
        EmployeeNo = employeeNo;
        UpdatedAt = DateTime.UtcNow;
    }
}
