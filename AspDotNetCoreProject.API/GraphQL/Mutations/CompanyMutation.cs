using AspDotNetCoreProject.Context.Company;
using AspDotNetCoreProject.Model.Company;
using ExpressMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspDotNetCoreProject.API.GraphQL.Mutations
{
    public class CompanyMutation
    {
        private readonly ICompanyService _companyService;
        private readonly string userId = "1234567890";

        public CompanyMutation(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<bool> AddCompany(CompanyModel companyModel)
        {
            var company = Mapper.Map<CompanyModel, CompanyEntity>(companyModel);
            return await _companyService.CreateCompany(userId, company);
        }

        public async Task<bool> UpdateCompany(long companyId, [FromBody] CompanyModel companyModel)
        {
            var company = Mapper.Map<CompanyModel, CompanyEntity>(companyModel);
            company.CompanyId = companyId;
            return await _companyService.UpdateCompany(userId, company);
        }

        public async Task<bool> DeleteCompany(long companyId)
        {
            return await _companyService.DeleteCompany(userId, companyId);
        }
    }
}
