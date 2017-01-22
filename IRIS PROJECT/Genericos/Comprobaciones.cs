using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Genericos
{
    public class Comprobaciones
    {
        public bool CheckCredentialsFormat(int t, string p1, string p2)
        {
            p1 = p1.Trim();
            p2 = p2.Trim();
            bool result = true;
            if (t == 0 && (p1.Length != 8 || p2.Length != 14))
            {
                result = false;
            }
            else if (t == 1 && (p1.Length < 5 || p2.Length < 8))
            {
                result = false;
            }
            else if (t == 2 && (p1.Length < 5 || p1.Length > 10 || p2.Length != 60))
            {
                result = false;
            }
            return result;
        }
    }
}
