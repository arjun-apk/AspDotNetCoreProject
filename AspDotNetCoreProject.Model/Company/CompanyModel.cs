using System.ComponentModel.DataAnnotations;

namespace AspDotNetCoreProject.Model.Company
{
    public class CompanyModel
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string MobileNumber { get; set; }
        [Required]
        public required string StreetNumber { get; set; }
        [Required]
        public required string StreetName { get; set; }
        [Required]
        public required string City { get; set; }
        [Required]
        public required string District { get; set; }
        [Required]
        public required string State { get; set; }
        [Required]
        public required string Country { get; set; }
        [Required]
        public required string Pincode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
    }
}
