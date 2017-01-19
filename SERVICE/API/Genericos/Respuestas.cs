namespace API.Genericos
{
    public class Respuestas
    {
        public object Generar(int t, int c, string m, bool s = false, float b = 0.0f, float co = 0.0f)
        {
            var r = (object)null;

            if (t == 0) //RESPUESTA SIMPLE
            {
                r = new
                {
                    status = s,
                    code = c,
                    message = m
                };
            }
            else if (t == 1) //RESPUESTA CON BALANCE
            {
                r = new
                {
                    status = s,
                    code = c,
                    message = m,
                    balance = b
                };
            }
            else if (t == 2) //RESPUESTA CON BALANCE Y COSTO
            {
                r = new
                {
                    status = s,
                    code = c,
                    message = m,
                    balance = b,
                    cost = co
                };
            }
            else if (t == 3) //RESPUESTA CON TOKEN
            {
                r = new
                {
                    status = s,
                    code = c,
                    token = m
                };
            }

            return r;
        }
    }
}