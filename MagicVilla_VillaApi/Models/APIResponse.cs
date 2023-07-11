using System.Net;

namespace MagicVilla_VillaApi.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool isSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
