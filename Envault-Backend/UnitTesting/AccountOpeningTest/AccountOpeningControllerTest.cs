using ApplicationLayer.Interfaces;
using CoreModels;
using CoreModels.Entities;
using CoreModels.Models;
using Envault_Backend.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.AccountOpeningTest
{
    public class AccountOpeningControllerTest
    {
        private AccountOpeningController _accountOpeningController;
        private Mock<IAccountOpeningRepository> _accountOpeningRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IConfiguration> _mockConfig;
        private Mock<ILogger<AccountOpeningController>> _logger;
        [OneTimeSetUp]
        public void SetUp()
        {

        }
        [SetUp]
        public void ReintializeTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _logger = new Mock<ILogger<AccountOpeningController>>();

            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(a => a.GetSection("Key").Value).Returns("Devika@123#$%^&*&^%$#$%^&*&^%$#%^&*(*&$#@#%^&*&^$#%^&*&^$#$%^&*&^%$#$%&*&^$#$%&*");
            configSection.Setup(a => a.GetSection("Issuer").Value).Returns("Devika@12345");
            configSection.Setup(a => a.GetSection("Audience").Value).Returns("Devika@12345");
            configSection.Setup(a => a["GenericMessages:Values:duplicateEmailError"]).Returns("A user already exists with given email");
            configSection.Setup(a => a["GenericMessages:Values:duplicatePANError"]).Returns("A user already exists with given PAN Number");
            configSection.Setup(a => a["GenericMessages:Values:LoadingBranchFail"]).Returns("Unable to fetch all branches right now");

            _mockConfig.Setup(a => a.GetSection("Jwt")).Returns(configSection.Object);

            _accountOpeningController = new AccountOpeningController(_unitOfWork.Object, configSection.Object, _logger.Object);
            _accountOpeningRepository = new Mock<IAccountOpeningRepository>();
            _unitOfWork.Setup(a => a.AccountOpeningRepository).Returns(_accountOpeningRepository.Object);
        }
        [Test]
        [TestCase(12345678903456, 9345678698, "2000-01-01")]
        [TestCase(12349869879678, 8578676878, "2003-09-07")]
        public void CheckUserExistence(long aadharNumber, long mobileNumber, string dateOfBirthString)
        {
            DateOnly dateOfBirth = DateOnly.Parse(dateOfBirthString);
            if(aadharNumber == 12345678903456)
            {
                _accountOpeningRepository.Setup(existence => existence.CheckUserExistence(aadharNumber, mobileNumber, dateOfBirth)).Returns(new BasicDetailsEntity());
                var response = _accountOpeningController.CheckUserExistence(aadharNumber, mobileNumber, dateOfBirth);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
            else if(aadharNumber == 12349869879678)
            {
                _accountOpeningRepository.Setup(existence => existence.CheckUserExistence(aadharNumber, mobileNumber, dateOfBirth));
                var response = _accountOpeningController.CheckUserExistence(aadharNumber, mobileNumber, dateOfBirth);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
        }
        [Test]
        public void CheckUserExistence_Exception()
        {
            DateOnly dateOfBirth = DateOnly.Parse("2001-09-07");
            _accountOpeningRepository.Setup(existence => existence.CheckUserExistence(12345, 345, dateOfBirth)).Throws(new Exception("Something went wrong"));
            var response = _accountOpeningController.CheckUserExistence(12345, 345, dateOfBirth);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase("abc@gmail.com")]
        [TestCase("xyz@gmail.com")]
        public void IsUniqueEmail(string email)
        {
            if (email == "abc@gmail.com")
            {
                _accountOpeningRepository.Setup(unique => unique.IsUniqueEmail(email)).Returns(true);
                var response = _accountOpeningController.IsUniqueEmail(email);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
            else if(email == "xyz@gmail.com")
            {
                _accountOpeningRepository.Setup(unique => unique.IsUniqueEmail(email)).Returns(false);
                var response = _accountOpeningController.IsUniqueEmail(email);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
                Assert.That(response.Result.Data, Is.EqualTo("A user already exists with given email"));
            }
        }
        [Test]
        public void IsUniqueEmail_Exception()
        {
            _accountOpeningRepository.Setup(unique => unique.IsUniqueEmail("abc")).Throws(new Exception("Something went wrong"));
            var response = _accountOpeningController.IsUniqueEmail("abc");
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase("MKTGH0098H")]
        [TestCase("ACVGF3456K")]
        public void IsUniquePANNumber (string panNumber)
        {
            if (panNumber == "MKTGH0098H")
            {
                _accountOpeningRepository.Setup(unique => unique.IsUniquePANNumber(panNumber)).Returns(true);
                var response = _accountOpeningController.IsUniquePANNumber(panNumber);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
            else if (panNumber == "ACVGF3456K")
            {
                _accountOpeningRepository.Setup(unique => unique.IsUniquePANNumber(panNumber)).Returns(false);
                var response = _accountOpeningController.IsUniquePANNumber(panNumber);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
                Assert.That(response.Result.Data, Is.EqualTo("A user already exists with given PAN Number"));
            }
        }
        [Test]
        public void IsUniquePANNumber_Exception()
        {
            _accountOpeningRepository.Setup(unique => unique.IsUniquePANNumber("AAAAA0000A")).Throws(new Exception("Something went wrong"));
            var response = _accountOpeningController.IsUniquePANNumber("AAAAA0000A");
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        public void GetAllBranches()
        {
            _accountOpeningRepository.Setup(branch => branch.GetAllBranches()).Returns([new BranchDetails()]);
            var response = _accountOpeningController.GetAllBranches();
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
        }
        [Test]
        public void GetAllBranches_NoBranches()
        {
            _accountOpeningRepository.Setup(branch => branch.GetAllBranches()).Returns([]);
            var response = _accountOpeningController.GetAllBranches();
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
            Assert.That(response.Result.Data, Is.EqualTo("Unable to fetch all branches right now"));
        }
        [Test]
        public void GetAllBranches_Exception()
        {
            _accountOpeningRepository.Setup(branch => branch.GetAllBranches()).Throws(new Exception("Something went wrong"));
            var response = _accountOpeningController.GetAllBranches();
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        public void CreateNewAccount()
        {
            var basicDetails = new BasicDetailsEntity
            {
                CustomerName = "Vani",
                DateOfBirth = DateOnly.Parse("2003-06-02"),
                Gender = "Female",
                AadharNumber = 1234567893456,
                MobileNumber = 98765432085,
                PANNumber = "MNHJT2345L",
                CustomerEmail = "abc@gmail.com",
                EmployementType = "Private Sector",
                AnnualIncome = 9876543,
                Nationality = true,
                TaxResidentOfIndia = true
            };
            _accountOpeningRepository.Setup(account => account.CreateNewAccount(basicDetails)).Returns(new {});
            var response = _accountOpeningController.CreateNewAccount(basicDetails);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
            _accountOpeningRepository.Setup(account => account.CreateNewAccount(basicDetails)).Returns(null);
            var failResponse = _accountOpeningController.CreateNewAccount(basicDetails);
            Assert.That(failResponse, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(failResponse.Result.Status, Is.True);
        }
        [Test]
        public void CreateNewAccount_Exception()
        {
            var basicDetails = new BasicDetailsEntity();
            _accountOpeningRepository.Setup(account => account.CreateNewAccount(basicDetails)).Throws(new Exception("Something went wrong"));
            var response = _accountOpeningController.CreateNewAccount(basicDetails);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        public void CreateAnotherAccount()
        {
            var account = new AccountsEntity()
            {
                AccountTypeId = 1
            };
            _accountOpeningRepository.Setup(customer => customer.CreateAnotherAccount(account)).Returns(new {});
            var response = _accountOpeningController.CreateAnotherAccount(account);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
            _accountOpeningRepository.Setup(customer => customer.CreateAnotherAccount(account));
            var failResponse = _accountOpeningController.CreateAnotherAccount(account);
            Assert.That(failResponse, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(failResponse.Result.Status, Is.True);
        }
        [Test]
        public void CreateAnotherAccount_Exception()
        {
            var account = new AccountsEntity();
            _accountOpeningRepository.Setup(customer => customer.CreateAnotherAccount(account)).Throws(new Exception("Something went wrong"));
            var response = _accountOpeningController.CreateAnotherAccount(account);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
    }
}
