using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class DetalheVendaRepositoryTests
    {
        [TestMethod()]
        public void DetalheVendaRepository_InsertGetAndFindDBTest()
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

                var expectedVenda = new Venda
                {
                };
                unitOfWork.Vendas.Add(expectedVenda);

                unitOfWork.Complete();
                var DateExpected = DateTime.Today;

                //act

                var expectedDetalhe1 = new DetalheVenda
                {
                    Desconto = 0,
                    PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 0m,
                    Estoque = expectedEstoque1,
                    Venda = expectedVenda,
                };
                var expectedDetalhe2 = new DetalheVenda
                {
                    Desconto = 50.0m,
                    PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 50.0m,
                    Estoque = expectedEstoque2,
                    Venda = expectedVenda,
                };

                unitOfWork.DetalheVendas.Add(expectedDetalhe1);
                unitOfWork.DetalheVendas.Add(expectedDetalhe2);
                unitOfWork.Complete();

                var actualDetalheVendas = unitOfWork.DetalheVendas.GetAll();
                var actualFirst = unitOfWork.DetalheVendas.Get(actualDetalheVendas.FirstOrDefault().Identificador);

                //assert
                Assert.IsNotNull(actualDetalheVendas);
                Assert.IsNotNull(actualFirst);
                Assert.AreEqual(2, actualDetalheVendas.Count());

            }
        }

        [TestMethod()]
        public void DetalheVendaRepository_AddRangeAndGetAllDBTest()
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

                var expectedVenda = new Venda
                {
                };
                unitOfWork.Vendas.Add(expectedVenda);

                unitOfWork.Complete();

                var expected = new List<DetalheVenda>() {
                    new DetalheVenda
                    {
                        Desconto = 0,
                        PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 0m,
                        Estoque = expectedEstoque1,
                        Venda = expectedVenda,
                    },
                    new DetalheVenda
                    {
                        Desconto = 50.0m,
                        PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 50.0m,
                        Estoque = expectedEstoque2,
                        Venda = expectedVenda,
                    }
                };
                unitOfWork.DetalheVendas.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.DetalheVendas.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void DetalheVendaRepository_RemoveDBTest()
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
                var expectedEstoque3 = new Estoque
                {
                    NumeroSerie = "SN/2",
                    Produto = expectedProduto,
                    DataEntrada = DateTime.Today,

                };
                unitOfWork.Estoques.Add(expectedEstoque2);

                var expectedVenda = new Venda
                {
                };
                unitOfWork.Vendas.Add(expectedVenda);

                unitOfWork.Complete();

                var expected = new List<DetalheVenda>() {
                    new DetalheVenda
                    {
                        Identificador = guid,
                        Desconto = 0,
                        PrecoFinal = 0,
                        Estoque = expectedEstoque1,
                        Venda = expectedVenda,
                    },
                    new DetalheVenda
                    {
                        Desconto = 0,
                        PrecoFinal = 0,
                        Estoque = expectedEstoque2,
                        Venda = expectedVenda,
                    },
                    new DetalheVenda
                    {
                        Desconto = 0,
                        PrecoFinal = 0,
                        Estoque = expectedEstoque3,
                        Venda = expectedVenda,

                    }
                };
                unitOfWork.DetalheVendas.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.DetalheVendas.GetAll();

                //act
                var firstElement = unitOfWork.DetalheVendas.Get(guid);
                unitOfWork.DetalheVendas.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.DetalheVendas.GetAll();

                unitOfWork.DetalheVendas.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.DetalheVendas.GetAll();

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());

            }
        }

        [TestMethod()]
        public void DetalheVendaRepository_UpdateDBTest()
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
                var expectedEstoque3 = new Estoque
                {
                    NumeroSerie = "SN/2",
                    Produto = expectedProduto,
                    DataEntrada = DateTime.Today,

                };
                unitOfWork.Estoques.Add(expectedEstoque2);

                var expectedVenda = new Venda
                {
                };
                unitOfWork.Vendas.Add(expectedVenda);

                unitOfWork.Complete();

                var expected = new List<DetalheVenda>() {
                    new DetalheVenda
                    {
                        Identificador = guid,
                        Desconto = 0,
                        PrecoFinal = 0,
                        Estoque = expectedEstoque1,
                        Venda = expectedVenda,
                    },
                    new DetalheVenda
                    {
                        Desconto = 0,
                        PrecoFinal = 0,
                        Estoque = expectedEstoque2,
                        Venda = expectedVenda,
                    },
                    new DetalheVenda
                    {
                        Desconto = 0,
                        PrecoFinal = 0,
                        Estoque = expectedEstoque3,
                        Venda = expectedVenda,

                    }
                };
                unitOfWork.DetalheVendas.AddRange(expected);
                unitOfWork.Complete();

                var actualBefore = unitOfWork.DetalheVendas.GetAll();

                var dadoEsperado = 5.0m;

                //act
                var firstElement = unitOfWork.DetalheVendas.Get(guid);
                firstElement.Desconto = dadoEsperado;
                unitOfWork.DetalheVendas.Update(firstElement);
                unitOfWork.Complete();

                firstElement = unitOfWork.DetalheVendas.Get(guid);
                var actualFirstUpdate = unitOfWork.DetalheVendas.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.Desconto = dadoEsperado;
                }
                unitOfWork.DetalheVendas.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.DetalheVendas.GetAll();


                var dadosAlterados = actualSecondUpdate.Count(x => x.Desconto == dadoEsperado);

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(dadoEsperado, firstElement.Desconto);
                Assert.AreEqual(3, dadosAlterados);


            }
        }


    }




}