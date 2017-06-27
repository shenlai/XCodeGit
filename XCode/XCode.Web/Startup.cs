using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XCode.Web.Startup))]
namespace XCode.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
