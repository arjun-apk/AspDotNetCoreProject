namespace AspDotNetCoreProject.Data.MySQLQuery
{
    public class UserQuery
    {
        public const string GetUserByUsername = "SELECT * FROM users WHERE Username = @Username";

        public const string CreateUser = @"INSERT INTO users (Username, Password, FirstName, LastName, Email, EmailConfirmed, MobileNumber, MobileNumberConfirmed, IsActive, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn) VALUES (@Username, @Password, @FirstName, @LastName, @Email, @EmailConfirmed, @MobileNumber, @MobileNumberConfirmed, @IsActive, @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn); SELECT LAST_INSERT_ID();";
    }
}
