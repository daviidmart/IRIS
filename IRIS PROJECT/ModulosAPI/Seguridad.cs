using API.Genericos;
using Calabonga.Utils.TokenGenerator;
using Jwt;
using Nancy;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace API.ModulosAPI
{
    public class Seguridad : NancyModule
    {
        static string semilla = ConfigurationManager.AppSettings["semilla"];
        static string secretKey = ConfigurationManager.AppSettings["jwtsec"];
        static string v = ConfigurationManager.AppSettings["version"];
        public Respuestas RES = new Respuestas();
        private Logs lg = new Logs();

        public Seguridad() : base(Seguridad.v)
        {
            Get["/auth/{user}/{pasw}"] = p =>
            {
                object r = null;
                HttpStatusCode statusCode = HttpStatusCode.BadRequest;
                try
                {
                    string authu = p.user;
                    string authp = p.pasw;
                    if (!new Comprobaciones().CheckCredentialsFormat(1, authu, authp))
                    {
                        r = RES.Generar(0, 4001, "La longitud del usuario o la contraseña no es valida", false, 0f, 0f);
                        statusCode = HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        Database database = new Database();
                        GetHttpStatus getHttpStatus = new GetHttpStatus();
                        TokenGenerator.Generate(25).ToUpper();
                        JObject data = JObject.Parse("{ user: '" + authu + "', pasw: '" + BCrypt.Net.BCrypt.HashPassword(authp, semilla) + "' }");
                        JObject jObject = database.EjecutarProc("AUTUS", data);
                        statusCode = getHttpStatus.ToHttpStatusCode((int)jObject["codigo"]);
                        var payload = new Dictionary<string, object>()
                        {
                            { "user", authu },
                            { "pasw", BCrypt.Net.BCrypt.HashPassword(authp, semilla) }
                        };
                        if ((string)jObject["mensaje"] == "true")
                        {
                            string m = JsonWebToken.Encode(payload, secretKey, JwtHashAlgorithm.HS256);
                            r = RES.Generar((int)jObject["tipo"], (int)jObject["codigo"], m, (bool)jObject["status"], 0f, 0f);
                        }
                        else
                        {
                            r = RES.Generar((int)jObject["tipo"], (int)jObject["codigo"], (string)jObject["mensaje"], (bool)jObject["status"], 0f, 0f);
                        }
                    }
                }
                catch (Exception e)
                {
                    r = RES.Generar(0, 4000, "Error desconocido", false);
                    lg.Nuevo(3, "SEGURIDAD/GET", e.ToString());
                }
                return Negotiate.WithStatusCode(statusCode).WithContentType("application/json").WithModel(r);
            };
        }
    }
}