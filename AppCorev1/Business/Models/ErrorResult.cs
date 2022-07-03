using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCorev1.Business.Models
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {
        }
        public ErrorResult():base(false, "")
        {
        }
    }
    public class ErrorResult<TResultType> : Result<TResultType>
    {
        public ErrorResult(string message, TResultType data) : base(false, message, data)
        {

        }
        public ErrorResult(TResultType data) : base(false, "", data)
        {

        }
        public ErrorResult() : base(false, "", default)
        {

        }
        public ErrorResult(string message) : base(false, message, default)
        {

        }
    }

}
