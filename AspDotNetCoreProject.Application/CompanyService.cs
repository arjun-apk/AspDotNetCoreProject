using AspDotNetCoreProject.Context.Company;
using AspDotNetCoreProject.Context.Database;

namespace AspDotNetCoreProject.Application
{
    public class CompanyService : ICompanyService
    {
        private readonly IDatabaseManager _databaseManager;
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(IDatabaseManager databaseManager, ICompanyRepository companyRepository)
        {
            _databaseManager = databaseManager;
            _companyRepository = companyRepository;
        }

        public async Task<List<CompanyEntity>> GetAllCompanies()
        {
            using var dbConnection = _databaseManager.GetConnection;
            return await _companyRepository.GetAllCompanies(dbConnection);
        }

        public async Task<CompanyEntity?> GetCompanyById(long companyId)
        {
            using var dbConnection = _databaseManager.GetConnection;
            return await _companyRepository.GetCompanyById(companyId, dbConnection);
        }

        public async Task<bool> CreateCompany(string userId, CompanyEntity company)
        {
            company.CreatedBy = userId;
            company.CreatedOn = DateTime.UtcNow;
            using var dbConnection = _databaseManager.GetConnection;
            using var dbTransaction = dbConnection.BeginTransaction();
            return await _companyRepository.CreateCompany(company, dbConnection, dbTransaction);
        }

        public async Task<bool> UpdateCompany(string userId, CompanyEntity company)
        {
            company.UpdatedBy = userId;
            company.UpdatedOn = DateTime.UtcNow; 
            using var dbConnection = _databaseManager.GetConnection;
            using var dbTransaction = dbConnection.BeginTransaction();
            return await _companyRepository.UpdateCompany(company, dbConnection, dbTransaction);
        }

        public async Task<bool> DeleteCompany(string userId, long companyId)
        {
            using var dbConnection = _databaseManager.GetConnection;
            using var dbTransaction = dbConnection.BeginTransaction();
            return await _companyRepository.DeleteCompany(userId, companyId, dbConnection, dbTransaction);
        }
    }
}
