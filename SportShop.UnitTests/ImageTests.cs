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
    public class ImageTests
    {
        [TestMethod]
        public void CanRetrieveImageData()
        {
            //arrange

            Product p = new Product
            {
                ProductID = 2,
                Name = "Test",
                ImageData = new byte[] {},
                ImageMimeType = "image/png"
            };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductID = 1,Name = "P1"},
                p,
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object);


            //act
            ActionResult result = target.GetImage(2);

            //assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result,typeof(FileResult));
            Assert.AreEqual(p.ImageMimeType,((FileResult)result).ContentType);
        }

        [TestMethod]
        public void CannotRetrieveImageDataForInvalidID()
        {
            //arrange

            Product p = new Product
            {
                ProductID = 2,
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductID = 1,Name = "P1"},
                p,
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable());

            ProductController target = new ProductController(mock.Object);

            //act
            ActionResult result = target.GetImage(100);

            //assert
            Assert.IsNull(result);
        }
    }
}
