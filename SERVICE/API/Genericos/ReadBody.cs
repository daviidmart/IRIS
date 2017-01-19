using Nancy.IO;
using Nancy.Json;
using System.IO;

namespace API.Genericos
{
    public static class RequestBodyExtensions
    {
        public class DataSource
        {
            public int Hola { get; set; }
        }

        public static string ReadAsString(this RequestStream requestStream)
        {
            using (var reader = new StreamReader(requestStream))
            {
                string ds = reader.ReadToEnd();
                return ds;
            }
        }
    }
}
