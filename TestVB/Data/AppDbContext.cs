using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Data.Entity;
using Microsoft.Extensions.Logging;
using TestVB.Models;
using System.Data.Entity.Infrastructure;

namespace TestVB.Data
{
    public class AppDbContext : DbContext
    {
        private readonly ILogger _logger;

        // Public Property Customers() As DbSet(Of Customer)

        public AppDbContext(ILogger<AppDbContext> logger) : base("DevConnection")
        {
            _logger = logger;

            this.Database.Log = cmd =>
            {
                _logger.LogDebug(cmd);
            };
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
               .ToTable("Customers")
               .HasMany<CustomerShop>(c => c.Shops)
               .WithRequired(s => s.Customer)
               .HasForeignKey(s => s.CustomerId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerShop> Customershops { get; set; }
    }
}