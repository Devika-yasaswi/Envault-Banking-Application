using CoreModels.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Utils;

namespace UnitTesting.AccountOpeningTest
{
    public class AccountOpeningRepositoryTest : DbContextMock
    {
        [Test]
        public void CheckUserExistence()
        {
            var response = _accountOpeningRepository.CheckUserExistence(981234567890, 9876543210, DateOnly.Parse("1978-01-01"));
            Assert.That(response.CustomerId, Is.EqualTo(10000001));
        }
        [Test]
        public void CheckUserExistence_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _accountOpeningRepository.CheckUserExistence(0,0, DateOnly.Parse("1978-01-01")));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase("abc@gmail.com")]
        [TestCase("sriram@gmail.com")]
        public void IsUniqueEmail(string email)
        {
            var response = _accountOpeningRepository.IsUniqueEmail(email);
            if (email == "abc@gmail.com")
                Assert.That(response, Is.True);
            else if (email == "sriram@gmail.com")
                Assert.That(response, Is.False);
        }
        [Test]
        public void IsUniqueEmail_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _accountOpeningRepository.IsUniqueEmail(""));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase("MKTPK0515H")]
        [TestCase("MKTPK0515M")]
        public void IsUniquePANNumber(string panNumber)
        {
            var response = _accountOpeningRepository.IsUniquePANNumber(panNumber);
            if (panNumber == "MKTPK0515H")
                Assert.That(response, Is.True);
            else if (panNumber == "MKTPK0515M")
                Assert.That(response, Is.False);
        }
        [Test]
        public void IsUniquePANNumber_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _accountOpeningRepository.IsUniquePANNumber(""));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void GetAllBranches()
        {
            var response = _accountOpeningRepository.GetAllBranches();
            Assert.That(response, Is.Not.Null);
        }
        [Test]
        public void GetAllBranches_Exception()
        {
            _mockContext.Setup(a => a.Set<BranchEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _accountOpeningRepository.GetAllBranches());
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(1)]
        [TestCase(7)]
        [TestCase(8)]
        public void GenerateAccountNumber(int accountTypeId)
        {
            AccountsEntity account = new() { AccountTypeId = accountTypeId };
            var response = _accountOpeningRepository.GenerateAccountNumber(account);

        }
        [Test]
        public void GenerateAccountNumber_Exception()
        {
            AccountsEntity account = new() { AccountTypeId=1 };
            _mockContext.Setup(a => a.Set<AccountsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _accountOpeningRepository.GenerateAccountNumber(account));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void CreateNewAccount()
        {
            
        }
        [Test]
        public void CreateNewAccount_Exception()
        {
            var response = Assert.Throws<Exception>(() => _accountOpeningRepository.CreateNewAccount(new BasicDetailsEntity()));
            Assert.That(response.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }
        [Test]
        public void CreateAnotherAccount()
        {
            AccountsEntity account = new()
            {
                AccountNumber = 100000000005,
                CustomerId = 10000004,
                BranchID = 3,
                AccountBalance = 0,
                AccountTypeId = 3,
                FreezeWithdrawl = false,
                MaxBalanceWarningAmount = 0,
                MinBalanceWarningAmount = 0
            };
            var response = _accountOpeningRepository.CreateAnotherAccount(account);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void CreateAnotherAccount_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _accountOpeningRepository.CreateAnotherAccount(new AccountsEntity()));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
    }
}
