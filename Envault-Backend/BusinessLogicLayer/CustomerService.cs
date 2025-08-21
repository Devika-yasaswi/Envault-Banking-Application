using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using CoreModels.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public BasicDetailsEntity GetBasicDetails(long customerId)
        {
            return _unitOfWork.CustomerRepository.GetBasicDetails(customerId);
        }
        public CustomerAddressEntity GetCustomerAddress(long customerId)
        {
            return _unitOfWork.CustomerRepository.GetCustomerAddress(customerId);
        }
        public CustomerFamilyAndNomineeDetailsEntity GetCustomerFamilyAndNomineeDetails(long customerId)
        {
            return _unitOfWork.CustomerRepository.GetCustomerFamilyAndNomineeDetails(customerId);
        }
        public void EditBasicDetails(BasicDetailsEntity basicDetails)
        {
            _unitOfWork.CustomerRepository.EditBasicDetails(basicDetails);
        }
        public void EditCustomerAddress(CustomerAddressEntity customerAddress)
        {
            _unitOfWork.CustomerRepository.EditCustomerAddress(customerAddress);
        }
        public void EditCustomerFamilyAndNomineeDetails(CustomerFamilyAndNomineeDetailsEntity customerFamilyAndNomineeDetails)
        {
            _unitOfWork.CustomerRepository.EditCustomerFamilyAndNomineeDetails(customerFamilyAndNomineeDetails);
        }
        public List<AccountDetails> GetAllAccounts(long customerId)
        {
            return _unitOfWork.CustomerRepository.GetAllAccounts(customerId);
        }
        public string KYCStatus(long customerId)
        {
            return _unitOfWork.CustomerRepository.KYCStatus(customerId);
        }
        public void CompleteKYC(KYCModel customerKYCDetails)
        {
            _unitOfWork.CustomerRepository.CompleteKYC(customerKYCDetails);
        }
        public void DepositMoney(TransactionsEntity transactionDetails)
        {
            _unitOfWork.CustomerRepository.DepositMoney(transactionDetails);
        }
        public bool IsValidAccountNumber(long accountNumber)
        {
            return _unitOfWork.CustomerRepository.IsValidAccountNumber(accountNumber);
        }
        public bool HasSufficientBalance(long accountNumber, double amount)
        {
            return _unitOfWork.CustomerRepository.HasSufficientBalance(accountNumber, amount);
        }
        public bool IsExceedingMinBalanceWarningAmount(long accountNumber, double amount)
        {
            return _unitOfWork.CustomerRepository.IsExceedingMinBalanceWarningAmount(accountNumber, amount);
        }
        public bool IsExceedingMaxBalanceWarningAmount(long accountNumber, double amount)
        {
            return _unitOfWork.CustomerRepository.IsExceedingMaxBalanceWarningAmount(accountNumber,amount);
        }
        public void TransferMoney(TransactionsEntity transactionDetails)
        {
            _unitOfWork.CustomerRepository.TransferMoney(transactionDetails);
        }
        public void EditThresholdValue(long accountNumber, Double minBalanceWarningAmount, Double maxBalanceWarningAmount)
        {
            _unitOfWork.CustomerRepository.EditThresholdValue(accountNumber, minBalanceWarningAmount, maxBalanceWarningAmount);
        }
        public void FreezeWithdrawls(long accountNumber)
        {
            _unitOfWork.CustomerRepository.FreezeWithdrawls(accountNumber);
        }
        public void UnFreezeWithdrawls(long accountNumber)
        {
            _unitOfWork.CustomerRepository.UnFreezeWithdrawls(accountNumber);
        }
        public List<TransactionsEntity> GetAllTransactions(long accountNumber)
        {
            return _unitOfWork.CustomerRepository.GetAllTransactions(accountNumber);
        }
    }
}
