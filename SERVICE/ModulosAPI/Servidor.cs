using API.Genericos;
using DataSecurity;
using Jwt;
using Nancy;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace API.ModulosAPI
{
    public class Servidor : NancyModule
    {
        static string semilla = ConfigurationManager.AppSettings["semilla"];
        static string secretKey = ConfigurationManager.AppSettings["jwtsec"];
        static string v = ConfigurationManager.AppSettings["version"];
        public Respuestas RES = new Respuestas();
        private Logs lg = new Logs();

        public Servidor() : base(v)
        {
            Post["/gsm/{token}"] = p =>
            {
                object r = null;
                HttpStatusCode statusCode = HttpStatusCode.BadRequest;
                try
                {
                    GetHttpStatus getHttpStatus = new GetHttpStatus();
                    JObject jObject = JObject.Parse(Context.Request.Body.ReadAsString());
                    string se = (string)jObject["servidor"];
                    string us = (string)jObject["usuario"];
                    string pa = (string)jObject["password"];

                    try
                    {
                        Dictionary<string, object> jsontoken = JsonWebToken.DecodeToObject<Dictionary<string, object>>(p.token, secretKey, true);
                        string authu = jsontoken["user"].ToString();
                        string authp = jsontoken["pasw"].ToString();
                        if (!new Comprobaciones().CheckCredentialsFormat(2, authu, authp))
                        {
                            r = RES.Generar(0, 4001, "La longitud del usuario o la contraseña no es valida", false);
                            statusCode = HttpStatusCode.BadRequest;
                        }
                        else
                        {
                            Database database = new Database();
                            JObject serverData = new PeticionesHTTP().CallServer(se, us, pa);
                            JObject serverStatus = JObject.Parse("{}");
                            if ((bool)serverData["success"])
                            {
                                JObject jObject4 = JObject.Parse("{puerto: [{},{},{},{},{},{},{},{}]}");
                                serverStatus["server"] = se;
                                serverStatus["su"] = us;
                                serverStatus["sp"] = DataSecurityManager.EncryptDESData(pa);
                                serverStatus["cantidad"] = (int)serverData["max-ports"];
                                serverStatus["authu"] = authu;
                                serverStatus["authp"] = authp;
                                int num = 0;
                                foreach (JObject temp in serverData["status"])
                                {
                                    if ((int)temp["st"] == 3)
                                    {
                                        jObject4["puerto"][num]["status"] = 1;
                                    }
                                    else
                                    {
                                        jObject4["puerto"][num]["status"] = 0;
                                    }
                                    jObject4["puerto"][num]["opr"] = temp["opr"].ToString().ToUpper().Trim();
                                    jObject4["puerto"][num]["st"] = (int)temp["st"];
                                    jObject4["puerto"][num]["sn"] = (string)temp["sn"];
                                    jObject4["puerto"][num]["imei"] = (string)temp["imei"];
                                    jObject4["puerto"][num]["port"] = temp["port"].ToString().Substring(0, 1);
                                    num++;
                                }
                                serverStatus["puertos"] = jObject4["puerto"];
                            }
                            JObject jObject6 = database.EjecutarProc("NUGSM", serverStatus);
                            statusCode = getHttpStatus.ToHttpStatusCode((int)jObject6["codigo"]);
                            r = RES.Generar((int)jObject6["tipo"], (int)jObject6["codigo"], (string)jObject6["mensaje"], (bool)jObject6["status"], 0f, 0f);
                        }
                    }
                    catch (Exception e)
                    {
                        if (e.Source == "JsonWebTokens")
                        {
                            r = RES.Generar(0, 4001, "Token invalido", false, 0f, 0f);
                            statusCode = getHttpStatus.ToHttpStatusCode(4001);
                        }
                        else
                        {
                            r = RES.Generar(0, 4000, "Error desconocido", false);
                            lg.Nuevo(3, "Servidor/POST", e.ToString());
                        }
                    }
                }
                catch (Exception e)
                {
                    r = RES.Generar(0, 4000, "Error desconocido", false);
                    lg.Nuevo(3, "Servidor/POST", e.ToString());
                }
                return Negotiate.WithStatusCode(statusCode).WithContentType("application/json").WithModel(r);
            };

            Post["/gsmr/"] = p =>
            {
                Database database = new Database();
                try
                {
                    Console.WriteLine(base.Context.Request.Body.ReadAsString());
                    JObject expr_3A = JObject.Parse(base.Context.Request.Body.ReadAsString());
                    bool flag = (bool)expr_3A["rpts"][0]["sent"];
                    int tid = (int)expr_3A["rpts"][0]["tid"];
                    string to = (string)expr_3A["rpts"][0]["sdr"][0][1];
                    database.ReleasePort(tid, to);
                    if (flag)
                    {
                        database.UpdateSMS(tid, to);
                    }
                }
                catch (Exception ex)
                {
                    string text = string.Format("{0} | ERROR | Callback GSM | {1}", DateTime.Now.ToString().ToUpper(), ex.Message);
                    File.AppendAllText("errores.txt", text + Environment.NewLine);
                    Console.WriteLine(text);
                }
                return Negotiate.WithStatusCode(HttpStatusCode.Accepted).WithContentType("application/json").WithModel("{ status: true}");
            };
        }
    }
}
