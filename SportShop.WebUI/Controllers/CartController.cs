using System.Linq;
using System.Web.Mvc;
using SportShop.Domain.Abstract;
using SportShop.Domain.Entities;
using SportShop.WebUI.Models;

namespace SportShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repo;

        public CartController(IProductRepository repo)
        {
            _repo = repo;
        }
        
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart =cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart,int productId, string returnUrl)
        {
            Product product = _repo.Products.FirstOrDefault(x => x.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new {returnUrl});
        }

        public RedirectToRouteResult RemoverFromCart(Cart cart,int productId, string returnUrl)
        {
            Product product = _repo.Products.FirstOrDefault(x => x.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new {returnUrl});
        }
}
}