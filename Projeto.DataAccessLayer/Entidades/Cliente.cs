using System;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Cliente : Entidade
    {
        public string NumeroFiscal { get; set; }
        public string Nome { get; set; }
        public Morada MoradaEntrega { get; set; }
        public Morada MoradaFaturacao { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }


        public override string ToString()
        {
            return $"Id: {Identificador} - Nome: {Nome} - Nif {NumeroFiscal} - Telefone {Telefone}";
        }

    }
}