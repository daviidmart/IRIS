using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Genericos
{
    public class Comprobaciones
    {
        public bool CheckCredentialsFormat(int t, string p1, string p2) //COMPRUEBA SI EL FORMATO DE LAS CREDENCIALES ES CORRECTO
        {
            p1 = p1.Trim();
            p2 = p2.Trim();
            bool r = true;

            if (t == 0)    //FORMATO PARA API
            {
                if (p1.Length != 8 || p2.Length != 12)
                {

                    r = false;
                }
            }
            else if(t == 1) //FORMATO PARA USUARIO
            {
                if (p1.Length < 5 || p1.Length > 10 || p2.Length < 8 || p2.Length > 15)
                {
                    r = false;
                }
            }
            else if (t == 2) //FORMATO PARA TOKEN
            {
                if (p1.Length < 5 || p1.Length > 10 || p2.Length != 30)
                {
                    r = false;
                }
            }

            return r;
        }
    }
}
