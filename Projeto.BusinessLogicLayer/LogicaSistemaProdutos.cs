using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {

        public List<Produto> GetAllProdutos()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.Produtos.GetAll().ToList();
                return result;
            }
        }

        public Resultado InsereProduto(string nome, Guid categoria, Guid marca, string ean, decimal preco)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var insCategoria = unitOfWork.CategoriaProdutos.Get(categoria);
                if (insCategoria == null)
                {
                    return new Resultado(false, "Categoria inexistente");
                }
                var insMarca = unitOfWork.MarcaProdutos.Get(marca);
                if (insMarca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }

                Produto produto = new Produto
                {
                    Nome = nome,
                    Categoria = insCategoria,
                    Marca = insMarca,
                    Ean = ean,
                    PrecoUnitario = preco

                };

                unitOfWork.Produtos.Add(produto);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Produto não inserido");
                }

                return new Resultado(true, "Produto inserido com sucesso", produto);
            }
        }

        public Resultado AlteraProduto(Guid identificador, string nome, Guid categoria, Guid marca, string ean, decimal preco)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var insCategoria = unitOfWork.CategoriaProdutos.Get(categoria);
                if (insCategoria == null)
                {
                    return new Resultado(false, "Categoria inexistente");
                }
                var insMarca = unitOfWork.MarcaProdutos.Get(marca);
                if (insMarca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }


                var produto = unitOfWork.Produtos.Get(identificador);
                if (produto == null)
                {
                    return new Resultado(false, "Produto inexistente");
                }

                produto.Nome = nome;
                produto.Categoria = insCategoria;
                produto.Marca = insMarca;
                produto.Ean = ean;
                produto.PrecoUnitario = preco;

                unitOfWork.Produtos.Update(produto);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Produto não alterado");
                }
                return new Resultado(true, "Produto alterado com sucesso", produto);
            }
        }

        public Resultado ObtemProduto(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var produto = unitOfWork.Produtos.Find(x => x.Identificador == identificador).FirstOrDefault();

                return new Resultado(true, "Produto lido com sucesso", produto);
            }
        }

        public Resultado ObtemProdutosPorCategoria(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var produtos = unitOfWork.Produtos.Find(x => x.Categoria.Identificador == identificador).ToList();

                return new Resultado(true, "Produtos lidos com sucesso", produtos);
            }
        }

        public Resultado ApagaProduto(Guid identificador)
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var produto = unitOfWork.Produtos.Get(identificador);
                if (produto == null)
                {
                    return new Resultado(false, "Produto inexistente");
                }

                unitOfWork.Produtos.Remove(produto);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Produto não removido");
                }

                return new Resultado(true, "Produto removido com sucesso");
            }
        }
    }
}
