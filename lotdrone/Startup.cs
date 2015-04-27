using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lotdrone.Startup))]
namespace lotdrone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
