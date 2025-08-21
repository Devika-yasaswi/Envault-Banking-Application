using ApplicationLayer.Interfaces;
using Azure;
using Castle.Core.Resource;
using CoreModels;
using CoreModels.Entities;
using CoreModels.Models;
using DataAccessLayer.Infrastructure;
using Envault_Backend.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.CustomerTest
{
    public class CustomerControllerTest
    {
        private CustomerController _customerController;
        private Mock<ICustomerRepository> _customerRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IConfiguration> _mockConfig;
        private Mock<ILogger<CustomerController>> _logger;
        [OneTimeSetUp]
        public void SetUp()
        {

        }
        [SetUp]
        public void ReintializeTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _logger = new Mock<ILogger<CustomerController>>();

            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(a => a.GetSection("Key").Value).Returns("Devika@123#$%^&*&^%$#$%^&*&^%$#%^&*(*&$#@#%^&*&^$#%^&*&^$#$%^&*&^%$#$%&*&^$#$%&*");
            configSection.Setup(a => a.GetSection("Issuer").Value).Returns("Devika@12345");
            configSection.Setup(a => a.GetSection("Audience").Value).Returns("Devika@12345");
            configSection.Setup(a => a["GenericMessages:Values:dataUpdationSuccess"]).Returns("Updated successfully");
            configSection.Setup(a => a["GenericMessages:Values:kycUploaded"]).Returns("Your KYC documents have been successfully uploaded");
            configSection.Setup(a => a["GenericMessages:Values:depositSuccessful"]).Returns("Money deposited into your account successfully");
            configSection.Setup(a => a["GenericMessages:Values:noAccountExists"]).Returns("No customer exists with the given receiver account number");
            configSection.Setup(a => a["GenericMessages:Values:insufficientBalance"]).Returns("Insufficient amount in your account to process the transaction");
            configSection.Setup(a => a["GenericMessages:Values:exceedingMinBalance"]).Returns("Your balance has fallen below the minimum threshold");
            configSection.Setup(a => a["GenericMessages:Values:exceedingMaxBalance"]).Returns("Your balance is exceeding maximum threshold");
            configSection.Setup(a => a["GenericMessages:Values:moneyTransferSuccessful"]).Returns("Money transferred successfully");
            configSection.Setup(a => a["GenericMessages:Values:thresholdLimitSet"]).Returns("Threshold balance alert set successfully");
            configSection.Setup(a => a["GenericMessages:Values:transactionFreezed"]).Returns("Freezed transactions successfully");
            configSection.Setup(a => a["GenericMessages:Values:transactionUnFreezed"]).Returns("Unfreezed transactions successfully");
            configSection.Setup(a => a["GenericMessages:Values:noTransactions"]).Returns("No transactions are performed");
            _mockConfig.Setup(a => a.GetSection("Jwt")).Returns(configSection.Object);

            _customerController = new CustomerController(_unitOfWork.Object, configSection.Object, _logger.Object);
            _customerRepository = new Mock<ICustomerRepository>();
            _unitOfWork.Setup(a => a.CustomerRepository).Returns(_customerRepository.Object);
        }
        [Test]
        [TestCase(10000001)]
        public void GetBasicDetails(long customerId)
        {
            _customerRepository.Setup(customer => customer.GetBasicDetails(customerId)).Returns(new BasicDetailsEntity());
            var response = _customerController.GetBasicDetails(customerId);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
        }
        [Test]
        public void GetBasicDetails_Exception()
        {
            _customerRepository.Setup(customer => customer.GetBasicDetails(0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.GetBasicDetails(0);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase(10000001)]
        public void GetCustomerAddress(long customerId)
        {
            _customerRepository.Setup(customer => customer.GetCustomerAddress(customerId)).Returns(new CustomerAddressEntity());
            var response = _customerController.GetCustomerAddress(customerId);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
        }
        [Test]
        public void GetCustomerAddress_Exception()
        {
            _customerRepository.Setup(customer => customer.GetCustomerAddress(0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.GetCustomerAddress(0);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase(10000001)]
        public void GetCustomerFamilyAndNomineeDetails(long customerId)
        {
            _customerRepository.Setup(customer => customer.GetCustomerFamilyAndNomineeDetails(customerId)).Returns(new CustomerFamilyAndNomineeDetailsEntity());
            var response = _customerController.GetCustomerFamilyAndNomineeDetails(customerId);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
        }
        [Test]
        public void GetCustomerFamilyAndNomineeDetails_Exception()
        {
            _customerRepository.Setup(customer => customer.GetCustomerFamilyAndNomineeDetails(0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.GetCustomerFamilyAndNomineeDetails(0);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        public void EditBasicDetails()
        {
            _customerRepository.Setup(customer => customer.EditBasicDetails(new BasicDetailsEntity()));
            var response = _customerController.EditBasicDetails(new BasicDetailsEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("Updated successfully"));
        }
        [Test]
        public void EditBasicDetails_Exception()
        {
            _customerRepository.Setup(customer => customer.EditBasicDetails(It.IsAny< BasicDetailsEntity>())).Throws(new Exception("Something went wrong"));
            var response = _customerController.EditBasicDetails(new BasicDetailsEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void EditCustomerAddress()
        {
            _customerRepository.Setup(customer => customer.EditCustomerAddress(new CustomerAddressEntity()));
            var response = _customerController.EditCustomerAddress(new CustomerAddressEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("Updated successfully"));
        }
        [Test]
        public void EditCustomerAddress_Exception()
        {
            _customerRepository.Setup(customer => customer.EditCustomerAddress(It.IsAny<CustomerAddressEntity>())).Throws(new Exception("Something went wrong"));
            var response = _customerController.EditCustomerAddress(new CustomerAddressEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void EditCustomerFamilyAndNomineeDetails()
        {
            _customerRepository.Setup(customer => customer.EditCustomerFamilyAndNomineeDetails(new CustomerFamilyAndNomineeDetailsEntity()));
            var response = _customerController.EditCustomerFamilyAndNomineeDetails(new CustomerFamilyAndNomineeDetailsEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("Updated successfully"));
        }
        [Test]
        public void EditCustomerFamilyAndNomineeDetails_Exception()
        {
            _customerRepository.Setup(customer => customer.EditCustomerFamilyAndNomineeDetails(It.IsAny<CustomerFamilyAndNomineeDetailsEntity>())).Throws(new Exception("Something went wrong"));
            var response = _customerController.EditCustomerFamilyAndNomineeDetails(new CustomerFamilyAndNomineeDetailsEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void GetAllAccounts()
        {
            _customerRepository.Setup(customer => customer.GetAllAccounts(10000001)).Returns(new List<AccountDetails>());
            var response = _customerController.GetAllAccounts(10000001);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
        }
        [Test]
        public void GetAllAccounts_Exception()
        {
            _customerRepository.Setup(customer => customer.GetAllAccounts(0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.GetAllAccounts(0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void KYCStatus()
        {
            _customerRepository.Setup(customer => customer.KYCStatus(10000001)).Returns("Approved");
            var response = _customerController.KYCStatus(10000001);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
        }
        [Test]
        public void KYCStatus_Exception()
        {
            _customerRepository.Setup(customer => customer.KYCStatus(0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.KYCStatus(0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void CompleteKYC()
        {
            _customerRepository.Setup(customer => customer.CompleteKYC(new KYCModel()));
            var response = _customerController.CompleteKYC(new KYCModel());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("Your KYC documents have been successfully uploaded"));
        }
        [Test]
        public void CompleteKYC_Exception()
        {
            _customerRepository.Setup(customer => customer.CompleteKYC(It.IsAny<KYCModel>())).Throws(new Exception("Something went wrong"));
            var response = _customerController.CompleteKYC(new KYCModel());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void DepositMoney()
        {
            _customerRepository.Setup(customer => customer.DepositMoney(new TransactionsEntity()));
            var response = _customerController.DepositMoney(new TransactionsEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("Money deposited into your account successfully"));
        }
        [Test]
        public void DepositMoney_Exception()
        {
            _customerRepository.Setup(customer => customer.DepositMoney(It.IsAny<TransactionsEntity>())).Throws(new Exception("Something went wrong"));
            var response = _customerController.DepositMoney(new TransactionsEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        [TestCase(100000000001)]
        [TestCase(100000000000)]
        public void IsValidAccountNumber(long accountNumber)
        {
            if (accountNumber == 100000000001)
            {
                _customerRepository.Setup(customer => customer.IsValidAccountNumber(accountNumber)).Returns(true);
                var response = _customerController.IsValidAccountNumber(accountNumber);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
            }
            else if (accountNumber == 100000000000)
            {
                _customerRepository.Setup(customer => customer.IsValidAccountNumber(accountNumber)).Returns(false);
                var response = _customerController.IsValidAccountNumber(accountNumber);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
                Assert.That(response.Data, Is.EqualTo("No customer exists with the given receiver account number"));
            }
        }
        [Test]
        public void IsValidAccountNumber_Exception()
        {
            _customerRepository.Setup(customer => customer.IsValidAccountNumber(0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.IsValidAccountNumber(0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        [TestCase(100000000002, 100)]
        [TestCase(100000000001, 10)]
        public void HasSufficientBalance(long accountNumber, double amount)
        {
            if (accountNumber == 100000000002)
            {
                _customerRepository.Setup(customer => customer.HasSufficientBalance(accountNumber, amount)).Returns(true);
                var response = _customerController.HasSufficientBalance(accountNumber, amount);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
            }
            else if(accountNumber == 100000000001)
            {
                _customerRepository.Setup(customer => customer.HasSufficientBalance(accountNumber, amount)).Returns(false);
                var response = _customerController.HasSufficientBalance(accountNumber, amount);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
                Assert.That(response.Data, Is.EqualTo("Insufficient amount in your account to process the transaction"));
            }
        }
        [Test]
        public void HasSufficientBalance_Exception()
        {
            _customerRepository.Setup(customer => customer.HasSufficientBalance(0,0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.HasSufficientBalance(0,0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        [TestCase(100000000002, 100)]
        [TestCase(100000000001, 10)]
        public void IsExceedingMinBalanceWarningAmount(long accountNumber, double amount)
        {
            if (accountNumber == 100000000001)
            {
                _customerRepository.Setup(customer => customer.IsExceedingMinBalanceWarningAmount(accountNumber, amount)).Returns(true);
                var response = _customerController.IsExceedingMinBalanceWarningAmount(accountNumber, amount);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
                Assert.That(response.Data, Is.EqualTo("Your balance has fallen below the minimum threshold"));
            }
            else if (accountNumber == 100000000002)
            {
                _customerRepository.Setup(customer => customer.IsExceedingMinBalanceWarningAmount(accountNumber, amount)).Returns(false);
                var response = _customerController.IsExceedingMinBalanceWarningAmount(accountNumber, amount);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
            }
        }
        [Test]
        public void IsExceedingMinBalanceWarningAmount_Exception()
        {
            _customerRepository.Setup(customer => customer.IsExceedingMinBalanceWarningAmount(0, 0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.IsExceedingMinBalanceWarningAmount(0, 0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        [TestCase(100000000001, 1500)]
        [TestCase(100000000002, 100)]
        public void IsExceedingMaxBalanceWarningAmount(long accountNumber, double amount)
        {
            if(accountNumber == 100000000001)
            {
                _customerRepository.Setup(customer => customer.IsExceedingMaxBalanceWarningAmount(accountNumber, amount)).Returns(true);
                var response = _customerController.IsExceedingMaxBalanceWarningAmount(accountNumber, amount);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
                Assert.That(response.Data, Is.EqualTo("Your balance is exceeding maximum threshold"));
            }
            else if(accountNumber == 100000000002)
            {
                _customerRepository.Setup(customer => customer.IsExceedingMaxBalanceWarningAmount(accountNumber, amount)).Returns(false);
                var response = _customerController.IsExceedingMaxBalanceWarningAmount(accountNumber, amount);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
            }
        }
        [Test]
        public void IsExceedingMaxBalanceWarningAmount_Exception()
        {
            _customerRepository.Setup(customer => customer.IsExceedingMaxBalanceWarningAmount(0, 0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.IsExceedingMaxBalanceWarningAmount(0, 0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void TransferMoney()
        {
            _customerRepository.Setup(customer => customer.TransferMoney(new TransactionsEntity()));
            var response = _customerController.TransferMoney(new TransactionsEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("Money transferred successfully"));
        }
        [Test]
        public void TransferMoney_Exception()
        {
            _customerRepository.Setup(customer => customer.TransferMoney(It.IsAny<TransactionsEntity>())).Throws(new Exception("Something went wrong"));
            var response = _customerController.TransferMoney(new TransactionsEntity());
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void EditThresholdValue()
        {
            _customerRepository.Setup(customer => customer.EditThresholdValue(10000001, 100, 100));
            var response = _customerController.EditThresholdValue(10000001, 100, 100);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("Threshold balance alert set successfully"));
        }
        [Test]
        public void EditThresholdValue_Exception()
        {
            _customerRepository.Setup(customer => customer.EditThresholdValue(0, 0, 0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.EditThresholdValue(0, 0, 0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void FreezeWithdrawls()
        {
            _customerRepository.Setup(customer => customer.FreezeWithdrawls(10000001));
            var response = _customerController.FreezeWithdrawls(10000001);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("Freezed transactions successfully"));
        }
        [Test]
        public void FreezeWithdrawls_Exception()
        {
            _customerRepository.Setup(customer => customer.FreezeWithdrawls(0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.FreezeWithdrawls(0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void UnFreezeWithdrawls()
        {
            _customerRepository.Setup(customer => customer.UnFreezeWithdrawls(10000001));
            var response = _customerController.UnFreezeWithdrawls(10000001);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("Unfreezed transactions successfully"));
        }
        [Test]
        public void UnFreezeWithdrawls_Exception()
        {
            _customerRepository.Setup(customer => customer.UnFreezeWithdrawls(0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.UnFreezeWithdrawls(0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        [TestCase(10000000)]
        [TestCase(10000001)]
        public void GetAllTransactions(long customerId)
        {
            if(customerId == 10000000)
            {
                _customerRepository.Setup(customer => customer.GetAllTransactions(customerId)).Returns(new List<TransactionsEntity>());
                var response = _customerController.GetAllTransactions(customerId);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
                Assert.That(response.Data, Is.EqualTo("No transactions are performed"));
            }
            else if (customerId == 10000001)
            {
                _customerRepository.Setup(customer => customer.GetAllTransactions(customerId)).Returns([new TransactionsEntity()]);
                var response = _customerController.GetAllTransactions(customerId);
                Assert.That(response, Is.InstanceOf<GenericResponse>());
                Assert.That(response.Status, Is.True);
            }
        }
        [Test]
        public void GetAllTransactions_Exception()
        {
            _customerRepository.Setup(customer => customer.GetAllTransactions(0)).Throws(new Exception("Something went wrong"));
            var response = _customerController.GetAllTransactions(0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
    }
}
