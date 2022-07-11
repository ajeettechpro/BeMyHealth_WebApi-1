using BeMyHealth_WebApi.Constants;

namespace BeMyHealth_WebApi.Helpers
{
    public class ApiResponse
    {
        public static ResponseModel CreateResponse<T>(T data, int statusCode)
        {
            bool error = false;
            if (StatusConstants.StatusCode200 != statusCode)
            {
                error = true;
            }
            return new ResponseModel(data, statusCode, StatusConstants.GetStatus(statusCode), error);
        }
    }
}
