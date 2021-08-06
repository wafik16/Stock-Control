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
        public DbSet<WholesalePrices> WholesalePrices { get; set; }
        public DbSet<WholesaleOrderedItem> WholesaleOrderedItems { get; set; }
        public DbSet<WholesaleOrder> WholesaleOrders { get; set; }
        public DbSet<WholesaleStockLevel> wholesaleStockLevels { get; set; }

        public DbSet<WholesaleNewStockLists> wholesaleNewStockLists { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<OrderedItem>()
                .HasKey(c => new { c.OrderId, c.ProductId });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WholesalePrices>()
                .HasKey(c => new { c.CustomerId, c.ProductId });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WholesaleOrderedItem>()
                .HasKey(c => new { c.OrderId, c.ProductId });

            modelBuilder.Entity<PaymentMode>().HasData(
       new PaymentMode { PaymentModeId = 1, ModeOfPayment = "CASH" },
       new PaymentMode { PaymentModeId = 2, ModeOfPayment = "POS" },
       new PaymentMode { PaymentModeId = 3, ModeOfPayment = "TRANSFER" },
       new PaymentMode { PaymentModeId = 4, ModeOfPayment = "CREDIT" }
        );

            modelBuilder.Entity<DebitMode>().HasData(
       new DebitMode { DebitModeId = 1, ModeOfDebit = "DAMAGE" },
       new DebitMode { DebitModeId = 2, ModeOfDebit = "PR" },
       new DebitMode { DebitModeId = 3, ModeOfDebit = "WEIGH LOSS" },
       new DebitMode { DebitModeId = 4, ModeOfDebit = "WEIGHT GAIN" }
        );
        }



        public DbSet<StockMVC.ViewModel.CreateOtherDebitVM> CreateOtherDebitVM { get; set; }

       
        
   

        
    }
}
