using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class CategoriaProdutoRepositoryTests
    {
        [TestMethod()]
        public void CategoriaProdutoRepository_InsertDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange

                var unitOfWork = new UnitOfWork(contexto);

                var guid = Guid.NewGuid();
                var expected = new CategoriaProduto
                {
                    Identificador = guid,
                    Nome = "Fruta",
                    OrdemApresentacao = 100
                };
                var DateExpected = DateTime.Today;

                //act
                unitOfWork.CategoriaProdutos.Add(expected);
                unitOfWork.Complete();

                var actual = unitOfWork.CategoriaProdutos.Find(x => x.Identificador == guid).FirstOrDefault();
                var DateActual = new DateTime(actual.DataCriacao.Value.Year,actual.DataCriacao.Value.Month, actual.DataCriacao.Value.Day, 0,0,0); 

                //assert
                Assert.IsNotNull(actual);
                Assert.IsNull(actual.DataUltimaAlteracao);
                Assert.AreEqual(DateExpected, DateActual);
                Assert.IsTrue(actual.Ativo);
                Assert.AreEqual(expected.Identificador, actual.Identificador);

            }
        }

        [TestMethod()]
        public void CategoriaProdutoRepository_GetDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();

                var expected = new CategoriaProduto
                {
                    Identificador = guid,
                    Nome = "Legumes",
                };

                unitOfWork.CategoriaProdutos.Add(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.CategoriaProdutos.Get(guid);

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expected.Identificador, actual.Identificador);

            }
        }

        [TestMethod()]
        public void CategoriaProdutoRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);

                var expected = new List<CategoriaProduto>() {
                    new CategoriaProduto{
                        Nome = "Legumes",
                    },
                    new CategoriaProduto{
                        Nome = "Frutas",
                    },
                };
                unitOfWork.CategoriaProdutos.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.CategoriaProdutos.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void CategoriaProdutoRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();
                //arrange
                var expected = new List<CategoriaProduto>() {
                    new CategoriaProduto{
                        Identificador = guid,
                        Nome = "Legumes",
                        OrdemApresentacao = 1
                    },
                    new CategoriaProduto{
                        Identificador = Guid.NewGuid(),
                        Nome = "Frutas",
                        OrdemApresentacao = 2
                    },
                    new CategoriaProduto{
                        Nome = "Peixes",
                        OrdemApresentacao = 3
                    },
                };

                unitOfWork.CategoriaProdutos.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.CategoriaProdutos.GetAll();

                //act
                var firstElement = unitOfWork.CategoriaProdutos.Get(guid);
                unitOfWork.CategoriaProdutos.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.CategoriaProdutos.GetAll();

                unitOfWork.CategoriaProdutos.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.CategoriaProdutos.GetAll();

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());

            }
        }

        [TestMethod()]
        public void CategoriaProdutoRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();

                //arrange
                var expected = new List<CategoriaProduto>() {
                    new CategoriaProduto{
                        Identificador = guid,
                        Nome = "Legumes",
                        OrdemApresentacao = 1
                    },
                    new CategoriaProduto{
                        Identificador = Guid.NewGuid(),
                        Nome = "Frutas",
                        OrdemApresentacao = 2
                    },
                    new CategoriaProduto{
                        Nome = "Peixes",
                        OrdemApresentacao = 3
                    },
                };

                unitOfWork.CategoriaProdutos.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.CategoriaProdutos.GetAll();

                var nomeEsperado = "Nome Esperado";
                var ordemApresentacaoAlterada = 5;

                var DateExpected = DateTime.Today;

                //act
                var firstElement = unitOfWork.CategoriaProdutos.Get(guid);
                firstElement.Nome = nomeEsperado;
                unitOfWork.CategoriaProdutos.Update(firstElement);
                unitOfWork.Complete();

                firstElement = unitOfWork.CategoriaProdutos.Get(guid);
                var actualFirstUpdate = unitOfWork.CategoriaProdutos.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.OrdemApresentacao = ordemApresentacaoAlterada;
                }
                unitOfWork.CategoriaProdutos.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.CategoriaProdutos.GetAll();


                var DateModifiedCount = actualSecondUpdate.Count(x =>
                       x.DataUltimaAlteracao.Value.Year == DateExpected.Year
                    && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
                    && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

                var ordensAlteradas = actualSecondUpdate.Count(x => x.OrdemApresentacao == ordemApresentacaoAlterada);

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(nomeEsperado, firstElement.Nome);
                Assert.AreEqual(3, ordensAlteradas);
                Assert.AreEqual(3, DateModifiedCount);

            }
        }


    }




}