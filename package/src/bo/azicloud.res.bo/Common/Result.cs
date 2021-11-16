using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azicloud.res.bo.Common
{
    public class Result<T>
    {
        public bool IsSuccessed { get; set; }

        public T ResultObj { get; set; }
    }
}
