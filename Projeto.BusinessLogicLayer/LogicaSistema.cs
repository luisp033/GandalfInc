using Projeto.DataAccessLayer;
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





        #region Insercoes

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

   


        #endregion

        #region GestaoUtilizador
        public Resultado InsereUtilizador(string nome, string email, string senha , TipoUtilizadorEnum tipo) 
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
                var utilizador = unitOfWork.Utilizadores.Find(x=>x.Identificador == identificador).FirstOrDefault();

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
    }
}
