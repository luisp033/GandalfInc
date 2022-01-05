using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {
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



    }
}
