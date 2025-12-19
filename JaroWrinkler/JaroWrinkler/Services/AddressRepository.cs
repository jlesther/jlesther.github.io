using JaroWrinklerSimilarity.Data;
using JaroWrinklerSimilarity.Models;
using Microsoft.EntityFrameworkCore;

namespace JaroWrinklerSimilarity.Services
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerAddress>> GetAllAddressesAsync()
        {
            return await _context.CustomerAddresses
                .AsNoTracking()
                .Select(a => new CustomerAddress
                {
                    CUST_ADDRESS1 = a.CUST_ADDRESS1,
                    CUST_ADDRESS2 = a.CUST_ADDRESS2,
                    CUST_CITY = a.CUST_CITY,
                    CUST_STATE = a.CUST_STATE
                })
                .ToListAsync();
        }
    }
}

public class AppDbContext : DbContext
{
    // Other DbSet properties

    public DbSet<CustomerAddress> CustomerAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.ToTable("CUSTOMER_ADDRESS", "ACTUAL_OWNER"); // uppercase schema name

            // Configure other properties and relationships
        });

        // Other entity configurations
    }
}