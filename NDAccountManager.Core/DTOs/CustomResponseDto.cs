using System.Text.Json.Serialization;

namespace NDAccountManager.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        [JsonIgnore] // Sadece kod içerisinde lazım, Json'a gönderirken gerek yok
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }  // Yeni özellik eklendi
        public List<string> Errors { get; set; }

        // Static factory methods
        // new ile yeni nesne uretmeye gerek kalmadan nesne create
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccess = true, Errors = null };
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, IsSuccess = true };
        }
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, IsSuccess = false, Errors = errors };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, IsSuccess = false, Errors = new List<string> { error } };
        }
    }
}
