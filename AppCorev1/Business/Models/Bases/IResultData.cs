using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCorev1.Business.Models.Bases
{
    public interface IResultData<out TResulType>
    {
        TResulType Data { get; }
    }
}
