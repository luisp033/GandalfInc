using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {
        public List<Estoque> GetAllEstoque()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Estoques.GetAll().ToList();
                return result;
            }
        }

        public Resultado InsereEstoque(Guid produto, int qtd)
        {

            #region Validacoes
            if (qtd < 1)
            {
                return new Resultado(false, "Quantidade deve ser superior a zero");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var insProduto = unitOfWork.Produtos.Get(produto);
                if (insProduto == null)
                {
                    return new Resultado(false, "Produto inexistente");
                }

                List<Estoque> listaEstoques = new List<Estoque>();
                for (int i = 0; i < qtd; i++)
                {
                    Estoque estoque = new Estoque
                    {
                        Produto = insProduto,
                        DataEntrada = DateTime.Today,
                        NumeroSerie = "Não definido"
                    };
                    listaEstoques.Add(estoque);
                }

                unitOfWork.Estoques.AddRange(listaEstoques);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Estoque não inserido");
                }

                return new Resultado(true, "Estoque inserido com sucesso", listaEstoques);
            }
        }

        public Resultado ObtemEstoque(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var estoque = unitOfWork.Estoques.Find(x => x.Identificador == identificador).FirstOrDefault();

                return new Resultado(true, "Estoque lido com sucesso", estoque);
            }
        }

        public Resultado AlteraEstoque(Guid identificador, string serie)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var estoque = unitOfWork.Estoques.Get(identificador);
                if (estoque == null)
                {
                    return new Resultado(false, "Estoque inexistente");
                }

                estoque.NumeroSerie = serie;

                unitOfWork.Estoques.Update(estoque);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Estoque não alterado");
                }
                return new Resultado(true, "Estoque alterado com sucesso", estoque);
            }
        }

        public Resultado ApagaEstoque(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var estoque = unitOfWork.Estoques.Get(identificador);
                if (estoque == null)
                {
                    return new Resultado(false, "Estoque inexistente");
                }

                unitOfWork.Estoques.Remove(estoque);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Estoque não removido");
                }

                return new Resultado(true, "Estoque removido com sucesso");
            }
        }

    }
}
