namespace NexusBilling.Core.Application.Security.Contracts;

public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string plainPassword, string hashedPassword);
}
