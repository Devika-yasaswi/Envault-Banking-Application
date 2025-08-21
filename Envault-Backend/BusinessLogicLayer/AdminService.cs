using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class AdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<KYCEntity> GetAllPendingRequests()
        {
            return _unitOfWork.AdminRepository.GetAllPendingRequests();
        }
        public List<KYCEntity> GetAllApprovedRequests()
        {
            return _unitOfWork.AdminRepository.GetAllApprovedRequests();
        }
        public List<KYCEntity> GetAllRejectedRequests()
        {
            return _unitOfWork.AdminRepository.GetAllRejectedRequests();
        }
        public BasicDetailsEntity GetCustomerDetails(long customerId)
        {
            return _unitOfWork.AdminRepository.GetCustomerDetails(customerId);
        }
        public void ApproveRequest(long customerId)
        {
            _unitOfWork.AdminRepository.ApproveRequest(customerId);
        }
        public void RejectRequest(long customerId)
        {
            _unitOfWork.AdminRepository.RejectRequest(customerId);
        }
    }
}
