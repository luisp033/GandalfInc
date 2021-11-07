﻿using Projeto.Lib.Entidades;
using Projeto.Lib.Faturacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Lib.Repositorios
{
    public class RepositorioPontoDeVenda : IRepositorio<PontoDeVenda>
    {

        private readonly List<PontoDeVenda> ListaPontosDeVenda;

        private readonly RepositorioUtilizador repoUtilizadores;

        public RepositorioPontoDeVenda(RepositorioUtilizador utilizadores, RepositorioLoja lojas, Loja dados = null)
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
        }

        public void Apagar(PontoDeVenda t)
        {
            ListaPontosDeVenda.Remove(t);
        }

       public void Atualizar(PontoDeVenda tOld, PontoDeVenda tNew)
        {
            throw new NotImplementedException();
        }

        public void Criar(PontoDeVenda t)
        {
            ListaPontosDeVenda.Add(t);
        }

        public PontoDeVenda ObterPorIdentificador(Guid guid)
        {
            return ListaPontosDeVenda.Where(x=>x.Identificador == guid).FirstOrDefault();
        }

        public List<PontoDeVenda> ObterTodos()
        {
            return ListaPontosDeVenda;
        }

        public PontoDeVenda ObterPorNome(string nome)
        {
            return ListaPontosDeVenda.Where(x => x.Nome == nome).FirstOrDefault();
        }

        public PontoDeVenda ObterPorNomeActivo(string nome)
        {
            return ListaPontosDeVenda.Where(x => x.Nome == nome && x.Ativo).FirstOrDefault();
        }

        public List<PontoDeVenda> ObterPorLojaActiva(Guid identificadorLoja)
        {
            return ListaPontosDeVenda.Where(x => 
                        x.Loja.Identificador == identificadorLoja
                        && x.Loja.Ativo
                        && x.Ativo).ToList();
        }

        public Utilizador Login(PontoDeVenda t, string email, string senha)
        {
            var postoDeVenda = ObterPorIdentificador(t.Identificador);
            var utilizador = repoUtilizadores.Login(email, senha);

            postoDeVenda.UtilizadorLogado = utilizador;

            return utilizador;
        }
        public void Logout(PontoDeVenda t) 
        {
            t.UtilizadorLogado = null;
        }



    }
}
