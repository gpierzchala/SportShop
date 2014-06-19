using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportShop.WebUI.Startup))]
namespace SportShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
