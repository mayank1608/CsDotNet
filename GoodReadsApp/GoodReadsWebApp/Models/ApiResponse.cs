namespace GoodReadsWebApp.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                Success = true,
                StatusCode = 200,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> Created(T data, string message = "Created successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                StatusCode = 201,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> BadRequest(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                StatusCode = 400,
                Message = message
            };
        }

        public static ApiResponse<T> NotFound(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                StatusCode = 404,
                Message = message
            };
        }
    }
}