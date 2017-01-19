using System;
using Nancy;
using API.Genericos;

namespace API.ModulosAPI
{
    public class SMS : NancyModule
    {
        public Respuestas RES = new Respuestas();
        static string v = "/v1";

        public SMS() : base(v) //Asignamos la version de la api
        {
            Post["/sms/{apikey}/{apisecret}/{to}/{text}"] = parameters => //ENVIAR NUEVO SMS
            {
                string apik = parameters.apikey;                            //API KET
                string apis = parameters.apisecret;                         //API SECRET
                string to = parameters.to;                                  //NUMERO QUE RECIBE
                string text = parameters.text;                              //TEXTO QUE SE ENVIA
                object r = RES.Generar(0, 4000, "Error desconocido");       //RESPUESTA DEFAULT
                HttpStatusCode HTTPStat = HttpStatusCode.Unauthorized;      //CODIGO HTTP DEFAULT
                Comprobaciones c = new Comprobaciones();                    //INSTANCIAMOS LAS COMPROBACIONES
                bool check = c.CheckCredentialsFormat(0, apik, apis);       //COMPROBAMOS EL FORMATO DE LAS CREDENCIALES (0 API | 1 USUARIO | 2 TOKEN)

                if (!check)
                {
                    r = RES.Generar(0, 4001, "La longitud de la API Key o el API Secret no es valida");
                    HTTPStat = HttpStatusCode.BadRequest;
                }
                else
                {
                    
                    Database db = new Database();
                    int AuthAPI = db.AuthAPI(apik, apis, Request.UserHostAddress);

                    if (AuthAPI == 2)
                    {
                        r = RES.Generar(0, 4003, "Host no autorizado");
                    }
                    else if(AuthAPI == 1)
                    {
                        r = RES.Generar(0, 4002, "Credenciales invalidas");
                    }
                    else if (AuthAPI == 0)
                    {
                        r = RES.Generar(2, 1001, "SMS ha sido enviado satisfactoriamente", true, 9.9f, 0.1f);
                        HTTPStat = HttpStatusCode.OK;
                    }

                    
                }

                return Negotiate.WithStatusCode(HTTPStat).WithContentType("application/json").WithModel(r);
            };
        }

    }
}