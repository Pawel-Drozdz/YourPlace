using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YourPlace.Startup))]
namespace YourPlace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
