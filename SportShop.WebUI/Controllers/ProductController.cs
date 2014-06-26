using System.Linq;
using System.Web.Mvc;
using SportShop.Domain.Abstract;
using SportShop.Domain.Entities;
using SportShop.WebUI.Models;

namespace SportShop.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _producRepo;
        public int PageSize = 4;
        public ProductController(IProductRepository productRepo)
        {
            _producRepo = productRepo;
        }

        public ViewResult List(string category,int page = 1)
        {
            var viewModel = new ProductsListViewModel
            {
                Products = _producRepo.Products
                    .Where(x=>x.Category == null || x.Category == category)
                    .OrderBy(x => x.ProductID)
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize),

                PaginingInfo = new PaginingInfo
                {
                    CurrentPage = page,
                    ItemstPerPage = PageSize,
                    TotalItems = category == null ? _producRepo.Products.Count() : _producRepo.Products.Where(x=>x.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(viewModel);
        }

        public FileContentResult GetImage(int productId)
        {
            Product product = _producRepo.Products.FirstOrDefault(x => x.ProductID == productId);

            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}