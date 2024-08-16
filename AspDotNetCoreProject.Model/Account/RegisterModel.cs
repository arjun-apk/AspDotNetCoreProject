﻿namespace AspDotNetCoreProject.Model.Account
{
    public class RegisterModel
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
    }
}
