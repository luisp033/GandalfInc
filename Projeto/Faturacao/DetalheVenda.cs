using Projeto.Lib.Entidades.Produtos;
using System.Collections.Generic;

namespace Projeto.Lib.Faturacao
{
    public class DetalheVenda
    {
        public Produto Produto { get; set; }
        public decimal Desconto { get; set; }
        public decimal PrecoFinal { get; set; }

    }
}