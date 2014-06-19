using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportShop.Domain.Abstract;

namespace SportShop.WebUI.Controllers
{
    public class NavController : Controller
    {
        private readonly IProductRepository _repo;

        public NavController(IProductRepository repo)
        {
            _repo = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = _repo.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView("_Menu",categories);
        }

    }
}