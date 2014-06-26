using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportShop.Domain.Abstract;
using SportShop.Domain.Entities;
using SportShop.WebUI.Controllers;

namespace SportShop.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void IndexContainsAllProducts()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductID = 1,Name = "P1"},
                 new Product {ProductID = 2,Name = "P2"},
                  new Product {ProductID = 3,Name = "P3"},
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            //act
            Product[] result = ((IEnumerable<Product>)target.Index().ViewData.Model).ToArray();

            //assert
            Assert.AreEqual(result.Length,3);
            Assert.AreEqual("P1",result[0].Name);
            Assert.AreEqual("P2", result[1].Name);
            Assert.AreEqual("P3", result[2].Name);
        }

        [TestMethod]
        public void CanEditProduct()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductID = 1,Name = "P1"},
                 new Product {ProductID = 2,Name = "P2"},
                  new Product {ProductID = 3,Name = "P3"},
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            //act
            Product p1 = target.Edit(1).ViewData.Model as Product;
            Product p2 = target.Edit(2).ViewData.Model as Product;
            Product p3 = target.Edit(3).ViewData.Model as Product;

            //assert
            Assert.AreEqual(1,p1.ProductID);
            Assert.AreEqual(2, p2.ProductID);
            Assert.AreEqual(3, p3.ProductID);
        }

        [TestMethod]
        public void CannotEditNonexistentProduct()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductID = 1,Name = "P1"},
                 new Product {ProductID = 2,Name = "P2"},
                  new Product {ProductID = 3,Name = "P3"},
            }.AsQueryable());

            AdminController target = new AdminController(mock.Object);

            //act
            Product result = (Product) target.Edit(4).ViewData.Model;

            //assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CanSaveValidChanges()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);

            Product product = new Product {Name = "Test"};

            //act
            ActionResult result = target.Edit(product,null);

            //assert
            mock.Verify(x=>x.Save(product));
            Assert.IsNotInstanceOfType(result,typeof(ViewResult));
        }

        [TestMethod]
        public void CannotSaveInvalidChanges()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            AdminController target = new AdminController(mock.Object);
            Product product = new Product {Name = "Test"};
            target.ModelState.AddModelError("error","error");

            //act
            ActionResult result = target.Edit(product,null);

            //assert
            mock.Verify(x=>x.Save(It.IsAny<Product>()),Times.Never());
            Assert.IsInstanceOfType(result,typeof(ViewResult));
        }

        [TestMethod]
        public void CanDeleteValidProducts()
        {
            //arrange
            Product product = new Product {ProductID = 2, Name = "P2"};

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
              new Product {ProductID = 1, Name = "P1"},
              product,
              new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable);

            AdminController target = new AdminController(mock.Object);

            //act
            target.Delete(product.ProductID);

            //assert
            mock.Verify(x=>x.DeleteProduct(product.ProductID));

        }
    }
}
