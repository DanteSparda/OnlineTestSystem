using Microsoft.Owin;

using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineTestSystem.Web.Startup))]

namespace OnlineTestSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
