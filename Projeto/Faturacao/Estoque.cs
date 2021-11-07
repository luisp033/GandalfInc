using Projeto.Lib.Entidades.Produtos;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Lib.Faturacao
{
    public class Estoque
    {
        public List<Produto> Produtos { get; set; }

        public bool ValidarDisponibilidade(Dictionary<Produto, int> quantidadeItens) 
        {

            foreach (var item in quantidadeItens.Keys)
            {

                var quantidadeEmEstoque = Produtos.Where(x => x.Nome == item.Nome).Count();
                var valorSolicitado = 0;

                quantidadeItens.TryGetValue(item, out valorSolicitado);

                if (valorSolicitado > quantidadeEmEstoque)
                {
                    return false;
                }
            }
            return true;

        }

    }
}
