using DataSecurity;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API.Genericos
{
    class PeticionesHTTP
    {
        private Logs lg = new Logs();

        public async Task<JObject> EnviarSMS(JObject data, JObject body, int tid, string apik)
        {
            JObject.Parse("{ status: false, tipo: 0, codigo: 4000, mensaje: 'Error desconocido'}");
            HttpClient httpClient = new HttpClient();
            Database db = new Database();
            string str1 = (string)data["L"];
            string str2 = (string)data["U"];
            string str3 = DataSecurityManager.DecryptDESData((string)data["P"]);
            string str4 = (string)data["CP"];
            string numero = (string)body["to"];
            string mensaje = (string)body["text"];
            float price = (float)data["PRICE"];
            int cid = (int)data["CID"];

            JObject jobject1 = JObject.Parse("{" +
                @"type: 'send - sms', \r\n                
                task_num: '1',\r\n                
                sr_cnt: 1, \r\n                
                tasks: [{ \r\n                    
                    tid: " + (object)tid + "," +
                    "from: '" + str4 + "',"+
                    "to: '" + numero + "',"+
                    "sms: '" + mensaje + "',"+
                    "tmo: 15,"+
                    "sdr: 0,"+
                    "fdr: 0 ,"+
                    "sr_cnt: 1"+
                "}]"+
            "}");

            JObject jobject2 = JObject.Parse((await (await httpClient.PostAsync(string.Format("{0}goip_post_sms.html?username=", (object)str1) + str2 + "&password=" + str3, (HttpContent)new StringContent(jobject1.ToString(), Encoding.UTF8, "application/json"))).Content.ReadAsStringAsync()).Replace("\"", "'"));
            db.InsertSMS(tid, cid, apik, price, mensaje, numero);
  
            return !((string)jobject2["reason"] == "OK") ? JObject.Parse("{ status: false, tipo: 0, codigo: 4000, mensaje: 'Error al enviar'}") : JObject.Parse("{ status: true, tipo: 0, codigo: 1001, mensaje: 'Envió en proceso'}");
        }

        public JObject CallServer(string servidor, string u, string p)
        {
            HttpClient httpClient = new HttpClient();
            JObject r = null;
            try
            {
                r = JObject.Parse(httpClient.GetStringAsync(string.Format($"{servidor}goip_get_status.html?username={u}&password={p}")).GetAwaiter().GetResult());
                r["success"] = true;
            }
            catch (Exception e)
            {
                lg.Nuevo(3, "PeticionesHTTP/CallServe", e.ToString());
            }
            return r;
        }
    }
}
