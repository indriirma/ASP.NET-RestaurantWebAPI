using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Repository
{
    public class TransactionDetailRepository : ITransactionDetailRepository
    {
        private readonly PubContext pubContext;

        public TransactionDetailRepository(PubContext pubContext)
        {
            this.pubContext = pubContext;
        }
        public async Task<TransactionDetail> AddTransactionDetail(TransactionDetail transactionDetail)
        {
            var result = await pubContext.TransactionDetails.AddAsync(transactionDetail);
            await pubContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteTransactionDetail(int transactionDetailId)
        {
            var result = await pubContext.TransactionDetails.FirstOrDefaultAsync(e => e.TransactionDetailId == transactionDetailId);
            if (result != null)
            {
                pubContext.TransactionDetails.Remove(result);
                await pubContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TransactionDetail>> GetTransactionDetails()
        {
            return await pubContext.TransactionDetails.ToListAsync();
        }

        public async Task<TransactionDetail> UpdateTransactionDetail(TransactionDetail transactionDetail)
        {
            var result = await pubContext.TransactionDetails.FirstOrDefaultAsync(e => e.TransactionDetailId == transactionDetail.TransactionDetailId);
            if (result != null)
            {
                result.TransactionId = transactionDetail.TransactionId;
                result.Quantity = transactionDetail.Quantity;
                result.FoodId = transactionDetail.FoodId;
                result.Subtotal = transactionDetail.Subtotal;
                if (transactionDetail.TransactionDetailId != 0)
                {
                    result.TransactionDetailId = transactionDetail.TransactionDetailId;

                }
                await pubContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
