using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Models
{
    public class TransactionViewModel
    {
        public Transaction Transaction { get; set; }
        public TransactionDetail TransactionDetail { get; set; }
    }
}
