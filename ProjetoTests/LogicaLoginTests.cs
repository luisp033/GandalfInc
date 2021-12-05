using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.BusinessLogicLayer.Tests
{
    [TestClass()]
    public class LogicaLoginTests
    {
        [TestMethod()]
        public void LoginComSucesso_Test()
        {
            //Teste with a Dabase in sqlite ...

            //arrange

            Utilizador expectedUtilizador = null;
            Loja expectedLoja = null;
            PontoDeVenda expectedPos = null;
            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = TipoUtilizador.Empregado,
                    Senha = "123"
                };
                unitOfWork.Utilizadores.Add(expectedUtilizador);

                expectedLoja = new Loja
                {
                    Nome = "Loja 1",
                };
                unitOfWork.Lojas.Add(expectedLoja);

                expectedPos = new PontoDeVenda
                {
                    Nome = "POS Teste1",
                    Loja = expectedLoja
                };
                unitOfWork.PontoDeVendas.Add(expectedPos);
                unitOfWork.Complete();
            }

            // Act
            var x = new LogicaLogin(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite));
            var posSessao = x.Login(expectedPos, expectedUtilizador);
            var DateLoginActual = new DateTime(posSessao.DataLogin.Year, posSessao.DataLogin.Month, posSessao.DataLogin.Day, 0, 0, 0);
            var DateLoginExpected = DateTime.Today;

            // Assert
            Assert.IsNotNull(posSessao);
            Assert.AreEqual(posSessao.Utilizador.Identificador, expectedUtilizador.Identificador);
            Assert.AreEqual(posSessao.PontoDeVenda.Identificador, expectedPos.Identificador);
            Assert.IsNull(posSessao.DataLogout);
            Assert.AreEqual(DateLoginExpected, DateLoginActual);

        }
    }
}