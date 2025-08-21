using ApplicationLayer.Interfaces;
using BusinessLogicLayer;
using CoreModels.Entities;
using CoreModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreModels.Models;

namespace Envault_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountOpeningController : ControllerBase
    {
        private readonly AccountOpeningService _accountOpeningService;
        private readonly IConfiguration _configuration;
        private readonly TokenService _tokenService;
        private readonly ILogger<AccountOpeningController> _logger;
        public AccountOpeningController(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<AccountOpeningController> logger)
        {
            _accountOpeningService = new AccountOpeningService(unitOfWork);
            _configuration = configuration;
            _tokenService = new TokenService(configuration);
            _logger = logger;
        }
        [HttpPost]
        [Route("CheckUserExistence")]
        public async Task<GenericResponse> CheckUserExistence(long aadharNumber, long mobileNumber, DateOnly dateOfBirth)
        {
            try
            {
                BasicDetailsEntity? userDetails = await Task.FromResult(_accountOpeningService.CheckUserExistence(aadharNumber, mobileNumber, dateOfBirth));
                if (userDetails != null)
                {
                    return new GenericResponse { Status = true, Data = userDetails  };
                }
                return new GenericResponse { Status = true, Data = userDetails };
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("IsUniqueEmail")]
        public async Task<GenericResponse> IsUniqueEmail(string email)
        {
            try
            {
                bool result = await Task.FromResult(_accountOpeningService.IsUniqueEmail(email));
                if (result)
                    return new GenericResponse { Status = true, Data = result };
                return new GenericResponse() { Status = true, Data = _configuration["GenericMessages:Values:duplicateEmailError"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("IsUniquePANNumber")]
        public async Task<GenericResponse> IsUniquePANNumber(string panNumber)
        {
            try
            {
                bool result = await Task.FromResult(_accountOpeningService.IsUniquePANNumber(panNumber));
                if (result)
                    return new GenericResponse { Status = true, Data = result };
                return new GenericResponse() { Status = true, Data = _configuration["GenericMessages:Values:duplicatePANError"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GetAllBranches")]
        public async Task<GenericResponse> GetAllBranches()
        {
            try
            {
                List<BranchDetails> branches = await Task.FromResult(_accountOpeningService.GetAllBranches());
                if (branches.Count != 0)
                {
                    return new GenericResponse { Status = true, Data = branches };
                }
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:LoadingBranchFail"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("CreateNewAccount")]
        public async Task<GenericResponse> CreateNewAccount(BasicDetailsEntity basicDetails)
        {
            try
            {
                Object accountDetails = await Task.FromResult(_accountOpeningService.CreateNewAccount(basicDetails));
                if (accountDetails != null)
                {
                    return new GenericResponse { Status = true, Data = accountDetails };
                }
                return new GenericResponse { Status = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("CreateAnotherAccount")]
        public async Task<GenericResponse> CreateAnotherAccount(AccountsEntity account)
        {
            try
            {
                Object accountDetails = await Task.FromResult(_accountOpeningService.CreateAnotherAccount(account));
                if (accountDetails != null)
                {
                    return new GenericResponse() { Status = true, Data = accountDetails };
                }
                return new GenericResponse() { Status = true};
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
    }
}
