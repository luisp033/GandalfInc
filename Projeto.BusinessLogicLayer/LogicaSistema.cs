﻿using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public class LogicaSistema
    {

        private readonly ProjetoDBContext _context;
        public LogicaSistema(ProjetoDBContext context)
        {
            _context = context;
        }

        #region Acessos
        public Resultado AberturaSessao(Utilizador utilizador, PontoDeVenda pontoDeVenda)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                #region Validacoes
                if (utilizador == null)
                {
                    return new Resultado(false, "Utilizador obrigatório.");
                }
                if (pontoDeVenda == null)
                {
                    return new Resultado(false, "Ponto de venda obrigatório.");
                }
                #endregion

                var UtilizadorComSessaoAberta = unitOfWork.PontoDeVendaSessoes.Find(x => x.Utilizador.Identificador == utilizador.Identificador && x.DataLogout == null).FirstOrDefault();
                if (UtilizadorComSessaoAberta != null)
                {
                    if (UtilizadorComSessaoAberta.PontoDeVenda.Identificador == pontoDeVenda.Identificador)
                    {
                        return new Resultado(true, "Continuação da sessão que já estava aberta neste ponto de venda para este utilizador.", UtilizadorComSessaoAberta);
                    }

                    return new Resultado(false, $"O utilizador tem uma sessão aberta no Ponto de venda {UtilizadorComSessaoAberta.PontoDeVenda.Nome}");
                }

                var PontoDeVendaComSessaoAberta = unitOfWork.PontoDeVendaSessoes.Find(x => x.PontoDeVenda.Identificador == pontoDeVenda.Identificador && x.DataLogout == null).FirstOrDefault();
                if (PontoDeVendaComSessaoAberta != null)
                {
                    return new Resultado(false, $"Este ponto de venda tem uma sessão aberta para o utilizador {PontoDeVendaComSessaoAberta.Utilizador.Nome}");
                }

                var pontoDeVendaSessao = new PontoDeVendaSessao
                {
                    Utilizador = utilizador,
                    PontoDeVenda = pontoDeVenda,
                    DataLogin = DateTime.Now,
                };
                unitOfWork.PontoDeVendaSessoes.Add(pontoDeVendaSessao);
                unitOfWork.Complete();

                return new Resultado(true, $"Ponto de Venda: {pontoDeVenda.Nome} logado com sucesso pelo utilizador: {utilizador.Nome}.", pontoDeVendaSessao);
            }
        }

        public Resultado Login(string email, string senha)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                //unitOfWork.TipoUtilizadores.GetAll();
                var utilizador = unitOfWork.Utilizadores
                                            .Find(x => x.Email == email && x.Senha == senha && x.Ativo)
                                            .FirstOrDefault();

                if (utilizador == null)
                {
                    return new Resultado(false, "Utilizador ou senha inválidos");
                }

                if (utilizador.Tipo.TipoId == (int)TipoUtilizadorEnum.Gerente)
                {
                    return new Resultado(true, "Utilizador valido para abertura", utilizador);
                }
                else if (utilizador.Tipo.TipoId == (int)TipoUtilizadorEnum.Empregado)
                {

                    //var sessaoAbertaParaUtilizador = unitOfWork.PontoDeVendaSessoes
                    //                                        .Find(x => x.Utilizador.Identificador == utilizador.Identificador && x.DataLogout == null)
                    //                                        .FirstOrDefault();
                    //if (sessaoAbertaParaUtilizador != null)
                    //{
                    //    return new Resultado(true, $"Utilizador logado com sucesso no POS {sessaoAbertaParaUtilizador.PontoDeVenda.Nome} da Loja {sessaoAbertaParaUtilizador.PontoDeVenda.Loja.Nome}", sessaoAbertaParaUtilizador);
                    //}

                    return new Resultado(true, $"Utilizador valido para abertura", utilizador);
                }
                else
                {
                    return new Resultado(false, "Tipo de utilizador não implementado.");
                }
            }
        }


        public bool VerificaSeExisteUmUtilizadorGerente()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                return unitOfWork.Utilizadores.Find(x => x.Tipo.TipoId == (int)TipoUtilizadorEnum.Gerente && x.Ativo).Any();
            }
        }

        public Resultado Logout(PontoDeVendaSessao pontoDeVendaSessao)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var sessaoAberta = unitOfWork.PontoDeVendaSessoes.Get(pontoDeVendaSessao.Identificador);

                if (sessaoAberta == null || sessaoAberta.DataLogout != null)
                {
                    return new Resultado(true, "Sessão inválida ou inexistente");
                }

                sessaoAberta.DataLogout = DateTime.Now;
                unitOfWork.PontoDeVendaSessoes.Update(sessaoAberta);
                unitOfWork.Complete();
            }

            return new Resultado(true, "Sessão fechada com sucesso");
        }

        #endregion

        #region GestaoPontoDeVenda

        public Resultado InserePontoDeVenda(string nome, Loja loja)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }

            if (loja == null)
            {
                return new Resultado(false, "Loja obrigatória");
            }

            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                PontoDeVenda pontoDeVenda = new PontoDeVenda
                {
                    Nome = nome,
                    Loja = loja
                };

                unitOfWork.PontoDeVendas.Add(pontoDeVenda);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Ponto de venda não inserido");
                }

                return new Resultado(true, "Ponto de venda inserido com sucesso", pontoDeVenda);
            }
        }

        public List<PontoDeVenda> GetAllPontoDeVendas()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.PontoDeVendas.GetAll().ToList();
                return result;
            }
        }

        public Resultado ObtemPontoDeVenda(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var pontoDeVenda = unitOfWork.PontoDeVendas.Find(x => x.Identificador == identificador).FirstOrDefault();

                return new Resultado(true, "Ponto de venda lido com sucesso", pontoDeVenda);
            }
        }

        public Resultado AlteraPontoDeVenda(Guid identificador, string nome, Guid lojaId)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var loja = unitOfWork.Lojas.Get(lojaId);
                if (loja == null)
                {
                    return new Resultado(false, "Loja inexistente");
                }

                var pontoDeVenda = unitOfWork.PontoDeVendas.Get(identificador);
                if (pontoDeVenda == null)
                {
                    return new Resultado(false, "Ponto de venda inexistente");
                }
                pontoDeVenda.Nome = nome;
                pontoDeVenda.Loja = loja;

                unitOfWork.PontoDeVendas.Update(pontoDeVenda);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Ponto de venda não alterado");
                }

                return new Resultado(true, "Ponto de venda alterado com sucesso", pontoDeVenda);
            }
        }

        public Resultado ApagaPontoDeVenda(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var pontoDeVenda = unitOfWork.PontoDeVendas.Get(identificador);
                if (pontoDeVenda == null)
                {
                    return new Resultado(false, "Ponto de venda inexistente");
                }

                unitOfWork.PontoDeVendas.Remove(pontoDeVenda);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Ponto de venda não removido");
                }

                return new Resultado(true, "Ponto de venda removido com sucesso");
            }
        }

        #endregion

        #region GestaoUtilizador
        public Resultado InsereUtilizador(string nome, string email, string senha, TipoUtilizadorEnum tipo)
        {
            #region Validacoes

            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            if (String.IsNullOrEmpty(email))
            {
                return new Resultado(false, "Email obrigatório");
            }
            if (String.IsNullOrEmpty(senha))
            {
                return new Resultado(false, "Senha obrigatória");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var tipoUtilizador = unitOfWork.TipoUtilizadores.GetTipoUtilizadorByEnum(tipo);

                Utilizador user = new Utilizador
                {
                    Nome = nome,
                    Email = email,
                    Senha = senha,
                    Tipo = tipoUtilizador
                };

                unitOfWork.Utilizadores.Add(user);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Utilizador não registado");
                }

                return new Resultado(true, "Utilizador registado com sucesso", user);
            }

        }

        public List<Utilizador> GetAllUtilizadores()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Utilizadores.GetAll().ToList();
                return result;
            }
        }

        public Resultado ObtemUtilizador(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var utilizador = unitOfWork.Utilizadores.Find(x => x.Identificador == identificador).FirstOrDefault();

                return new Resultado(true, "Utilizador lido com sucesso", utilizador);
            }
        }

        public Resultado AlteraUtilizador(Guid identificador, string nome, string email, string senha, TipoUtilizadorEnum tipo)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            if (String.IsNullOrEmpty(email))
            {
                return new Resultado(false, "Email obrigatório");
            }
            if (String.IsNullOrEmpty(senha))
            {
                return new Resultado(false, "Senha obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var utilizador = unitOfWork.Utilizadores.Get(identificador);
                if (utilizador == null)
                {
                    return new Resultado(false, "Utilizador inexistente");
                }

                var tipoUtilizador = unitOfWork.TipoUtilizadores.GetTipoUtilizadorByEnum(tipo);

                utilizador.Nome = nome;
                utilizador.Email = email;
                utilizador.Senha = senha;
                utilizador.Tipo = tipoUtilizador;

                unitOfWork.Utilizadores.Update(utilizador);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Utilizador não alterado");
                }

                return new Resultado(true, "Utilizador alterado com sucesso", utilizador);
            }
        }

        public Resultado ApagaUtilizador(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var utilizador = unitOfWork.Utilizadores.Get(identificador);
                if (utilizador == null)
                {
                    return new Resultado(false, "Utilizador inexistente");
                }

                unitOfWork.Utilizadores.Remove(utilizador);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Utilizador não removido");
                }

                return new Resultado(true, "Utilizador removido com sucesso");
            }
        }

        #endregion

        #region GestaoLoja

        public Resultado InsereLoja(string nome, string numeroFiscal, string email, string telefone, Morada morada)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                Loja loja = new Loja
                {
                    Nome = nome,
                    NumeroFiscal = numeroFiscal,
                    Email = email,
                    Telefone = telefone,
                    Morada = morada
                };

                unitOfWork.Lojas.Add(loja);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Loja não inserida");
                }

                return new Resultado(true, "Loja inserida com sucesso", loja);
            }
        }

        public Resultado AlteraLoja(Guid identificador, string nome, string numeroFiscal, string email, string telefone, Morada morada)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var loja = unitOfWork.Lojas.Get(identificador);
                if (loja == null)
                {
                    return new Resultado(false, "Loja inexistente");
                }

                loja.Nome = nome;

                unitOfWork.Lojas.Update(loja);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Loja não alterada");
                }

                return new Resultado(true, "Loja alterada com sucesso", loja);
            }
        }

        public Resultado ObtemLoja(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var loja = unitOfWork.Lojas.Get(identificador);

                return new Resultado(true, "Loja lida com sucesso", loja);
            }
        }

        public Resultado ApagaLoja(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var loja = unitOfWork.Lojas.Get(identificador);
                if (loja == null)
                {
                    return new Resultado(false, "Loja inexistente");
                }

                unitOfWork.Lojas.Remove(loja);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Loja não removida");
                }

                return new Resultado(true, "Loja removida com sucesso");
            }
        }

        public List<Loja> GetAllLojas()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Lojas.GetAll().ToList();
                return result;
            }
        }

        #endregion

        #region Marcas

        public Resultado InsereMarca(string nome, string origem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                MarcaProduto marca = new MarcaProduto
                {
                    Nome = nome,
                    Origem = origem
                };

                unitOfWork.MarcaProdutos.Add(marca);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Marca não inserida");
                }

                return new Resultado(true, "Marca inserida com sucesso", marca);
            }
        }

        public List<MarcaProduto> GetAllMarcas()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.MarcaProdutos.GetAll().ToList();
                return result;
            }
        }


        public Resultado ObtemMarca(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var marca = unitOfWork.MarcaProdutos.Get(identificador);

                return new Resultado(true, "Marca lida com sucesso", marca);
            }
        }

        public Resultado AlteraMarca(Guid identificador, string nome, string origem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var marca = unitOfWork.MarcaProdutos.Get(identificador);
                if (marca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }

                marca.Nome = nome;
                marca.Origem = origem;

                unitOfWork.MarcaProdutos.Update(marca);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Marca não alterada");
                }

                return new Resultado(true, "Marca alterada com sucesso", marca);
            }
        }

        public Resultado ApagaMarca(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var marca = unitOfWork.MarcaProdutos.Get(identificador);
                if (marca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }

                unitOfWork.MarcaProdutos.Remove(marca);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Marca não removida");
                }

                return new Resultado(true, "Marca removida com sucesso");
            }
        }

        #endregion

        #region Categorias

        public Resultado InsereCategoria(string nome, int ordem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                CategoriaProduto categoria = new CategoriaProduto
                {
                    Nome = nome,
                    OrdemApresentacao = ordem
                };

                unitOfWork.CategoriaProdutos.Add(categoria);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Categoria não inserida");
                }

                return new Resultado(true, "Categoria inserida com sucesso", categoria);
            }
        }

        public List<CategoriaProduto> GetAllCategorias()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.CategoriaProdutos.GetAll().ToList();
                return result;
            }
        }

        public Resultado ObtemCategoria(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var categoria = unitOfWork.CategoriaProdutos.Get(identificador);

                return new Resultado(true, "Categoria lida com sucesso", categoria);
            }
        }

        public Resultado AlteraCategoria(Guid identificador, string nome, int ordem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var categoria = unitOfWork.CategoriaProdutos.Get(identificador);
                if (categoria == null)
                {
                    return new Resultado(false, "Categoria inexistente");
                }

                categoria.Nome = nome;
                categoria.OrdemApresentacao = ordem;

                unitOfWork.CategoriaProdutos.Update(categoria);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Categoria não alterada");
                }

                return new Resultado(true, "Categoria alterada com sucesso", categoria);
            }
        }

        public Resultado ApagaCategoria(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var categoria = unitOfWork.CategoriaProdutos.Get(identificador);
                if (categoria == null)
                {
                    return new Resultado(false, "Categoria inexistente");
                }

                unitOfWork.CategoriaProdutos.Remove(categoria);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Categoria não removida");
                }

                return new Resultado(true, "Categoria removida com sucesso");
            }
        }


        #endregion

    }
}
