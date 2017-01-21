using API.Genericos;
using Microsoft.CSharp.RuntimeBinder;
using Nancy;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace API.ModulosAPI
{
    public class SMS : NancyModule
    {
        public Respuestas RES = new Respuestas();
        private Logs lg = new Logs();
        static string v = ConfigurationManager.AppSettings["version"];

        public SMS() : base(v) //Asignamos la version de la api
        {
            Post["/sms/{apikey}/{apisecret}", true] = async (x, ct) =>
            {
                string apik = x.apikey;
                string apis = x.apisecret;
                object r = null;
                HttpStatusCode HTTPStat = HttpStatusCode.BadRequest;
                try
                {
                    if (!new Comprobaciones().CheckCredentialsFormat(0, apik, apis))
                    {
                        r = RES.Generar(0, 4001, "La longitud de la API Key o el API Secret no es valida", false);
                    }
                    else
                    {
                        Database database = new Database();
                        GetHttpStatus stc = new GetHttpStatus();
                        JObject data = JObject.Parse("{ authu:'" + apik + "', autht:'" + apis + "', ip:'" + Request.UserHostAddress + "' }");
                        JObject CallProc = database.EjecutarProc("SMANA", data);
                        if ((bool)CallProc["status"])
                        {
                            JObject body = JObject.Parse(Context.Request.Body.ReadAsString());
                            PeticionesHTTP peticionesHttp = new PeticionesHTTP();
                            int num = new Random().Next(0, 999999999);
                            int tid = num;
                            JObject respuestaSMS = await peticionesHttp.EnviarSMS(CallProc, body, tid, apik);
                            HTTPStat = stc.ToHttpStatusCode((int)respuestaSMS["codigo"]);
                            r = RES.Generar((int)respuestaSMS["tipo"], (int)respuestaSMS["codigo"], (string)respuestaSMS["mensaje"], (bool)respuestaSMS["status"]);
                        }
                        else
                        {
                            HTTPStat = stc.ToHttpStatusCode((int)CallProc["codigo"]);
                            r = RES.Generar((int)CallProc["tipo"], (int)CallProc["codigo"], (string)CallProc["mensaje"], (bool)CallProc["status"]);
                        }
                        stc = (GetHttpStatus)null;
                        CallProc = (JObject)null;
                    }
                }
                catch (Exception e)
                {
                    r = RES.Generar(0, 4000, "Error desconocido", false, 0.0f, 0.0f);
                    lg.Nuevo(3, "SMS/POST", e.ToString());
                }
                return Negotiate.WithStatusCode(HTTPStat).WithContentType("application/json").WithModel(r);
            };
        }
    }
}