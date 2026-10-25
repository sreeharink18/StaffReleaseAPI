using System.Net;

namespace StaffReleaseAPI.Services.Helper
{
    public class ApiResponseHelper
    {
        public static ApiResponse SuccessResponse(object result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ApiResponse
            {
                IsSuccess = true,
                Result = result,
                StatusCode = statusCode
            };
        }
        public static ApiResponse ErrorResponse(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new ApiResponse
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                StatusCode = statusCode
            };
        }
    }
}
