using App.Domain.Enums;
using System;

namespace App.Domain.Entities
{
    public class Cliente
    {
        private string _nome;

        public int Id { get; private set; }
        public string Nome
        {
            get => _nome;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Nome é obrigatório");

                _nome = value;
            }
        }

        public string Documento { get; private set; }
        public TipoPessoa Tipo { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public bool Ativo { get; private set; }

        public Cliente(string nome, string documento, TipoPessoa tipo)
        {
            if (string.IsNullOrWhiteSpace(documento))
                throw new ArgumentException("Documento é obrigatório.");

            Nome = nome;
            Documento = documento;
            Tipo = tipo;
            DataCadastro = DateTime.Now;
            Ativo = true;
        }

        public void AtualizarDados(string nome, string email, string telefone, bool? ativo)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            if (ativo.HasValue) Ativo = ativo.Value;
        }

        public void Desativar() => Ativo = false;
        public void Ativar() => Ativo = true;
    }
}