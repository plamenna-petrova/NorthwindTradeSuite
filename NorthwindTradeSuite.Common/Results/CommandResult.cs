using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindTradeSuite.Common.Results
{
    public class CommandResult<T> where T : class
    {
        public CommandResult(string message, T details)
        {
            Message = message;
            Details = details;
        }

        public string Message { get; set; } = null!;

        public T? Details { get; set; } = null!;
    }
}
