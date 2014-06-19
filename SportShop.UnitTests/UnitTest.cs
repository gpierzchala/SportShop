using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportShop.Domain.Abstract;
using SportShop.Domain.Entities;
using SportShop.WebUI.Controllers;
using SportShop.WebUI.HtmlHelpers;
using SportShop.WebUI.Models;

namespace SportShop.UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CanPaginate()
        {
            //arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product{ProductID = 1,Name = "P1"},
                new Product{ProductID = 2,Name = "P2"},
                new Product{ProductID = 3,Name = "P3"},
                new Product{ProductID = 4,Name = "P4"},
                new Product{ProductID = 5,Name = "P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            
            
            //act 
            ProductsListViewModel result = (ProductsListViewModel)controller.List(2).Model;

            //assert
            Product[] products = result.Products.ToArray();
            Assert.IsTrue(products.Length == 2);
            Assert.AreEqual(products[0].Name, "P4");
            Assert.AreEqual(products[1].Name, "P5");
        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {

            // arrange
            HtmlHelper helper = null;

            PaginingInfo paginingInfo = new PaginingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemstPerPage = 10
            };

            Antlr.Runtime.Misc.Func<int, string> pageUrlDelegate = i => "Page" + i;

            // act

            MvcHtmlString result = helper.PageLinks(paginingInfo, pageUrlDelegate);

            //assert
            string resultString = "<a href=\"Page1\">1</a><a class=\"selected\" href=\"Page2\">2</a><a href=\"Page3\">3</a>";
            Assert.AreEqual(result.ToString(), resultString);
        }

        [TestMethod]
        public void CanSendPaginationViewModel()
        {
            //arrange

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product{ProductID = 1,Name = "P1"},
                new Product{ProductID = 2,Name = "P2"},
                new Product{ProductID = 3,Name = "P3"},
                new Product{ProductID = 4,Name = "P4"},
                new Product{ProductID = 5,Name = "P5"}
            }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //act 

            ProductsListViewModel result = (ProductsListViewModel) controller.List(2).Model;

            //assert
            PaginingInfo pageInfo = result.PaginingInfo;
            Assert.AreEqual(pageInfo.CurrentPage,2);
            Assert.AreEqual(pageInfo.ItemstPerPage,3);
            Assert.AreEqual(pageInfo.TotalItems,5);
            Assert.AreEqual(pageInfo.TotalPages,2);
        }
    }
}
