using System;

namespace Agro.Model.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException( string message) : base( message)
        {

        }
    }
}
