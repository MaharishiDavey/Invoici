using Invoici.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Invoici.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Invoice to InvoiceItem one-to-many relationship
            modelBuilder.Entity<Invoice>()
                .HasMany(i => i.InvoiceItems)
                .WithOne(ii => ii.Invoices)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for InvoiceItems when Invoice is deleted

            // Seed data for Invoice
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice { InvoiceId = "1", PartyName = "ABC Corporation", Description = "Office Supplies Purchase", Date = DateTime.Now, GSTNumber = "27AABCU9603R1ZV" },
                new Invoice { InvoiceId = "2", PartyName = "XYZ Industries", Description = "IT Services Billing", Date = DateTime.Now, GSTNumber = "27AAECS1223M1ZV" },
                new Invoice { InvoiceId = "3", PartyName = "LMN Traders", Description = "Raw Materials Invoice", Date = DateTime.Now, GSTNumber = "27AAGCS1223R1ZV" }
            );

            // Seed data for InvoiceItem
            modelBuilder.Entity<InvoiceItem>().HasData(
                new InvoiceItem { InvoiceItemId = 1, Particular = "Product A", HSNCode = "1234", Quantity = 5, Rate = 100, InvoiceId = "1" },
                new InvoiceItem { InvoiceItemId = 2, Particular = "Product B", HSNCode = "5678", Quantity = 3, Rate = 200, InvoiceId = "1"},
                new InvoiceItem { InvoiceItemId = 3, Particular = "Service C", HSNCode = "9012", Quantity = 1, Rate = 500, InvoiceId = "2" }
            );
        }

    }

}
