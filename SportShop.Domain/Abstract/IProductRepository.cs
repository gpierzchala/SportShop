using System.Linq;
using SportShop.Domain.Entities;

namespace SportShop.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void Save(Product product);
        Product DeleteProduct(int productId);
    }
}
