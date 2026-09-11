using System;

namespace AuthenticApi.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Email e/ou senha incorreto(s)") { }
    }
} 