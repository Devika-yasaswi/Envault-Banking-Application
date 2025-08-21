using CoreModels.Entities;
using CoreModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IAccountOpeningRepository
    {
        BasicDetailsEntity? CheckUserExistence(long aadharNumber, long mobileNumber, DateOnly dateOfBirth);
        bool IsUniqueEmail(string email);
        bool IsUniquePANNumber(string panNumber);
        List<BranchDetails> GetAllBranches();
        AccountsEntity GenerateAccountNumber(AccountsEntity accountDetails);
        Object CreateNewAccount(BasicDetailsEntity basicDetails);
        Object CreateAnotherAccount(AccountsEntity account);
    }
}
