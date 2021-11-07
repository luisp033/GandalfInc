using Projeto.Lib.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Lib.Repositorios
{
    public class RepositorioUtilizador : IRepositorio<Utilizador>
    {

        private readonly List<Utilizador> ListaUtilizadores;

        public RepositorioUtilizador(List<Utilizador> dados = null)
        {
            if (dados == null) { 
                ListaUtilizadores = new List<Utilizador> {

                    new Utilizador(){ Tipo = TipoUtilizador.Gerente, Nome = "Gabriel", Senha = "1234", Email = "gabriel@mail.pt"},
                    new Utilizador(){ Tipo = TipoUtilizador.Gerente, Nome = "Gertrudes", Senha = "1234", Email = "gertrudes@mail.pt"},
                    new Utilizador(){ Tipo = TipoUtilizador.Gerente, Nome = "Gaudio", Senha = "1234", Email = "galio@mail.pt"},
                    new Utilizador(){ Tipo = TipoUtilizador.Empregado, Nome = "Eduardo", Senha = "1234", Email = "eduardo@mail.pt"},
                    new Utilizador(){ Tipo = TipoUtilizador.Empregado, Nome = "Elsa", Senha = "1234", Email = "elsa@mail.pt"},
                };
            }
            else
            {
                ListaUtilizadores = dados;
            }
        }

        public void Apagar(Utilizador t)
        {
            ListaUtilizadores.Remove(t);
        }

        public void Atualizar(Utilizador tOld, Utilizador tNew)
        {
            var atual = ObterPorIdentificador(tOld.Identificador);
            atual.Ativo = tNew.Ativo;
            atual.DataUltimaAlteracao = DateTime.Now;
            atual.Email = tNew.Email;
            atual.Nome = tNew.Nome;
            atual.Senha = tNew.Senha;
            atual.Tipo = tNew.Tipo;
        }

        public Utilizador Criar(Utilizador t)
        {
            ListaUtilizadores.Add(t);
            return t;
        }

        public Utilizador ObterPorIdentificador(Guid guid)
        {
            return ListaUtilizadores.Where(x=>x.Identificador == guid).FirstOrDefault();
        }

        public List<Utilizador> ObterTodos()
        {
            return ListaUtilizadores;
        }

        public Utilizador ObterPorNome(string nome)
        {
            return ListaUtilizadores.Where(x => x.Nome == nome).FirstOrDefault();
        }


        public Utilizador Login(string email, string senha) 
        {
            return ListaUtilizadores.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();
        }


    }
}
