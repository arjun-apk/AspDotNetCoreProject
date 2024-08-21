using System.Data;

namespace AspDotNetCoreProject.Context.Company
{
    public interface ICompanyRepository
    {
        Task<List<CompanyEntity>> GetAllCompanies(IDbConnection dbConnection);
        Task<CompanyEntity?> GetCompanyById(long companyId, IDbConnection dbConnection);
        Task<CompanyEntity?> GetCompanyByName(string companyName, IDbConnection dbConnection);
        Task<CompanyEntity?> GetCompanyByNameExceptId(long companyId, string companyName, IDbConnection dbConnection);
        Task<bool> CreateCompany(CompanyEntity company, IDbConnection dbConnection, IDbTransaction dbTransaction);
        Task<bool> UpdateCompany(CompanyEntity company, IDbConnection dbConnection, IDbTransaction dbTransaction);
        Task<bool> DeleteCompany(string userId, long companyId, IDbConnection dbConnection, IDbTransaction dbTransaction);
    }
}
