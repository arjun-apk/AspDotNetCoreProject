using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspDotNetCoreProject.Context.Company
{
    public interface ICompanyService
    {
        Task<List<CompanyEntity>> GetAllCompanies();
        Task<CompanyEntity?> GetCompanyById(long companyId);
        Task<bool> CreateCompany(string userId, CompanyEntity company);
        Task<bool> UpdateCompany(string userId, CompanyEntity company);
        Task<bool> DeleteCompany(string userId, long companyId);
    }
}
