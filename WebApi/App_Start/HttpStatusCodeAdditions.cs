using System.Net;

namespace WebApi
{
    public class HttpStatusCodeAdditions
    {
        public static readonly HttpStatusCodeAdditions UnprocessableEntity = new HttpStatusCodeAdditions { Code = 422 };

        private int Code { get; set; }

        public static implicit operator HttpStatusCode(HttpStatusCodeAdditions addition)
        {
            return (HttpStatusCode)addition.Code;
        }
    }
}