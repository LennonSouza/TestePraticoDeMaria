using System;

namespace App.Infrastructure.Exceptions
{
    public class ConcorrenciaException : Exception
    {
        public ConcorrenciaException(string mensagem) : base(mensagem) { }
    }
}