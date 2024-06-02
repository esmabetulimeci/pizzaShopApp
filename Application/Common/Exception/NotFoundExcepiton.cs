using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exception
{
    public class NotFoundExcepiton : System.Exception
    {
        public NotFoundExcepiton(string message) : base(message)
        {

        }
    }
}
