using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace API.Genericos
{
    public class Database
    {
        SqlCommand cmd;
        SqlDataReader data;
        SqlConnection dbc;
        private Logs lg = new Logs();
        static string dbCatalog = ConfigurationManager.AppSettings["dbCatalog"];
        static string dbPassword = ConfigurationManager.AppSettings["dbPassword"];
        static string dbServer = ConfigurationManager.AppSettings["dbServer"];
        static string dbUser = ConfigurationManager.AppSettings["dbUser"];
        static string semilla = ConfigurationManager.AppSettings["semilla"];

        public Database()
        {
            dbc = new SqlConnection($"Data Source={dbServer};Initial Catalog={dbCatalog};User ID={dbUser};Password={dbPassword}; Timeout=60");
        }

        public void DBConect()
        {
            try
            {
                dbc.Open();
            }
            catch (Exception e)
            {
                lg.Nuevo(3, "Database/DBConect", e.ToString());
            }
        }

        public JObject EjecutarProc(string p, JObject data)
        {
            JObject r = JObject.Parse("{ status: false, tipo: 0, codigo: 4000, mensaje: 'Error desconocido'}");
            DBConect();
            try
            {
                cmd = new SqlCommand("EjecutarProc", dbc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@P", SqlDbType.VarChar, 100).Value = p;
                cmd.Parameters.Add("@RES", SqlDbType.Bit, 1).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@PRO", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                string cmdText = cmd.Parameters["@PRO"].Value.ToString();
                if (bool.Parse(cmd.Parameters["@RES"].Value.ToString()))
                {
                    try
                    {
                        cmd = new SqlCommand(cmdText, dbc);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@DATA", SqlDbType.VarChar, 100000).Value = data.ToString();
                        cmd.Parameters.Add("@RES", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        r = JObject.Parse(cmd.Parameters["@RES"].Value.ToString());
                    }
                    catch (SqlException e)
                    {
                        lg.Nuevo(3, "Database/EjecutarProc", e.ToString());
                    }
                }
                else
                {
                    r = JObject.Parse("{ status: false, tipo: 0, codigo: 4008, mensaje: 'No se encontro un procedimiento con esta clave'}");
                }
                
            }
            catch (SqlException e)
            {
                lg.Nuevo(3, "Database/EjecutarProc", e.ToString());
            }
            finally
            {
                dbc.Close();
            }
            return r;
        }

        public void InsertSMS(int tid, int cid, string apik, float p, string text, string to)
        {
            DBConect();
            try
            {
                cmd = new SqlCommand(string.Format("INSERT INTO mensajes (tid, cid, apik, [text], [to], [price]) VALUES ({0}, {1}, '{2}', '{3}', '{4}', {5})", new object[] { tid, cid, apik, text, to, p }), dbc);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                lg.Nuevo(3, "Database/InsertSMS", e.ToString());
            }
            finally
            {
                dbc.Close();
            }
        }

        public void ReleasePort(int tid, string to)
        {
            DBConect();
            try
            {
                cmd = new SqlCommand(string.Format("UPDATE CLIENTES SET [status] = 1 WHERE id = (SELECT TOP 1 cid FROM mensajes WHERE tid = {0} AND [to] = '{1}' AND fecha >= DATEADD(day, -1, convert(date, GETDATE())))", tid, to), dbc);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                lg.Nuevo(3, "Database/ReleasePort", e.ToString());
            }
            finally
            {
                dbc.Close();
            }
        }

        public void UpdateSMS(int tid, string to)
        {
            DBConect();
            try
            {
                cmd = new SqlCommand(string.Format("UPDATE mensajes SET [status] = 1 WHERE tid = {0} AND [to] = '{1}' AND fecha >= DATEADD(day, -1, convert(date, GETDATE()))", tid, to), this.dbc);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                lg.Nuevo(3, "Database/UpdateSMS", e.ToString());
            }
            finally
            {
                dbc.Close();
            }
        }
    }
}