using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
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


        public Tuple<PontoDeVendaSessao, string> Login(PontoDeVenda pontoDeVenda, Utilizador utilizador)
        {
            PontoDeVendaSessao pontoDeVendaSessao = null;
            string msg = null;
            using (var unitOfWork = new UnitOfWork(_context))
            {

                var UtilizadorComSessaoAberta = unitOfWork.PontoDeVendaSessoes.Find(x => x.Utilizador.Identificador == utilizador.Identificador && x.DataLogout == null).FirstOrDefault();
                var PontoDeVendaComSessaoAberta = unitOfWork.PontoDeVendaSessoes.Find(x => x.PontoDeVenda.Identificador == pontoDeVenda.Identificador && x.DataLogout == null).FirstOrDefault();

                if (UtilizadorComSessaoAberta != null)
                {
                    msg = "Utilizador já tem uma sessão aberta";
                    return Tuple.Create((PontoDeVendaSessao)null, msg);
                }
                if (PontoDeVendaComSessaoAberta != null)
                {
                    msg = "Ponto de Venda já se encontra aberto";
                    return Tuple.Create((PontoDeVendaSessao)null, msg);
                }

                pontoDeVendaSessao = new PontoDeVendaSessao
                {
                    Utilizador = utilizador,
                    PontoDeVenda = pontoDeVenda,
                    DataLogin = DateTime.Now,
                };
                unitOfWork.PontoDeVendaSessoes.Add(pontoDeVendaSessao);
                unitOfWork.Complete();

                msg = $"Ponto de Venda: {pontoDeVenda.Nome} logado com sucesso pelo utilizador: {utilizador.Nome}.";
            }

            return Tuple.Create(pontoDeVendaSessao, msg);
        }

        public string Logout(PontoDeVendaSessao pontoDeVendaSessao)
        {
            string result = null;

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var sessaoAberta = unitOfWork.PontoDeVendaSessoes.Get(pontoDeVendaSessao.Identificador);

                if (sessaoAberta == null || sessaoAberta.DataLogout != null)
                {
                    return $"Sessão inválida ou já terminada";
                }

                sessaoAberta.DataLogout = DateTime.Now;
                unitOfWork.PontoDeVendaSessoes.Update(sessaoAberta);
                unitOfWork.Complete();
            }

            return result;

        }


      
        

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
                //var tipoUtilizador = unitOfWork.TipoUtilizadores.Find(x=>x.Id ==(int)tipo).FirstOrDefault();

                Utilizador user = new Utilizador
                {
                    Nome = nome,
                    Email = email,
                    Senha = senha,
                    Tipo = unitOfWork.TipoUtilizadores.GetTipoUtilizadorByEnum(tipo)
                };

                unitOfWork.Utilizadores.Add(user);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Utilizador não registado");
                }

                return new Resultado(true, "Utilizador registado com sucesso");
            }

        }


        public bool VerificaSeExisteUmUtilizadorGerente()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                return unitOfWork.Utilizadores.Find(x=>x.Tipo.Id == (int)TipoUtilizadorEnum.Gerente && x.Ativo).Any();
            }
        }
    }
}
