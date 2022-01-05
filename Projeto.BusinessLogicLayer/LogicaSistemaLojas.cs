using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {
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

        public List<Loja> GetAllLojasComPontosDeVenda()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Lojas.GetAllLojasComPontosDeVenda();
                return result;
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
    }
}
