using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class LoginAndRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginAndRegistrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public bool CheckUserExistence(long customerId)
        {
            return _unitOfWork.LoginAndRegistrationRepository.CheckUserExistence(customerId);
        }
        public bool IsRegisteredCustomer(long customerId)
        {
            return _unitOfWork.LoginAndRegistrationRepository.IsRegisteredCustomer(customerId);
        }
        public bool RegisterNewUser(LoginCredentialsEntity loginCredentials)
        {
            return _unitOfWork.LoginAndRegistrationRepository.RegisterNewUser(loginCredentials);
        }
        public string GetUserSecurityMessage(long customerId)
        {
            return _unitOfWork.LoginAndRegistrationRepository.GetUserSecurityMessage(customerId);
        }
        public bool ValidateUserToLogin(long customerId, string password)
        {
            return _unitOfWork.LoginAndRegistrationRepository.ValidateUserToLogin(customerId, password);
        }
        public bool CheckUserExistenceWithAadhar(long aadharNumber)
        {
            return _unitOfWork.LoginAndRegistrationRepository.CheckUserExistenceWithAadhar(aadharNumber);
        }
        public long GetCustomerId(long aadharNumber)
        {
            return _unitOfWork.LoginAndRegistrationRepository.GetCustomerId(aadharNumber);
        }
        public bool ResetPassword(long customerId, string password)
        {
            return _unitOfWork.LoginAndRegistrationRepository.ResetPassword(customerId, password);
        }
    }
}
