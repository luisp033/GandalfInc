using Projeto.Lib.Entidades;
using System;

namespace Projeto.Lib.Faturacao
{
    public class DetalheVenda
    {
        public Guid EstoqueIdentificador { get; set; }
        public Produto Produto { get; set; }
        public decimal Desconto { get; set; }
        public decimal PrecoFinal { get; set; }
        public string NumeroSerie { get; set; }

    }
}