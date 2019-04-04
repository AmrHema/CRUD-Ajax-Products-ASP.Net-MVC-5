using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUD_jQuery_Ajax.Startup))]
namespace CRUD_jQuery_Ajax
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
