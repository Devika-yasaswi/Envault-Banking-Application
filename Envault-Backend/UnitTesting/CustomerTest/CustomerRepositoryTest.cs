using Azure;
using Castle.Core.Resource;
using CoreModels.Entities;
using CoreModels.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Utils;

namespace UnitTesting.CustomerTest
{
    public class CustomerRepositoryTest : DbContextMock
    {
        [Test]
        [TestCase(10000001)]
        public void GetBasicDetails(long customerId)
        {
            var response = _customerRepository.GetBasicDetails(customerId);
            Assert.IsNotNull(response);
            Assert.That(response.CustomerId, Is.EqualTo(customerId));
        }
        [Test]
        public void GetBasicDetails_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.GetBasicDetails(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(10000001)]
        public void GetCustomerAddress(long customerId)
        {
            var response = _customerRepository.GetCustomerAddress(customerId);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.CustomerId, Is.EqualTo(customerId));
        }
        [Test]
        public void GetCustomerAddress_Exception()
        {
            _mockContext.Setup(a => a.Set<CustomerAddressEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.GetCustomerAddress(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(10000001)]
        public void GetCustomerFamilyAndNomineeDetails(long customerId)
        {
            var response = _customerRepository.GetCustomerFamilyAndNomineeDetails(customerId);
            Assert.IsNotNull(response);
            Assert.That(response.CustomerId, Is.EqualTo(customerId));
        }
        [Test]
        public void GetCustomerFamilyAndNomineeDetails_Exception()
        {
            _mockContext.Setup(a => a.Set<CustomerFamilyAndNomineeDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.GetCustomerFamilyAndNomineeDetails(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void EditBasicDetails()
        {
            BasicDetailsEntity customerDetails = new BasicDetailsEntity()
            {
                CustomerId = 10000001,
                CustomerName = "Sri Ram",
                DateOfBirth = DateOnly.Parse("1978-01-01"),
                Gender = "Male",
                AadharNumber = 981234567890,
                MobileNumber = 9876543210,
                PANNumber = "MKTPK0515M",
                CustomerEmail = "sriram@gmail.com",
                EmployementType = "Public Sector",
                AnnualIncome = 99999,
                Nationality = true,
                TaxResidentOfIndia = true
            };
            _customerRepository.EditBasicDetails(customerDetails);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void EditBasicDetails_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.EditBasicDetails(new BasicDetailsEntity()));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void EditCustomerAddress()
        {
            var customerAddress = new CustomerAddressEntity
            {
                CustomerId = 10000001,
                PermanentHouseNo = "2/89",
                PermanentStreet = "Vidyanagar 1st line",
                PermanentCity = "Singarayakonda",
                PermanentState = "Andhra Pradesh",
                PermanentPincode = 123456,
                PresentHouseNo = "2/89",
                PresentStreet = "Vidyanagar 1st line",
                PresentCity = "Singarayakonda",
                PresentState = "Andhra Pradesh",
                PresentPincode = 123456
            };
            _customerRepository.EditCustomerAddress(customerAddress);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void EditCustomerAddress_Exception()
        {
            _mockContext.Setup(a => a.Set<CustomerAddressEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.EditCustomerAddress(new CustomerAddressEntity()));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void EditCustomerFamilyAndNomineeDetails()
        {
            var customerFamilyAndNominee = new CustomerFamilyAndNomineeDetailsEntity()
            {
                CustomerId = 10000001,
                FatherName = "Dasaradh",
                MotherName = "Kousalya",
                SpouseName = "Seetha",
                NomineeName = "Dasaradh",
                NomineeDateOfBirth = DateOnly.Parse("1947-08-15"),
                RelationWithNominee = "Father",
                NomineeHouseNo = "3-25",
                NomineeStreet = "Vidyanagar 7th line",
                NomineeCity = "Singarayakonda",
                NomineeState = "Andhra Pradesh",
                NomineePincode = 123456
            };
            _customerRepository.EditCustomerFamilyAndNomineeDetails(customerFamilyAndNominee);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void EditCustomerFamilyAndNomineeDetails_Exception()
        {
            _mockContext.Setup(a => a.Set<CustomerFamilyAndNomineeDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.EditCustomerFamilyAndNomineeDetails(new CustomerFamilyAndNomineeDetailsEntity()));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(10000001)]
        public void GetAllAccounts(long customerId)
        {
            var response = _customerRepository.GetAllAccounts(10000001);
            Assert.That(response, Is.Not.Null);
        }
        [Test]
        public void GetAllAccounts_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.GetAllAccounts(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(10000001)]
        [TestCase(10000000)]
        public void KYCStatus(long customerId)
        {
            var response = _customerRepository.KYCStatus(customerId);
            if (customerId == 10000001)
                Assert.That(response, Is.Not.Null);
            else if (customerId == 10000000)
                Assert.That(response, Is.Null);
        }
        [Test]
        public void KYCStatus_Exception()
        {
            _mockContext.Setup(a => a.Set<KYCEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.KYCStatus(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(10000003, "base64,QUI=", "base64,eHl6", "base64,a2xt")]
        [TestCase(10000004, "base64,Z2Vo", "base64,ams=", "base64,b2Vw")]
        public void CompleteKYC(long customerId, string aadharCard, string panCard, string customerPhoto)
        {
            var customerKYC = new KYCModel()
            {
                CustomerId = customerId,
                AadharCard = aadharCard,
                PANCard = panCard,
                CustomerPhoto = customerPhoto
            };
            _customerRepository.CompleteKYC(customerKYC);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);

        }
        [Test]
        public void CompleteKYC_Exception()
        {
            _mockContext.Setup(a => a.Set<KYCEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.CompleteKYC(new KYCModel()));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void DepositMoney()
        {
            var transactiondetails = new TransactionsEntity()
            {
                TransactionID = 1000003,
                SenderAccountNumber = 100000000001,
                ReceiverAccountNumber = 100000000001,
                Amount = 1,
                TransactionTime = DateTime.Now
            };
            _customerRepository.DepositMoney(transactiondetails);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void DepositMoney_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.DepositMoney(new TransactionsEntity()));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(100000000001)]
        [TestCase(100000000000)]
        public void IsValidAccountNumber(long accountNumber)
        {
            var response = _customerRepository.IsValidAccountNumber(accountNumber);
            if (accountNumber == 100000000001)
                Assert.That(response, Is.True);
            else if (accountNumber == 100000000000)
                Assert.That(response, Is.False);
        }
        [Test]
        public void IsValidAccountNumber_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.IsValidAccountNumber(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(100000000002, 100)]
        [TestCase(100000000001, 10)]
        public void HasSufficientBalance(long accountNumber, double amount)
        {
            var response = _customerRepository.HasSufficientBalance(accountNumber, amount);
            if (accountNumber == 100000000002)
                Assert.That(response, Is.True);
            else if (accountNumber == 100000000001)
                Assert.That(response, Is.False);
        }
        [Test]
        public void HasSufficientBalance_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.HasSufficientBalance(0, 0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(100000000002, 100)]
        [TestCase(100000000001, 10)]
        public void IsExceedingMinBalanceWarningAmount(long accountNumber, double amount)
        {
            var response = _customerRepository.IsExceedingMinBalanceWarningAmount(accountNumber, amount);
            if (accountNumber == 100000000001)
                Assert.That(response, Is.True);
            else if (accountNumber == 100000000002)
                Assert.That(response, Is.False);
        }
        [Test]
        public void IsExceedingMinBalanceWarningAmount_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.IsExceedingMinBalanceWarningAmount(0, 0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(100000000001, 1500)]
        [TestCase(100000000002, 100)]
        public void IsExceedingMaxBalanceWarningAmount(long accountNumber, double amount)
        {
            var response = _customerRepository.IsExceedingMaxBalanceWarningAmount(accountNumber, amount);
            if (accountNumber == 100000000001)
                Assert.That(response, Is.True);
            else if (accountNumber == 100000000002)
                Assert.That(response, Is.False);
        }
        [Test]
        public void IsExceedingMaxBalanceWarningAmount_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.IsExceedingMaxBalanceWarningAmount(0, 0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void TransferMoney()
        {
            var transactionDetails = new TransactionsEntity()
            {
                TransactionID = 1000001,
                SenderAccountNumber = 100000000001,
                ReceiverAccountNumber = 100000000002,
                Amount = 1000,
                TransactionTime = DateTime.Now,
                Remarks = "Transferring to my self account"
            };
            _customerRepository.TransferMoney(transactionDetails);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void TransferMoney_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.TransferMoney(new TransactionsEntity()));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(100000000001, 100,1000)]
        public void EditThresholdValue(long accountNumber, double minBalanceWarningAmount, double maxBalanceWarningAmount)
        {
            _customerRepository.EditThresholdValue(accountNumber, minBalanceWarningAmount, maxBalanceWarningAmount);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void EditThresholdValue_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.EditThresholdValue(0,0,0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void FreezeWithdrawls()
        {
            _customerRepository.FreezeWithdrawls(100000000001);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void FreezeWithdrawls_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.FreezeWithdrawls(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void UnFreezeWithdrawls()
        {
            _customerRepository.UnFreezeWithdrawls(100000000001);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void UnFreezeWithdrawls_Exception()
        {
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.UnFreezeWithdrawls(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void GetAllTransactions()
        {
            var response = _customerRepository.GetAllTransactions(100000000001);
            Assert.That(response, Is.Not.Null);
        }
        [Test]
        public void GetAllTransactions_Exception()
        {
            _mockContext.Setup(a => a.Set<TransactionsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _customerRepository.GetAllTransactions(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
    }
}
