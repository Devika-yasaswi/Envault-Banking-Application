using ApplicationLayer.Interfaces;
using BusinessLogicLayer;
using CoreModels;
using CoreModels.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Envault_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IUnitOfWork unitOfWork, IConfiguration configuration, ILogger<AdminController> logger)
        {
            _adminService = new AdminService(unitOfWork);
            _configuration = configuration;
            _logger = logger;
        }
        [HttpGet]
        [Route("GetAllPendingRequests")]
        public GenericResponse GetAllPendingRequests()
        {
            try
            {
                List<KYCEntity> pendingRequests = _adminService.GetAllPendingRequests();
                return new GenericResponse { Status = true, Data = pendingRequests };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GetAllApprovedRequests")]
        public GenericResponse GetAllApprovedRequests()
        {
            try
            {
                List<KYCEntity> approvedRequests = _adminService.GetAllApprovedRequests();
                return new GenericResponse { Status = true, Data = approvedRequests };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GetAllRejectedRequests")]
        public GenericResponse GetAllRejectedRequests()
        {
            try
            {
                List<KYCEntity> rejectedRequests = _adminService.GetAllRejectedRequests();
                return new GenericResponse { Status = true, Data = rejectedRequests };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpGet]
        [Route("GetCustomerDetails")]
        public GenericResponse GetCustomerDetails(long customerId)
        {
            try
            {
                BasicDetailsEntity customerDetails = _adminService.GetCustomerDetails(customerId);
                return new GenericResponse { Status = true, Data = customerDetails };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPut]
        [Route("ApproveRequest")]
        public GenericResponse ApproveRequest(long customerId)
        {
            try
            {
                _adminService.ApproveRequest(customerId);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:kycApproved"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
        [HttpPut]
        [Route("RejectRequest")]
        public GenericResponse RejectRequest(long customerId)
        {
            try
            {
                _adminService.RejectRequest(customerId);
                return new GenericResponse { Status = true, Data = _configuration["GenericMessages:Values:kycRejected"] };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new GenericResponse { Status = false, Error = new Error() { Description = "" } };
            }
        }
    }
}
