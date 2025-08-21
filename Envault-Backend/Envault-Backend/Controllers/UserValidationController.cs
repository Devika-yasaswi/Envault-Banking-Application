using ApplicationLayer.Interfaces;
using BusinessLogicLayer;
using CoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Envault_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserValidationController : ControllerBase
    {
        private readonly UserValidationService _userValidationService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserValidationController> _logger;
        public static int generatedOTP;
        public static string generatedCaptcha;
        public UserValidationController(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<UserValidationController> logger)
        {
            _userValidationService = new UserValidationService(unitOfWork);
            _configuration = configuration;
            _logger = logger;
        }
        [HttpGet]
        [Route("GenerateOtp")]
        public async Task<GenericResponse> GenerateOtp()
        {
            try
            {
                generatedOTP = await Task.FromResult(_userValidationService.GenerateOtp());
                if (generatedOTP != 0)
                    return new GenericResponse() { Status = true, Data = generatedOTP };
                else
                    return new GenericResponse() { Status = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("ValidateOTP")]
        public async Task<GenericResponse> ValidateOTP(int userEnteredOtp)
        {
            try
            {
                bool ValidationResult = await Task.FromResult(_userValidationService.ValidateOtp(generatedOTP, userEnteredOtp));
                if (ValidationResult)
                    return new GenericResponse() { Status = true, Data = true };
                else
                    return new GenericResponse() { Status = true, Data = _configuration["GenericMessages:Values:OTPMismatched"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GenerateCaptcha")]
        public async Task<GenericResponse> GenerateCaptcha()
        {
            try
            {
                generatedCaptcha = await Task.FromResult(_userValidationService.GenerateCaptcha());
                return new GenericResponse() { Status = true, Data = generatedCaptcha };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("ValidateCaptcha")]
        public async Task<GenericResponse> ValidateCaptcha(string userEnteredCaptcha)
        {
            try
            {
                bool result = await Task.FromResult(_userValidationService.ValidateCaptcha(generatedCaptcha, userEnteredCaptcha));
                if (result)
                {
                    _logger.LogInformation("CAPTCHA Matched");
                    return new GenericResponse() { Status = true, Data = result };
                }
                else
                {
                    _logger.LogInformation(_configuration["GenericMessages:Values:CAPTCHAMismatched"]);
                    return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:CAPTCHAMismatched"] };
                }    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
    }
}
