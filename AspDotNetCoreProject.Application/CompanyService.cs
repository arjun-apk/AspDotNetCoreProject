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
            var existingCompany = await _companyRepository.GetCompanyByName(company.Name, dbConnection);
            if (existingCompany != null) return false;
            var result = await _companyRepository.CreateCompany(company, dbConnection, dbTransaction);
            if (result) dbTransaction.Commit();
            return result;
        }

        public async Task<bool> UpdateCompany(string userId, CompanyEntity company)
        {
            using var dbConnection = _databaseManager.GetConnection;
            using var dbTransaction = dbConnection.BeginTransaction();
            var existingCompanyByName = await _companyRepository.GetCompanyByNameExceptId(company.CompanyId, company.Name, dbConnection);
            if (existingCompanyByName != null) return false;
            var existingCompanyById = await _companyRepository.GetCompanyById(company.CompanyId, dbConnection);
            if (existingCompanyById == null) return false;
            company.CreatedBy = existingCompanyById.CreatedBy;
            company.CreatedOn = existingCompanyById.CreatedOn;
            company.UpdatedBy = userId;
            company.UpdatedOn = DateTime.UtcNow;
            var result = await _companyRepository.UpdateCompany(company, dbConnection, dbTransaction);
            if (result) dbTransaction.Commit();
            return result;
        }

        public async Task<bool> DeleteCompany(string userId, long companyId)
        {
            using var dbConnection = _databaseManager.GetConnection;
            using var dbTransaction = dbConnection.BeginTransaction();
            var result = await _companyRepository.DeleteCompany(userId, companyId, dbConnection, dbTransaction);
            if (result) dbTransaction.Commit();
            return result;
        }
    }
}
