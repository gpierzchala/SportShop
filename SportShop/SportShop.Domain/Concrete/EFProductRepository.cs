using System.Linq;
using SportShop.Domain.Abstract;
using SportShop.Domain.Entities;

namespace SportShop.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EfdbContext _context = new EfdbContext();
        public IQueryable<Product> Products { get { return _context.Products; } }
    }
}
