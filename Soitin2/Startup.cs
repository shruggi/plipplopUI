using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Soitin2.Startup))]
namespace Soitin2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
