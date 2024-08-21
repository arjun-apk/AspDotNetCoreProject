using AspDotNetCoreProject.Context.Company;
using AspDotNetCoreProject.Model.Company;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspDotNetCoreProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                _logger.LogInformation("Request reached GetAllCompanies endpoint");
                var companies = await _companyService.GetAllCompanies();
                _logger.LogInformation($"Companies retrieved successfully, companies {JsonConvert.SerializeObject(companies)}");
                return OkHttpResult<List<CompanyEntity>, List<CompanyViewModel>>(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all companies");
                return InternalServerErrorHttpResult();
            }
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCompanyById(long companyId)
        {
            try
            {
                _logger.LogInformation($"Request reached GetCompanyById endpoint for companyId {companyId}");
                var company = await _companyService.GetCompanyById(companyId);
                _logger.LogInformation($"Company retrieved successfully, company {JsonConvert.SerializeObject(company)}");
                if (company == null)
                {
                    _logger.LogInformation($"Company not found, companyId {companyId}");
                    return BadRequestHttpResult("Company not found");
                }
                return OkHttpResult<CompanyEntity, CompanyViewModel>(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while getting company by Id {companyId}");
                return InternalServerErrorHttpResult();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyModel companyModel)
        {
            try
            {
                _logger.LogInformation($"Request reached CreateCompany endpoint, companyModel {JsonConvert.SerializeObject(companyModel)}");
                var company = MapData<CompanyModel, CompanyEntity>(companyModel);
                var result = await _companyService.CreateCompany(UserId, company);
                if (!result)
                {
                    _logger.LogWarning($"Company already exists, {company.Name}");
                    return BadRequestHttpResult("Company already exists");
                }
                _logger.LogInformation($"Company created successfully, {company.Name}");
                return CreatedHttpResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred during CreateCompany for company, companyModel {JsonConvert.SerializeObject(companyModel)}");
                return InternalServerErrorHttpResult();
            }
        }

        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateCompany(long companyId, [FromBody] CompanyModel companyModel)
        {
            try
            {
                _logger.LogInformation($"Request reached UpdateCompany endpoint for companyId {companyId}, companyModel {JsonConvert.SerializeObject(companyModel)}");
                var company = MapData<CompanyModel, CompanyEntity>(companyModel);
                company.CompanyId = companyId;
                var result = await _companyService.UpdateCompany(UserId, company);
                if (!result)
                {
                    _logger.LogInformation($"Company name already exists, companyModel {companyModel}");
                    return BadRequestHttpResult("Company name already exists");
                }
                _logger.LogInformation($"Company updated successfully, {company.Name}");
                return UpdatedHttpResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred during UpdateCompany for companyId {companyId}, companyModel {JsonConvert.SerializeObject(companyModel)}");
                return InternalServerErrorHttpResult();
            }
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteCompany(long companyId)
        {
            try
            {
                _logger.LogInformation($"Request reached DeleteCompany endpoint for companyId {companyId}");
                var result = await _companyService.DeleteCompany(UserId, companyId);
                if (!result)
                {
                    _logger.LogInformation($"Company not found, companyId {companyId}");
                    return BadRequestHttpResult("Company not found");
                }
                _logger.LogInformation($"Company deleted successfully, companyId {companyId}");
                return DeletedHttpResult();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred during DeleteCompany for companyId {companyId}");
                return InternalServerErrorHttpResult();
            }
        }
    }
}
