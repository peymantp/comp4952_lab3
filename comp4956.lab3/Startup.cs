using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(comp4956.lab3.Startup))]
namespace comp4956.lab3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
