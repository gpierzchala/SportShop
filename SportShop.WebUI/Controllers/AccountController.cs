using System.Web.Mvc;
using SportShop.WebUI.Infrastructure.Abstract;
using SportShop.WebUI.Models;

namespace SportShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthProvider _authProvider;

        public AccountController(IAuthProvider provider)
        {
            _authProvider = provider;
        }

        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.Username, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("","Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

    }
}