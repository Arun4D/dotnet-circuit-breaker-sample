using Microsoft.EntityFrameworkCore;

namespace SdCustomer.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base (options)
        {

        }
        public DbSet<CustomerDetails> CustomerDetailsList { get; set; } = null!;
    }
}
