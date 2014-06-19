using System.Linq;
using System.Web.Mvc;
using SportShop.Domain.Abstract;
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

        public ViewResult List(int page = 1)
        {
            var viewModel = new ProductsListViewModel
            {
                Products = _producRepo.Products
                    .OrderBy(x => x.ProductID)
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize),

                PaginingInfo = new PaginingInfo
                {
                    CurrentPage = page,
                    ItemstPerPage = PageSize,
                    TotalItems = _producRepo.Products.Count()
                }
            };

            return View(viewModel);
        }
    }
}