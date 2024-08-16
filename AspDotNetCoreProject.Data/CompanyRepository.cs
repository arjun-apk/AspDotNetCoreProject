using AspDotNetCoreProject.Context.Company;
using AspDotNetCoreProject.Context.Database;
using AspDotNetCoreProject.Data.MySQLQuery;
using Dapper;
using System.Data;

namespace AspDotNetCoreProject.Data
{
    public class CompanyRepository : ICompanyRepository
    {
        public async Task<List<CompanyEntity>> GetAllCompanies(IDbConnection dbConnection)
        {
            var companies = await dbConnection.QueryAsync<CompanyEntity>(CompanyQuery.GetAllCompanies);
            if (companies == null)
            {
                return [];
            }
            return companies.ToList();
        }

        public async Task<CompanyEntity?> GetCompanyById(long companyId, IDbConnection dbConnection)
        {
            var company = await dbConnection.QueryFirstOrDefaultAsync<CompanyEntity>(CompanyQuery.GetCompanyById, new { CompanyId = companyId });
            return company;
        }

        public async Task<bool> CreateCompany(CompanyEntity company, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var result = await dbConnection.ExecuteAsync(CompanyQuery.CreateCompany, company);
            return result > 0;
        }

        public async Task<bool> UpdateCompany(CompanyEntity company, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var result = await dbConnection.ExecuteAsync(CompanyQuery.UpdateCompany, company, dbTransaction);
            return result > 0;
        }

        public async Task<bool> DeleteCompany(string userId, long companyId, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var result = await dbConnection.ExecuteAsync(CompanyQuery.DeleteCompany, new { CompanyId = companyId, UpdatedBy = userId, UpdatedOn = DateTime.UtcNow }, dbTransaction);
            return result > 0;
        }
    }
}
