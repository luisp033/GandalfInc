using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class MoaradaRepositoryTests
    {
        [TestMethod()]
        public void MoradaRepository_InsertDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {

                //arrange
                var id = Guid.NewGuid();
                var expected = new Morada
                {
                    Identificador = id,
                    Endereco = "Rua do Teste 1",
                    Localidade = "Odivelas",
                    CodigoPostal = "1000999",
                    Observacoes = "Observacoes para teste"
                };
                var DateExpected = DateTime.Today;

                //act
                unitOfWork.Moradas.Add(expected);
                unitOfWork.Complete();
                var actual = unitOfWork.Moradas.Get(id);
                var actualFind = unitOfWork.Moradas.Find(x=>x.Identificador == id).FirstOrDefault();
                var DateActual = new DateTime(actual.DataCriacao.Value.Year, actual.DataCriacao.Value.Month, actual.DataCriacao.Value.Day, 0, 0, 0);

                //assert
                Assert.IsNotNull(actual);
                Assert.IsNull(actual.DataUltimaAlteracao);
                Assert.AreEqual(DateExpected, DateActual);
                Assert.IsTrue(actual.Ativo);
                Assert.AreEqual(expected.Identificador, actual.Identificador);
                Assert.AreEqual(expected.Endereco, actualFind.Endereco);
            }
        }

        [TestMethod()]
        public void MoradaRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange

                var expected = new List<Morada>() {
                    new Morada
                    {
                        //Identificador = Guid.NewGuid(),
                        Endereco = "Rua do Teste 1",
                        Localidade = "Odivelas",
                        CodigoPostal = "1000999",
                        Observacoes = "Observacoes para teste"
                    },
                    new Morada
                    {
                        Identificador = Guid.NewGuid(),
                        Endereco = "Rua do Teste 1",
                        Localidade = "Odivelas",
                        CodigoPostal = "1000999",
                        Observacoes = "Observacoes para teste"
                    },
                };
                unitOfWork.Moradas.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.Moradas.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void MoradaRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange


                var guid = Guid.NewGuid();
                var expected = new List<Morada>() {

                    new Morada
                    {
                        Identificador = guid,
                        Endereco = "Rua do Teste 1",
                        Localidade = "Odivelas",
                        CodigoPostal = "1000999",
                        Observacoes = "Observacoes para teste"
                    },
                    new Morada
                    {
                        Endereco = "Rua do Teste 1",
                        Localidade = "Odivelas",
                        CodigoPostal = "1000999",
                        Observacoes = "Observacoes para teste"
                    },
                    new Morada
                    {
                        Identificador = Guid.NewGuid(),
                        Endereco = "Rua do Teste 1",
                        Localidade = "Odivelas",
                        CodigoPostal = "1000999",
                        Observacoes = "Observacoes para teste"
                    },

                };

                unitOfWork.Moradas.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Moradas.GetAll();

                //act
                var firstElement = unitOfWork.Moradas.Get(guid);
                unitOfWork.Moradas.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.Moradas.GetAll();

                unitOfWork.Moradas.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.Moradas.GetAll();


                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());
            }
        }

        [TestMethod()]
        public void MoradaRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange

                var guid = Guid.NewGuid();
                var expected = new List<Morada>() {
                    new Morada
                    {
                        Identificador = guid,
                        Endereco = "Rua do Teste 1",
                        Localidade = "Odivelas",
                        CodigoPostal = "1000999",
                        Observacoes = "Observacoes para teste"
                    },
                    new Morada
                    {
                        Endereco = "Rua do Teste 1",
                        Localidade = "Odivelas",
                        CodigoPostal = "1000999",
                        Observacoes = "Observacoes para teste"
                    },
                    new Morada
                    {
                        Endereco = "Rua do Teste 1",
                        Localidade = "Odivelas",
                        CodigoPostal = "1000999",
                        Observacoes = "Observacoes para teste"
                    },

                };

                unitOfWork.Moradas.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Moradas.GetAll();

                var enderecoEsperado = "Rua do Teste 2";
                var dadoAlterado = "9999000";

                var DateExpected = DateTime.Today;

                //act
                var firstElement = unitOfWork.Moradas.Get(guid);
                firstElement.Endereco = enderecoEsperado;

                unitOfWork.Moradas.Update(firstElement);
                unitOfWork.Complete();
                firstElement = unitOfWork.Moradas.Get(guid);
                var actualFirstUpdate = unitOfWork.Moradas.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.CodigoPostal = dadoAlterado;
                }
                unitOfWork.Moradas.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.Moradas.GetAll();


                var DateModifiedCount = actualSecondUpdate.Count(x =>
                       x.DataUltimaAlteracao.Value.Year == DateExpected.Year
                    && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
                    && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

                var dadosAlterados = actualSecondUpdate.Count(x => x.CodigoPostal == dadoAlterado);

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(enderecoEsperado, firstElement.Endereco);
                Assert.AreEqual(3, dadosAlterados);
                Assert.AreEqual(3, DateModifiedCount);

            }
        }


    }
}