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
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Purchases> Purchase { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }



    }
}
