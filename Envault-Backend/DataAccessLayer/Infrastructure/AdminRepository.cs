using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public AdminRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<KYCEntity> GetAllPendingRequests()
        {
            try
            {
                return _dbContext.Set<KYCEntity>().Where(status => status.KYCStatus == "uploaded").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<KYCEntity> GetAllApprovedRequests()
        {
            try
            {
                return _dbContext.Set<KYCEntity>().Where(status => status.KYCStatus == "approved").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<KYCEntity> GetAllRejectedRequests()
        {
            try
            {
                return _dbContext.Set<KYCEntity>().Where(status => status.KYCStatus == "rejected").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public BasicDetailsEntity GetCustomerDetails(long customerId)
        {
            try
            {
                BasicDetailsEntity customerDetails =  _dbContext.Set<BasicDetailsEntity>().Include(customer => customer.CustomerAddress).Include(customer => customer.CustomerFamilyAndNomineeDetails).Include(customer => customer.KYC).Where(customer => customer.CustomerId == customerId).First();
                return customerDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void ApproveRequest(long customerId)
        {
            try
            {
                KYCEntity customerDetails = _dbContext.Set<KYCEntity>().Where(customer => customer.CustomerId == customerId).First();
                customerDetails.KYCStatus = "approved";
                customerDetails.ModifiedBy = "admin@gmail.com";
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void RejectRequest(long customerId)
        {
            try
            {
                KYCEntity customerDetails = _dbContext.Set<KYCEntity>().Where(customer => customer.CustomerId == customerId).First();
                customerDetails.KYCStatus = "rejected";
                customerDetails.ModifiedBy = "admin@gmail.com";
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
