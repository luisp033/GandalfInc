using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class EstoqueRepositoryTests
    {
        [TestMethod()]
        public void EstoqueRepository_InsertGetAndFindDBTest()
        {
            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange

                var unitOfWork = new UnitOfWork(contexto);

                var guid = Guid.NewGuid();

                var expectedCategoria = new CategoriaProduto
                {
                    Nome = "SmartPhones"
                };
                unitOfWork.CategoriaProdutos.Add(expectedCategoria);

                var expectedMarca = new MarcaProduto
                {
                    Nome = "Apple",
                    Origem = "USA"
                };
                unitOfWork.MarcaProdutos.Add(expectedMarca);

                var expectedProduto = new Produto
                {
                    Identificador = guid,
                    Nome = "Iphone 1",
                    Categoria = expectedCategoria,
                    Marca = expectedMarca,
                    PrecoUnitario = 100,
                    Ean = "GTR5373529-56",
                };
                unitOfWork.Produtos.Add(expectedProduto);
                unitOfWork.Complete();
                var DateExpected = DateTime.Today;

                //act

                var expectedEstoque1 = new Estoque
                {
                    NumeroSerie = "SN/1",
                    Produto = expectedProduto,
                    DataEntrada = DateTime.Today,
                };
                unitOfWork.Estoques.Add(expectedEstoque1);
                var expectedEstoque2 = new Estoque
                {
                    NumeroSerie = "SN/2",
                    Produto = expectedProduto,
                    DataEntrada = DateTime.Today,

                };
                unitOfWork.Estoques.Add(expectedEstoque2);
                unitOfWork.Complete();

                var actualEstoque = unitOfWork.Estoques.GetAll();
                var actualFirstEstoque = unitOfWork.Estoques.Get(actualEstoque.FirstOrDefault().Identificador);
                var DateActual = new DateTime(actualFirstEstoque.DataCriacao.Value.Year, actualFirstEstoque.DataCriacao.Value.Month, actualFirstEstoque.DataCriacao.Value.Day, 0, 0, 0);
                var DateEntrada = new DateTime(actualFirstEstoque.DataEntrada.Year, actualFirstEstoque.DataEntrada.Month, actualFirstEstoque.DataEntrada.Day, 0, 0, 0);


                //assert
                Assert.IsNotNull(actualEstoque);
                Assert.IsNotNull(actualFirstEstoque);
                Assert.IsNull(actualFirstEstoque.DataUltimaAlteracao);
                Assert.AreEqual(DateExpected, DateActual);
                Assert.AreEqual(DateExpected, DateEntrada);
                Assert.IsNull(actualFirstEstoque.DetalheVendaId);
                Assert.IsNull(actualFirstEstoque.DetalheVenda);
                Assert.AreEqual(2, actualEstoque.Count());
                Assert.AreNotEqual(actualEstoque.ToList()[0].Identificador, actualEstoque.ToList()[1].Identificador);

            }
        }

        [TestMethod()]
        public void ProdutoRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);

                var expectedCategoria = new CategoriaProduto
                {
                    Nome = "SmartPhones"
                };
                unitOfWork.CategoriaProdutos.Add(expectedCategoria);

                var expectedMarca = new MarcaProduto
                {
                    Nome = "Apple",
                    Origem = "USA"
                };
                unitOfWork.MarcaProdutos.Add(expectedMarca);

                var expectedProduto = new Produto
                {
                    Nome = "Iphone 1",
                    Categoria = expectedCategoria,
                    Marca = expectedMarca,
                    PrecoUnitario = 100,
                    Ean = "GTR5373529-56",
                };
                unitOfWork.Produtos.Add(expectedProduto);
                unitOfWork.Complete();
                var DateExpected = DateTime.Today;

                var expected = new List<Estoque>() {
                    new Estoque
                    {
                    NumeroSerie = "SN/1",
                    Produto = expectedProduto,
                    DataEntrada = DateTime.Today,
                    },
                    new Estoque
                    {
                    NumeroSerie = "SN/1",
                    Produto = expectedProduto,
                    DataEntrada = DateTime.Today,
                    },
                };
                unitOfWork.Estoques.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.Estoques.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void ProdutoRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();

                var expectedCategoria = new CategoriaProduto
                {
                    Nome = "SmartPhones"
                };
                unitOfWork.CategoriaProdutos.Add(expectedCategoria);

                var expectedMarca = new MarcaProduto
                {
                    Nome = "Apple",
                    Origem = "USA"
                };
                unitOfWork.MarcaProdutos.Add(expectedMarca);

                var expectedProduto = new Produto
                {
                    Nome = "Iphone 1",
                    Categoria = expectedCategoria,
                    Marca = expectedMarca,
                    PrecoUnitario = 100,
                    Ean = "GTR5373529-56",
                };
                unitOfWork.Produtos.Add(expectedProduto);
                unitOfWork.Complete();

                var expected = new List<Estoque>() {
                    new Estoque
                    {
                        Identificador = guid,
                        NumeroSerie = "SN/1",
                        Produto = expectedProduto,
                        DataEntrada = DateTime.Today,
                    },
                    new Estoque
                    {
                        NumeroSerie = "SN/1",
                        Produto = expectedProduto,
                        DataEntrada = DateTime.Today,
                    },
                };
                unitOfWork.Estoques.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Estoques.GetAll();

                //act
                var firstElement = unitOfWork.Estoques.Get(guid);
                unitOfWork.Estoques.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.Estoques.GetAll();

                unitOfWork.Estoques.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.Estoques.GetAll();

                //assert
                Assert.AreEqual(2, actualBefore.Count());
                Assert.AreEqual(1, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());

            }
        }

        [TestMethod()]
        public void EstoqueRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                    var unitOfWork = new UnitOfWork(contexto);
                    var guid = Guid.NewGuid();

                    var expectedCategoria = new CategoriaProduto
                    {
                        Nome = "SmartPhones"
                    };
                    unitOfWork.CategoriaProdutos.Add(expectedCategoria);

                    var expectedMarca = new MarcaProduto
                    {
                        Nome = "Apple",
                        Origem = "USA"
                    };
                    unitOfWork.MarcaProdutos.Add(expectedMarca);

                    var expectedProduto = new Produto
                    {
                        Nome = "Iphone 1",
                        Categoria = expectedCategoria,
                        Marca = expectedMarca,
                        PrecoUnitario = 100,
                        Ean = "GTR5373529-56",
                    };
                    unitOfWork.Produtos.Add(expectedProduto);
                    unitOfWork.Complete();
                    var DateExpected = DateTime.Today;

                    var expected = new List<Estoque>() {
                    new Estoque
                    {
                        Identificador = guid,
                        NumeroSerie = "SN/1",
                        Produto = expectedProduto,
                        DataEntrada = DateTime.Today,
                    },
                    new Estoque
                    {
                        NumeroSerie = "SN/1",
                        Produto = expectedProduto,
                        DataEntrada = DateTime.Today,
                    },
                };
                    unitOfWork.Estoques.AddRange(expected);
                    unitOfWork.Complete();
                    var actualBefore = unitOfWork.Estoques.GetAll();

                var numeroSerieEsperado = "SN/XXX";

                //act
                var firstElement = unitOfWork.Estoques.Get(guid);
                firstElement.NumeroSerie = numeroSerieEsperado;
                unitOfWork.Estoques.Update(firstElement);
                unitOfWork.Complete();

                firstElement = unitOfWork.Estoques.Get(guid);
                var actualFirstUpdate = unitOfWork.Estoques.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.NumeroSerie = numeroSerieEsperado;
                }
                unitOfWork.Estoques.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.Estoques.GetAll();


                var DateModifiedCount = actualSecondUpdate.Count(x =>
                       x.DataUltimaAlteracao.Value.Year == DateExpected.Year
                    && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
                    && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

                var dadosAlterados = actualSecondUpdate.Count(x => x.NumeroSerie == numeroSerieEsperado);

                //assert
                Assert.AreEqual(2, actualBefore.Count());
                Assert.AreEqual(numeroSerieEsperado, firstElement.NumeroSerie);
                Assert.AreEqual(2, dadosAlterados);
                Assert.AreEqual(2, DateModifiedCount);

            }
        }


    }




}