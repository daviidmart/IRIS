using Nancy;
using Calabonga.Utils.TokenGenerator;
using Newtonsoft.Json.Linq;
using API.Genericos;

namespace API.ModulosAPI
{
    public class Usuarios : NancyModule
    {
        public Respuestas RES = new Respuestas();
        static string v = "/v1";

        public Usuarios() : base(v)
        {
            Get["/auth/{user}/{pasw}"] = p =>                               //GENERAR TOKEN USADO PARA TODAS LAS TRANSACCIONES DE USUARIO
            {
                string user = p.user;                                       //USUARIO
                string pasw = p.pasw;                                       //PASSWORD
                object r = RES.Generar(0, 4000, "Error desconocido");       //RESPUESTA DEFAULT
                HttpStatusCode HTTPStat = HttpStatusCode.Unauthorized;      //CODIGO HTTP DEFAULT
                Comprobaciones c = new Comprobaciones();                    //INSTANCIAMOS LAS COMPROBACIONES
                bool check = c.CheckCredentialsFormat(1, user, pasw);       //COMPROBAMOS EL FORMATO DE LAS CREDENCIALES (0 API | 1 USUARIO | 2 TOKEN)

                if (!check)
                {
                    r = RES.Generar(0, 4001, "La longitud del usuario o la contraseña no es valida");
                    HTTPStat = HttpStatusCode.BadRequest;
                }
                else
                {
                    Database db = new Database();
                    string token = TokenGenerator.Generate(30);
                    int AuthAPI = db.AuthUser(user, pasw, token);

                    if (AuthAPI == 1)
                    {
                        r = RES.Generar(0, 4002, "Credenciales invalidas");
                    }
                    else if (AuthAPI == 0)
                    {
                        r = RES.Generar(3, 1002, token, true);
                        HTTPStat = HttpStatusCode.OK;
                    }
                }

                return Negotiate.WithStatusCode(HTTPStat).WithContentType("application/json").WithModel(r);
            };

            Post["/user/{user}/{token}"] = p =>                              //GENERAR TOKEN USADO PARA TODAS LAS TRANSACCIONES DE USUARIO
            {
                string user = p.user;                                       //USUARIO
                string token = p.token;                                     //PASSWORD
                object r = RES.Generar(0, 4000, "Error desconocido");       //RESPUESTA DEFAULT
                string data = Request.Body.ReadAsString();
                HttpStatusCode HTTPStat = HttpStatusCode.Unauthorized;      //CODIGO HTTP DEFAULT
                Comprobaciones c = new Comprobaciones();                    //INSTANCIAMOS LAS COMPROBACIONES
                bool check = c.CheckCredentialsFormat(2, user, token);      //COMPROBAMOS EL FORMATO DE LAS CREDENCIALES (0 API | 1 USUARIO | 2 TOKEN)

                if (!check)
                {
                    r = RES.Generar(0, 4001, "La longitud del usuario o la contraseña no es valida");
                    HTTPStat = HttpStatusCode.BadRequest;
                }
                else
                {
                    Database db = new Database();
                    int CreateUser = db.CreateUser(user, token, data);

                    //if (CreateUser == 1)
                    //{
                        //r = RES.Generar(0, 4002, "Credenciales invalidas");
                    //}
                    //else if (CreateUser == 0)
                    //{
                        //r = RES.Generar(3, 1002, token, true);
                        //HTTPStat = HttpStatusCode.OK;
                    //}
                }

                return Negotiate.WithStatusCode(HTTPStat).WithContentType("application/json").WithModel(r);
            };
        }
    }
}
