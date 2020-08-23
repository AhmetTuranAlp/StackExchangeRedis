using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchangeRedis.Api.Common
{
    public class ListResponse<T>
    {
        public List<T> Result { get; set; }
        public int TotalCount { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public Exception Exception { get; set; }
    }
}
