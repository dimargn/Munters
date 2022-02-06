using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munters.Tools
{
    public class Result
    {
        public Result(bool isSuccess = false, string json = "")
        {
            IsSuccess = isSuccess;
            ResultJson = json;
        }

        public bool IsSuccess { get; set; }
        public string ResultJson { get; set; }

        public string Error { get; set; }
    }
}
