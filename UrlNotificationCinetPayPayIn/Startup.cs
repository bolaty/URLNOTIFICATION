using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UrlNotificationCinetPayPayIn.Startup))]
namespace UrlNotificationCinetPayPayIn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
