namespace AspDotNetCoreProject.Context.User
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserByUsername(string username);
        Task<long> CreateUser(UserEntity user);
    }
}
