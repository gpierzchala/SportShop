﻿using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportShop.Domain.Abstract;
using SportShop.Domain.Entities;

namespace SportShop.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _repo;

        public AdminController(IProductRepository repo)
        {
            _repo = repo;
        }

        public ViewResult Index()
        {
            return View(_repo.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = _repo.Products.FirstOrDefault(x => x.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                _repo.Save(product);
                TempData["message"] = string.Format("Saved {0}", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = _repo.DeleteProduct(productId);

            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("Product {0} was deleted", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}