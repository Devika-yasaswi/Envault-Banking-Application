using ApplicationLayer.Interfaces;
using Castle.Core.Logging;
using CoreModels;
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

namespace UnitTesting.UserValidationTest
{
    public class UserValidationControllerTest
    {
        private UserValidationController _userValidationController;
        private Mock<IUserValidationRepository> _userValidationRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IConfiguration> _mockConfig;
        private Mock<ILogger<UserValidationController>> _logger;
        [OneTimeSetUp]
        public void SetUp()
        {

        }
        [SetUp]
        public void ReintializeTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _logger = new Mock<ILogger<UserValidationController>>();

            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(a => a.GetSection("Key").Value).Returns("Devika@123#$%^&*&^%$#$%^&*&^%$#%^&*(*&$#@#%^&*&^$#%^&*&^$#$%^&*&^%$#$%&*&^$#$%&*");
            configSection.Setup(a => a.GetSection("Issuer").Value).Returns("Devika@12345");
            configSection.Setup(a => a.GetSection("Audience").Value).Returns("Devika@12345");

            configSection.Setup(a => a["GenericMessages:Values:OTPMismatched"]).Returns("The OTP you entered does not match");
            configSection.Setup(a => a["GenericMessages:Values:CAPTCHAMismatched"]).Returns("CAPTCHA mismatched. Please try again");
            
            _mockConfig.Setup(a => a.GetSection("Jwt")).Returns(configSection.Object);

            _userValidationController = new UserValidationController(_unitOfWork.Object, configSection.Object, _logger.Object);
            _userValidationRepository = new Mock<IUserValidationRepository>();
            _unitOfWork.Setup(a => a.UserValidationRepository).Returns(_userValidationRepository.Object);

            UserValidationController.generatedCaptcha = "Asdfg4";
        }
        [Test]
        public void GenerateOtp()
        {
            _userValidationRepository.Setup(customer => customer.GenerateOtp()).Returns(123456);
            var response = _userValidationController.GenerateOtp();
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
            _userValidationRepository.Setup(customer => customer.GenerateOtp()).Returns(0);
            var failResponse = _userValidationController.GenerateOtp();
            Assert.That(failResponse, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(failResponse.Result.Status, Is.True);
        }
        [Test]
        public void GenerateOtp_Exception()
        {
            _userValidationRepository.Setup(customer => customer.GenerateOtp()).Throws(new Exception("Something went wrong"));
            var response = _userValidationController.GenerateOtp();
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase(123456)]
        [TestCase(1111111)]
        public void ValidateOTP_Correct_OTP(int userEnteredOtp)
        {
            if(userEnteredOtp == 123456)
            {
                _userValidationRepository.Setup(otp => otp.ValidateOtp(0,userEnteredOtp)).Returns(true);
                var response = _userValidationController.ValidateOTP(userEnteredOtp);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
            else if(userEnteredOtp == 1111111)
            {
                _userValidationRepository.Setup(otp => otp.ValidateOtp(0, userEnteredOtp)).Returns(false);
                var response = _userValidationController.ValidateOTP(userEnteredOtp);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
                Assert.That(response.Result.Data, Is.EqualTo("The OTP you entered does not match"));
            }
        }
        [Test]
        public void ValidateOTP_Exception()
        {
            _userValidationRepository.Setup(otp => otp.ValidateOtp(0, 0)).Throws(new Exception("Something went wrong"));
            var response = _userValidationController.ValidateOTP(0);
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        public void GenerateCaptcha()
        {
            _userValidationRepository.Setup(captcha => captcha.GenerateCaptcha()).Returns("Asdfg4");
            var response = _userValidationController.GenerateCaptcha();
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.True);
        }
        [Test]
        public void GenerateCaptcha_Exception()
        {
            _userValidationRepository.Setup(captcha => captcha.GenerateCaptcha()).Throws(new Exception("Something went wrong"));
            var response = _userValidationController.GenerateCaptcha();
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
        [Test]
        [TestCase("Asdfg4")]
        [TestCase("aSRTUI")]
        public void ValidateCaptcha_CorrectCaptcha(string userEnteredCaptcha)
        {
            if(userEnteredCaptcha == "Asdfg4")
            {
                _userValidationRepository.Setup(captcha => captcha.ValidateCaptcha(It.IsAny<string>(), userEnteredCaptcha)).Returns(true);
                var response = _userValidationController.ValidateCaptcha(userEnteredCaptcha);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
            }
            else if(userEnteredCaptcha == "aSRTUI")
            {
                _userValidationRepository.Setup(captcha => captcha.ValidateCaptcha(It.IsAny<string>(), userEnteredCaptcha)).Returns(false);
                var response = _userValidationController.ValidateCaptcha(userEnteredCaptcha);
                Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
                Assert.That(response.Result.Status, Is.True);
                Assert.That(response.Result.Data, Is.EqualTo("CAPTCHA mismatched. Please try again"));
            }
        }
        [Test]
        public void ValidateCaptcha_Exception()
        {
            _userValidationRepository.Setup(captcha => captcha.ValidateCaptcha(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception("Something went wrong"));
            var response = _userValidationController.ValidateCaptcha("12");
            Assert.That(response, Is.InstanceOf<Task<GenericResponse>>());
            Assert.That(response.Result.Status, Is.False);
        }
    }
}