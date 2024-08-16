using AspDotNetCoreProject.Context.User;

namespace AspDotNetCoreProject.Context.Account
{
    public interface IAccountService
    {
        Task<long> Register(UserEntity user);
        Task<UserEntity?> Login(string username, string password);
        string GenerateJwtToken(UserEntity user);
    }
}
