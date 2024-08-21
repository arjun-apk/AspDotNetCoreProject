namespace AspDotNetCoreProject.Context.User
{
    public class UserEntity
    {
        public required long Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? MobileNumber { get; set; }
        public bool MobileNumberConfirmed { get; set; }
        public bool IsActive { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
