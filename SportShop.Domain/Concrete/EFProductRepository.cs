using System.Linq;
using SportShop.Domain.Abstract;
using SportShop.Domain.Entities;

namespace SportShop.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        private readonly EfdbContext _context = new EfdbContext();
        public IQueryable<Product> Products { get { return _context.Products; } }
        public void Save(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                Product productFromDB = _context.Products.Find(product.ProductID);
                if (productFromDB != null)
                {
                    productFromDB.Name = product.Name;
                    productFromDB.Description = product.Description;
                    productFromDB.Category = product.Category;
                    productFromDB.Price = product.Price;
                    productFromDB.ImageMimeType = product.ImageMimeType;
                    productFromDB.ImageData = product.ImageData;
                }
            }
            _context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product productFromDB = _context.Products.FirstOrDefault(x => x.ProductID == productId);

            if (productFromDB != null)
            {
                _context.Products.Remove(productFromDB);
                _context.SaveChanges();
            }
            return productFromDB;
        }
    }
}
