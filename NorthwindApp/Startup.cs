using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NorthwindApp.Startup))]
namespace NorthwindApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
