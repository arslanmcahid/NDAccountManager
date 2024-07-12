using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NDAccountManager.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        [JsonIgnore] // sadece kod içerisinde lazım Json'a gonderirken gerek yok
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; }

        //static fac method
        //new ile yeni nesne uretmeye gerek kalmadan nesne create
        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, Errors = null};
        }
        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> {StatusCode = statusCode};
        }
        public static CustomResponseDto<T> Fail(int statusCode,List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode ,Errors = errors};
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }
    }
}
