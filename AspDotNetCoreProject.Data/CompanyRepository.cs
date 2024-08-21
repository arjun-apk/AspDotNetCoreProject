using AspDotNetCoreProject.Context.Company;
using AspDotNetCoreProject.Data.MySQLQuery;
using Dapper;
using ExpressMapper;
using System.Data;

namespace AspDotNetCoreProject.Data
{
    public class CompanyRepository : ICompanyRepository
    {
        private static async Task<bool> CreateCompanyHistory(CompanyHistoryEntity company, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var result = await dbConnection.ExecuteAsync(CompanyQuery.CreateCompanyHistory, company, dbTransaction);
            return result > 0;
        }

        public async Task<List<CompanyEntity>> GetAllCompanies(IDbConnection dbConnection)
        {
            var companies = await dbConnection.QueryAsync<CompanyEntity>(CompanyQuery.GetAllCompanies);
            if (companies == null) return [];
            return companies.ToList();
        }

        public async Task<CompanyEntity?> GetCompanyById(long companyId, IDbConnection dbConnection)
        {
            var company = await dbConnection.QueryFirstOrDefaultAsync<CompanyEntity>(CompanyQuery.GetCompanyById, new { CompanyId = companyId });
            return company;
        }

        public async Task<CompanyEntity?> GetCompanyByName(string companyName, IDbConnection dbConnection)
        {
            var company = await dbConnection.QueryFirstOrDefaultAsync<CompanyEntity>(CompanyQuery.GetCompanyByName, new { CompanyName = companyName });
            return company;
        }

        public async Task<CompanyEntity?> GetCompanyByNameExceptId(long companyId, string companyName, IDbConnection dbConnection)
        {
            var company = await dbConnection.QueryFirstOrDefaultAsync<CompanyEntity>(CompanyQuery.GetCompanyByNameExceptId, new { CompanyId = companyId, CompanyName = companyName });
            return company;
        }

        public async Task<bool> CreateCompany(CompanyEntity company, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var result = await dbConnection.ExecuteScalarAsync<long>(CompanyQuery.CreateCompany, company, dbTransaction);
            if (result <= 0) return false;
            company.CompanyId = result;
            var historyCompany = Mapper.Map<CompanyEntity, CompanyHistoryEntity>(company);
            return await CreateCompanyHistory(historyCompany, dbConnection, dbTransaction);
        }

        public async Task<bool> UpdateCompany(CompanyEntity company, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var result = await dbConnection.ExecuteAsync(CompanyQuery.UpdateCompany, company, dbTransaction);
            if (result <= 0) return false;
            var historyCompany = Mapper.Map<CompanyEntity, CompanyHistoryEntity>(company);
            return await CreateCompanyHistory(historyCompany, dbConnection, dbTransaction);
        }

        public async Task<bool> DeleteCompany(string userId, long companyId, IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            var company = await GetCompanyById(companyId, dbConnection);
            if (company == null) return false;
            company.IsDeleted = true;
            company.UpdatedBy = userId;
            company.UpdatedOn = DateTime.UtcNow;
            var result = await dbConnection.ExecuteAsync(CompanyQuery.DeleteCompany, new { CompanyId = companyId, company.UpdatedBy,  company.UpdatedOn }, dbTransaction);
            if (result <= 0) return false;
            var historyCompany = Mapper.Map<CompanyEntity, CompanyHistoryEntity>(company);
            return await CreateCompanyHistory(historyCompany, dbConnection, dbTransaction);
        }
    }
}
