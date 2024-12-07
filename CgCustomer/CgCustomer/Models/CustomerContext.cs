using Microsoft.EntityFrameworkCore;

namespace CgCustomer.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base (options)
        {

        }
        public DbSet<CgCustomerDetails> CustomerDetailsList { get; set; } = null!;
    }
}
