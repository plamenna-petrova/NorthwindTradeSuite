using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.Results
{
    public class RequestResult
    {
        protected RequestResult() 
        {
            
        }

        public bool IsSuccess { get; protected set; }

        public string[] Errors { get; protected set; } = Array.Empty<string>(); 

        public static RequestResult Success(string message = null!) => new() 
        {
            IsSuccess = true, 
            Errors = message == null ? Array.Empty<string>() : new[] { message } 
        };

        public static RequestResult Failure(params string[] errors) => new() 
        { 
            IsSuccess = false, 
            Errors = errors 
        };
    }

    public class RequestResult<T> : RequestResult
    {
        private RequestResult() 
        {
            
        }

        public T? Value { get; private set; }

        public static new RequestResult<T> Success(T value) => new() { IsSuccess = true, Value = value };

        public static new RequestResult<T> Failure(params string[] errors) => new() { IsSuccess = false, Errors = errors };
    }
}
