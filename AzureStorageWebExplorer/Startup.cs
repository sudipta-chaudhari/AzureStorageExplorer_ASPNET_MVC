using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AzureStorageWebExplorer.Startup))]

namespace AzureStorageWebExplorer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            ConfigureAuth(app);
        }
        public void ConfigureAuth(IAppBuilder app)
        {
        }
    }
}
