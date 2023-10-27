using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Data;
using RestaurantWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly PubContext pubContext;

        public CustomerRepository(PubContext pubContext)
        {
            this.pubContext = pubContext;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            var result = await pubContext.Customers.AddAsync(customer);
            await pubContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCustomer(int customerId)
        {
            var result = await pubContext.Customers.FirstOrDefaultAsync(e=>e.CustomerId==customerId);
            if(result!=null)
            {
                pubContext.Customers.Remove(result);
                await pubContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await pubContext.Customers.ToListAsync();
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var result = await pubContext.Customers.FirstOrDefaultAsync(e=>e.CustomerId==customer.CustomerId);
            if(result!=null)
            {
                result.CustomerName = customer.CustomerName;
                result.Address = customer.Address;
                if(customer.CustomerId!=0)
                {
                    result.CustomerId = customer.CustomerId;
                     
                }
                await pubContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
