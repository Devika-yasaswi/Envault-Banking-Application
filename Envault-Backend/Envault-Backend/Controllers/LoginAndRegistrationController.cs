using ApplicationLayer.Interfaces;
using BusinessLogicLayer;
using CoreModels.Entities;
using CoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Envault_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginAndRegistrationController : ControllerBase
    {
        private readonly LoginAndRegistrationService _loginAndRegistrationService;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly ILogger<LoginAndRegistrationController> _logger;
        public LoginAndRegistrationController(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<LoginAndRegistrationController> logger)
        {
            _loginAndRegistrationService = new LoginAndRegistrationService(unitOfWork);
            _configuration = configuration;
            _tokenService = new TokenService(configuration);
            _logger = logger;
        }
        [HttpPost]
        [Route("CheckUserExistence")]
        public async Task<GenericResponse> CheckUserExistence(long customerId)
        {
            try
            {
                bool result = await Task.FromResult(_loginAndRegistrationService.CheckUserExistence(customerId));
                if (result)
                    return new GenericResponse { Status = true, Data = result };
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:customerNotExist"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("IsRegisteredCustomer")]
        public async Task<GenericResponse> IsRegisteredCustomer(long customerId)
        {
            try
            {
                bool result = await Task.FromResult(_loginAndRegistrationService.IsRegisteredCustomer(customerId));
                if (result)
                    return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:duplicateRegistration"] };
                return new GenericResponse { Status = true, Data = result};
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("RegisterNewUser")]
        public async Task<GenericResponse> RegisterNewUser(LoginCredentialsEntity loginCredentials)
        {
            try
            {
                bool result = await Task.FromResult(_loginAndRegistrationService.RegisterNewUser(loginCredentials));
                if (result)
                    return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:registrationSuccess"] };
                return new GenericResponse { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("GetUserSecurityMessage")]
        public async Task<GenericResponse> GetUserSecurityMessage(long customerId)
        {
            try
            {
                string securityMessage = await Task.FromResult(_loginAndRegistrationService.GetUserSecurityMessage(customerId));
                return new GenericResponse { Status = true, Data = securityMessage };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("ValidateUserToLogin")]
        public async Task<GenericResponse> ValidateUserToLogin(long customerId, string password)
        {
            try
            {
                bool result = await Task.FromResult(_loginAndRegistrationService.ValidateUserToLogin(customerId, password));
                if (result)
                {
                    string token = _tokenService.GetToken(customerId.ToString(), true);
                    return new GenericResponse { Status = true, Data = token };
                }
                return new GenericResponse { Status = true, Data = result };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("CheckUserExistenceWithAadhar")]
        public async Task<GenericResponse> CheckUserExistenceWithAadhar(long aadharNumber)
        {
            try
            {
                bool result = await Task.FromResult(_loginAndRegistrationService.CheckUserExistenceWithAadhar(aadharNumber));
                if (result)
                    return new GenericResponse { Status = true, Data = result };
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:customerNotExist"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GetCustomerId")]
        public async Task<GenericResponse> GetCustomerId(long aadharNumber)
        {
            try
            {
                long customerId = await Task.FromResult(_loginAndRegistrationService.GetCustomerId(aadharNumber));
                return new GenericResponse { Status = true, Data = customerId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<GenericResponse> ResetPassword(long customerId, string password)
        {
            try
            {
                bool result = await Task.FromResult(_loginAndRegistrationService.ResetPassword(customerId, password));
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:passwordResetSuccess"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
    }
}
