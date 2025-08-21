using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface ILoginAndRegistrationRepository
    {
        bool CheckUserExistence(long customerId);
        bool IsRegisteredCustomer(long customerId);
        bool RegisterNewUser(LoginCredentialsEntity loginCredentials);
        string GetUserSecurityMessage(long customerId);
        bool ValidateUserToLogin(long customerId, string password);
        bool CheckUserExistenceWithAadhar(long aadharNumber);
        long GetCustomerId(long aadharNumber);
        bool ResetPassword(long customerId, string password);
    }
}
