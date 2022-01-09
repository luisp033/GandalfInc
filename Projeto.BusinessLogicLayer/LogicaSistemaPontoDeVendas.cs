using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {
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

                var lojaDb = unitOfWork.Lojas.Get(loja.Identificador);

                PontoDeVenda pontoDeVenda = new PontoDeVenda
                {
                    Nome = nome,
                    Loja = lojaDb
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

        public List<PontoDeVenda> GetAllPontoDeVendasByLoja(Guid idLoja)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.PontoDeVendas.Find(x => x.Loja.Identificador == idLoja).ToList();
                return result;
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

    }
}
