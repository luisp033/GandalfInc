using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {
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
    }
}
