using Nancy;

namespace API.Genericos
{
    class GetHttpStatus
    {
        public HttpStatusCode ToHttpStatusCode(int codigo)
        {
            switch (codigo)
            {
                case 1000:
                    return HttpStatusCode.OK;

                case 1001:
                    return HttpStatusCode.OK;

                case 1002:
                    return HttpStatusCode.OK;

                case 4000:
                    return HttpStatusCode.BadRequest;

                case 4001:
                    return HttpStatusCode.BadRequest;

                case 4002:
                    return HttpStatusCode.Unauthorized;

                case 4003:
                    return HttpStatusCode.Unauthorized;

                case 4004:
                    return HttpStatusCode.Unauthorized;

                case 4005:
                    return HttpStatusCode.PaymentRequired;

                case 4006:
                    return HttpStatusCode.BadRequest;

                case 4007:
                    return HttpStatusCode.Unauthorized;

                case 4008:
                    return HttpStatusCode.NotFound;

                case 4009:
                    return HttpStatusCode.BadRequest;
            }
            return HttpStatusCode.BadRequest;
        }


    }
}
