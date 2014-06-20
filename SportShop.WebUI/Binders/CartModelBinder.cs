using System.Web.Mvc;
using SportShop.Domain.Entities;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;

namespace SportShop.WebUI.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext modelBindingContext)
        {
            Cart cart = (Cart) controllerContext.HttpContext.Session[sessionKey];

            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }
            return cart;
        }
    }
}