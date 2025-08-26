using Microsoft.EntityFrameworkCore;
using Models;

namespace ShoppingPOC.Services
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        // Define DbSet properties for your entities here
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Purchase> Purchase { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetail { get; set; }
        public DbSet<TicketDetail> TicketDetail { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Stock)              // Usuario tiene un Pasaporte
                .WithOne(s => s.Product)               // Pasaporte tiene un Usuario
                .HasForeignKey<Stock>(p => p.ProductId); // Clave foránea en Pasaporte
            
            modelBuilder.Entity<PurchaseDetail>()
                .HasMany(p => p.Product)              // Producto has many StockMovimientos
                .WithOne(d => d.PurchaseDetail)                 // Each StockMovimiento has one Producto
                .HasForeignKey(d => d.ProductId);        // Foreign key in StockMovimiento

            modelBuilder.Entity<Product>()
                .HasMany(p => p.SalesDetail)
                .WithOne(d => d.Product)
                .HasForeignKey(d => d.ProductId);

            modelBuilder.Entity<Purchase>()
                .HasMany(p => p.PurchaseDetail)
                .WithOne(d => d.Purchase)
                .HasForeignKey(d => d.PurchaseId);








            modelBuilder.Entity<Sale>()
                .ToTable("Sale"); // Nombre deseado en la base de datos

        
    }
}
}





