using Serilog;
using System.Configuration;

namespace API.Genericos
{
    public class Logs
    {
        private static string lgKey = ConfigurationManager.AppSettings["lgKey"];
        public void Nuevo(int tipo = 0, string origen = null, string mensaje= null, string extra = null)
        {
            var log = new LoggerConfiguration().WriteTo.Logentries(lgKey).CreateLogger();
            switch (tipo)
            {
                case 1:
                    //EN PROCESO
                    log.Information($"status=1 origen='{origen}' mensaje='{mensaje}' extra='{extra}'");
                    break;
                case 2:
                    //FALLIDO
                    log.Error($"status=2 origen='{origen}' mensaje='{mensaje}' extra='{extra}'");
                    break;
                case 3:
                    //ERROR CRITICO
                    log.Fatal($"status=3 origen='{origen}' mensaje='{mensaje}' extra='{extra}'");
                    break;
                case 4:
                    //ADVERTENCIA
                    log.Warning($"status=4 origen='{origen}' mensaje='{mensaje}' extra='{extra}'");
                    break;
                case 5:
                    //EXITOSO
                    log.Information($"status=5 origen='{origen}' mensaje='{mensaje}' extra='{extra}'");
                    break;
                case 6:
                    //API SUCCESS
                    log.Information($"status=6 origen='{origen}' mensaje='{mensaje}' extra='{extra}'");
                    break;
                case 7:
                    //API ERROR
                    log.Information($"status=7 origen='{origen}' mensaje='{mensaje}' extra='{extra}'");
                    break;
                default:
                    //SIN STATUS
                    log.Information($"status=0 origen='{origen}' mensaje='{mensaje}' extra='{extra}'");
                    break;
            }
        }
    }
}
