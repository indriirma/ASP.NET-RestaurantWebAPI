using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace RestaurantWebAPI.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly PubContext pubContext;
        public TransactionRepository(PubContext pubContext)
        {
            this.pubContext = pubContext;
        }
        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            var result = await pubContext.Transactions.AddAsync(transaction);
            await pubContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteTransaction(int transactionId)
        {
            var result = await pubContext.Transactions.FirstOrDefaultAsync(e => e.TransactionId == transactionId);
            if (result != null)
            {
                pubContext.Transactions.Remove(result);
                await pubContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Object>> GetTransactions()
        {
            var query = from transaction in pubContext.Transactions
                        join transactionDetail in pubContext.TransactionDetails
                        on transaction.TransactionId equals transactionDetail.TransactionId
                        select new  
                        {
                            Transaction = transaction,
                            TransactionDetail = transactionDetail
                        };
            return await query.ToListAsync();
        }

        public async Task<Transaction> UpdateTransaction(Transaction transaction)
        {
            var result = await pubContext.Transactions.FirstOrDefaultAsync(e => e.TransactionId == transaction.TransactionId);
            if (result != null)
            {
                result.TransactionId = transaction.TransactionId;
                result.TransactionDate = transaction.TransactionDate;
                if (transaction.TransactionId != 0)
                {
                    result.TransactionId = transaction.TransactionId;

                }
                await pubContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
