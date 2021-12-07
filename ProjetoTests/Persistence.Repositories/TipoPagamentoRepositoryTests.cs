using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class TipoPagamentoRepositoryTests
    {

        [TestMethod()]
        public void TipoPagamentoRepository_SeedDataVersusEnumeradoDBTest()
        {
            //Teste with a Dabase in sqlite ...
            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var expectedEnums = Enum.GetValues(typeof(TipoPagamentoEnum)).Length;
                var allMatch = true;

                //act
                var actual = unitOfWork.TipoPagamentos.GetAll();
                foreach (var item in actual)
                {
                    if (!Enum.IsDefined(typeof(TipoPagamentoEnum), item.Id))
                    {
                        allMatch = false;
                    }
                }

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedEnums, actual.Count());
                Assert.IsTrue(allMatch);
            }
        }

        [TestMethod()]
        public void TipoPagamentoRepository_InsertDBTest()
        {
            //Teste with a Dabase in sqlite ...
            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange

                var unitOfWork = new UnitOfWork(contexto);
                var expectedName = "Cheque";
                var enumsOnCode = Enum.GetValues(typeof(TipoPagamentoEnum)).Length + 1;

                var expected = new TipoPagamento
                {
                    Id = enumsOnCode,
                    Name = expectedName
                };

                //act
                unitOfWork.TipoPagamentos.Add(expected);
                unitOfWork.Complete();

                var actual = unitOfWork.TipoPagamentos.Find(x => x.Id == enumsOnCode).FirstOrDefault();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(enumsOnCode, actual.Id);
                Assert.AreEqual(expectedName, actual.Name);

            }
        }


        [TestMethod()]
        public void TipoPagamentoRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...
            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);

                var enumsOnCode = Enum.GetValues(typeof(TipoPagamentoEnum)).Length + 1;

                var expected = new List<TipoPagamento>() {
                    new TipoPagamento
                    {
                        Id = enumsOnCode++,
                        Name = "Dollar"
                    },
                    new TipoPagamento
                    {
                        Id = enumsOnCode,
                        Name = "Euro"
                    }
                };

                unitOfWork.TipoPagamentos.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.TipoPagamentos.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(enumsOnCode, actual.Count());

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
                var actualBefore = unitOfWork.TipoPagamentos.GetAll();

                //act
                var firstElement = unitOfWork.TipoPagamentos.Find(x=>x.Id == 1).FirstOrDefault();
                unitOfWork.TipoPagamentos.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.TipoPagamentos.GetAll();

                unitOfWork.TipoPagamentos.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.TipoPagamentos.GetAll();

                //assert
                Assert.AreEqual(4, actualBefore.Count());
                Assert.AreEqual(3, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());
            }
        }

        [TestMethod()]
        public void TipoPagamentoRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);

                //act
                var firstElement = unitOfWork.TipoPagamentos.Find(x => x.Id == 1).FirstOrDefault();
                firstElement.Name = "E"+firstElement.Name;
                unitOfWork.TipoPagamentos.Update(firstElement);
                unitOfWork.Complete();

                var actualFirstUpdate = unitOfWork.TipoPagamentos.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.Name = "E" + item.Name;
                }
                unitOfWork.TipoPagamentos.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.TipoPagamentos.GetAll();

                var nomesAlterados = actualSecondUpdate.Count(x => x.Name.StartsWith("E"));

                //assert
                Assert.AreEqual(4, nomesAlterados);

            }
        }


    }




}