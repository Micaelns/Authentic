using System;

namespace AuthenticApi.Exceptions
{
    public class ForbiddenSoftwareAccessException : Exception
    {
        public ForbiddenSoftwareAccessException() : base("Usuário sem acesso a esse sistema. Fale com um Administrador.") { }
    }
}