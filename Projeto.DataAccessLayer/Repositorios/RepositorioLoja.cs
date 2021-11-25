using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Repositorios
{
    public class RepositorioLoja : IRepositorio<Loja>
    {

        private readonly List<Loja> ListaLojas;

        public RepositorioLoja(RepositorioUtilizador utilizadores, List<Loja> dados = null)
        {
            if (dados == null)
            {

                ListaLojas = new List<Loja> {
                    new Loja(){ Ativo = true, Email = "Loja1@mail.pt", NumeroFiscal = "123456789", Telefone="987654321", Nome = "Loja 1",
                                Morada = new Morada(){ Endereco = "Rua da loja 1", CodigoPostal = "1000", Localidade="Lisboa"},
                                Responsavel = utilizadores.ObterPorNome("Gabriel")
                    },
                    new Loja(){ Ativo = false, Email = "Loja2@mail.pt", NumeroFiscal = "123456789", Telefone="987654322", Nome = "Loja 2",
                                Morada = new Morada(){ Endereco = "Rua da loja 2", CodigoPostal = "3000", Localidade="faro"},
                                Responsavel = utilizadores.ObterPorNome("Gaudio")
                    },
                    new Loja(){ Ativo = false, Email = "Loja3@mail.pt", NumeroFiscal = "123456789", Telefone="987654323", Nome = "Loja 3",
                                Morada = new Morada(){ Endereco = "Rua da loja 3", CodigoPostal = "2000", Localidade="Porto"},
                                Responsavel = utilizadores.ObterPorNome("Gertrudes")
                    },
                };
            }
            else 
            {
                ListaLojas = dados;
            }
        }

        public void Apagar(Loja t)
        {
            ListaLojas.Remove(t);
        }

        public void Atualizar(Loja tOld, Loja tNew)
        {
            var atual = ObterPorIdentificador(tOld.Identificador);
            atual.Ativo = tNew.Ativo;
            atual.DataUltimaAlteracao = DateTime.Now;
            atual.Email = tNew.Email;
            atual.Nome = tNew.Nome;
            atual.Morada = tNew.Morada;
            atual.NumeroFiscal = tNew.NumeroFiscal;
            atual.Responsavel = tNew.Responsavel;

        }

        public Loja Criar(Loja t)
        {
            ListaLojas.Add(t);
            return t;
        }

        public Loja ObterPorIdentificador(Guid guid)
        {
            return ListaLojas.FirstOrDefault(x=>x.Identificador == guid);
        }

        public List<Loja> ObterTodos()
        {
            return ListaLojas;
        }

        public Loja ObterPorNome(string nome)
        {
            return ListaLojas.FirstOrDefault(x => x.Nome == nome);
        }

        public Loja ObterPorNomeActivo(string nome)
        {
            return ListaLojas.FirstOrDefault(x => x.Nome == nome && x.Ativo);
        }
    }
}
