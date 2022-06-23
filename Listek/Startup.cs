using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Listek.Startup))]
namespace Listek
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
