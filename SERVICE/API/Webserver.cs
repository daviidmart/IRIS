using System.Configuration;
using Microsoft.Owin.Hosting;
using System;

namespace API
{
    public class Webserver
    {
        private IDisposable _webapp;
        private static string puerto = ConfigurationManager.AppSettings["puerto"];
        private static string urls = ConfigurationManager.AppSettings["url"];

        public void Start()
        {
            string url = urls + puerto;
            _webapp = WebApp.Start<Startup>(url);
        }

        public void Stop()
        {
            _webapp?.Dispose();
        }
    }
}
