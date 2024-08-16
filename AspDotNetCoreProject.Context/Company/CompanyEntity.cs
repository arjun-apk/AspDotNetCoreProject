namespace AspDotNetCoreProject.Context.Company
{
    public class CompanyEntity
    {
        public required long CompanyId { get; set; }
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
        public bool IsDeleted { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
