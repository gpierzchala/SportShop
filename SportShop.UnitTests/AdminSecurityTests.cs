using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportShop.WebUI.Controllers;
using SportShop.WebUI.Infrastructure.Abstract;
using SportShop.WebUI.Models;

namespace SportShop.UnitTests
{
    [TestClass]
    public class AdminSecurityTests
    {
        [TestMethod]
        public void CanLoginWithValidCredentials()
        {
            //arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(x => x.Authenticate("admin", "P@ssw0rd")).Returns(true);

            LoginViewModel model = new LoginViewModel
            {
                Password = "P@ssw0rd",
                Username = "admin"
            };

            AccountController target = new AccountController(mock.Object);

            //act
            ActionResult result = target.Login(model, "/MyUrl");

            //assert
            Assert.IsInstanceOfType(result,typeof(RedirectResult));
            Assert.AreEqual("/MyUrl",((RedirectResult)result).Url);
        }

        [TestMethod]
        public void CannotLoginWithInvalidCredentials()
        {
            //arrange
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(x => x.Authenticate("xxx", "yyy")).Returns(false);

            LoginViewModel model = new LoginViewModel
            {
                Password = "yyy",
                Username = "xxx"
            };

            AccountController target = new AccountController(mock.Object);

            //act
            ActionResult result = target.Login(model, "/MyUrl");

            //assert
            Assert.IsInstanceOfType(result,typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }

    }
}
