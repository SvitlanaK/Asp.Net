using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Market.WEB.UI.Startup))]
namespace Market.WEB.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
