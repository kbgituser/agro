using System;

namespace PlatF.Model.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException( string message) : base( message)
        {

        }
    }
}
