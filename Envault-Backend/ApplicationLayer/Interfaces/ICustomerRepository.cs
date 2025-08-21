using CoreModels.Entities;
using CoreModels.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface ICustomerRepository
    {
        BasicDetailsEntity GetBasicDetails(long customerId);
        CustomerAddressEntity GetCustomerAddress(long customerId);
        CustomerFamilyAndNomineeDetailsEntity GetCustomerFamilyAndNomineeDetails(long customerId);
        void EditBasicDetails(BasicDetailsEntity basicDetails);
        void EditCustomerAddress(CustomerAddressEntity customerAddress);
        void EditCustomerFamilyAndNomineeDetails(CustomerFamilyAndNomineeDetailsEntity customerFamilyAndNomineeDetails);
        List<AccountDetails> GetAllAccounts(long customerId);
        string KYCStatus(long customerId);
        void CompleteKYC (KYCModel customerKYCDetails);
        void DepositMoney(TransactionsEntity transactionDetails);
        bool IsValidAccountNumber(long accountNumber);
        bool HasSufficientBalance(long accountNumber, double amount);
        bool IsExceedingMinBalanceWarningAmount(long accountNumber, double amount);
        bool IsExceedingMaxBalanceWarningAmount(long accountNumber, double amount);
        void TransferMoney(TransactionsEntity transactionDetails);
        void EditThresholdValue(long accountNumber, double minBalanceWarningAmount, double maxBalanceWarningAmount);
        void FreezeWithdrawls(long accountNumber);
        void UnFreezeWithdrawls(long accountNumber);
        List<TransactionsEntity> GetAllTransactions(long accountNumber);
    }
}
