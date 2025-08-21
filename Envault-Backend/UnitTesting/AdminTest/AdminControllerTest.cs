using ApplicationLayer.Interfaces;
using CoreModels;
using CoreModels.Entities;
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

namespace UnitTesting.AdminTest
{
    public class AdminControllerTest
    {
        private AdminController _adminController;
        private Mock<IAdminRepository> _adminRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IConfiguration> _mockConfig;
        private Mock<ILogger<AdminController>> _logger;
        [OneTimeSetUp]
        public void SetUp()
        {

        }
        [SetUp]
        public void ReintializeTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mockConfig = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            _logger = new Mock<ILogger<AdminController>>();

            var configSection = new Mock<IConfigurationSection>();
            configSection.Setup(a => a.GetSection("Key").Value).Returns("Devika@123#$%^&*&^%$#$%^&*&^%$#%^&*(*&$#@#%^&*&^$#%^&*&^$#$%^&*&^%$#$%&*&^$#$%&*");
            configSection.Setup(a => a.GetSection("Issuer").Value).Returns("Devika@12345");
            configSection.Setup(a => a.GetSection("Audience").Value).Returns("Devika@12345");
            configSection.Setup(a => a["GenericMessages:Values:kycApproved"]).Returns("KYC of the customer is approved");
            configSection.Setup(a => a["GenericMessages:Values:kycRejected"]).Returns("KYC of the customer is Rejected");
            _mockConfig.Setup(a => a.GetSection("Jwt")).Returns(configSection.Object);

            _adminController = new AdminController(_unitOfWork.Object, configSection.Object, _logger.Object);
            _adminRepository = new Mock<IAdminRepository>();
            _unitOfWork.Setup(a => a.AdminRepository).Returns(_adminRepository.Object);
        }
        [Test]
        public void GetAllPendingRequests()
        {
            _adminRepository.Setup(customer => customer.GetAllPendingRequests()).Returns(new List<KYCEntity>());
            var response = _adminController.GetAllPendingRequests();
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
        }
        [Test]
        public void GetAllPendingRequests_Exception()
        {
            _adminRepository.Setup(customer => customer.GetAllPendingRequests()).Throws(new Exception("Something went wrong"));
            var response = _adminController.GetAllPendingRequests();
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void GetAllApprovedRequests()
        {
            _adminRepository.Setup(customer => customer.GetAllApprovedRequests()).Returns(new List<KYCEntity>());
            var response = _adminController.GetAllApprovedRequests();
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
        }
        [Test]
        public void GetAllApprovedRequests_exception()
        {
            _adminRepository.Setup(customer => customer.GetAllApprovedRequests()).Throws(new Exception("Something went wrong"));
            var response = _adminController.GetAllApprovedRequests();
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void GetAllRejectedRequests()
        {
            _adminRepository.Setup(customer => customer.GetAllRejectedRequests()).Returns(new List<KYCEntity>());
            var response = _adminController.GetAllRejectedRequests();
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
        }
        [Test]
        public void GetAllRejectedRequests_Exception()
        {
            _adminRepository.Setup(customer => customer.GetAllRejectedRequests()).Throws(new Exception("Something went wrong"));
            var response = _adminController.GetAllRejectedRequests();
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void GetCustomerDetails()
        {
            _adminRepository.Setup(customer => customer.GetCustomerDetails(10000001)).Returns(new BasicDetailsEntity());
            var response = _adminController.GetCustomerDetails(10000001);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
        }
        [Test]
        public void GetCustomerDetails_Exception()
        {
            _adminRepository.Setup(customer => customer.GetCustomerDetails(0)).Throws(new Exception("Something went wrong"));
            var response = _adminController.GetCustomerDetails(0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void ApproveRequest()
        {
            _adminRepository.Setup(customer => customer.ApproveRequest(10000001));
            var response = _adminController.ApproveRequest(10000001);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("KYC of the customer is approved"));
        }
        [Test]
        public void ApproveRequest_Exception()
        {
            _adminRepository.Setup(customer => customer.ApproveRequest(0)).Throws(new Exception("Something went wrong"));
            var response = _adminController.ApproveRequest(0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
        [Test]
        public void RejectRequest()
        {
            _adminRepository.Setup(customer => customer.RejectRequest(10000001));
            var response = _adminController.RejectRequest(10000001);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.True);
            Assert.That(response.Data, Is.EqualTo("KYC of the customer is Rejected"));
        }
        [Test]
        public void RejectRequest_Exception()
        {
            _adminRepository.Setup(customer => customer.RejectRequest(0)).Throws(new Exception("Something went wrong"));
            var response = _adminController.RejectRequest(0);
            Assert.That(response, Is.InstanceOf<GenericResponse>());
            Assert.That(response.Status, Is.False);
        }
    }
}
