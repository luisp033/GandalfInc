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
    public class LogicaSistemaTests
    {
        [TestMethod()]
        public void LoginComSucesso_Test()
        {
            //Teste with a Dabase in sqlite ...

            //arrange

            Utilizador expectedUtilizador = null;
            Loja expectedLoja = null;
            PontoDeVenda expectedPos = null;
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);

                var tipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.Id == (int)TipoUtilizadorEnum.Empregado).First();

                expectedUtilizador = new Utilizador
                {
                    Nome = "User Teste",
                    Email = "email@teste.pt",
                    Tipo = tipoUtilizador,
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

                // Act
                var x = new LogicaSistema(contexto);
                var posSessao = x.Login(expectedPos, expectedUtilizador);
                var DateLoginActual = new DateTime(posSessao.Item1.DataLogin.Year, posSessao.Item1.DataLogin.Month, posSessao.Item1.DataLogin.Day, 0, 0, 0);
                var DateLoginExpected = DateTime.Today;

                // Assert
                Assert.IsNotNull(posSessao.Item1);
                Assert.AreEqual(posSessao.Item1.Utilizador.Identificador, expectedUtilizador.Identificador);
                Assert.AreEqual(posSessao.Item1.PontoDeVenda.Identificador, expectedPos.Identificador);
                Assert.IsNull(posSessao.Item1.DataLogout);
                Assert.AreEqual(DateLoginExpected, DateLoginActual);

            }
        }

        [TestMethod()]
        public void LogoutComSucesso_Test()
        {
            //Teste with a Dabase in sqlite ...

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.Id == (int)TipoUtilizadorEnum.Empregado).First();

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
                unitOfWork.Complete();

                var x = new LogicaSistema(contexto);
                var expectSessao = x.Login(expectedPos, expectedUtilizador);

                // Act
                var msgActual = x.Logout(expectSessao.Item1);

                var actualSessao = unitOfWork.PontoDeVendaSessoes.Find(x => x.Identificador == expectSessao.Item1.Identificador).FirstOrDefault();
                var dataLogoutExpected = new DateTime(actualSessao.DataLogout.Value.Year, actualSessao.DataLogout.Value.Month, actualSessao.DataLogout.Value.Day);

                //Assert
                Assert.IsNull(msgActual);
                Assert.AreEqual(DateTime.Today, dataLogoutExpected);

            }


        }

        [TestMethod()]
        public void LoginFalhadoPostoJaEstaAberto_Test()
        {
            //Teste with a Dabase in sqlite ...

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);

                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.Id == (int)TipoUtilizadorEnum.Empregado).First();

                var utilizadores = new List<Utilizador>{
                    new Utilizador
                    {
                        Nome = "User Teste1",
                        Email = "email@teste.pt",
                        Tipo = expectedTipoUtilizador,
                        Senha = "123"
                    },
                    new Utilizador
                    {
                        Nome = "User Teste2",
                        Email = "email@teste.pt",
                        Tipo = expectedTipoUtilizador,
                        Senha = "123"
                    }
                };
                unitOfWork.Utilizadores.AddRange(utilizadores);

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
                unitOfWork.Complete();

                var x = new LogicaSistema(contexto);
                var otherSessao = x.Login(expectedPos, utilizadores[0]);


                // Act
                var actualSessao = x.Login(expectedPos, utilizadores[1]);

                //Assert
                Assert.IsNull(actualSessao.Item1);
                Assert.AreEqual("Ponto de Venda já se encontra aberto", actualSessao.Item2);

            }
        }

        [TestMethod()]
        public void LoginFalhadoUtilizadorJaEstaLogado_Test()
        {
            //Teste with a Dabase in sqlite ...

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.Id == (int)TipoUtilizadorEnum.Empregado).First();

                var utilizadores = new List<Utilizador>{
                    new Utilizador
                    {
                        Nome = "User Teste1",
                        Email = "email@teste.pt",
                        Tipo = expectedTipoUtilizador,
                        Senha = "123"
                    },
                    new Utilizador
                    {
                        Nome = "User Teste2",
                        Email = "email@teste.pt",
                        Tipo = expectedTipoUtilizador,
                        Senha = "123"
                    }
                };
                unitOfWork.Utilizadores.AddRange(utilizadores);

                var expectedLoja = new Loja
                {
                    Nome = "Loja 1",
                };
                unitOfWork.Lojas.Add(expectedLoja);

                var posList = new List<PontoDeVenda>
                {
                    new PontoDeVenda{
                        Nome = "POS Teste1",
                        Loja = expectedLoja
                    },
                    new PontoDeVenda{
                        Nome = "POS Teste2",
                        Loja = expectedLoja
                    }
                };
                unitOfWork.PontoDeVendas.AddRange(posList);
                unitOfWork.Complete();

                var x = new LogicaSistema(contexto);
                var otherSessao = x.Login(posList[0], utilizadores[0]);

                // Act
                var actualSessao1 = x.Login(posList[0], utilizadores[0]);
                var actualSessao2 = x.Login(posList[1], utilizadores[0]);

                //Assert
                Assert.IsNull(actualSessao1.Item1);
                Assert.IsNull(actualSessao2.Item1);
                Assert.AreEqual("Utilizador já tem uma sessão aberta", actualSessao1.Item2);
                Assert.AreEqual("Utilizador já tem uma sessão aberta", actualSessao2.Item2);

            }
        }

        [TestMethod()]
        public void VerificaSeExistePeloMenosUmUtilizadorGerenteTest()
        {

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {

                //Arrange
                var unitOfWork = new UnitOfWork(contexto);

                var utilizadorEmpregado = unitOfWork.TipoUtilizadores.Find(x => x.Id == (int)TipoUtilizadorEnum.Empregado).First();
                var utilizadorGerente = unitOfWork.TipoUtilizadores.Find(x => x.Id == (int)TipoUtilizadorEnum.Gerente).First();

                var logicaSistema = new LogicaSistema(contexto);

                //Act

                var expectedNotFound1 = logicaSistema.VerificaSeExisteUmUtilizadorGerente();
                var utilizadores = new List<Utilizador>{
                    new Utilizador
                    {
                        Nome = "User Teste1",
                        Email = "email@teste.pt",
                        Tipo = utilizadorEmpregado,
                        Senha = "123"
                    },

                };
                unitOfWork.Utilizadores.AddRange(utilizadores);
                unitOfWork.Complete();
                var expectedNotFound2 = logicaSistema.VerificaSeExisteUmUtilizadorGerente();

                var utilizadores2 = new List<Utilizador>{
                    new Utilizador
                    {
                        Nome = "User Teste1",
                        Email = "email@teste.pt",
                        Tipo = utilizadorGerente,
                        Senha = "123"
                    },

                };
                unitOfWork.Utilizadores.AddRange(utilizadores2);
                unitOfWork.Complete();
                var expectedFound = logicaSistema.VerificaSeExisteUmUtilizadorGerente();


                //Assert
                Assert.IsFalse(expectedNotFound1);
                Assert.IsFalse(expectedNotFound2);
                Assert.IsTrue(expectedFound);

            }


        }

        [TestMethod()]
        public void InsereUtilizadorTest()
        {

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Luis";
                string expectedEmail = "Luis@mail.pt";
                string expectedSenha = "456";

                var resultadoNOKSemNome = logicaSistema.InsereUtilizador(null, expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);
                var resultadoNOKSemEmail = logicaSistema.InsereUtilizador(null, expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);
                var resultadoNOKSemSenha = logicaSistema.InsereUtilizador(null, expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);
                var resultadoOK = logicaSistema.InsereUtilizador(expectedNome, expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);

                //Assert
                Assert.IsFalse(resultadoNOKSemNome.Sucesso);
                Assert.IsFalse(resultadoNOKSemEmail.Sucesso);
                Assert.IsFalse(resultadoNOKSemSenha.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);

            }
        }
    }
}