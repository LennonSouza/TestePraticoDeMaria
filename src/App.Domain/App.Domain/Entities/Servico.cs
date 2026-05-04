using System;

namespace App.Domain.Entities
{
    public class Servico
    {
        private string _nome;
        private decimal _valorBase;
        private decimal _percentualImposto;

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

        public decimal ValorBase
        {
            get => _valorBase;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("ValorBase deve ser maior que zero");

                _valorBase = value;
            }
        }

        public decimal PercentualImposto
        {
            get => _percentualImposto;
            private set
            {
                if (value < 0m || value > 100m)
                    throw new ArgumentException("PercentualImposto deve estar entre 0 e 100");

                _percentualImposto = value;
            }
        }

        public bool Ativo { get; private set; }

        public Servico(string nome, decimal valorBase, decimal percentualImposto)
        {
            Nome = nome;
            ValorBase = valorBase;
            PercentualImposto = percentualImposto;
            Ativo = true;
        }

        public void Atualizar(string nome, decimal valorBase, decimal percentualImposto, bool? ativo)
        {
            Nome = nome;
            ValorBase = valorBase;
            PercentualImposto = percentualImposto;
            if (ativo.HasValue) Ativo = ativo.Value;
        }

        public void Desativar() => Ativo = false;
        public void Ativar() => Ativo = true;
    }
}