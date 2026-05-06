using System;

namespace App.Application.Exceptions
{
    public class DocumentoDuplicadoException : Exception
    {
        public DocumentoDuplicadoException()
            : base("Já existe um cliente cadastrado com este documento.") { }
    }
}