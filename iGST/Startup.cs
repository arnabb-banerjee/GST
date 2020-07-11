using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(iGST.Startup))]
namespace iGST
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
