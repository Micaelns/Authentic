using System;

namespace AuthenticApi.Exceptions
{
    public class NotFoundRefreshTokenException : Exception
    {
        public NotFoundRefreshTokenException() : base("RefreshToken não existe.") { }
    }
}