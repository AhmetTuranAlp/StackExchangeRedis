using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchangeRedis.Api.Common
{
    public class Response<T>
    {
        public T Result { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public Exception Exception { get; set; }
    }
}
