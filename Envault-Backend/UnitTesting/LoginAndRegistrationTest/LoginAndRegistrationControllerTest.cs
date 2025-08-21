using ApplicationLayer.Interfaces;
using BusinessLogicLayer;
using Castle.Core.Logging;
using Castle.Core.Resource;
using CoreModels;
using CoreModels.Entities;
using Envault_Backend.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Utils;

namespace UnitTesting.LoginAndRegistrationTest
{
    public class LoginAndRegistrationControllerTest
    {
        private LoginAndRegistrationController _loginAndRegistrationController;
        private Mock<ILoginAndRegistrationRepository> _loginAndRegistrationRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IConfiguration> _mockConfig;
        private Mock<ILogger<LoginAndRegistrationController>> _logger;

       
        [OneTimeSetUp]
        public void SetUp()
        {
        }
        [SetUp]
        public void ReintializeTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _logger = new Mock<ILogger<LoginAndRegistrationController>>();

            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(a => a.GetSection("Key").Value).Returns("Devika@123#$%^&*&^%$#$%^&*&^%$#%^&*(*&$#@#%^&*&^$#%^&*&^$#$%^&*&^%$#$%&*&^$#$%&*");
            configSection.Setup(a => a.GetSection("Issuer").Value).Returns("Devika@12345");
            configSection.Setup(a => a.GetSection("Audience").Value).Returns("Devika@12345");
            _mockConfig.Setup(a => a.GetSection("Jwt")).Returns(configSection.Object);

            var messageSection = new Mock<IConfigurationSection>();
            messageSection.Setup(a => a["customerNotExist"]).Returns("Customer doesn't exist");
            messageSection.Setup(a => a["duplicateRegistration"]).Returns("Customer is already registered for netbanking");
            messageSection.Setup(a => a["GenericMessages:Values:registrationSuccess"]).Returns("Registration successful");
            messageSection.Setup(a => a["GenericMessages:Values:passwordResetSuccess"]).Returns("Password reset is successful");
            _mockConfig.Setup(a => a.GetSection("GenericMessages:Values")).Returns(messageSection.Object);

