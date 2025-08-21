using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    public class LoginAndRegistrationRepository : ILoginAndRegistrationRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public LoginAndRegistrationRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Checks whether the user exists or not - Used for registering customer for netbanking & resetting password
        public bool CheckUserExistence(long customerId)
        {
            try
            {
                BasicDetailsEntity? customerDetails = _dbContext.Set<BasicDetailsEntity>().Where(account => account.CustomerId == customerId).FirstOrDefault();
                if (customerDetails != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Checks whether the customer already registered for netbanking or not
        public bool IsRegisteredCustomer(long customerId)
        {
            try
            {
                LoginCredentialsEntity? loginCredentials = _dbContext.Set<LoginCredentialsEntity>().Where(loginData => loginData.CustomerId == customerId).FirstOrDefault();
                if (loginCredentials != null)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Registers the customer for net banking 
        public bool RegisterNewUser(LoginCredentialsEntity loginCredentials)
        {
            try
            {
                loginCredentials.CreatedBy = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.CustomerId == loginCredentials.CustomerId).Select(customer => customer.CustomerEmail).FirstOrDefault();
                loginCredentials.ModifiedBy = loginCredentials.CreatedBy;
                _dbContext.Set<LoginCredentialsEntity>().Add(loginCredentials);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Gives security message of the customer for application authentication
        public string GetUserSecurityMessage(long customerId)
        {
            try
            {
                LoginCredentialsEntity? loginCredentials = _dbContext.Set<LoginCredentialsEntity>().Where(user => user.CustomerId == customerId).FirstOrDefault();
                if (loginCredentials != null)
                {
                    return loginCredentials.SecurityMessage;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Checks weather the login credentials entered by the user are correct or not
        public bool ValidateUserToLogin(long customerId, string password)
        {
            try
            {
                LoginCredentialsEntity? loginCredentials = _dbContext.Set<LoginCredentialsEntity>().Where(user => user.CustomerId == customerId && user.CustomerPassword == password).FirstOrDefault();
                if (loginCredentials != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Checks whether the user exists or not - Used for forgot customer ID
        public bool CheckUserExistenceWithAadhar(long aadharNumber)
        {
            try
            {
                BasicDetailsEntity? basicDetails = _dbContext.Set<BasicDetailsEntity>().Where(user => user.AadharNumber == aadharNumber).FirstOrDefault();
                if (basicDetails != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Gives customer ID of the customer based on their aadhar number
        public long GetCustomerId(long aadharNumber)
        {
            try
            {
                long customerId = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.AadharNumber == aadharNumber).Select(customer => customer.CustomerId).FirstOrDefault();
                return customerId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Resets the password of the customer - Forgot Password
        public bool ResetPassword(long customerId, string password)
        {
            try
            {
                LoginCredentialsEntity loginCredentials = _dbContext.Set<LoginCredentialsEntity>().Where(customer => customer.CustomerId == customerId).First();
                loginCredentials.CustomerPassword = password;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
    }
}
