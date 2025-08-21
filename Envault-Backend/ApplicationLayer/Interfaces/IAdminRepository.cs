using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IAdminRepository
    {
        List<KYCEntity> GetAllPendingRequests();
        List<KYCEntity> GetAllApprovedRequests();
        List<KYCEntity> GetAllRejectedRequests();
        BasicDetailsEntity GetCustomerDetails(long customerId);
        void ApproveRequest(long customerId);
        void RejectRequest(long customerId);
    }
}
