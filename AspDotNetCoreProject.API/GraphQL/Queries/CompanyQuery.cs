using AspDotNetCoreProject.Context.Company;

namespace AspDotNetCoreProject.API.GraphQL.Queries
{
    public class CompanyQuery
    {
        private readonly ICompanyService _companyService;

        public CompanyQuery(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<List<CompanyEntity>> GetAllCompanies()
        {
            return await _companyService.GetAllCompanies();
        }

        public async Task<CompanyEntity?> GetCompanyById(long id)
        {
            return await _companyService.GetCompanyById(id);
        }
    }
}
