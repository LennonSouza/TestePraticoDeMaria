using System;

namespace App.Application.Exceptions
{
    public class ConcorrenciaException : Exception
    {
        public ConcorrenciaException(string mensagem) : base(mensagem) { }
    }
}