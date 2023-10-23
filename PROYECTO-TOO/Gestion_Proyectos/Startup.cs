using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gestion_Proyectos.Startup))]
namespace Gestion_Proyectos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
