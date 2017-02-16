using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(chartEx.Startup))]
namespace chartEx
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
