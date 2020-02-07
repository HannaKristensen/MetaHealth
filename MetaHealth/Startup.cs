using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MetaHealth.Startup))]
namespace MetaHealth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