            _loginAndRegistrationController = new LoginAndRegistrationController(_unitOfWork.Object, _mockConfig.Object, _logger.Object);
            _loginAndRegistrationRepository = new Mock<ILoginAndRegistrationRepository>();
            _unitOfWork.Setup(a => a.LoginAndRegistrationRepository).Returns(_loginAndRegistrationRepository.Object);
        }
        [Test]
        [TestCase(10000000)]
        [TestCase(12345)]
        public void CheckUserExistence(long customerId)
        {
            if (customerId == 10000000)
            {
                _loginAndRegistrationRepository.Setup(existence => existence.CheckUserExistence(customerId)).Returns(true);
                var response = _loginAndRegistrationController.CheckUserExistence(customerId);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
                Assert.That(response.Result.Data, Is.True);
            }
            else if (customerId == 12345)
            {
                _loginAndRegistrationRepository.Setup(existence => existence.CheckUserExistence(customerId)).Returns(false);
                var response = _loginAndRegistrationController.CheckUserExistence(customerId);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
        }
        [Test]
        public void CheckUserExistence_Exception()
        {
            _loginAndRegistrationRepository.Setup(existence => existence.CheckUserExistence(1234)).Throws(new Exception("Something went wrong"));
            var failResponse = _loginAndRegistrationController.CheckUserExistence(1234);
            Assert.That(failResponse, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(failResponse.Result.Status, Is.False);
        }
        [Test]
        [TestCase(10000000)]
        [TestCase(12345)]
        public void IsRegisteredCustomer(long customerId) 
        {
            if (customerId == 10000000)
            {
                _loginAndRegistrationRepository.Setup(existence => existence.IsRegisteredCustomer(customerId)).Returns(true);
                var response = _loginAndRegistrationController.IsRegisteredCustomer(customerId);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
            else if (customerId == 12345)
            {
                _loginAndRegistrationRepository.Setup(existence => existence.IsRegisteredCustomer(customerId)).Returns(false);
                var response = _loginAndRegistrationController.IsRegisteredCustomer(customerId);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
        }
        [Test]
        public void IsRegisteredCustomer_Exception()
        {
            _loginAndRegistrationRepository.Setup(existence => existence.IsRegisteredCustomer(1234)).Throws(new Exception("Something went wrong"));
            var failResponse = _loginAndRegistrationController.IsRegisteredCustomer(1234);
            Assert.That(failResponse, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(failResponse.Result.Status, Is.False);
        }
        [Test]
        [TestCase(1000000000, "abc@123", "Hello")]
        [TestCase(12345, "xyz@123", "World")]
        public void RegisterNewUser(int customerId, string customerPassword, string securityMessage) 
        {
            var loginCredentials = new LoginCredentialsEntity
            {
                CustomerId = customerId,
                CustomerPassword = customerPassword,
                SecurityMessage = securityMessage
            };
            if(customerId == 1000000000)
            {
                _loginAndRegistrationRepository.Setup(newUser => newUser.RegisterNewUser(loginCredentials)).Returns(true);
                var response = _loginAndRegistrationController.RegisterNewUser(loginCredentials);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
            else if(customerId == 12345)
            {
                _loginAndRegistrationRepository.Setup(newUser => newUser.RegisterNewUser(loginCredentials)).Returns(false);
                var response = _loginAndRegistrationController.RegisterNewUser(loginCredentials);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
        }
        [Test]
        [TestCase(1000000000, "abc@123", "Hello")]
        public void RegisterNewUser_Exception(int customerId, string customerPassword, string securityMessage)
        {
            var loginCredentials = new LoginCredentialsEntity
            {
                CustomerId = customerId,
                CustomerPassword = customerPassword,
                SecurityMessage = securityMessage
            };
            _loginAndRegistrationRepository.Setup(newUser => newUser.RegisterNewUser(loginCredentials)).Throws(new Exception("Something went wrong"));
            var response = _loginAndRegistrationController.RegisterNewUser(loginCredentials);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase(1123456)]
        public void GetUserSecurityMessage(long customerId) 
        {
            _loginAndRegistrationRepository.Setup(customer => customer.GetUserSecurityMessage(customerId)).Returns("Hello");
            var response = _loginAndRegistrationController.GetUserSecurityMessage(customerId);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
        }
        [Test]
        public void GetUserSecurityMessage_Exception()
        {
            _loginAndRegistrationRepository.Setup(customer => customer.GetUserSecurityMessage(0)).Throws(new Exception("Something went wrong"));
            var response = _loginAndRegistrationController.GetUserSecurityMessage(0);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase(1000000, "asdf")]
        [TestCase(12345, "hello")]
        public void ValidateUserToLogin(long customerId, string password) 
        {
            if (customerId == 1000000)
            {
                _loginAndRegistrationRepository.Setup(customer => customer.ValidateUserToLogin(customerId, password)).Returns(true);
                var response = _loginAndRegistrationController.ValidateUserToLogin(customerId, password);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
            else if (customerId == 12345)
            {
                _loginAndRegistrationRepository.Setup(customer => customer.ValidateUserToLogin(customerId, password)).Returns(false);
                var response = _loginAndRegistrationController.ValidateUserToLogin(customerId, password);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
        }
        [Test]
        [TestCase(1000000, "asdf")]
        public void ValidateUserToLogin_Exception(long customerId, string password)
        {
            _loginAndRegistrationRepository.Setup(customer => customer.ValidateUserToLogin(customerId, password)).Throws(new Exception("Something went wrong"));
            var response = _loginAndRegistrationController.ValidateUserToLogin(customerId, password);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase(987654321989)]
        [TestCase(123456789034)]
        public void CheckUserExistenceWithAadhar(long aadharNumber) 
        {
            if(aadharNumber == 987654321989)
            {
                _loginAndRegistrationRepository.Setup(customer => customer.CheckUserExistenceWithAadhar(aadharNumber)).Returns(true);
                var response = _loginAndRegistrationController.CheckUserExistenceWithAadhar(aadharNumber);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
            else if (aadharNumber == 123456789034)
            {
                _loginAndRegistrationRepository.Setup(customer => customer.CheckUserExistenceWithAadhar(aadharNumber)).Returns(false);
                var response = _loginAndRegistrationController.CheckUserExistenceWithAadhar(aadharNumber);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
        }
        [Test]
        public void CheckUserExistenceWithAadhar_Exception()
        {
            _loginAndRegistrationRepository.Setup(customer => customer.CheckUserExistenceWithAadhar(0)).Throws(new Exception("Something went wrong"));
            var response = _loginAndRegistrationController.CheckUserExistenceWithAadhar(0);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase(1234567)]
        public void GetCustomerId(long aadharNumber) 
        {
            _loginAndRegistrationRepository.Setup(customer => customer.GetCustomerId(aadharNumber)).Returns(12345);
            var response = _loginAndRegistrationController.GetCustomerId(aadharNumber);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
        }
        [Test]
        public void GetCustomerId_Exception()
        {
            _loginAndRegistrationRepository.Setup(customer => customer.GetCustomerId(0)).Throws(new Exception("Something went wrong"));
            var response = _loginAndRegistrationController.GetCustomerId(0);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase(123456,"hello")]
        public void ResetPassword(long customerId, string password) 
        {
            _loginAndRegistrationRepository.Setup(customer => customer.ResetPassword(customerId, password));
            var response = _loginAndRegistrationController.ResetPassword(customerId, password);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
        }
        [Test]
        public void ResetPassword_Exception()
        {
            _loginAndRegistrationRepository.Setup(customer => customer.ResetPassword(0, "")).Throws(new Exception("Something went wrong"));
            var response = _loginAndRegistrationController.ResetPassword(0, "");
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
    }
}
