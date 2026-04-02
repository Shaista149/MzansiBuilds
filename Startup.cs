using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MzansiBuilds.Startup))]
namespace MzansiBuilds
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
