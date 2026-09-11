using System;

namespace AuthenticApi.Exceptions
{
    public class DisabledRefreshTokenException : Exception
    {
        public DisabledRefreshTokenException() : base("RefreshToken está inativo.") { }
    }
}