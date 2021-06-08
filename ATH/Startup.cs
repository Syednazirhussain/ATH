using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ATH.Startup))]
namespace ATH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
