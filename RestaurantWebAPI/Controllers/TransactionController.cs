using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantWebAPI.Models;
using RestaurantWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository transactionRepository;
        private readonly ITransactionDetailRepository transactionDetailRepository;

        public TransactionController(ITransactionRepository transactionRepository, ITransactionDetailRepository transactionDetailRepository)
        {
            this.transactionRepository = transactionRepository;
            this.transactionDetailRepository = transactionDetailRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTransaction()
        {
            try
            {
                return Ok(await transactionRepository.GetTransactions());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Object>> CreateTransaction(TransactionViewModel transactionViewModel)
        {
            try
            {
                if (transactionViewModel==null)
                    return BadRequest();

                // Simpan Transaction terlebih dahulu
                var createdTrans = await transactionRepository.AddTransaction(transactionViewModel.Transaction);

                // Jika Transaction berhasil disimpan, gunakan TransactionId yang baru di-generate
                if (createdTrans != null)
                {
                    transactionViewModel.TransactionDetail.TransactionId = createdTrans.TransactionId;
                    var createdTransDetails = await transactionDetailRepository.AddTransactionDetail(transactionViewModel.TransactionDetail);

                    if (createdTransDetails != null)
                    {                        
                       return CreatedAtAction("CreateTransaction", new { id = createdTransDetails.TransactionDetailId }, createdTransDetails);
                    }
                }

                return BadRequest("Failed to create transaction record");            
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new transaction record");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<Object>> UpdateTransaction(int id, TransactionViewModel transactionViewModel)
        {
            try
            {
                if (id != transactionViewModel.Transaction.TransactionId)
                {
                    return BadRequest("Transaction ID mismatch");
                }
                var transactionUpdateResult = await transactionRepository.UpdateTransaction(transactionViewModel.Transaction);
                var transactionDetailUpdateResult = await transactionDetailRepository.UpdateTransactionDetail(transactionViewModel.TransactionDetail);

                if (transactionUpdateResult != null && transactionDetailUpdateResult != null)
                {                     
                    return Ok("Transaction updated successfully.");
                }
                else
                {                    
                    return BadRequest("Failed to update transaction records.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error update transaction record");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTransaction(int id)
        {
            try
            {
                await transactionRepository.DeleteTransaction(id);
                return Ok($"Transaction with Id = {id} deleted");

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting transaction record");
            }
        }
    }
}
