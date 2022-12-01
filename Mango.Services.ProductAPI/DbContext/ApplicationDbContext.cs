
using Mango.Services.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // db set products entities
        public DbSet<Product> Products { get; set; }
        public DbSet<CartHeader> CartHeaders { get;set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<Coupon> Coupons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // build table Products 
        }
    }
}
