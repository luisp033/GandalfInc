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
    public class UtilizadorRepositoryTests
    {
        [TestMethod()]
        public void UtilizadorRepository_InsertDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange
                var expected = new Utilizador
                {
                    Identificador = Guid.NewGuid(),
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = TipoUtilizador.Empregado,
                    Senha = "123"
                };
                var DateExpected = DateTime.Today;

                //act
                unitOfWork.Utilizadores.Add(expected);
                unitOfWork.Complete();
                var actual = unitOfWork.Utilizadores.Find(x => x.Nome == "User Teste").FirstOrDefault();
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
        public void UtilizadorRepository_GetDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                var guid = Guid.NewGuid();

                //arrange
                var expected = new Utilizador
                {
                    Identificador = guid,
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = TipoUtilizador.Indefinido,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.Utilizadores.Get(guid);

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expected.Identificador, actual.Identificador);

            }
        }

        [TestMethod()]
        public void UtilizadorRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange
                var expected = new List<Utilizador>() {
                    new Utilizador{
                        Identificador = Guid.NewGuid(),
                        Nome = "User Teste",
                        Email = "email@teste.pt",
                        Tipo = TipoUtilizador.Indefinido,
                        Senha = "123"
                    },
                    new Utilizador{
                        Identificador = Guid.NewGuid(),
                        Nome = "User Teste 2",
                        Email = "email2@teste.pt",
                        Tipo = TipoUtilizador.Gerente,
                        Senha = "123"
                    },
                };
                unitOfWork.Utilizadores.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.Utilizadores.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void UtilizadorRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                var guid = Guid.NewGuid();
                //arrange
                var expected = new List<Utilizador>() {
                    new Utilizador{
                        Identificador = guid,
                        Nome = "User Teste",
                        Email = "email@teste.pt",
                        Tipo = TipoUtilizador.Indefinido,
                        Senha = "123"
                    },
                    new Utilizador{
                        Identificador = Guid.NewGuid(),
                        Nome = "User Teste 2",
                        Email = "email2@teste.pt",
                        Tipo = TipoUtilizador.Gerente,
                        Senha = "123"
                    },
                    new Utilizador{
                        Identificador = Guid.NewGuid(),
                        Nome = "User Teste 2",
                        Email = "email2@teste.pt",
                        Tipo = TipoUtilizador.Gerente,
                        Senha = "123"
                    },
                };

                unitOfWork.Utilizadores.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Utilizadores.GetAll();

                //act
                var firstElement = unitOfWork.Utilizadores.Get(guid);
                unitOfWork.Utilizadores.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.Utilizadores.GetAll();

                unitOfWork.Utilizadores.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.Utilizadores.GetAll();

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());

            }
        }

        [TestMethod()]
        public void UtilizadorRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                var guid = Guid.NewGuid();
                //arrange
                var expected = new List<Utilizador>() {
                    new Utilizador{
                        Identificador = guid,
                        Nome = "User Teste",
                        Email = "email@teste.pt",
                        Tipo = TipoUtilizador.Indefinido,
                        Senha = "123"
                    },
                    new Utilizador{
                        Identificador = Guid.NewGuid(),
                        Nome = "User Teste 2",
                        Email = "email2@teste.pt",
                        Tipo = TipoUtilizador.Gerente,
                        Senha = "123"
                    },
                    new Utilizador{
                        Identificador = Guid.NewGuid(),
                        Nome = "User Teste 2",
                        Email = "email2@teste.pt",
                        Tipo = TipoUtilizador.Gerente,
                        Senha = "123"
                    },
                };

                unitOfWork.Utilizadores.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Utilizadores.GetAll();

                var nomeEsperado = "Nome Esperado";
                var senhaAlterada = "Senha Alterada";

                var DateExpected = DateTime.Today;

                //act
                var firstElement = unitOfWork.Utilizadores.Get(guid);
                firstElement.Nome = nomeEsperado;
                unitOfWork.Utilizadores.Update(firstElement);
                unitOfWork.Complete();
                firstElement = unitOfWork.Utilizadores.Get(guid);
                var actualFirstUpdate = unitOfWork.Utilizadores.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.Senha = senhaAlterada;
                }
                unitOfWork.Utilizadores.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.Utilizadores.GetAll();

                
                var DateModifiedCount = actualSecondUpdate.Count(x=>
                       x.DataUltimaAlteracao.Value.Year == DateExpected.Year 
                    && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
                    && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

                var passAlteradas = actualSecondUpdate.Count(x=>x.Senha == senhaAlterada);

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(nomeEsperado, firstElement.Nome);
                Assert.AreEqual(3, passAlteradas);
                Assert.AreEqual(3, DateModifiedCount);

            }
        }


    }




}