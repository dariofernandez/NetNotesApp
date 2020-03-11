using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetNotes.Startup))]
namespace NetNotes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
