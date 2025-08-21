using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using CoreModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    public class AccountOpeningRepository : IAccountOpeningRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public AccountOpeningRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Checks whether the user exists or not - Used for creating new Account
        public BasicDetailsEntity? CheckUserExistence(long aadharNumber, long mobileNumber, DateOnly dateOfBirth)
        {
            try
            {
                return _dbContext.Set<BasicDetailsEntity>().Include(customer => customer.CustomerAddress).Include(customer => customer.CustomerFamilyAndNomineeDetails).Where(customer => customer.AadharNumber == aadharNumber && customer.MobileNumber == mobileNumber && customer.DateOfBirth == dateOfBirth).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Checks whethers the email is unique or not - Used for creating new Account
        public bool IsUniqueEmail(string email)
        {
            try
            {
                BasicDetailsEntity? customer = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.CustomerEmail == email).FirstOrDefault();
                if (customer == null)
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
        //Checks whethers the PAN Number is unique or not - Used for creating new Account
        public bool IsUniquePANNumber(string panNumber)
        {
            try
            {
                BasicDetailsEntity? customer = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.PANNumber == panNumber).FirstOrDefault();
                if(customer == null)
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
        //returns all the branches available in the bank
        public List<BranchDetails> GetAllBranches()
        {
            try
            {
                return _dbContext.Set<BranchEntity>().Where(branch => branch.IsActive).Select(branch => new BranchDetails { BranchID = branch.BranchID ,BranchLocatedState = branch.BranchLocatedState, City = branch.City, BranchAddress = branch.BranchAddress, IFSCCode = branch.IFSCCode}).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public AccountsEntity GenerateAccountNumber(AccountsEntity account)
        {
            try
            {
                if (account.AccountTypeId == 1 || account.AccountTypeId == 2 || account.AccountTypeId == 3 || account.AccountTypeId == 4 || account.AccountTypeId == 5 || account.AccountTypeId == 6)
                {
                    long accountNumber = _dbContext.Set<AccountsEntity>().Where(account => account.AccountTypeId == 1 || account.AccountTypeId == 2 || account.AccountTypeId == 3 || account.AccountTypeId == 4 || account.AccountTypeId == 5 || account.AccountTypeId == 6).OrderByDescending(account => account.AccountNumber).Select(account => account.AccountNumber).FirstOrDefault();
                    if (accountNumber != 0)
                    {
                        account.AccountNumber = accountNumber + 1;
                    }
                    else
                    {
                        account.AccountNumber = 100000000001;
                    }
                }
                else if (account.AccountTypeId == 7)
                {
                    long accountNumber = _dbContext.Set<AccountsEntity>().Where(account => account.AccountTypeId == 7).OrderByDescending(account => account.AccountNumber).Select(account => account.AccountNumber).FirstOrDefault();
                    if (accountNumber != 0)
                    {
                        account.AccountNumber = accountNumber + 1;
                    }
                    else
                    {
                        account.AccountNumber = 500000000001;
                    }
                }
                else if (account.AccountTypeId == 8 || account.AccountTypeId == 9 || account.AccountTypeId == 10 || account.AccountTypeId == 11 || account.AccountTypeId == 12 || account.AccountTypeId == 13 || account.AccountTypeId == 14)
                {
                    long accountNumber = _dbContext.Set<AccountsEntity>().Where(account => account.AccountTypeId == 8 || account.AccountTypeId == 9 || account.AccountTypeId == 10 || account.AccountTypeId == 11 || account.AccountTypeId == 12 || account.AccountTypeId == 13 || account.AccountTypeId == 14).OrderByDescending(account => account.AccountNumber).Select(account => account.AccountNumber).FirstOrDefault();
                    if (accountNumber != 0)
                    {
                        account.AccountNumber = accountNumber + 1;
                    }
                    else
                    {
                        account.AccountNumber = 700000000001;
                    }
                }
                return account;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Creates New Account for the user - For first account of customer
        public Object CreateNewAccount(BasicDetailsEntity basicDetails)
        {
            try
            {
                basicDetails.CreatedBy = basicDetails.CustomerEmail;
                basicDetails.ModifiedBy = basicDetails.CustomerEmail;
                basicDetails.CustomerAddress.CreatedBy = basicDetails.CustomerEmail;
                basicDetails.CustomerAddress.ModifiedBy = basicDetails.CustomerEmail;
                basicDetails.CustomerFamilyAndNomineeDetails.CreatedBy = basicDetails.CustomerEmail;
                basicDetails.CustomerFamilyAndNomineeDetails.ModifiedBy = basicDetails.CustomerEmail;
                AccountsEntity account = basicDetails.Accounts.First();
                account = GenerateAccountNumber(account);
                foreach (AccountsEntity accountCreated in basicDetails.Accounts)
                {
                    accountCreated.CreatedBy = basicDetails.CustomerEmail;
                    accountCreated.ModifiedBy = basicDetails.CustomerEmail;
                }
                _dbContext.Set<BasicDetailsEntity>().Add(basicDetails);
                _dbContext.SaveChanges();
                return new { customerId = basicDetails.CustomerId, accountNumber = account.AccountNumber };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Creates another account for customer - only for existing customer
        public Object CreateAnotherAccount(AccountsEntity account)
        {
            try
            {
                account.CreatedBy = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.CustomerId == account.CustomerId).Select(customer => customer.CustomerEmail).FirstOrDefault();
                account.ModifiedBy = account.CreatedBy;
                account = GenerateAccountNumber(account);
                _dbContext.Set<AccountsEntity>().Add(account);
                _dbContext.SaveChanges();
                return new { customerId = account.CustomerId, accountNumber = account.AccountNumber };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
