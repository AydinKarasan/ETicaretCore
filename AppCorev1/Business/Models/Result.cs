using AppCorev1.Business.Models.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCorev1.Business.Models
{
    public class Result
    {
        public bool IsSuccessful { get; } // readonly yaptýk (set i sildik)
        public string Message { get; set; }

        public Result (bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
    public class Result<TResultType>:Result, IResultData<TResultType>
    {
        public TResultType Data { get; }
        public Result(bool isSuccessful, string message, TResultType data) : base (isSuccessful, message)
        {
            Data = data; 
        }
    }
}
