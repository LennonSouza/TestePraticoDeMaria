using System;

namespace App.Infrastructure.Exceptions
{
    public class DocumentoDuplicadoException : Exception
    {
        public DocumentoDuplicadoException()
            : base("Já existe um cliente cadastrado com este documento.") { }
    }
}