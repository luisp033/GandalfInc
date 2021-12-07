using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class ClienteRepositoryTests
    {
        [TestMethod()]
        public void ClienteRepository_InsertGetAndFindDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange

                var unitOfWork = new UnitOfWork(contexto);

                var guid = Guid.NewGuid();

                var expectedMorada = new Morada
                {
                    Endereco = "Rua do Teste 1",
                    Localidade = "Odivelas",
                    CodigoPostal = "1000999",
                    Observacoes = "Observacoes para teste"
                };
                unitOfWork.Moradas.Add(expectedMorada);

                var expected = new Cliente
                {
                    Identificador = guid,
                    Nome = "Luis",
                    DataNascimento = new DateTime(1971,12,17),
                    Email = "luis@mail.pt",
                    Telefone = "123456789",
                    NumeroFiscal = "123456789",
                    MoradaEntrega = expectedMorada,
                    MoradaFaturacao = expectedMorada,
                };

                var DateExpected = DateTime.Today;

                //act
                unitOfWork.Clientes.Add(expected);
                unitOfWork.Complete();

                var actual = unitOfWork.Clientes.Find(x => x.Identificador == guid).FirstOrDefault();
                var actualGet = unitOfWork.Clientes.Get(guid);
                var DateActual = new DateTime(actual.DataCriacao.Value.Year,actual.DataCriacao.Value.Month, actual.DataCriacao.Value.Day, 0,0,0); 

                //assert
                Assert.IsNotNull(actual);
                Assert.IsNotNull(actual.MoradaFaturacao);
                Assert.IsNotNull(actual.MoradaEntrega);
                Assert.IsNull(actual.DataUltimaAlteracao);
                Assert.AreEqual(DateExpected, DateActual);
                Assert.IsTrue(actual.Ativo);
                Assert.AreEqual(expected.Identificador, actual.Identificador);
                Assert.AreEqual(actual.Identificador, actualGet.Identificador);

            }
        }

        [TestMethod()]
        public void ClienteRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);


                var expectedMorada = new Morada
                {
                    Endereco = "Rua do Teste 1",
                    Localidade = "Odivelas",
                    CodigoPostal = "1000999",
                    Observacoes = "Observacoes para teste"
                };
                unitOfWork.Moradas.Add(expectedMorada);

                var expected = new List<Cliente>() {
                    new Cliente
                    {
                        Nome = "Luis",
                        DataNascimento = new DateTime(1971,12,17),
                        Email = "luis@mail.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        MoradaEntrega = expectedMorada,
                        MoradaFaturacao = expectedMorada,
                    },
                    new Cliente
                    {
                        Nome = "Luis",
                        DataNascimento = new DateTime(1971,12,17),
                        Email = "luis@mail.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        MoradaEntrega = expectedMorada,
                        MoradaFaturacao = expectedMorada,
                    },
                };
                unitOfWork.Clientes.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.Clientes.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void ClienteRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();

                var expectedMorada = new Morada
                {
                    Endereco = "Rua do Teste 1",
                    Localidade = "Odivelas",
                    CodigoPostal = "1000999",
                    Observacoes = "Observacoes para teste"
                };
                unitOfWork.Moradas.Add(expectedMorada);

                var expected = new List<Cliente>() {
                    new Cliente
                    {
                        Identificador = guid,
                        Nome = "Luis",
                        DataNascimento = new DateTime(1971,12,17),
                        Email = "luis@mail.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        MoradaEntrega = expectedMorada,
                        MoradaFaturacao = expectedMorada,
                    },
                    new Cliente
                    {
                        Nome = "Luis",
                        DataNascimento = new DateTime(1971,12,17),
                        Email = "luis@mail.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        MoradaEntrega = expectedMorada,
                        MoradaFaturacao = expectedMorada,
                    },
                    new Cliente
                    {
                        Nome = "Luis",
                        DataNascimento = new DateTime(1971,12,17),
                        Email = "luis@mail.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        MoradaEntrega = expectedMorada,
                        MoradaFaturacao = expectedMorada,
                    },
                };

                unitOfWork.Clientes.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Clientes.GetAll();

                //act
                var firstElement = unitOfWork.Clientes.Get(guid);
                unitOfWork.Clientes.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.Clientes.GetAll();

                unitOfWork.Clientes.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.Clientes.GetAll();

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());

            }
        }

        [TestMethod()]
        public void ClienteRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();

                var expectedMorada = new Morada
                {
                    Endereco = "Rua do Teste 1",
                    Localidade = "Odivelas",
                    CodigoPostal = "1000999",
                    Observacoes = "Observacoes para teste"
                };
                unitOfWork.Moradas.Add(expectedMorada);


                var expected = new List<Cliente>() {
                    new Cliente
                    {
                        Identificador = guid,
                        Nome = "Luis 1",
                        DataNascimento = new DateTime(1971,12,17),
                        Email = "luis@mail.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        MoradaEntrega = expectedMorada,
                        MoradaFaturacao = expectedMorada,
                    },
                    new Cliente
                    {
                        Nome = "Luis 2",
                        DataNascimento = new DateTime(1971,12,17),
                        Email = "luis@mail.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        MoradaEntrega = expectedMorada,
                        MoradaFaturacao = expectedMorada,
                    },
                    new Cliente
                    {
                        Nome = "Luis 3",
                        DataNascimento = new DateTime(1971,12,17),
                        Email = "luis@mail.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        MoradaEntrega = expectedMorada,
                        MoradaFaturacao = expectedMorada,
                    },
                };

                unitOfWork.Clientes.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Clientes.GetAll();

                var nomeEsperado = "Nome Esperado";
                var telefoneAlterado = "111000111";

                var DateExpected = DateTime.Today;

                //act
                var firstElement = unitOfWork.Clientes.Get(guid);
                firstElement.Nome = nomeEsperado;
                unitOfWork.Clientes.Update(firstElement);
                unitOfWork.Complete();

                firstElement = unitOfWork.Clientes.Get(guid);
                var actualFirstUpdate = unitOfWork.Clientes.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.Telefone = telefoneAlterado;
                }
                unitOfWork.Clientes.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.Clientes.GetAll();


                var DateModifiedCount = actualSecondUpdate.Count(x =>
                       x.DataUltimaAlteracao.Value.Year == DateExpected.Year
                    && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
                    && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

                var ordensAlteradas = actualSecondUpdate.Count(x => x.Telefone == telefoneAlterado);

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(nomeEsperado, firstElement.Nome);
                Assert.AreEqual(3, ordensAlteradas);
                Assert.AreEqual(3, DateModifiedCount);

            }
        }


    }




}