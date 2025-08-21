using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using CoreModels.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CustomerRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Returns basic details of the customer - For viewing Profile
        public BasicDetailsEntity GetBasicDetails(long customerId)
        {
            try
            {
                BasicDetailsEntity customerDetails = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.CustomerId == customerId).First();
                return customerDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Returns stored addresses of the customer - For viewing Profile
        public CustomerAddressEntity GetCustomerAddress(long customerId)
        {
            try
            {
                CustomerAddressEntity customerDetails = _dbContext.Set<CustomerAddressEntity>().Where(customer => customer.CustomerId == customerId).First();
                return customerDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Returns family and nominee details of the customer - For viewing Profile
        public CustomerFamilyAndNomineeDetailsEntity GetCustomerFamilyAndNomineeDetails(long customerId)
        {
            try
            {
                CustomerFamilyAndNomineeDetailsEntity customerDetails = _dbContext.Set<CustomerFamilyAndNomineeDetailsEntity>().Where(customer => customer.CustomerId == customerId).First();
                return customerDetails;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Updates the existing basic details of customer - Edit Profile invokes this
        public void EditBasicDetails(BasicDetailsEntity basicDetails)
        {
            try
            {
                BasicDetailsEntity customerDetails = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.CustomerId == basicDetails.CustomerId).First();
                customerDetails.EmployementType = basicDetails.EmployementType;
                customerDetails.AnnualIncome = basicDetails.AnnualIncome;
                customerDetails.ModifiedBy = basicDetails.CustomerEmail;
                customerDetails.ModifiedOn = DateTime.UtcNow;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Updates the existing addresses of customer - Edit Profile invokes this
        public void EditCustomerAddress(CustomerAddressEntity customerAddress)
        {
            try
            {
                CustomerAddressEntity customerDetails = _dbContext.Set<CustomerAddressEntity>().Where(customer => customer.CustomerId == customerAddress.CustomerId).First();
                customerDetails.PermanentHouseNo = customerAddress.PermanentHouseNo;
                customerDetails.PermanentStreet = customerAddress.PermanentStreet;
                customerDetails.PermanentCity = customerAddress.PermanentCity;
                customerDetails.PermanentState = customerAddress.PermanentState;
                customerDetails.PermanentPincode = customerAddress.PermanentPincode;
                customerDetails.PresentHouseNo = customerAddress.PresentHouseNo;
                customerDetails.PresentStreet = customerAddress.PresentStreet;
                customerDetails.PresentCity = customerAddress.PresentCity;
                customerDetails.PresentState = customerAddress.PresentState;
                customerDetails.PresentPincode = customerAddress.PresentPincode;
                customerDetails.ModifiedBy = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.CustomerId == customerAddress.CustomerId).Select(customer => customer.CustomerEmail).FirstOrDefault();
                customerDetails.ModifiedOn = DateTime.UtcNow;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Updates the existing Nominee details of customer - Edit Profile invokes this
        public void EditCustomerFamilyAndNomineeDetails(CustomerFamilyAndNomineeDetailsEntity customerFamilyAndNomineeDetails)
        {
            try
            {
                CustomerFamilyAndNomineeDetailsEntity customerDetails = _dbContext.Set<CustomerFamilyAndNomineeDetailsEntity>().Where(customer => customer.CustomerId == customerFamilyAndNomineeDetails.CustomerId).First();
                customerDetails.SpouseName = customerFamilyAndNomineeDetails.SpouseName;
                customerDetails.NomineeName = customerFamilyAndNomineeDetails.NomineeName;
                customerDetails.NomineeHouseNo = customerFamilyAndNomineeDetails.NomineeHouseNo;
                customerDetails.NomineeStreet = customerFamilyAndNomineeDetails.NomineeStreet;
                customerDetails.NomineeCity = customerFamilyAndNomineeDetails.NomineeCity;
                customerDetails.NomineeState = customerFamilyAndNomineeDetails.NomineeState;
                customerDetails.NomineePincode = customerFamilyAndNomineeDetails.NomineePincode;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<AccountDetails> GetAllAccounts(long customerId)
        {

            try
            {
                List<AccountDetails> accounts = _dbContext.Set<AccountsEntity>()
                        .Where(customer => customer.CustomerId == customerId)
                        .Join(_dbContext.Set<AccountTypeEntity>(),
                              account => account.AccountTypeId,
                              accountTypes => accountTypes.AccountTypeId,
                        (account, accountTypes) => new AccountDetails
                        {
                            AccountNumber = account.AccountNumber,
                            AccountType = accountTypes.AccountType,
                            AccountBalance = account.AccountBalance,
                            FreezeWithdrawl = account.FreezeWithdrawl,
                            MaxBalanceWarningAmount = account.MaxBalanceWarningAmount,
                            MinBalanceWarningAmount = account.MinBalanceWarningAmount
                        }).ToList();
                return accounts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string KYCStatus(long customerId)
        {
            try
            {
                string? kycStatus = _dbContext.Set<KYCEntity>().Where(customer => customer.CustomerId == customerId).Select(customer => customer.KYCStatus).FirstOrDefault();
                if (kycStatus != null)
                    return kycStatus;
                return kycStatus;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //KYC details will be addded into the data
        public void CompleteKYC(KYCModel customerKYCDetails)
        {
            try
            {
                KYCEntity? customerKYC = _dbContext.Set<KYCEntity>().Where(kyc => kyc.CustomerId == customerKYCDetails.CustomerId).FirstOrDefault();
                if (customerKYC == null)
                {
                    KYCEntity newCustomerKyc = new()
                    {
                        CustomerId = customerKYCDetails.CustomerId,
                        KYCStatus = "uploaded",
                        AadharCard = Convert.FromBase64String(customerKYCDetails.AadharCard.Split(',')[1]),
                        PANCard = Convert.FromBase64String(customerKYCDetails.PANCard.Split(",")[1]),
                        CustomerPhoto = Convert.FromBase64String(customerKYCDetails.CustomerPhoto.Split(",")[1]),
                        CreatedBy = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.CustomerId == customerKYCDetails.CustomerId).Select(customer => customer.CustomerEmail).FirstOrDefault()
                    };
                    newCustomerKyc.ModifiedBy = newCustomerKyc.CreatedBy;
                    _dbContext.Set<KYCEntity>().Add(newCustomerKyc);
                }
                else
                {
                    customerKYC.AadharCard = Convert.FromBase64String(customerKYCDetails.AadharCard.Split(',')[1]);
                    customerKYC.PANCard = Convert.FromBase64String(customerKYCDetails.PANCard.Split(",")[1]);
                    customerKYC.CustomerPhoto = Convert.FromBase64String(customerKYCDetails.CustomerPhoto.Split(",")[1]);
                    customerKYC.KYCStatus = "uploaded";
                    customerKYC.ModifiedBy = _dbContext.Set<BasicDetailsEntity>().Where(customer => customer.CustomerId == customerKYCDetails.CustomerId).Select(customer => customer.CustomerEmail).FirstOrDefault();
                    customerKYC.ModifiedOn = DateTime.UtcNow;
                }
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Deposits money into customer account
        public void DepositMoney(TransactionsEntity transactionDetails)
        {
            try
            {
                transactionDetails.SenderAccountNumber = transactionDetails.ReceiverAccountNumber;
                transactionDetails.TransactionTime = DateTime.UtcNow;
                transactionDetails.Remarks = "Amount Deposit by self";
                transactionDetails.CreatedBy = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == transactionDetails.SenderAccountNumber)
                                                .Join(_dbContext.Set<BasicDetailsEntity>(),
                                                      account => account.CustomerId,
                                                      basicDetails => basicDetails.CustomerId,
                                                      (account, basicDetails) => new { basicDetails.CustomerEmail })
                                                .Select(result => result.CustomerEmail).FirstOrDefault();
                transactionDetails.ModifiedBy = transactionDetails.CreatedBy;
                AccountsEntity accountDetails = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == transactionDetails.SenderAccountNumber).First();
                accountDetails.AccountBalance = accountDetails.AccountBalance + transactionDetails.Amount;
                _dbContext.Set<TransactionsEntity>().Add(transactionDetails);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Checks whether the account number exists or not
        public bool IsValidAccountNumber(long accountNumber)
        {
            try
            {
                AccountsEntity? account = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == accountNumber).FirstOrDefault();
                if (account != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Checks whether user has sufficient balance to perform transaction or not
        public bool HasSufficientBalance(long accountNumber, double amount)
        {
            try
            {
                AccountsEntity accountDetails = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == accountNumber).First();
                if (accountDetails.AccountBalance < amount)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Checks whether the balance is exceeding the min threshold value or not
        public bool IsExceedingMinBalanceWarningAmount(long accountNumber, double amount)
        {
            try
            {
                AccountsEntity accountDetails = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == accountNumber).First();
                if (accountDetails.AccountBalance - amount < accountDetails.MinBalanceWarningAmount && accountDetails.MinBalanceWarningAmount != 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Checks whether the balance is exceeding the max threshold value or not
        public bool IsExceedingMaxBalanceWarningAmount(long accountNumber, double amount)
        {
            try
            {
                AccountsEntity accountDetails = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == accountNumber).First();
                if (accountDetails.AccountBalance + amount > accountDetails.MaxBalanceWarningAmount && accountDetails.MaxBalanceWarningAmount != 0)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Internal fund transfer will be done
        public void TransferMoney(TransactionsEntity transactionDetails)
        {
            try
            {
                var accounts = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == transactionDetails.SenderAccountNumber || account.AccountNumber == transactionDetails.ReceiverAccountNumber).ToList();
                AccountsEntity senderAccount = accounts.Where(account => account.AccountNumber == transactionDetails.SenderAccountNumber).First();
                senderAccount.AccountBalance = senderAccount.AccountBalance - transactionDetails.Amount;
                AccountsEntity receiverAccount = accounts.Where(account => account.AccountNumber == transactionDetails.ReceiverAccountNumber).First();
                receiverAccount.AccountBalance = receiverAccount.AccountBalance + transactionDetails.Amount;
                transactionDetails.TransactionTime = DateTime.UtcNow;
                transactionDetails.CreatedBy = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == transactionDetails.SenderAccountNumber)
                                                .Join(_dbContext.Set<BasicDetailsEntity>(),
                                                      account => account.CustomerId,
                                                      basicDetails => basicDetails.CustomerId,
                                                      (account, basicDetails) => new { basicDetails.CustomerEmail })
                                                .Select(result => result.CustomerEmail).FirstOrDefault();
                transactionDetails.ModifiedBy = transactionDetails.CreatedBy;
                _dbContext.Set<TransactionsEntity>().Add(transactionDetails);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Edits balance threshold value - customer will get an alert if the balance decreases more than that
        public void EditThresholdValue(long accountNumber, double minBalanceWarningAmount, double maxBalanceWarningAmount)
        {
            try
            {
                AccountsEntity accountDetails = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == accountNumber).First();
                accountDetails.MinBalanceWarningAmount = minBalanceWarningAmount;
                accountDetails.MaxBalanceWarningAmount = maxBalanceWarningAmount;
                accountDetails.ModifiedOn = DateTime.UtcNow;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Freezes withdrawls of the customer
        public void FreezeWithdrawls(long accountNumber)
        {
            try
            {
                AccountsEntity account = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == accountNumber).First();
                account.FreezeWithdrawl = true;
                account.ModifiedOn = DateTime.UtcNow;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Unfreezes the account and allows customer to transfer/ withdraw their amount
        public void UnFreezeWithdrawls(long accountNumber)
        {
            try
            {
                AccountsEntity account = _dbContext.Set<AccountsEntity>().Where(account => account.AccountNumber == accountNumber).First();
                account.FreezeWithdrawl = false;
                account.ModifiedOn = DateTime.UtcNow;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Gives all the transactions of the customer based on their account number
        public List<TransactionsEntity> GetAllTransactions(long accountNumber)
        {
            try
            {
                return _dbContext.Set<TransactionsEntity>().Where(transaction => transaction.SenderAccountNumber == accountNumber || transaction.ReceiverAccountNumber == accountNumber).OrderByDescending(transaction => transaction.TransactionTime).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
