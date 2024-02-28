using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatF.Model.Exceptions
{
    public class PasswordDoesnMatchException : Exception
    {
        public PasswordDoesnMatchException(string message) : base(message)
        {
        }
    }
}
