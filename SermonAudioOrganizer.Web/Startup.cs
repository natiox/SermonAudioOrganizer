using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SermonAudioOrganizer.Startup))]
namespace SermonAudioOrganizer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
