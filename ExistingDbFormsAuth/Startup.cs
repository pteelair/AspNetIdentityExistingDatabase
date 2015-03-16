using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ExistingDbFormsAuth.Startup))]
namespace ExistingDbFormsAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
