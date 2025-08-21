using ApplicationLayer.Interfaces;
using CoreModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Infrastructure
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public LoanRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public double GetLastYearTransactionAmount(long receiverAccountNumber)
        {
            try
            {
                var oneYearAgo = DateTime.Now.AddYears(-1);
                double transactionAmount = _dbContext.Set<TransactionsEntity>().Where(transaction => transaction.ReceiverAccountNumber == receiverAccountNumber && transaction.TransactionTime >= oneYearAgo).Sum(amount => amount.Amount);
                return transactionAmount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
