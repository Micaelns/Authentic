using System;

namespace AuthenticApi.Exceptions
{
    public class UserBlockedException : Exception
    {
        public UserBlockedException() : base("Usuário Bloqueado temporariamente") { }
    }
}