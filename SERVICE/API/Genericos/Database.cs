using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace API.Genericos
{
    public class Database
    {
        SqlConnection dbc = new SqlConnection("Data Source=dev.supportdesk.com.mx;Initial Catalog=SMS;User ID=sms_usr;Password=T0r0nt02017; Timeout=60");
        SqlCommand cmd;
        SqlDataReader data;

        public void DBConect()
        {
            try
            {
                dbc.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public int AuthAPI(string k, string s, string ip)
        {
            JObject r = JObject.Parse("{ code: 0 }");
            try
            {
                DBConect();
                cmd = new SqlCommand();
                cmd.CommandText = "AuthAPI";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = dbc;
                cmd.Parameters.Add("@APIK", SqlDbType.VarChar, 100).Value = k;
                cmd.Parameters.Add("@APIS", SqlDbType.VarChar, 100).Value = BCrypt.Net.BCrypt.HashPassword(s, "$2a$10$Esev9suggnCmG0QPZ6BAZe");
                cmd.Parameters.Add("@IP", SqlDbType.VarChar, 100).Value = ip;
                r = JObject.Parse((string)cmd.ExecuteScalar());
                dbc.Close();
            }catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return (int)r["code"];
        }

        public int AuthUser(string u, string p, string t)
        {
            JObject r = JObject.Parse("{ code: 0 }");
            try
            {
                DBConect();
                cmd = new SqlCommand();
                cmd.CommandText = "AuthUser";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = dbc;
                cmd.Parameters.Add("@USER", SqlDbType.VarChar, 100).Value = u;
                cmd.Parameters.Add("@PASW", SqlDbType.VarChar, 100).Value = BCrypt.Net.BCrypt.HashPassword(p, "$2a$10$Esev9suggnCmG0QPZ6BAZe");
                cmd.Parameters.Add("@TOKEN", SqlDbType.VarChar, 100).Value = t;
                r = JObject.Parse((string)cmd.ExecuteScalar());
                dbc.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return (int)r["code"];
        }

        public int CreateUser(string u, string t, string data)
        {
            JObject ds = JObject.Parse(data);
            Console.WriteLine(ds);
            int r = 0;
            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return r;
        }
    }
}