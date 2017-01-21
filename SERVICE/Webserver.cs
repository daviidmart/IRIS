using System.Configuration;
using Microsoft.Owin.Hosting;
using API.Genericos;
using System;
using API.Workers;
using System.Threading;

namespace API
{
    public class Webserver
    {
        private IDisposable _webapp;
        static string puerto = ConfigurationManager.AppSettings["puerto"];
        static string urls = ConfigurationManager.AppSettings["url"];
        static int checkTime = int.Parse(ConfigurationManager.AppSettings["checkTime"]);
        private Logs lg = new Logs();
        GSM gsm = new GSM();

        public void Start()
        {
            try
            {
                lg.Nuevo(1, "Webserver/Start", "Iniciando IRIS");
                string url = urls + puerto;
                _webapp = WebApp.Start<Startup>(url);
                new Timer(e => gsm.ServerStatus(), null, TimeSpan.Zero, TimeSpan.FromMinutes(checkTime));
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
