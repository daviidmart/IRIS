using API.Genericos;
using DataSecurity;
using Newtonsoft.Json.Linq;
using System;

namespace API.Workers
{
    public class GSM
    {
        private Logs lg = new Logs();
        public void ServerStatus()
        {
            try
            {
                Database database = new Database();
                JObject proc = database.EjecutarProc("OBSVS", JObject.Parse("{}"));
                foreach (JObject servidor in proc["servidores"])
                {
                    JObject serverData = new PeticionesHTTP().CallServer((string)servidor["url"], (string)servidor["usr"], DataSecurityManager.DecryptDESData((string)servidor["pas"]));
                    JObject serverStatus = JObject.Parse("{}");
                    if ((bool)serverData["success"])
                    {
                        JObject pueto = JObject.Parse("{puerto: [{},{},{},{},{},{},{},{}]}");
                        serverStatus["server"] = (string)servidor["url"];
                        serverStatus["cantidad"] = (int)serverData["max-ports"];
                        int num = 0;
                        foreach (JObject temp in serverData["status"])
                        {
                            if ((int)temp["st"] == 3)
                            {
                                pueto["puerto"][num]["status"] = 1;
                            }
                            else
                            {
                                pueto["puerto"][num]["status"] = 0;
                            }
                            pueto["puerto"][num]["opr"] = temp["opr"].ToString().ToUpper().Trim();
                            pueto["puerto"][num]["st"] = (int)temp["st"];
                            pueto["puerto"][num]["sn"] = (string)temp["sn"];
                            pueto["puerto"][num]["imei"] = (string)temp["imei"];
                            pueto["puerto"][num]["port"] = temp["port"].ToString().Substring(0, 1);
                            num++;
                        }
                        serverStatus["puertos"] = pueto["puerto"];
                    }
                    JObject actualizar = database.EjecutarProc("UPGSM", serverStatus);
                    lg.Nuevo(5, "GSM/ServerStatus", "Se ha actualizado el servidor: "+ (string)servidor["url"]);
                }
            }
            catch (Exception e)
            {
                lg.Nuevo(3, "GSM/ServerStatus", e.ToString());
            }
        }
    }
}
