using System.Net;

namespace StaffReleaseAPI.Services.Helper
{
    public class ApiResponse
    {
        public object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
