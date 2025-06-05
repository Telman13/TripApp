using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Concrete.SuccessResults
{
    public class SuccessResult : Result
    {
        public SuccessResult(HttpStatusCode statusCode, string message) : base(true, statusCode, message)
        {
        }

        public SuccessResult(HttpStatusCode statusCode) : base(true, statusCode)
        {
        }
    }
}