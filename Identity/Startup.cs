using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

//[assembly: OwinStartup(typeof(Identity.IdentityConfig))]

namespace Identity
{
    public partial class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigurationAuth(app);
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
