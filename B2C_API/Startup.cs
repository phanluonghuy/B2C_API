using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(B2C_API.StartupOwin))]

namespace B2C_API
{
    public partial class StartupOwin
    {
        public void Configuration(IAppBuilder app)
        {
            //AuthStartup.ConfigureAuth(app);
        }
    }
}
