using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EnvioEmail.Startup))]
namespace EnvioEmail
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
