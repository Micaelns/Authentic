using System;

namespace AuthenticApi.Exceptions
{
    public class IncisiveRefreshTokenException : Exception
    {
        public IncisiveRefreshTokenException() : base("RefreshToken está inconsistente.") { }
    }
}