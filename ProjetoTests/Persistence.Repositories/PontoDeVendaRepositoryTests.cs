using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class PontoDeVendaRepositoryTests
    {
        [TestMethod()]
        public void PontoDeVendaRepository_InsertDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {

                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.Id == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = expectedTipoUtilizador,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expectedUtilizador);

                var expectedMorada = new Morada
                {
                    Endereco = "Rua do Teste 1",
                    Localidade = "Odivelas",
                    CodigoPostal = "1000999",
                    Observacoes = "Observacoes para teste"
                };
                unitOfWork.Moradas.Add(expectedMorada);

                var expectedLoja = new Loja
                {
                    Nome = "Loja Teste",
                    Email = "loja@teste.pt",
                    Telefone = "123456789",
                    NumeroFiscal = "123456789",
                    Responsavel = expectedUtilizador,
                    Morada = expectedMorada

                };

                var id = Guid.NewGuid();
                var expected = new PontoDeVenda
                {
                    Identificador = id,
                    Nome = "POS 1",
                    Loja = expectedLoja
                };
                var DateExpected = DateTime.Today;

                //act
                unitOfWork.PontoDeVendas.Add(expected);
                unitOfWork.Complete();


                var actual = unitOfWork.PontoDeVendas.Get(id);
                var actualFind = unitOfWork.PontoDeVendas.Find(x=>x.Identificador == id).FirstOrDefault();
                var DateActual = new DateTime(actual.DataCriacao.Value.Year, actual.DataCriacao.Value.Month, actual.DataCriacao.Value.Day, 0, 0, 0);

                //assert
                Assert.IsNotNull(actual);
                Assert.IsNull(actual.DataUltimaAlteracao);
                Assert.AreEqual(DateExpected, DateActual);
                Assert.IsTrue(actual.Ativo);
                Assert.AreEqual(expected.Identificador, actual.Identificador);
                Assert.AreEqual(actual.Loja.Nome, expectedLoja.Nome);
            }
        }

        [TestMethod()]
        public void PontoDeVendaRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.Id == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = expectedTipoUtilizador,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expectedUtilizador);

                var expectedMorada = new Morada
                {
                    Endereco = "Rua do Teste 1",
                    Localidade = "Odivelas",
                    CodigoPostal = "1000999",
                    Observacoes = "Observacoes para teste"
                };
                unitOfWork.Moradas.Add(expectedMorada);

                var expectedLoja = new Loja
                {
                    Nome = "Loja Teste",
                    Email = "loja@teste.pt",
                    Telefone = "123456789",
                    NumeroFiscal = "123456789",
                    Responsavel = expectedUtilizador,
                    Morada = expectedMorada

                };

                var expected = new List<PontoDeVenda>() {
                    new PontoDeVenda
                    {
                        Nome = "POS 1",
                        Loja = expectedLoja
                    },
                    new PontoDeVenda
                    {
                        Nome = "POS 2",
                    },
                };
                unitOfWork.PontoDeVendas.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.PontoDeVendas.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void PontoDeVendaRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange

                var expectedLoja = new Loja
                {
                    Nome = "Loja Teste",
                    Email = "loja@teste.pt",
                    Telefone = "123456789",
                    NumeroFiscal = "123456789",
                };

                var guid = Guid.NewGuid();
                var expected = new List<PontoDeVenda>() {

                    new PontoDeVenda
                    {
                        Identificador = guid,
                        Nome = "POS 1",
                    },
                    new PontoDeVenda
                    {
                        Nome = "POS 2",
                    },
                    new PontoDeVenda
                    {
                        Identificador = Guid.NewGuid(),
                        Nome = "POS 3",
                        Loja = expectedLoja
                    },

                };

                unitOfWork.PontoDeVendas.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.PontoDeVendas.GetAll();

                //act
                var firstElement = unitOfWork.PontoDeVendas.Get(guid);
                unitOfWork.PontoDeVendas.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.PontoDeVendas.GetAll();

                unitOfWork.PontoDeVendas.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.PontoDeVendas.GetAll();


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

                var expectedLoja = new Loja
                {
                    Nome = "Loja Teste",
                    Email = "loja@teste.pt",
                    Telefone = "123456789",
                    NumeroFiscal = "123456789",
                };

                var guid = Guid.NewGuid();
                var expected = new List<PontoDeVenda>() {
                    new PontoDeVenda
                    {
                        Identificador = guid,
                        Nome = "POS 1",
                        Loja = expectedLoja
                    },
                    new PontoDeVenda
                    {
                        Nome = "POS 2",
                    },
                    new PontoDeVenda
                    {
                        Nome = "POS 3",
                        Loja = expectedLoja
                    },

                };

                unitOfWork.PontoDeVendas.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.PontoDeVendas.GetAll();

                var nomeEsperado = "POS 001";

                var DateExpected = DateTime.Today;

                //act
                var firstElement = unitOfWork.PontoDeVendas.Get(guid);
                firstElement.Nome = nomeEsperado;

                unitOfWork.PontoDeVendas.Update(firstElement);
                unitOfWork.Complete();
                firstElement = unitOfWork.PontoDeVendas.Get(guid);
                var actualFirstUpdate = unitOfWork.PontoDeVendas.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.Nome = nomeEsperado;
                }
                unitOfWork.PontoDeVendas.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.PontoDeVendas.GetAll();

                var DateModifiedCount = actualSecondUpdate.Count(x =>
                       x.DataUltimaAlteracao.Value.Year == DateExpected.Year
                    && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
                    && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

                var dadosAlterados = actualSecondUpdate.Count(x => x.Nome == nomeEsperado);

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(nomeEsperado, firstElement.Nome);
                Assert.AreEqual(3, dadosAlterados);
                Assert.AreEqual(3, DateModifiedCount);

            }
        }


    }
}