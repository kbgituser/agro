using System;

namespace Agro.Model.Exceptions
{
    public class PasswordDoesnMatchException : Exception
    {
        public PasswordDoesnMatchException(string message) : base(message)
        {
        }
    }
}
