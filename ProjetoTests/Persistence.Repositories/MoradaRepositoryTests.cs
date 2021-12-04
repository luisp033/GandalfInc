using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        //[TestMethod()]
        //public void LojaRepository_AddRangeAndGetAllDBTest()
        //{

        //    //Teste with a Dabase in sqlite ...

        //    using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
        //    {
        //        //arrange

        //        var expectedUtilizador = new Utilizador
        //        {
        //            Identificador = Guid.NewGuid(),
        //            Nome = "User Teste",
        //            Email = "email@teste.pt",
        //            Tipo = TipoUtilizador.Empregado,
        //            Senha = "123"
        //        };
        //        unitOfWork.Utilizadores.Add(expectedUtilizador);

        //        var expected = new List<Loja>() {
        //            new Loja
        //            {
        //                Identificador = Guid.NewGuid(),
        //                Nome = "Loja Teste",
        //                Email = "loja@teste.pt",
        //                Telefone = "123456789",
        //                NumeroFiscal = "123456789",
        //                Responsavel = expectedUtilizador,
        //            },
        //            new Loja
        //            {
        //                Identificador = Guid.NewGuid(),
        //                Nome = "Loja Teste",
        //                Email = "loja@teste.pt",
        //                Telefone = "123456789",
        //                NumeroFiscal = "123456789",
        //                Responsavel = expectedUtilizador,
        //            },
        //        };
        //        unitOfWork.Lojas.AddRange(expected);
        //        unitOfWork.Complete();

        //        //act
        //        var actual = unitOfWork.Lojas.GetAll();

        //        var actualResponsaveis = actual.Count(x => x.Responsavel.Nome == "User Teste");

        //        //assert
        //        Assert.IsNotNull(actual);
        //        Assert.AreEqual(2, actual.Count());
        //        Assert.AreEqual(2, actualResponsaveis);

        //    }
        //}

        //[TestMethod()]
        //public void LojaRepository_RemoveDBTest()
        //{

        //    //Teste with a Dabase in sqlite ...

        //    using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
        //    {
        //        //arrange

        //        var expectedUtilizador = new Utilizador
        //        {
        //            Identificador = Guid.NewGuid(),
        //            Nome = "User Teste",
        //            Email = "email@teste.pt",
        //            Tipo = TipoUtilizador.Empregado,
        //            Senha = "123"
        //        };
        //        unitOfWork.Utilizadores.Add(expectedUtilizador);

        //        var guid = Guid.NewGuid();
        //        var expected = new List<Loja>() {

        //            new Loja
        //            {
        //                Identificador = guid,
        //                Nome = "Loja Teste 1",
        //                Email = "loja1@teste.pt",
        //                Telefone = "123456789",
        //                NumeroFiscal = "123456789",
        //                Responsavel = expectedUtilizador,
        //            },
        //            new Loja
        //            {
        //                Identificador = Guid.NewGuid(),
        //                Nome = "Loja Teste 2",
        //                Email = "loja2@teste.pt",
        //                Telefone = "123456789",
        //                NumeroFiscal = "123456789",
        //                Responsavel = expectedUtilizador,
        //            },
        //            new Loja
        //            {
        //                Identificador = Guid.NewGuid(),
        //                Nome = "Loja Teste 3",
        //                Email = "loja3@teste.pt",
        //                Telefone = "123456789",
        //                NumeroFiscal = "123456789",
        //                Responsavel = expectedUtilizador,
        //            },
                    
        //        };

        //        unitOfWork.Lojas.AddRange(expected);
        //        unitOfWork.Complete();
        //        var actualBefore = unitOfWork.Lojas.GetAll();

        //        //act
        //        var firstElement = unitOfWork.Lojas.Get(guid);
        //        unitOfWork.Lojas.Remove(firstElement);
        //        unitOfWork.Complete();
        //        var actualFirstDelete = unitOfWork.Lojas.GetAll();

        //        unitOfWork.Lojas.RemoveRange(actualFirstDelete);
        //        unitOfWork.Complete();
        //        var actualSecondDelete = unitOfWork.Lojas.GetAll();

        //        var utilizadorActual = unitOfWork.Utilizadores.GetAll();

        //        //assert
        //        Assert.AreEqual(3, actualBefore.Count());
        //        Assert.AreEqual(2, actualFirstDelete.Count());
        //        Assert.AreEqual(0, actualSecondDelete.Count());
        //        Assert.AreEqual(1, utilizadorActual.Count());
        //    }
        //}

        //[TestMethod()]
        //public void LojaRepository_UpdateDBTest()
        //{

        //    //Teste with a Dabase in sqlite ...

        //    using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
        //    {
        //        //arrange

        //        var expectedUtilizador = new Utilizador
        //        {
        //            Identificador = Guid.NewGuid(),
        //            Nome = "User Teste",
        //            Email = "email@teste.pt",
        //            Tipo = TipoUtilizador.Empregado,
        //            Senha = "123"
        //        };
        //        unitOfWork.Utilizadores.Add(expectedUtilizador);

        //        var guid = Guid.NewGuid();
        //        var expected = new List<Loja>() {
        //            new Loja
        //            {
        //                Identificador = guid,
        //                Nome = "Loja Teste 1",
        //                Email = "loja1@teste.pt",
        //                Telefone = "123456789",
        //                NumeroFiscal = "123456789",
        //                Responsavel = expectedUtilizador,
        //            },
        //            new Loja
        //            {
        //                Identificador = Guid.NewGuid(),
        //                Nome = "Loja Teste 2",
        //                Email = "loja2@teste.pt",
        //                Telefone = "123456789",
        //                NumeroFiscal = "123456789",
        //                Responsavel = expectedUtilizador,
        //            },
        //            new Loja
        //            {
        //                Identificador = Guid.NewGuid(),
        //                Nome = "Loja Teste 3",
        //                Email = "loja3@teste.pt",
        //                Telefone = "123456789",
        //                NumeroFiscal = "123456789",
        //                Responsavel = expectedUtilizador,
        //            },

        //        };

        //        unitOfWork.Lojas.AddRange(expected);
        //        unitOfWork.Complete();
        //        var actualBefore = unitOfWork.Lojas.GetAll();

        //        var nomeEsperado = "Nome Esperado";
        //        var dadoAlterado = "000000000";

        //        var DateExpected = DateTime.Today;

        //        //act
        //        var firstElement = unitOfWork.Lojas.Get(guid);
        //        firstElement.Nome = nomeEsperado;
        //        firstElement.Responsavel = null;
        //        unitOfWork.Lojas.Update(firstElement);
        //        unitOfWork.Complete();
        //        firstElement = unitOfWork.Lojas.Get(guid);
        //        var actualFirstUpdate = unitOfWork.Lojas.GetAll();

        //        foreach (var item in actualFirstUpdate)
        //        {
        //            item.NumeroFiscal = dadoAlterado;
        //        }
        //        unitOfWork.Lojas.UpdateRange(actualFirstUpdate);
        //        unitOfWork.Complete();
        //        var actualSecondUpdate = unitOfWork.Lojas.GetAll();


        //        var DateModifiedCount = actualSecondUpdate.Count(x =>
        //               x.DataUltimaAlteracao.Value.Year == DateExpected.Year
        //            && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
        //            && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

        //        var dadosAlterados = actualSecondUpdate.Count(x => x.NumeroFiscal == dadoAlterado);
        //        var actualResponsaveisCount = actualSecondUpdate.Count(x => x.Responsavel?.Nome == "User Teste");

        //        //assert
        //        Assert.AreEqual(3, actualBefore.Count());
        //        Assert.AreEqual(nomeEsperado, firstElement.Nome);
        //        Assert.AreEqual(3, dadosAlterados);
        //        Assert.AreEqual(3, DateModifiedCount);
        //        Assert.AreEqual(2, actualResponsaveisCount);

        //    }
        //}


    }
}