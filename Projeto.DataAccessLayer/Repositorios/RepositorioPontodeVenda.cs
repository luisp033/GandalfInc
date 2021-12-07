using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Repositorios
{
    public class RepositorioPontoDeVenda : IRepositorio<PontoDeVenda>
    {

        private readonly List<PontoDeVenda> ListaPontosDeVenda;

        private readonly RepositorioUtilizador repoUtilizadores;

        public RepositorioPontoDeVenda(RepositorioUtilizador utilizadores, RepositorioLoja lojas, List<PontoDeVenda> dados = null)
        {

            repoUtilizadores = utilizadores;


            if (dados == null) {

                ListaPontosDeVenda = new List<PontoDeVenda> {
                    new PontoDeVenda(){ Ativo = true, Nome = "POS 1", Loja = lojas.ObterPorNome("Loja 1")},
                    new PontoDeVenda(){ Ativo = true, Nome = "POS 2", Loja = lojas.ObterPorNome("Loja 1")},
                    new PontoDeVenda(){ Ativo = false, Nome = "POS 3", Loja = lojas.ObterPorNome("Loja 1")},
                    new PontoDeVenda(){ Ativo = true, Nome = "POS 4", Loja = lojas.ObterPorNome("Loja 2")},
                    new PontoDeVenda(){ Ativo = false, Nome = "POS 5", Loja = lojas.ObterPorNome("Loja 2")},
                    new PontoDeVenda(){ Ativo = true, Nome = "POS 6", Loja = lojas.ObterPorNome("Loja 2")},
                    new PontoDeVenda(){ Ativo = false, Nome = "POS 7", Loja = lojas.ObterPorNome("Loja 3")},
                    new PontoDeVenda(){ Ativo = true, Nome = "POS 8", Loja = lojas.ObterPorNome("Loja 3")},
                    new PontoDeVenda(){ Ativo = true, Nome = "POS 9", Loja = lojas.ObterPorNome("Loja 3")},
                };
            }
            else
            {
                ListaPontosDeVenda = dados;
            }
        }

        public void Apagar(PontoDeVenda t)
        {
            ListaPontosDeVenda.Remove(t);
        }

       public void Atualizar(PontoDeVenda tOld, PontoDeVenda tNew)
        {
            throw new NotImplementedException();
        }

        public PontoDeVenda Criar(PontoDeVenda t)
        {
            ListaPontosDeVenda.Add(t);
            return t;
        }

        public PontoDeVenda ObterPorIdentificador(Guid guid)
        {
            return ListaPontosDeVenda.FirstOrDefault(x=>x.Identificador == guid);
        }

        public List<PontoDeVenda> ObterTodos()
        {
            return ListaPontosDeVenda;
        }

        public PontoDeVenda ObterPorNome(string nome)
        {
            return ListaPontosDeVenda.FirstOrDefault(x => x.Nome == nome);
        }

        public PontoDeVenda ObterPorNomeActivo(string nome)
        {
            return ListaPontosDeVenda.FirstOrDefault(x => x.Nome == nome && x.Ativo);
        }

        public List<PontoDeVenda> ObterPorLojaActiva(Guid identificadorLoja)
        {
            return ListaPontosDeVenda.Where(x => 
                        x.Loja.Identificador == identificadorLoja
                        && x.Loja.Ativo
                        && x.Ativo).ToList();
        }

        //public Utilizador Login(PontoDeVenda t, string email, string senha)
        //{
        //    var postoDeVenda = ObterPorIdentificador(t.Identificador);
        //    var utilizador = repoUtilizadores.Login(email, senha);

        //    postoDeVenda.UtilizadorLogado = utilizador;

        //    return utilizador;
        //}
        //public static void Logout(PontoDeVenda t) 
        //{
        //    t.UtilizadorLogado = null;
        //}



    }
}
