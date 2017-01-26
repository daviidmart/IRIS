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
    public class Usuarios : NancyModule
    {
        static string semilla = ConfigurationManager.AppSettings["semilla"];
        static string secretKey = ConfigurationManager.AppSettings["jwtsec"];
        static string v = ConfigurationManager.AppSettings["version"];
        public Respuestas RES = new Respuestas();
        private Logs lg = new Logs();

        public Usuarios() : base(Usuarios.v)
        {
            Post["/user/{token}"] = p =>
            {
                object r = null;
                HttpStatusCode statusCode = HttpStatusCode.BadRequest;
                GetHttpStatus getHttpStatus = new GetHttpStatus();
                JObject jObject = JObject.Parse(Context.Request.Body.ReadAsString());
                string token = p.token;
                try
                {
                    Dictionary<string, object> jsontoken = JsonWebToken.DecodeToObject<Dictionary<string, object>>(token, secretKey, true);
                    string text = jsontoken["user"].ToString();
                    string text2 = jsontoken["pasw"].ToString();
                    if (!new Comprobaciones().CheckCredentialsFormat(2, text, text2))
                    {
                        r = RES.Generar(0, 4001, "La longitud del usuario o la contraseña no es valida", false, 0f, 0f);
                        statusCode = HttpStatusCode.BadRequest;
                    }
                    else
                    {
                        Database arg_1D3_0 = new Database();
                        jObject["authu"] = text;
                        jObject["authp"] = text2;
                        jObject["psdw"] = BCrypt.Net.BCrypt.HashPassword((string)jObject["psdw"], Usuarios.semilla);
                        jObject["apik"] = TokenGenerator.Generate(8).ToUpper();
                        jObject["apis"] = TokenGenerator.Generate(14).ToUpper();
                        JObject jObject2 = arg_1D3_0.EjecutarProc("NUSUA", jObject);
                        statusCode = getHttpStatus.ToHttpStatusCode((int)jObject2["codigo"]);
                        r = RES.Generar((int)jObject2["tipo"], (int)jObject2["codigo"], (string)jObject2["mensaje"], (bool)jObject2["status"], 0f, 0f);
                    }
                }
                catch (Exception e)
                {
                    if (e.Source == "JsonWebTokens")
                    {
                        r = RES.Generar(0, 4002, "Token invalido", false, 0f, 0f);
                        statusCode = getHttpStatus.ToHttpStatusCode(4002);
                    }
                    else
                    {
                        r = RES.Generar(0, 4000, "Error desconocido", false, 0f, 0f);
                        lg.Nuevo(3, "SMS/POST", e.ToString());
                    }
                }
                return Negotiate.WithStatusCode(statusCode).WithContentType("application/json").WithModel(r).WithHeader("Access-Control-Allow-Origin", "*").WithHeader("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE").WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            };

            Get["/user/{token}"] = p =>
            {
                GetHttpStatus getHttpStatus = new GetHttpStatus();
                object r = null;
                HttpStatusCode statusCode = HttpStatusCode.BadRequest;
                string token = p.token;
                try
                {
                    Dictionary<string, object> jsontoken = JsonWebToken.DecodeToObject<Dictionary<string, object>>(token, secretKey, true);
                    string text = jsontoken["user"].ToString();
                    string text2 = jsontoken["pasw"].ToString();
                    if (!new Comprobaciones().CheckCredentialsFormat(2, text, text2))
                    {
                        r = RES.Generar(0, 4001, "La longitud del usuario o la contraseña no es valida", false, 0f, 0f);
                        statusCode = getHttpStatus.ToHttpStatusCode(4001);
                    }
                    else
                    {
                        Database database = new Database();
                        JObject data = JObject.Parse(string.Concat(new string[]
                        {
                            "{ authu: '",
                            text,
                            "', authp: '",
                            text2,
                            "'}"
                        }));
                        JObject jObject = database.EjecutarProc("OBMID", data);
                        statusCode = getHttpStatus.ToHttpStatusCode((int)jObject["codigo"]);
                        if (!(bool)jObject["status"])
                        {
                            r = RES.Generar((int)jObject["tipo"], (int)jObject["codigo"], (string)jObject["mensaje"], (bool)jObject["status"], 0f, 0f);
                        }
                        else
                        {
                           r = new
                            {
                                status = true,
                                code = 1000,
                                usuario = (string)jObject["USN"],
                                apik = (string)jObject["UAK"],
                                apis = (string)jObject["UAS"],
                                balance = (float)jObject["UBA"],
                                price = (float)jObject["UPR"],
                                nivel = (int)jObject["UNI"]
                            };
                        }
                    }
                }
                catch (Exception e)
                {
                    if (e.Source == "JsonWebTokens")
                    {
                        r = RES.Generar(0, 4002, "Token invalido", false);
                        statusCode = getHttpStatus.ToHttpStatusCode(4001);
                    }
                    else
                    {
                        r = RES.Generar(0, 4000, "Error desconocido", false);
                        lg.Nuevo(3, "USURIOS/GET", e.ToString());
                    }
                }
                return Negotiate.WithStatusCode(statusCode).WithContentType("application/json").WithModel(r).WithHeader("Access-Control-Allow-Origin", "*").WithHeader("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE").WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type"); ;
            };
        }
    }
}