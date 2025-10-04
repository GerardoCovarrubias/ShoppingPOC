using Microsoft.EntityFrameworkCore;
using Models;

namespace ShoppingPOC.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        // Define DbSet properties for your entities here
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetail { get; set; }
        public DbSet<TicketDetail> TicketDetail { get; set; }
        public DbSet<SaleDetail> SalesDetail { get; set; }
        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Stock)             
                .WithOne(s => s.Product)               
                .HasForeignKey<Stock>(p => p.ProductId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.PurchaseDetail)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId);      

            modelBuilder.Entity<Product>()
                .HasMany(p => p.SaleDetail)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId);

            modelBuilder.Entity<Purchase>()
                .HasMany(p => p.PurchaseDetail)
                .WithOne(d => d.Purchase)
                .HasForeignKey(d => d.PurchaseId);

            modelBuilder.Entity<Purchase>()
                .HasMany(p => p.Payment)
                .WithOne(d => d.Purchase)
                .HasForeignKey(d => d.PurchaseId);

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(p => p.Payment)
                .WithOne(d => d.PaymentMethod)
                .HasForeignKey(d => d.PaymentMethodId);

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(p => p.SaleDetail)
                .WithOne(d => d.PaymentMethod)
                .HasForeignKey(d => d.PaymentMethodId);

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(p => p.PurchaseDetail)
                .WithOne(d => d.PaymentMethod)
                .HasForeignKey(d => d.PaymentMethodId);

            modelBuilder.Entity<PaymentMethod>()
               .HasMany(p => p.TicketDetail)
               .WithOne(d => d.PaymentMethod)
               .HasForeignKey(d => d.PaymentMethodId);

            modelBuilder.Entity<Sale>()
                .HasMany(p => p.SaleDetail)
                .WithOne(d => d.Sale)
                .HasForeignKey(d => d.SaleId);

            modelBuilder.Entity<Sale>()
               .HasOne(p => p.Ticket)               
               .WithOne(s => s.Sale)                
               .HasForeignKey<Ticket>(p => p.SaleId);  

            modelBuilder.Entity<Ticket>()
                .HasMany(p => p.TicketDetail)
                .WithOne(d => d.Ticket)
                .HasForeignKey(d => d.TicketId);












        }
}
}





