using ApplicationLayer.Interfaces;
using BusinessLogicLayer;
using CoreModels;
using CoreModels.Entities;
using CoreModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Envault_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<CustomerController> logger)
        {
            _customerService = new CustomerService(unitOfWork);
            _configuration = configuration;
            _logger = logger;
        }
        [HttpGet]
        [Route("GetBasicDetails")]
        [Authorize(Roles = "Customer")]
        public async Task<GenericResponse> GetBasicDetails(long customerId)
        {
            try
            {
                BasicDetailsEntity customerDetails = await Task.FromResult(_customerService.GetBasicDetails(customerId));
                return new GenericResponse { Status = true, Data = customerDetails };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while retriving basic details" + ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GetCustomerAddress")]
        [Authorize(Roles = "Customer")]
        public async Task<GenericResponse> GetCustomerAddress(long customerId)
        {
            try
            {
                CustomerAddressEntity customerDetails = await Task.FromResult(_customerService.GetCustomerAddress(customerId));
                return new GenericResponse { Status = true, Data = customerDetails };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GetCustomerFamilyAndNomineeDetails")]
        [Authorize(Roles = "Customer")]
        public async Task<GenericResponse> GetCustomerFamilyAndNomineeDetails(long customerId)
        {
            try
            {
                CustomerFamilyAndNomineeDetailsEntity customerDetails = await Task.FromResult(_customerService.GetCustomerFamilyAndNomineeDetails(customerId));
                return new GenericResponse { Status = true, Data = customerDetails };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("EditBasicDetails")]
        [Authorize(Roles = "Customer")]
        public GenericResponse EditBasicDetails(BasicDetailsEntity basicDetails)
        {
            try
            {
                _customerService.EditBasicDetails(basicDetails);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:dataUpdationSuccess"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("EditCustomerAddress")]
        [Authorize(Roles = "Customer")]
        public GenericResponse EditCustomerAddress(CustomerAddressEntity customerAddress)
        {
            try
            {
                _customerService.EditCustomerAddress(customerAddress);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:dataUpdationSuccess"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("EditCustomerFamilyAndNomineeDetails")]
        [Authorize(Roles = "Customer")]
        public GenericResponse EditCustomerFamilyAndNomineeDetails(CustomerFamilyAndNomineeDetailsEntity customerFamilyAndNomineeDetails)
        {
            try
            {
                _customerService.EditCustomerFamilyAndNomineeDetails(customerFamilyAndNomineeDetails);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:dataUpdationSuccess"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GetAllAccounts")]
        [Authorize(Roles = "Customer")]
        public GenericResponse GetAllAccounts(long customerId)
        {
            try
            {
                List<AccountDetails> accounts = _customerService.GetAllAccounts(customerId);
                return new GenericResponse { Status = true, Data = accounts };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("KYCStatus")]
        [Authorize(Roles = "Customer")]
        public GenericResponse KYCStatus(long customerId)
        {
            try
            {
                string kycStatus = _customerService.KYCStatus(customerId);
                return new GenericResponse { Status = true, Data = kycStatus };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("CompleteKYC")]
        [Authorize(Roles = "Customer")]
        public GenericResponse CompleteKYC(KYCModel customerKYCDetails)
        {
            try
            {
                _customerService.CompleteKYC(customerKYCDetails);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:kycUploaded"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("DepositMoney")]
        [Authorize(Roles = "Customer")]
        public GenericResponse DepositMoney(TransactionsEntity transactionDetails)
        {
            try
            {
                _customerService.DepositMoney(transactionDetails);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:depositSuccessful"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("IsValidAccountNumber")]
        [Authorize(Roles = "Customer")]
        public GenericResponse IsValidAccountNumber(long accountNumber)
        {
            try
            {
                bool isValid = _customerService.IsValidAccountNumber(accountNumber);
                if (isValid)
                    return new GenericResponse { Status = true, Data = isValid };
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:noAccountExists"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("HasSufficientBalance")]
        [Authorize(Roles = "Customer")]
        public GenericResponse HasSufficientBalance(long accountNumber, double amount)
        {
            try
            {
                bool isSufficient = _customerService.HasSufficientBalance(accountNumber, amount);
                if (isSufficient) 
                    return new GenericResponse { Status = true, Data = isSufficient };
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:insufficientBalance"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("IsExceedingMinBalanceWarningAmount")]
        [Authorize(Roles = "Customer")]
        public GenericResponse IsExceedingMinBalanceWarningAmount(long accountNumber, double amount)
        {
            try
            {
                bool isExceeding = _customerService.IsExceedingMinBalanceWarningAmount(accountNumber, amount);
                if(isExceeding)
                    return new GenericResponse { Status=true, Data = _configuration["GenericMessages:Values:exceedingMinBalance"] };
                return new GenericResponse { Status = true, Data = isExceeding };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("IsExceedingMaxBalanceWarningAmount")]
        [Authorize(Roles = "Customer")]
        public GenericResponse IsExceedingMaxBalanceWarningAmount(long accountNumber, double amount)
        {
            try
            {
                bool isExceeding = _customerService.IsExceedingMaxBalanceWarningAmount(accountNumber, amount);
                if (isExceeding) 
                    return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:exceedingMaxBalance"] };
                return new GenericResponse { Status = true, Data= isExceeding };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPost]
        [Route("TransferMoney")]
        [Authorize(Roles = "Customer")]
        public GenericResponse TransferMoney(TransactionsEntity transactionDetails)
        {
            try
            {
                _customerService.TransferMoney(transactionDetails);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:moneyTransferSuccessful"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPut]
        [Route("EditThresholdValue")]
        [Authorize(Roles = "Customer")]
        public GenericResponse EditThresholdValue(long accountNumber, Double minBalanceWarningAmount, Double maxBalanceWarningAmount)
        {
            try
            {
                _customerService.EditThresholdValue(accountNumber, minBalanceWarningAmount, maxBalanceWarningAmount); 
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:thresholdLimitSet"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPut]
        [Route("FreezeWithdrawls")]
        [Authorize(Roles = "Customer")]
        public GenericResponse FreezeWithdrawls(long accountNumber)
        {
            try
            {
                _customerService.FreezeWithdrawls(accountNumber);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:transactionFreezed"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPut]
        [Route("UnFreezeWithdrawls")]
        [Authorize(Roles = "Customer")]
        public GenericResponse UnFreezeWithdrawls(long accountNumber)
        {
            try
            {
                _customerService.UnFreezeWithdrawls(accountNumber);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:transactionUnFreezed"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GetAllTransactions")]
        [Authorize(Roles = "Customer")]
        public GenericResponse GetAllTransactions(long accountNumber)
        {
            try
            {
                List<TransactionsEntity> transactions = _customerService.GetAllTransactions(accountNumber);
                if (transactions.Count == 0)
                    return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:noTransactions"] };
                return new GenericResponse { Status = true, Data =transactions };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
    }
}
