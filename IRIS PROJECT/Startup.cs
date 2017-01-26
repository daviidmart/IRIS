using Nancy;
using Owin;

public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        app.UseNancy();
        app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
    }
}