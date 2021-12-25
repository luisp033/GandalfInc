using Projeto.DataAccessLayer.Persistence.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class PontoDeVendaSessaoRepositoryTests
    {
        [TestMethod()]
        public void PontoDeVendaSessaoRepository_InsertDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {

                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = expectedTipoUtilizador,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expectedUtilizador);

                var expectedLoja = new Loja
                {
                    Nome = "Loja 1",
                };
                unitOfWork.Lojas.Add(expectedLoja);

                var expectedPos = new PontoDeVenda
                {
                    Nome = "POS Teste1",
                    Loja = expectedLoja
                };
                unitOfWork.PontoDeVendas.Add(expectedPos);

                var id = Guid.NewGuid();
                var expected = new PontoDeVendaSessao
                {
                    Identificador = id,
                    Utilizador = expectedUtilizador,
                    PontoDeVenda = expectedPos,
                    DataLogin = DateTime.Today
                };
                var DateExpected = DateTime.Today;

                //act
                unitOfWork.PontoDeVendaSessoes.Add(expected);
                unitOfWork.Complete();

                var actual = unitOfWork.PontoDeVendaSessoes.Get(id);
                var actualFind = unitOfWork.PontoDeVendaSessoes.Find(x => x.Identificador == id).FirstOrDefault();
                var DateActual = new DateTime(actual.DataLogin.Year, actual.DataLogin.Month, actual.DataLogin.Day, 0, 0, 0);

                //assert
                Assert.IsNotNull(actual);
                Assert.IsNull(actual.DataLogout);
                Assert.AreEqual(DateExpected, DateActual);
                Assert.AreEqual(expected.Identificador, actualFind.Identificador);
                Assert.AreEqual(expected.Identificador, actual.Identificador);
                Assert.AreEqual(actual.Utilizador.Nome, expectedUtilizador.Nome);
                Assert.AreEqual(actual.PontoDeVenda.Nome, expectedPos.Nome);
            }
        }

        [TestMethod()]
        public void PontoDeVendaSessaoRepository_InsertLogoutDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {

                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = expectedTipoUtilizador,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expectedUtilizador);

                var expectedLoja = new Loja
                {
                    Nome = "Loja 1",
                };
                unitOfWork.Lojas.Add(expectedLoja);

                var expectedPos = new PontoDeVenda
                {
                    Nome = "POS Teste1",
                    Loja = expectedLoja
                };
                unitOfWork.PontoDeVendas.Add(expectedPos);

                var id = Guid.NewGuid();
                var DateLoginExpected = DateTime.Today.AddDays(-1);
                var DateLogoutExpected = DateTime.Today;
                var expected = new PontoDeVendaSessao
                {
                    Identificador = id,
                    Utilizador = expectedUtilizador,
                    PontoDeVenda = expectedPos,
                    DataLogin = DateLoginExpected,
                    DataLogout = DateLogoutExpected
                };

                //act
                unitOfWork.PontoDeVendaSessoes.Add(expected);
                unitOfWork.Complete();

                var actual = unitOfWork.PontoDeVendaSessoes.Get(id);
                var DateLoginActual = new DateTime(actual.DataLogin.Year, actual.DataLogin.Month, actual.DataLogin.Day, 0, 0, 0);
                var DateLogoutActual = new DateTime(actual.DataLogout.Value.Year, actual.DataLogout.Value.Month, actual.DataLogout.Value.Day, 0, 0, 0);

                //assert
                Assert.IsNotNull(actual);
                Assert.IsNotNull(actual.DataLogout);
                Assert.AreEqual(DateLoginExpected, DateLoginActual);
                Assert.AreEqual(DateLogoutExpected, DateLogoutActual);
                Assert.AreEqual(expected.Identificador, actual.Identificador);
                Assert.AreEqual(actual.Utilizador.Nome, expectedUtilizador.Nome);
                Assert.AreEqual(actual.PontoDeVenda.Nome, expectedPos.Nome);
            }
        }


        [TestMethod()]
        public void PontoDeVendaSessaoRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = expectedTipoUtilizador,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expectedUtilizador);

                var expectedLoja = new Loja
                {
                    Nome = "Loja 1",
                };
                unitOfWork.Lojas.Add(expectedLoja);

                var expectedPos = new PontoDeVenda
                {
                    Nome = "POS Teste1",
                    Loja = expectedLoja
                };
                unitOfWork.PontoDeVendas.Add(expectedPos);


                var expected = new List<PontoDeVendaSessao>() {
                    new PontoDeVendaSessao
                    {
                        Utilizador = expectedUtilizador,
                        PontoDeVenda = expectedPos,
                        DataLogin = DateTime.Today
                    },
                    new PontoDeVendaSessao
                    {
                        Utilizador = expectedUtilizador,
                        PontoDeVenda = expectedPos,
                        DataLogin = DateTime.Today,
                        DataLogout = DateTime.Today

                    },
                };
                unitOfWork.PontoDeVendaSessoes.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.PontoDeVendaSessoes.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void PontoDeVendaSessaoRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = expectedTipoUtilizador,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expectedUtilizador);

                var expectedLoja = new Loja
                {
                    Nome = "Loja 1",
                };
                unitOfWork.Lojas.Add(expectedLoja);

                var expectedPos = new PontoDeVenda
                {
                    Nome = "POS Teste1",
                    Loja = expectedLoja
                };
                unitOfWork.PontoDeVendas.Add(expectedPos);

                var guid = Guid.NewGuid();
                var expected = new List<PontoDeVendaSessao>() {

                    new PontoDeVendaSessao
                    {
                        Identificador = guid,
                        Utilizador = expectedUtilizador,
                        PontoDeVenda = expectedPos,
                        DataLogin = DateTime.Today
                    },
                    new PontoDeVendaSessao
                    {
                        Utilizador = expectedUtilizador,
                        PontoDeVenda = expectedPos,
                        DataLogin = DateTime.Today.AddDays(1)
                    },
                    new PontoDeVendaSessao
                    {
                        Identificador = Guid.NewGuid(),
                        Utilizador = expectedUtilizador,
                        PontoDeVenda = expectedPos,
                        DataLogin = DateTime.Today.AddDays(-1),
                        DataLogout = DateTime.Today
                    },

                };

                unitOfWork.PontoDeVendaSessoes.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.PontoDeVendaSessoes.GetAll();

                //act
                var firstElement = unitOfWork.PontoDeVendaSessoes.Get(guid);
                unitOfWork.PontoDeVendaSessoes.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.PontoDeVendaSessoes.GetAll();

                unitOfWork.PontoDeVendaSessoes.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.PontoDeVendaSessoes.GetAll();


                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());
            }
        }

        [TestMethod()]
        public void PontoDeVendaRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = expectedTipoUtilizador,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expectedUtilizador);

                var expectedLoja = new Loja
                {
                    Nome = "Loja 1",
                };
                unitOfWork.Lojas.Add(expectedLoja);

                var expectedPos = new PontoDeVenda
                {
                    Nome = "POS Teste1",
                    Loja = expectedLoja
                };
                unitOfWork.PontoDeVendas.Add(expectedPos);

                var guid = Guid.NewGuid();
                var expected = new List<PontoDeVendaSessao>() {
                    new PontoDeVendaSessao
                    {
                        Identificador = guid,
                        Utilizador = expectedUtilizador,
                        PontoDeVenda = expectedPos,
                        DataLogin = DateTime.Today,

                    },
                    new PontoDeVendaSessao
                    {
                        Identificador = Guid.NewGuid(),
                        Utilizador = expectedUtilizador,
                        PontoDeVenda = expectedPos,
                        DataLogin = DateTime.Today.AddDays(-1),
                    },
                    new PontoDeVendaSessao
                    {
                        Identificador = Guid.NewGuid(),
                        Utilizador = expectedUtilizador,
                        PontoDeVenda = expectedPos,
                        DataLogin = DateTime.Today.AddDays(-2),
                        DataLogout = DateTime.Today
                    },

                };

                unitOfWork.PontoDeVendaSessoes.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.PontoDeVendaSessoes.GetAll();

                var DateExpected = DateTime.Today;

                //act
                var firstElement = unitOfWork.PontoDeVendaSessoes.Get(guid);
                firstElement.DataLogout = DateExpected;

                unitOfWork.PontoDeVendaSessoes.Update(firstElement);
                unitOfWork.Complete();
                firstElement = unitOfWork.PontoDeVendaSessoes.Get(guid);
                var actualFirstUpdate = unitOfWork.PontoDeVendaSessoes.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.DataLogout = DateExpected;
                }
                unitOfWork.PontoDeVendaSessoes.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.PontoDeVendaSessoes.GetAll();

                var DateModifiedCount = actualSecondUpdate.Count(x =>
                       x.DataLogout.Value.Year == DateExpected.Year
                    && x.DataLogout.Value.Month == DateExpected.Month
                    && x.DataLogout.Value.Day == DateExpected.Day);


                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(3, DateModifiedCount);

            }
        }

        [TestMethod()]
        public void GetTotalSessaoTest()
        {

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {

                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = expectedTipoUtilizador,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expectedUtilizador);

                var expectedLoja = new Loja
                {
                    Nome = "Loja 1",
                };
                unitOfWork.Lojas.Add(expectedLoja);

                var expectedPos = new PontoDeVenda
                {
                    Nome = "POS Teste1",
                    Loja = expectedLoja
                };
                unitOfWork.PontoDeVendas.Add(expectedPos);

                var id = Guid.NewGuid();
                var DateLoginExpected = DateTime.Today.AddDays(-1);
                var DateLogoutExpected = DateTime.Today;
                var expectedSessao = new PontoDeVendaSessao
                {
                    Identificador = id,
                    Utilizador = expectedUtilizador,
                    PontoDeVenda = expectedPos,
                    DataLogin = DateLoginExpected,
                    DataLogout = DateLogoutExpected
                };
                unitOfWork.PontoDeVendaSessoes.Add(expectedSessao);


                //act

                var expectTotalList = unitOfWork.PontoDeVendaSessoes.GetTotalSessao(expectedSessao.Identificador);

                //Assert
                Assert.AreEqual(0,expectTotalList.Count);
            }

        }

        //[TestMethod()]
        //public void GetTotalSessaoSqlServerTest()
        //{

        //    using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext()))
        //    {

        //        //arrange

        //        var expectedSessao = unitOfWork.PontoDeVendaSessoes.Get(new Guid("2A410A65-C76F-47FD-24D6-08D9C18CCB66"));

        //        //act

        //        var expectTotalList = unitOfWork.PontoDeVendaSessoes.GetTotalSessao(expectedSessao.Identificador);

        //        //Assert
        //        Assert.AreEqual(2, expectTotalList.Count);
        //    }

        //}

    }
}