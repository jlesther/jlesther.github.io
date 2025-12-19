using JaroWrinklerSimilarity.Models;
using Microsoft.EntityFrameworkCore;

namespace JaroWrinklerSimilarity.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerAddress> CustomerAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.ToTable("CUSTOMER_ADDRESS", "ACTUAL_OWNER");

                entity.HasKey(e => e.EXTRNL_CLIENT_ID);

                entity.Property(e => e.EXTRNL_CLIENT_ID)
                      .HasColumnName("EXTRNL_CLIENT_ID")
                      .IsRequired();

                entity.Property(e => e.CUST_ADDRESS1)
                      .HasColumnName("CUST_ADDRESS1");

                entity.Property(e => e.CUST_ADDRESS2)
                      .HasColumnName("CUST_ADDRESS2");

                entity.Property(e => e.CUST_CITY)
                      .HasColumnName("CUST_CITY");

                entity.Property(e => e.CUST_STATE)
                      .HasColumnName("CUST_STATE");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}