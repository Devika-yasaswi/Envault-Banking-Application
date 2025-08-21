using Castle.Core.Resource;
using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Utils;

namespace UnitTesting.LoginAndRegistrationTest
{
    public class LoginAndRegistrationRepositoryTest : DbContextMock
    {

        [Test]
        [TestCase(10000001)]
        [TestCase(10000000)]
        public void CheckUserExistence(long customerId)
        {
            var result = _loginAndRegistrationRepository.CheckUserExistence(customerId);
            if(customerId == 10000001)
            {
                Assert.That(result, Is.True);
            }
            else if (customerId == 10000000)
            {
                Assert.That(result, Is.False);
            }
        }
        [Test]
        public void CheckUserExistence_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var result = Assert.Throws<Exception>(() => _loginAndRegistrationRepository.CheckUserExistence(0));
            Assert.That(result.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(10000001)]
        [TestCase(10000000)]
        public void IsRegisteredCustomer(long customerId)
        {
            var result = _loginAndRegistrationRepository.IsRegisteredCustomer(customerId);
            if (customerId == 10000001)
            {
                Assert.That(result, Is.True);
            }
            else if (customerId == 10000000)
            {
                Assert.That(result, Is.False);
            }
        }
        [Test]
        public void IsRegisteredCustomer_Exception()
        {
            _mockContext.Setup(a => a.Set<LoginCredentialsEntity>()).Throws(new Exception("Something went wrong"));
            var result = Assert.Throws<Exception>(() => _loginAndRegistrationRepository.IsRegisteredCustomer(0));
            Assert.That(result.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void RegisterNewUser()
        {
            var loginCredentials = new LoginCredentialsEntity
            {
                CustomerId = 10000002,
                CustomerPassword = "ABC@xyz",
                SecurityMessage = "Hello people"
            };
            var result = _loginAndRegistrationRepository.RegisterNewUser(loginCredentials);
            Assert.That(result, Is.True);
        }
        [Test]
        public void RegisterNewUser_Exception()
        {
            _mockContext.Setup(a => a.SaveChanges()).Throws(new Exception("Something went wrong"));
            var result = Assert.Throws<Exception>(() => _loginAndRegistrationRepository.RegisterNewUser(_loginCredentials.First()));
            Assert.That(result.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(10000001)]
        [TestCase(10000000)]
        public void GetUserSecurityMessage(long customerId)
        {
            var result = _loginAndRegistrationRepository.GetUserSecurityMessage(customerId);
            if (customerId == 10000001)
            {
                Assert.That(result, Is.EqualTo("Welcome"));
            }
            else if (customerId == 10000000)
            {
                Assert.That(result, Is.Null);
            }
        }
        [Test]
        public void GetUserSecurityMessage_Exception()
        {
            _mockContext.Setup(a => a.Set<LoginCredentialsEntity>()).Throws(new Exception("Something went wrong"));
            var result = Assert.Throws<Exception>(() => _loginAndRegistrationRepository.GetUserSecurityMessage(123));
            Assert.That(result.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(10000001, "ABC@123k")]
        [TestCase(10000000, "abc")]
        public void ValidateUserToLogin(long customerId, string password)
        {
            var result = _loginAndRegistrationRepository.ValidateUserToLogin(customerId, password);
            if (customerId == 10000001)
            {
                Assert.That(result, Is.True);
            }
            else if (customerId == 10000000)
            {
                Assert.That(result, Is.False);
            }
        }
        [Test]
        public void ValidateUserToLogin_Exception()
        {
            _mockContext.Setup(a => a.Set<LoginCredentialsEntity>()).Throws(new Exception("Something went wrong"));
            var result = Assert.Throws<Exception>(() => _loginAndRegistrationRepository.ValidateUserToLogin(123, "abc"));
            Assert.That(result.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(981234567890)]
        [TestCase(981234567891)]
        public void CheckUserExistenceWithAadhar(long aadharNumber)
        {
            var result = _loginAndRegistrationRepository.CheckUserExistenceWithAadhar(aadharNumber);
            if (aadharNumber == 981234567890)
            {
                Assert.That(result, Is.True);
            }
            else if (aadharNumber == 981234567891)
            {
                Assert.That(result, Is.False);
            }
        }
        [Test]
        public void CheckUserExistenceWithAadhar_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var result = Assert.Throws<Exception>(() => _loginAndRegistrationRepository.CheckUserExistenceWithAadhar(123));
            Assert.That(result.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(981234567890)]
        [TestCase(981234567891)]
        public void GetCustomerId(long aadharNumber)
        {
            var result = _loginAndRegistrationRepository.GetCustomerId(aadharNumber);
            if (aadharNumber == 981234567890)
            {
                Assert.That(result, Is.EqualTo(10000001));
            }
            else if (aadharNumber == 981234567891)
            {
                Assert.That(result, Is.EqualTo(0));
            }
        }
        [Test]
        public void GetCustomerId_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var result = Assert.Throws<Exception>(() => _loginAndRegistrationRepository.GetCustomerId(123));
            Assert.That(result.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        [TestCase(10000002, "ABC@123")]
        public void ResetPassword(long customerId, string password)
        {
            var result = _loginAndRegistrationRepository.ResetPassword(customerId, password);
            Assert.That(result, Is.True);
        }
        [Test]
        public void ResetPassword_Exception()
        {
            _mockContext.Setup(a => a.Set<LoginCredentialsEntity>()).Throws(new Exception("Something went wrong"));
            var result = Assert.Throws<Exception>(() => _loginAndRegistrationRepository.ResetPassword(123, "abc"));
            Assert.That(result.Message, Is.EqualTo("Something went wrong"));
        }
    }
}
