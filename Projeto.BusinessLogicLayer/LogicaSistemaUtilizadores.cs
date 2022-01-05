using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {

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

    }
}
