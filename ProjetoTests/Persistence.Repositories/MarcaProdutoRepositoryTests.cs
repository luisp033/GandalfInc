using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class MarcaProdutoRepositoryTests
    {
        [TestMethod()]
        public void MarcaProdutoRepository_InsertDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange

                var unitOfWork = new UnitOfWork(contexto);

                var guid = Guid.NewGuid();
                var expected = new MarcaProduto
                {
                    Identificador = guid,
                    Nome = "Compadres",
                    Origem = "Alentejo"
                };
                var DateExpected = DateTime.Today;

                //act
                unitOfWork.MarcaProdutos.Add(expected);
                unitOfWork.Complete();

                var actual = unitOfWork.MarcaProdutos.Find(x => x.Identificador == guid).FirstOrDefault();
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
        public void MarcaProdutoRepository_GetDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();

                var expected = new MarcaProduto
                {
                    Identificador = guid,
                    Nome = "Sony",
                };

                unitOfWork.MarcaProdutos.Add(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.MarcaProdutos.Get(guid);

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expected.Identificador, actual.Identificador);

            }
        }

        [TestMethod()]
        public void MarcaProdutoRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);

                var expected = new List<MarcaProduto>() {
                    new MarcaProduto{
                        Nome = "Apple",
                    },
                    new MarcaProduto{
                        Nome = "Samsung",
                        Origem = "Japão" 
                    },
                };
                unitOfWork.MarcaProdutos.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.MarcaProdutos.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void MarcaProdutoRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();
                //arrange
                var expected = new List<MarcaProduto>() {
                    new MarcaProduto{
                        Identificador = guid,
                        Nome = "Sony",
                        Origem = "Japão"
                    },
                    new MarcaProduto{
                        Identificador = Guid.NewGuid(),
                        Nome = "Xiaomi",
                        Origem = "China"
                    },
                    new MarcaProduto{
                        Nome = "Bmw",
                    },
                };

                unitOfWork.MarcaProdutos.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.MarcaProdutos.GetAll();

                //act
                var firstElement = unitOfWork.MarcaProdutos.Get(guid);
                unitOfWork.MarcaProdutos.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.MarcaProdutos.GetAll();

                unitOfWork.MarcaProdutos.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.MarcaProdutos.GetAll();

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());

            }
        }

        [TestMethod()]
        public void MarcaProdutoRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();

                //arrange
                var expected = new List<MarcaProduto>() {
                    new MarcaProduto{
                        Identificador = guid,
                        Nome = "Sony",
                        Origem = "Japão"
                    },
                    new MarcaProduto{
                        Identificador = Guid.NewGuid(),
                        Nome = "Xiaomi",
                        Origem = "China"
                    },
                    new MarcaProduto{
                        Nome = "Bmw",
                    },
                };

                unitOfWork.MarcaProdutos.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.MarcaProdutos.GetAll();

                var nomeEsperado = "Tesla";
                var origemAlterada = "Lisboa";

                var DateExpected = DateTime.Today;

                //act
                var firstElement = unitOfWork.MarcaProdutos.Get(guid);
                firstElement.Nome = nomeEsperado;
                unitOfWork.MarcaProdutos.Update(firstElement);
                unitOfWork.Complete();

                firstElement = unitOfWork.MarcaProdutos.Get(guid);
                var actualFirstUpdate = unitOfWork.MarcaProdutos.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.Origem = origemAlterada;
                }
                unitOfWork.MarcaProdutos.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.MarcaProdutos.GetAll();


                var DateModifiedCount = actualSecondUpdate.Count(x =>
                       x.DataUltimaAlteracao.Value.Year == DateExpected.Year
                    && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
                    && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

                var origensAlteradas = actualSecondUpdate.Count(x => x.Origem == origemAlterada);

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(nomeEsperado, firstElement.Nome);
                Assert.AreEqual(3, origensAlteradas);
                Assert.AreEqual(3, DateModifiedCount);

            }
        }


    }




}