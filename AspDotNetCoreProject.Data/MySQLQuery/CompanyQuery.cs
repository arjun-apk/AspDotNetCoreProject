namespace AspDotNetCoreProject.Data.MySQLQuery
{
    public static class CompanyQuery
    {
        public const string GetAllCompanies = "SELECT * FROM company WHERE IsDeleted = 0;";

        public const string GetCompanyById = "SELECT * FROM company WHERE CompanyId = @CompanyId AND IsDeleted = 0;";

        public const string GetCompanyByName = "SELECT * FROM company WHERE LOWER(Name) = LOWER(@CompanyName) AND IsDeleted = 0;";

        public const string GetCompanyByNameExceptId = "SELECT * FROM company WHERE LOWER(Name) = LOWER(@CompanyName) AND CompanyId != @CompanyId AND IsDeleted = 0;";

        public const string CreateCompany = "INSERT INTO company (Name, Email, MobileNumber, StreetNumber, StreetName, City, District, State, Country, Pincode, Latitude, Longitude, IsDeleted, CreatedBy, CreatedOn) VALUES (@Name, @Email, @MobileNumber, @StreetNumber, @StreetName, @City, @District, @State, @Country, @Pincode, @Latitude, @Longitude, 0, @CreatedBy, @CreatedOn);SELECT LAST_INSERT_ID();";

        public const string UpdateCompany = "UPDATE company SET Name = @Name, Email = @Email, MobileNumber = @MobileNumber, StreetNumber = @StreetNumber, StreetName = @StreetName, City = @City, District = @District, State = @State, Country = @Country, Pincode = @Pincode, Latitude = @Latitude, Longitude = @Longitude, UpdatedBy = @UpdatedBy, UpdatedOn = @UpdatedOn WHERE CompanyId = @CompanyId AND IsDeleted = 0;";

        public const string DeleteCompany = "UPDATE company SET IsDeleted = 1, UpdatedBy = @UpdatedBy, UpdatedOn = @UpdatedOn WHERE CompanyId = @CompanyId AND IsDeleted = 0;";

        public const string CreateCompanyHistory = "INSERT INTO company_history (CompanyId, Name, Email, MobileNumber, StreetNumber, StreetName, City, District, State, Country, Pincode, Latitude, Longitude, IsDeleted, CreatedBy, CreatedOn, UpdatedBy, UpdatedOn) VALUES (@CompanyId, @Name, @Email, @MobileNumber, @StreetNumber, @StreetName, @City, @District, @State, @Country, @Pincode, @Latitude, @Longitude, @IsDeleted, @CreatedBy, @CreatedOn, @UpdatedBy, @UpdatedOn);";
    }
}
