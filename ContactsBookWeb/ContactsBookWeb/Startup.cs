using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ContactsBookWeb.Startup))]
namespace ContactsBookWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
