using Projeto.DataAccessLayer.Faturacao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Repositorios
{
    public class RepositorioVenda: IRepositorio<Venda>
    {
        private readonly List<Venda> ListaVendas;

        public RepositorioVenda(List<Venda> dados = null)
        {
            if (dados == null)
            {
                ListaVendas = new List<Venda> {
                };
            }
            else
            {
                ListaVendas = dados;
            }
        }

        public void Apagar(Venda t)
        {
            ListaVendas.Remove(t);
        }

        public void Atualizar(Venda tOld, Venda tNew)
        {
            throw new NotImplementedException();
        }

        public Venda Criar(Venda t)
        {
            ListaVendas.Add(t);
            return t;
        }

        public Venda ObterPorIdentificador(Guid guid)
        {
            return ListaVendas.FirstOrDefault(x=>x.Identificador == guid);
        }

        public List<Venda> ObterTodos()
        {
            return ListaVendas;
        }





    }
}
