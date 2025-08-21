using CoreModels.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Utils;

namespace UnitTesting.AdminTest
{
    public class AdminRepositoryTest : DbContextMock
    {
        [Test]
        public void GetAllPendingRequests()
        {
            var response = _adminRepository.GetAllPendingRequests();
            Assert.That(response, Is.All.Property("KYCStatus").EqualTo("uploaded"));
        }
        [Test]
        public void GetAllPendingRequests_Exception()
        {
            _mockContext.Setup(a => a.Set<KYCEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _adminRepository.GetAllPendingRequests());
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void GetAllApprovedRequests()
        {
            var response = _adminRepository.GetAllApprovedRequests();
            Assert.That(response, Is.All.Property("KYCStatus").EqualTo("approved"));
        }
        [Test]
        public void GetAllApprovedRequests_Exception()
        {
            _mockContext.Setup(a => a.Set<KYCEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _adminRepository.GetAllApprovedRequests());
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void GetAllRejectedRequests()
        {
            var response = _adminRepository.GetAllRejectedRequests();
            Assert.That(response, Is.All.Property("KYCStatus").EqualTo("rejected"));
        }
        [Test]
        public void GetAllRejectedRequests_Exception()
        {
            _mockContext.Setup(a => a.Set<KYCEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _adminRepository.GetAllRejectedRequests());
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void GetCustomerDetails()
        {
            var response = _adminRepository.GetCustomerDetails(10000001);
            Assert.That(response, Is.InstanceOf<BasicDetailsEntity>());
        }
        [Test]
        public void GetCustomerDetails_Exception()
        {
            _mockContext.Setup(a => a.Set<BasicDetailsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _adminRepository.GetCustomerDetails(10000003));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void ApproveRequest()
        {
            _adminRepository.ApproveRequest(10000001);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void ApproveRequest_Exception()
        {
            _mockContext.Setup(a => a.Set<KYCEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _adminRepository.ApproveRequest(10000003));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
        [Test]
        public void RejectRequest()
        {
            _adminRepository.RejectRequest(10000003);
            _mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }
        [Test]
        public void RejectRequest_Exception()
        {
            _mockContext.Setup(a => a.Set<KYCEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _adminRepository.RejectRequest(10000003));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
    }
}
