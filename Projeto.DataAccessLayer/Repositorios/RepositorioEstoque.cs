using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Repositorios
{
    public class RepositorioEstoque : IRepositorio<Estoque>
    {
        private readonly List<Estoque> ListaEstoques;

        public RepositorioEstoque(RepositorioProduto repoProdutos, List<Estoque> dados = null)
        {
            

            if (dados == null)
            {
                ListaEstoques = new List<Estoque>();

                for (int i = 10001; i < 10021; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        ListaEstoques.Add(new Estoque(repoProdutos) { DataEntrada = DateTime.Today, Ean = i.ToString() , NumeroSerie = $"SN{i}/{DateTime.Now.Millisecond}"});
                    }
                }
            }
            else
            {
                ListaEstoques = dados;
            }

        }

        public void Apagar(Estoque t)
        {
            ListaEstoques.Remove(t);
        }

        public void Atualizar(Estoque tOld, Estoque tNew)
        {
            throw new NotImplementedException();
        }

        public Estoque Criar(Estoque t)
        {
            ListaEstoques.Add(t);
            return t;
        }

        public Estoque ObterPorIdentificador(Guid guid)
        {
            return ListaEstoques.FirstOrDefault(x => x.Identificador == guid);
        }

        public List<Estoque> ObterTodos()
        {
            return ListaEstoques;
        }

        public List<Estoque> ObterListaPorEan(string ean)
        {
            return ListaEstoques.Where(x => x.Ean == ean).ToList();
        }

        public List<Estoque> BloquearProdutosDoEstoqueParaVenda(string ean, int quantidade , Guid identificadorVenda) 
        {
            var result = ListaEstoques.Where(x => 
                                            x.Ean == ean && 
                                            x.Ativo && 
                                            !x.DataVenda.HasValue)
                                      .Take(quantidade).ToList();

            if (result.Count == quantidade)
            {
                var dataVenda = DateTime.Now;
                foreach (var item in result)
                {
                    item.DataVenda = dataVenda;
                    item.IdentificadorVenda = identificadorVenda;
                }
            }
            else
            {
                result = null;
            }

            return result;
        }

        public bool DesbloquearProdutosDoEstoqueEmVenda(Guid estoqueIdentificador) 
        {
            bool result =false;

            // só remove se estiver numa venda não finalizada (IdentificadorVenda == null)
            var estoque = ListaEstoques.FirstOrDefault(x =>
                                            x.Identificador == estoqueIdentificador &&
                                            x.Ativo &&
                                            x.DataVenda.HasValue &&
                                            x.IdentificadorVenda.HasValue);

            if (estoque != null)
            {
                estoque.DataVenda = null;
                estoque.IdentificadorVenda = null;
                result = true;
            }

            return result;
        }

        public void DesativarProdutosPagos(Guid vendaIdentificador)
        {
            var listaEstoque = ListaEstoques.Where(x => x.IdentificadorVenda == vendaIdentificador && x.Ativo);
            foreach (var item in listaEstoque)
            {
                item.Ativo = false;
            }
        }

    }
}
