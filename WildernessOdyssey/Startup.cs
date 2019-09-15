using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WildernessOdyssey.Startup))]
namespace WildernessOdyssey
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
