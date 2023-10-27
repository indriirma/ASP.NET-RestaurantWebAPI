using Microsoft.EntityFrameworkCore;
using RestaurantWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantWebAPI.Data
{
    public class PubContext : DbContext
    {
        public PubContext(DbContextOptions<PubContext> options) : base(options)
        { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Transaction> Transactions {get; set;}
        public DbSet<TransactionDetail> TransactionDetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Nana",
                    Address = "Bandung"
                }) ;

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Galih",
                    Address = "Jakarta"
                });

            modelBuilder.Entity<Food>().HasData(
                new Food
                {
                    FoodId = 1,
                    FoodName = "Nasi Goreng",
                    price = 10000
                });

            modelBuilder.Entity<Food>().HasData(
                new Food
                {
                    FoodId = 2,
                    FoodName = "Seblak",
                    price = 20000
                });
        }
    }
}
