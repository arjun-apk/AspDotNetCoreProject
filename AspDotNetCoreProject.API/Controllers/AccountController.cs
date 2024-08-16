using AspDotNetCoreProject.Context.Account;
using AspDotNetCoreProject.Context.Common;
using AspDotNetCoreProject.Context.User;
using AspDotNetCoreProject.Model.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace AspDotNetCoreProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly JwtConfig _jwtSettings;
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IOptions<AppSettings> appSettings, IAccountService accountService, ILogger<AccountController> logger)
        {
            _jwtSettings = appSettings.Value.JwtConfig;
            _accountService = accountService;
            _logger = logger;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] RegisterModel register)
        {
            try
            {
                _logger.LogInformation($"Request reached Signup endpoint, register {JsonConvert.SerializeObject(register)}");
                var userEntity = MapData<RegisterModel, UserEntity>(register);

                var userId = await _accountService.Register(userEntity);
                if (userId > 0)
                {
                    _logger.LogInformation($"User created successfully, {userId}");
                    return Ok(new { Message = "User created successfully", UserId = userId });
                }

                _logger.LogWarning($"User already exists, {register.Username}");
                return BadRequest(new { Message = "User already exists" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred during Signup for user, register {JsonConvert.SerializeObject(register)}");
                return InternalServerErrorHttpResult();
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                _logger.LogInformation($"Request reached Login endpoint, loginModel {JsonConvert.SerializeObject(loginModel)}");

                var user = await _accountService.Login(loginModel.Username, loginModel.Password);
                if (user == null)
                {
                    _logger.LogWarning($"Invalid login attempt for user: {loginModel.Username}");
                    return Unauthorized(new { Message = "Invalid username or password" });
                }

                var token = _accountService.GenerateJwtToken(user);
                var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpireMinutes);

                _logger.LogInformation($"User logged in successfully: {loginModel.Username}");
                return Ok(new
                {
                    Token = token,
                    Expiration = expiration
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred during login for user, loginModel {JsonConvert.SerializeObject(loginModel)}");
                return InternalServerErrorHttpResult();
            }
        }
    }
}
