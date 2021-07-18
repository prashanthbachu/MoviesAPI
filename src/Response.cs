using System.Net;

namespace MoviesAPI.Services
{
    public class Response <T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
    }
}
