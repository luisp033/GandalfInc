using Projeto.Lib.Entidades;
using Projeto.Lib.Faturacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Lib.Repositorios
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
            return ListaVendas.Where(x=>x.Identificador == guid).FirstOrDefault();
        }

        public List<Venda> ObterTodos()
        {
            return ListaVendas;
        }





    }
}
