using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class TipoUtilizadorRepositoryTests
    {

        [TestMethod()]
        public void TipoUtilizadorRepository_SeedDataVersusEnumeradoDBTest()
        {
            //Teste with a Dabase in sqlite ...
            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var expectedEnums = Enum.GetValues(typeof(TipoUtilizadorEnum)).Length;
                var allMatch = true;

                //act
                var actual = unitOfWork.TipoUtilizadores.GetAll();
                foreach (var item in actual)
                {
                    if (!Enum.IsDefined(typeof(TipoUtilizadorEnum), item.TipoId))
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
        public void TipoUtilizadorRepository_InsertDBTest()
        {
            //Teste with a Dabase in sqlite ...
            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange

                var unitOfWork = new UnitOfWork(contexto);
                var expectedName = "Boss";
                var enumsOnCode = Enum.GetValues(typeof(TipoUtilizadorEnum)).Length + 1;

                var expected = new TipoUtilizador
                {
                    TipoId = enumsOnCode,
                    Name = expectedName
                };

                //act
                unitOfWork.TipoUtilizadores.Add(expected);
                unitOfWork.Complete();

                var actual = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == enumsOnCode).FirstOrDefault();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(enumsOnCode, actual.TipoId);
                Assert.AreEqual(expectedName, actual.Name);

            }
        }


        [TestMethod()]
        public void TipoUtilizadorRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...
            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);

                var enumsOnCode = Enum.GetValues(typeof(TipoUtilizadorEnum)).Length + 1;

                var expected = new List<TipoUtilizador>() {
                    new TipoUtilizador
                    {
                        TipoId = enumsOnCode++,
                        Name = "Dollar"
                    },
                    new TipoUtilizador
                    {
                        TipoId = enumsOnCode,
                        Name = "Euro"
                    }
                };

                unitOfWork.TipoUtilizadores.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.TipoUtilizadores.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(enumsOnCode, actual.Count());

            }
        }

        [TestMethod()]
        public void TipoUtilizadorRepository_RemoveDBTest()
        {
            //Teste with a Dabase in sqlite ...
            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var actualBefore = unitOfWork.TipoUtilizadores.GetAll();

                //act
                var firstElement = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == 1).FirstOrDefault();
                unitOfWork.TipoUtilizadores.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.TipoUtilizadores.GetAll();

                unitOfWork.TipoUtilizadores.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.TipoUtilizadores.GetAll();

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());
            }
        }

        [TestMethod()]
        public void TipoUtilizadorRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);

                //act
                var firstElement = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == 1).FirstOrDefault();
                firstElement.Name = "E" + firstElement.Name;
                unitOfWork.TipoUtilizadores.Update(firstElement);
                unitOfWork.Complete();

                var actualFirstUpdate = unitOfWork.TipoUtilizadores.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.Name = "E" + item.Name;
                }
                unitOfWork.TipoUtilizadores.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.TipoUtilizadores.GetAll();

                var nomesAlterados = actualSecondUpdate.Count(x => x.Name.StartsWith("E"));

                //assert
                Assert.AreEqual(3, nomesAlterados);

            }
        }


    }




}