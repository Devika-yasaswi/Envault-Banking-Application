using DataAccessLayer.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Utils;
using static System.Net.WebRequestMethods;

namespace UnitTesting.UserValidationTest
{
    public class UserValidationRepositoryTest : DbContextMock
    {
        [Test]
        public void GenerateOtp()
        {
            var response = _userValidationRepository.GenerateOtp();
            Assert.That(response, Is.InRange(100000, 999999));
        }
        [Test]
        public void GenerateOtp_Exception()
        {
            
        }
        [Test]
        [TestCase(123456)]
        [TestCase(000000)]
        public void ValidateOtp(int userEnteredOtp)
        {
            var response = _userValidationRepository.ValidateOtp(123456, userEnteredOtp);
            if(userEnteredOtp == 123456)
                Assert.That(response, Is.True);
            else if (userEnteredOtp == 000000)
                Assert.That(response, Is.False);
        }
        [Test]
        public void ValidateOtp_Exception()
        {
            
        }
        [Test]
        public void GenerateCaptcha()
        {
            var response = _userValidationRepository.GenerateCaptcha();
            Assert.That(response.Length, Is.EqualTo(6));
        }
        [Test]
        public void GenerateCaptcha_Exception()
        {

        }
        [Test]
        [TestCase("hello1")]
        [TestCase("Hello1")]
        public void ValidateCaptcha(string userEnteredCaptcha)
        {
            var response = _userValidationRepository.ValidateCaptcha("hello1", userEnteredCaptcha);
            if (userEnteredCaptcha == "hello1")
                Assert.That(response, Is.True);
            else if (userEnteredCaptcha == "Hello1")
                Assert.That(response, Is.False);
        }
        [Test]
        public void ValidateCaptcha_Exception()
        {
            
        }
    }
}
