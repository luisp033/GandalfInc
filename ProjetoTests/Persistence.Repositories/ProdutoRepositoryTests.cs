using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Projeto.DataAccessLayer.Core;
using Projeto.DataAccessLayer.Core.Repositories;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class ProdutoRepositoryTests
    {
        [TestMethod()]
        public void ProdutoRepository_InsertGetAndFindDBTest()
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
                    Origem  = "USA"
                };
                unitOfWork.MarcaProdutos.Add(expectedMarca);

                var expected = new Produto
                {
                    Identificador = guid,
                    Nome = "Iphone 1",
                    Categoria = expectedCategoria,
                    Marca = expectedMarca,
                    PrecoUnitario = 100,
                    Ean = "GTR5373529-56",
                };

                var DateExpected = DateTime.Today;

                //act
                unitOfWork.Produtos.Add(expected);
                unitOfWork.Complete();

                var actual = unitOfWork.Produtos.Find(x => x.Identificador == guid).FirstOrDefault();
                var actualGet = unitOfWork.Produtos.Get(guid);
                var DateActual = new DateTime(actual.DataCriacao.Value.Year,actual.DataCriacao.Value.Month, actual.DataCriacao.Value.Day, 0,0,0); 

                //assert
                Assert.IsNotNull(actual);
                Assert.IsNotNull(actual.Marca);
                Assert.IsNotNull(actual.Categoria);
                Assert.IsNull(actual.DataUltimaAlteracao);
                Assert.AreEqual(DateExpected, DateActual);
                Assert.IsTrue(actual.Ativo);
                Assert.AreEqual(expected.Identificador, actual.Identificador);
                Assert.AreEqual(actual.Identificador, actualGet.Identificador);

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

                var expected = new List<Produto>() {
                    new Produto
                    {
                        Nome = "Iphone 1",
                        Categoria = expectedCategoria,
                        Marca = expectedMarca,
                        PrecoUnitario = 100,
                        Ean = "GTR5373529-56",
                    },
                    new Produto
                    {
                        Nome = "Iphone 2",
                        Categoria = expectedCategoria,
                        Marca = expectedMarca,
                        PrecoUnitario = 200,
                    },
                };
                unitOfWork.Produtos.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.Produtos.GetAll();

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

                var expected = new List<Produto>() {
                    new Produto
                    {
                        Identificador = guid,
                        Nome = "Iphone 1",
                        Categoria = expectedCategoria,
                        Marca = expectedMarca,
                        PrecoUnitario = 100,
                        Ean = "GTR5373529-56",
                    },
                    new Produto
                    {
                        Nome = "Iphone 2",
                        Categoria = expectedCategoria,
                        Marca = expectedMarca,
                        PrecoUnitario = 200,
                    },
                    new Produto
                    {
                        Nome = "Iphone 3",
                        Categoria = expectedCategoria,
                        Marca = expectedMarca,
                        PrecoUnitario = 300,
                    },
                };

                unitOfWork.Produtos.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Produtos.GetAll();

                //act
                var firstElement = unitOfWork.Produtos.Get(guid);
                unitOfWork.Produtos.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.Produtos.GetAll();

                unitOfWork.Produtos.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.Clientes.GetAll();

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());

            }
        }

        [TestMethod()]
        public void ProdutoRepository_UpdateDBTest()
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

                var expected = new List<Produto>() {
                    new Produto
                    {
                        Identificador = guid,
                        Nome = "Iphone 1",
                        Categoria = expectedCategoria,
                        Marca = expectedMarca,
                        PrecoUnitario = 100,
                        Ean = "GTR5373529-56",
                    },
                    new Produto
                    {
                        Nome = "Iphone 2",
                        Categoria = expectedCategoria,
                        Marca = expectedMarca,
                        PrecoUnitario = 200,
                    },
                    new Produto
                    {
                        Nome = "Iphone 3",
                        Categoria = expectedCategoria,
                        Marca = expectedMarca,
                        PrecoUnitario = 300,
                    },
                };

                unitOfWork.Produtos.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Produtos.GetAll();

                var nomeEsperado = "Xiomi";
                var precoAlterado = 12.34m;

                var DateExpected = DateTime.Today;

                //act
                var firstElement = unitOfWork.Produtos.Get(guid);
                firstElement.Nome = nomeEsperado;
                unitOfWork.Produtos.Update(firstElement);
                unitOfWork.Complete();

                firstElement = unitOfWork.Produtos.Get(guid);
                var actualFirstUpdate = unitOfWork.Produtos.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.PrecoUnitario = precoAlterado;
                }
                unitOfWork.Produtos.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.Produtos.GetAll();


                var DateModifiedCount = actualSecondUpdate.Count(x =>
                       x.DataUltimaAlteracao.Value.Year == DateExpected.Year
                    && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
                    && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

                var ordensAlteradas = actualSecondUpdate.Count(x => x.PrecoUnitario == precoAlterado);

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(nomeEsperado, firstElement.Nome);
                Assert.AreEqual(3, ordensAlteradas);
                Assert.AreEqual(3, DateModifiedCount);

            }
        }


    }




}