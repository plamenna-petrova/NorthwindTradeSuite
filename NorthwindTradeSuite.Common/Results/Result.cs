using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.Results
{
    public class Result
    {
        protected Result() 
        {
            
        }

        public bool IsSuccess { get; protected set; }

        public string[] Errors { get; protected set; } = Array.Empty<string>(); 

        public static Result Success(string message = null!) => new() 
        {
            IsSuccess = true, 
            Errors = message == null ? Array.Empty<string>() : new[] { message } 
        };

        public static Result Failure(params string[] errors) => new() 
        { 
            IsSuccess = false, 
            Errors = errors 
        };
    }

    public class Result<T> : Result
    {
        private Result() 
        {
            
        }

        public T? Value { get; private set; }

        public static new Result<T> Success(T value) => new() { IsSuccess = true, Value = value };

        public static new Result<T> Failure(params string[] errors) => new() { IsSuccess = false, Errors = errors };
    }
}
