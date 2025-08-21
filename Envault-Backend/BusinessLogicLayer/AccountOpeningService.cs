using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using CoreModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class AccountOpeningService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountOpeningService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BasicDetailsEntity? CheckUserExistence(long aadharNumber, long mobileNumber, DateOnly dateOfBirth)
        {
            return _unitOfWork.AccountOpeningRepository.CheckUserExistence(aadharNumber, mobileNumber, dateOfBirth);
        }
        public bool IsUniqueEmail(string email)
        {
            return _unitOfWork.AccountOpeningRepository.IsUniqueEmail(email);
        }
        public bool IsUniquePANNumber(string panNumber)
        {
            return _unitOfWork.AccountOpeningRepository.IsUniquePANNumber(panNumber);
        }
        public List<BranchDetails> GetAllBranches()
        {
            return _unitOfWork.AccountOpeningRepository.GetAllBranches();
        }
        public Object CreateNewAccount(BasicDetailsEntity basicDetails)
        {
            return _unitOfWork.AccountOpeningRepository.CreateNewAccount(basicDetails);
        }
        public Object CreateAnotherAccount(AccountsEntity account)
        {
            return _unitOfWork.AccountOpeningRepository.CreateAnotherAccount(account);
        }
    }
}
