using System.Data.Entity;
using SportShop.Domain.Entities;

namespace SportShop.Domain.Concrete
{
    public class EfdbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
 
    }
}
