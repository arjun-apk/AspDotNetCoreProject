using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspDotNetCoreProject.Model.Company
{
    public class CompanyViewModel
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
    }
}
