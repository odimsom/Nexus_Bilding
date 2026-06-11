using NexusBilling.Core.Application.Security.Contracts;

namespace NexusBilling.Infrastructure.Identity.Security;

public sealed class BcryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
        => BCrypt.Net.BCrypt.EnhancedHashPassword(password, 12);

    public bool Verify(string plainPassword, string hashedPassword)
        => BCrypt.Net.BCrypt.EnhancedVerify(plainPassword, hashedPassword);
}
