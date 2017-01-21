using System.Configuration;
using Microsoft.Owin.Hosting;
using API.Genericos;
using System;
using Serilog;

namespace API
{
    public class Webserver
    {
        private IDisposable _webapp;
        private static string puerto = ConfigurationManager.AppSettings["puerto"];
        private static string urls = ConfigurationManager.AppSettings["url"];
        private Logs lg = new Logs();

        public void Start()
        {
            try
            {
                lg.Nuevo(1, "Webserver/Start", "Iniciando IRIS");
                string url = urls + puerto;
                _webapp = WebApp.Start<Startup>(url);
                lg.Nuevo(5, "Webserver/Start", "IRIS iniciado correctamente");
            }
            catch (Exception e)
            {
                lg.Nuevo(3, "Webserver/Start", e.StackTrace);
                Stop();
            }
        }

        public void Stop()
        {
            lg.Nuevo(4, "Webserver/Stop", "IRIS ha sido detenido");
            _webapp?.Dispose();
        }
    }
}
