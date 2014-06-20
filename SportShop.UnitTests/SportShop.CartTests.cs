using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportShop.Domain.Abstract;
using SportShop.Domain.Entities;
using SportShop.WebUI.Controllers;
using SportShop.WebUI.Models;

namespace SportShop.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void CanAddNewLines()
        {
            //arrange
            Product p1 = new Product {ProductID = 1, Name = "P1"};
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            Cart target = new Cart();

            //act
            target.AddItem(p1,1);
            target.AddItem(p2,1);

            CartLine[] results = target.Lines.ToArray();

            //assert
            Assert.AreEqual(results.Length,2);
            Assert.AreEqual(results[0].Product,p1);
            Assert.AreEqual(results[1].Product,p2);

        }

        [TestMethod]
        public void CanAddQuantityForExistingLines()
        {
            //arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Cart target = new Cart();

            //act
            target.AddItem(p1,1);
            target.AddItem(p2,1);
            target.AddItem(p1,10);

            CartLine[] results = target.Lines.OrderBy(x => x.Product.ProductID).ToArray();

            //assert
            Assert.AreEqual(results.Length,2);
            Assert.AreEqual(results[0].Quantity,11);
            Assert.AreEqual(results[1].Quantity,1);
        }

        [TestMethod]
        public void CanRemoveLine()
        {
            //arrange
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };
            Product p3 = new Product { ProductID = 3, Name = "P3" };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2,1);

            //act
            target.RemoveLine(p2);

            //assert
            Assert.AreEqual(target.Lines.Where(x=>x.Product == p2).Count(),0);
            Assert.AreEqual(target.Lines.Count(),2);
        }

        [TestMethod]
        public void CalculateCartTotal()
        {
            //arrange
            Product p1 = new Product { ProductID = 1, Name = "P1",Price = 100M};
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M};

            Cart target = new Cart();

            //act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1,3);
            decimal result = target.ComputeTotalValue();

            //assert
            Assert.AreEqual(result,450M);
        }

        [TestMethod]
        public void CanClearContents()
        {
            //arrange 
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 100M };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            //act 
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            target.Clear();

            //assert
            Assert.AreEqual(target.Lines.Count(),0);
        }

        [TestMethod]
        public void CanAddToCart()
        {
            // arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x=>x.Products).Returns(new Product[]
            {
                new Product {ProductID = 1,Name = "P1", Category = "Apples"}
            }.AsQueryable);

            Cart cart = new Cart();
            CartController target = new CartController(mock.Object);

            //act
            target.AddToCart(cart, 1, null);

            //assert
            Assert.AreEqual(cart.Lines.Count(),1);
            Assert.AreEqual(cart.Lines.ToArray()[0].Product.ProductID,1);
        }

        [TestMethod]
        public void AddingProductToCartGoesToCartScreen()
        {
            // arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductID = 1,Name = "P1", Category = "Apples"}
            }.AsQueryable);

            Cart cart = new Cart();
            CartController target = new CartController(mock.Object);

            //act
            RedirectToRouteResult result = target.AddToCart(cart, 2, "myUrl");

            //assert

            Assert.AreEqual(result.RouteValues["action"],"Index");
            Assert.AreEqual(result.RouteValues["returnUrl"],"myUrl");
        }

        [TestMethod]
        public void CanViewCartContents()
        {
            //arrange
            Cart cart = new Cart();
            CartController target = new CartController(null);

            //act
            CartIndexViewModel result = (CartIndexViewModel) target.Index(cart, "myUrl").ViewData.Model;

            //assert
            Assert.AreSame(result.Cart,cart);
            Assert.AreEqual(result.ReturnUrl,"myUrl");
        }

    }
}
