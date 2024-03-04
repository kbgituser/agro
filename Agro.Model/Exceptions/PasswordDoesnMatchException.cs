using System;

namespace PlatF.Model.Exceptions
{
    public class PasswordDoesnMatchException : Exception
    {
        public PasswordDoesnMatchException(string message) : base(message)
        {
        }
    }
}
