using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTesting.Utils;

namespace UnitTesting.LoanTest
{
    public class LoanRepositoryTest :DbContextMock
    {
        [Test]
        public void GetLastYearTransactionAmount()
        {
            var response = _loanRepository.GetLastYearTransactionAmount(10000001);
            Assert.That(response, Is.AtLeast(0));
        }
        [Test]
        public void GetLastYearTransactionAmount_Exception()
        {
            _mockContext.Setup(a => a.Set<TransactionsEntity>()).Throws(new Exception("Something went wrong"));
            var response = Assert.Throws<Exception>(() => _loanRepository.GetLastYearTransactionAmount(0));
            Assert.That(response.Message, Is.EqualTo("Something went wrong"));
        }
    }
}
