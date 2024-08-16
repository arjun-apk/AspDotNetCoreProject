namespace AspDotNetCoreProject.Model.Company
{
    public class CompanyModel
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string MobileNumber { get; set; }
        public required string StreetNumber { get; set; }
        public required string StreetName { get; set; }
        public required string City { get; set; }
        public required string District { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public required string Pincode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
