using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCorev1.Business.Models
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {
        }
        public SuccessResult(): base(true, "")
        {
        }
    }
    public class SuccessResult<TResultType> : Result<TResultType>
    {
        public SuccessResult(string message, TResultType data) : base(true, message, data)
        {

        }
        public SuccessResult(TResultType data) : base (true, "", data)
        {

        }
        public SuccessResult() : base(true,"",default)
        {

        }
        public SuccessResult(string message) : base (true,message,default)
        {

        }
    }
}
