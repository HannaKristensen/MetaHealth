using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Class_Project.Startup))]
namespace Class_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
