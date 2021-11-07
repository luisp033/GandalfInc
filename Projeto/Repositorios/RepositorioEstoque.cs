using Projeto.Lib.Entidades;
using Projeto.Lib.Faturacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Lib.Repositorios
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
                        ListaEstoques.Add(new Estoque(repoProdutos) { DataEntrada = DateTime.Today, Ean = i.ToString() });
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

        public void Criar(Estoque t)
        {
            ListaEstoques.Add(t);
        }

        public Estoque ObterPorIdentificador(Guid guid)
        {
            return ListaEstoques.Where(x => x.Identificador == guid).FirstOrDefault();
        }

        public List<Estoque> ObterTodos()
        {
            return ListaEstoques;
        }

        public Estoque ObterPorEan(string ean)
        {
            return ListaEstoques.Where(x => x.Ean == ean).FirstOrDefault();
        }



    }
}
