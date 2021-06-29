using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockMVC.Models;
using StockMVC.ViewModel;

namespace StockMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<staff> staffs { get; set; }
        public DbSet<OrderedItem> orderedItems { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<PaymentMode> paymentModes { get; set; }
        public DbSet<StockLevel> StockLevels { get; set; }
        public DbSet<NewStockLists> NewStockLists { get; set; }
        public DbSet<OtherDebit> OtherDebits { get; set; }
        public DbSet<DebitMode> DebitModes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderedItem>()
                .HasKey(c => new { c.OrderId, c.ProductId });
        }



        public DbSet<StockMVC.ViewModel.CreateOtherDebitVM> CreateOtherDebitVM { get; set; }



        
    }
}
