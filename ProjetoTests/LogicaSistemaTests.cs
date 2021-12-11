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
        public void LogoutComSucesso_Test()
        {
            //Teste with a Dabase in sqlite ...

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var logicaSistema = new LogicaSistema(contexto);

                var expectedLoja = logicaSistema.InsereLoja("Loja 1", null, null, null, null);
                var expectedPos1 = logicaSistema.InserePontoDeVenda("POS 1", (Loja)expectedLoja.Objeto);
                var expectedUtilizador1 = logicaSistema.InsereUtilizador("Luis1", "luis1@mail.pt", "123", TipoUtilizadorEnum.Empregado);
                var expectedUtilizadorLogado1 = logicaSistema.Login("luis1@mail.pt", "123");
                var expectedSessaoMessage = "Sessão fechada com sucesso";
                var aberturadeSessao = logicaSistema.AberturaSessao((Utilizador)expectedUtilizadorLogado1.Objeto, (PontoDeVenda)expectedPos1.Objeto);

                //Act
                var fechoSessao = logicaSistema.Logout((PontoDeVendaSessao)aberturadeSessao.Objeto);

                //Assert
                Assert.IsNotNull(fechoSessao);
                Assert.AreEqual(expectedSessaoMessage, fechoSessao.Mensagem);
            }
        }

        [TestMethod()]
        public void LogoutComFalha_Test()
        {
            //Teste with a Dabase in sqlite ...

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var logicaSistema = new LogicaSistema(contexto);

                var expectedLoja = logicaSistema.InsereLoja("Loja 1", null, null, null, null);
                var expectedPos1 = logicaSistema.InserePontoDeVenda("POS 1", (Loja)expectedLoja.Objeto);
                var expectedUtilizador1 = logicaSistema.InsereUtilizador("Luis1", "luis1@mail.pt", "123", TipoUtilizadorEnum.Empregado);
                var expectedUtilizadorLogado1 = logicaSistema.Login("luis1@mail.pt", "123");
                var expectedSessaoMessage = "Sessão inválida ou inexistente";
                var aberturadeSessao = logicaSistema.AberturaSessao((Utilizador)expectedUtilizadorLogado1.Objeto, (PontoDeVenda)expectedPos1.Objeto);
                var fechoSessao = logicaSistema.Logout((PontoDeVendaSessao)aberturadeSessao.Objeto);

                //Act
                var atualFechoSessao = logicaSistema.Logout((PontoDeVendaSessao)aberturadeSessao.Objeto);

                //Assert
                Assert.IsNotNull(atualFechoSessao);
                Assert.AreEqual(expectedSessaoMessage, atualFechoSessao.Mensagem);
            }
        }

        [TestMethod()]
        public void VerificaSeExistePeloMenosUmUtilizadorGerenteTest()
        {

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {

                //Arrange
                var unitOfWork = new UnitOfWork(contexto);

                var utilizadorEmpregado = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();
                var utilizadorGerente = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Gerente).First();

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

        [TestMethod()]
        public void InsereLojaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Loja Teste";
                string expectedEmail = "Luis@mail.pt";
                string expectedTelefone = "960123456";
                string expectedNumeroFiscal = "123456789";
                Morada espectedMorada = new Morada() { Endereco = "Rua do la vai", CodigoPostal = "1234567", Localidade = "Lisboa" };

                //Act
                var resultadoNOKSemNome = logicaSistema.InsereLoja(null, expectedNumeroFiscal, expectedEmail, expectedTelefone, espectedMorada);
                var resultadoOKSoComNome = logicaSistema.InsereLoja(expectedNome, null, null, null, null);
                var resultadoOK = logicaSistema.InsereLoja(expectedNome, expectedNumeroFiscal, expectedEmail, expectedTelefone, espectedMorada);

                //Assert
                Assert.IsFalse(resultadoNOKSemNome.Sucesso);
                Assert.IsTrue(resultadoOKSoComNome.Sucesso);
                Assert.IsTrue(resultadoOKSoComNome.Objeto is Loja);
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsTrue(resultadoOK.Objeto is Loja);

            }
        }

        [TestMethod()]
        public void InserePontoDeVendaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Pos Teste";
                var expectedLojaCreated = logicaSistema.InsereLoja("Loja do teste", null, null, null, null);

                //Act
                var resultadoNOKSemNome = logicaSistema.InserePontoDeVenda(null, (Loja)expectedLojaCreated.Objeto);
                var resultadoOKSemLoja = logicaSistema.InserePontoDeVenda(expectedNome, null);
                var resultadoOK = logicaSistema.InserePontoDeVenda(expectedNome, (Loja)expectedLojaCreated.Objeto);

                //Assert
                Assert.IsFalse(resultadoNOKSemNome.Sucesso);
                Assert.IsFalse(resultadoOKSemLoja.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsTrue(resultadoOK.Objeto is PontoDeVenda);

            }
        }

        [TestMethod()]
        public void LoginCredenciaisErradasTest()
        {

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                //Act
                var expectedErroSemDados = logicaSistema.Login("luis@mail.pt", "888");
                var resultadoOK = logicaSistema.InsereUtilizador("Luis", "luis@mail.pt", "456", TipoUtilizadorEnum.Gerente);
                var expectedErroComDados1 = logicaSistema.Login("luis@mail.pt", "888");
                var expectedErroComDados2 = logicaSistema.Login("xxx@mail.pt", "456");
                var expectedErroComDados3 = logicaSistema.Login("", "");
                var expectedErroComDados4 = logicaSistema.Login(email: null, senha: null);

                //Assert
                Assert.IsFalse(expectedErroSemDados.Sucesso);
                Assert.IsFalse(expectedErroComDados1.Sucesso);
                Assert.IsFalse(expectedErroComDados2.Sucesso);
                Assert.IsFalse(expectedErroComDados3.Sucesso);
                Assert.IsFalse(expectedErroComDados4.Sucesso);
            }
        }

        [TestMethod()]
        public void LoginCredenciaisCorrectasGerente()
        {

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);
                logicaSistema.InsereUtilizador("Luis1", "luis1@mail.pt", "123", TipoUtilizadorEnum.Gerente);
                logicaSistema.InsereUtilizador("Luis2", "luis2@mail.pt", "456", TipoUtilizadorEnum.Empregado);

                //Act

                var expectedgerente = logicaSistema.Login("luis1@mail.pt", "123");

                //Assert
                Assert.IsNotNull(expectedgerente);
                Assert.IsTrue(expectedgerente.Objeto is Utilizador);
                Assert.AreEqual("Luis1", ((Utilizador)expectedgerente.Objeto).Nome);
            }
        }

        [TestMethod()]
        public void LoginCredenciaisCorrectasEmpregadoSemPOSAberto()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var expectedLoja = logicaSistema.InsereLoja("Loja 1",null,null,null,null);
                var expectedPos = logicaSistema.InserePontoDeVenda("POS 1", (Loja)expectedLoja.Objeto);
                var expectedUtilizador = logicaSistema.InsereUtilizador("Luis1", "luis1@mail.pt", "123", TipoUtilizadorEnum.Empregado);
                var expectedUtilizadorLogado = logicaSistema.Login("luis1@mail.pt", "123");
                var expectedSessaoMessage = $"Ponto de Venda: {((PontoDeVenda)expectedPos.Objeto).Nome} logado com sucesso pelo utilizador: {((Utilizador)expectedUtilizadorLogado.Objeto).Nome}.";

                //Act
                var actualSessaoNova = logicaSistema.AberturaSessao((Utilizador)expectedUtilizadorLogado.Objeto, (PontoDeVenda)expectedPos.Objeto);

                //Assert
                Assert.IsNotNull(actualSessaoNova);
                Assert.AreEqual(expectedSessaoMessage, actualSessaoNova.Mensagem);
            }
        }

        [TestMethod()]
        public void LoginCredenciaisCorrectasEmpregadoComoMesmoPOSAberto()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var expectedLoja = logicaSistema.InsereLoja("Loja 1", null, null, null, null);
                var expectedPos = logicaSistema.InserePontoDeVenda("POS 1", (Loja)expectedLoja.Objeto);
                var expectedUtilizador = logicaSistema.InsereUtilizador("Luis1", "luis1@mail.pt", "123", TipoUtilizadorEnum.Empregado);
                var expectedUtilizadorLogado = logicaSistema.Login("luis1@mail.pt", "123");
                var expectedSessaoMessage = "Continuação da sessão que já estava aberta neste ponto de venda para este utilizador.";

                //Act
                var primeiraAberturadeSessao = logicaSistema.AberturaSessao((Utilizador)expectedUtilizadorLogado.Objeto, (PontoDeVenda)expectedPos.Objeto);
                var mesmaSessaoJaAberta = logicaSistema.AberturaSessao((Utilizador)expectedUtilizadorLogado.Objeto, (PontoDeVenda)expectedPos.Objeto);

                //Assert
                Assert.IsNotNull(primeiraAberturadeSessao);
                Assert.IsNotNull(mesmaSessaoJaAberta);
                Assert.AreEqual(expectedSessaoMessage, mesmaSessaoJaAberta.Mensagem);

            }
        }
 
        [TestMethod()]
        public void LoginCredenciaisCorrectasEmpregadoComoOutorPOSAberto()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var expectedLoja = logicaSistema.InsereLoja("Loja 1", null, null, null, null);
                var expectedPos1 = logicaSistema.InserePontoDeVenda("POS 1", (Loja)expectedLoja.Objeto);
                var expectedPos2 = logicaSistema.InserePontoDeVenda("POS 2", (Loja)expectedLoja.Objeto);
                var expectedUtilizador = logicaSistema.InsereUtilizador("Luis1", "luis1@mail.pt", "123", TipoUtilizadorEnum.Empregado);
                var expectedUtilizadorLogado = logicaSistema.Login("luis1@mail.pt", "123");
                var expectedSessaoMessage = $"O utilizador tem uma sessão aberta no Ponto de venda {((PontoDeVenda)expectedPos1.Objeto).Nome}";

                //Act
                var primeiraAberturadeSessao = logicaSistema.AberturaSessao((Utilizador)expectedUtilizadorLogado.Objeto, (PontoDeVenda)expectedPos1.Objeto);
                var tentativaDeAberturaNoutroPos = logicaSistema.AberturaSessao((Utilizador)expectedUtilizadorLogado.Objeto, (PontoDeVenda)expectedPos2.Objeto);

                //Assert
                Assert.IsNotNull(primeiraAberturadeSessao);
                Assert.IsNotNull(tentativaDeAberturaNoutroPos);
                Assert.AreEqual(expectedSessaoMessage, tentativaDeAberturaNoutroPos.Mensagem);

            }
        }

        [TestMethod()]
        public void LoginCredenciaisCorrectasEmpregadoComoOutorPOSAbertoPorOutroUtilizador()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var expectedLoja = logicaSistema.InsereLoja("Loja 1", null, null, null, null);
                var expectedPos1 = logicaSistema.InserePontoDeVenda("POS 1", (Loja)expectedLoja.Objeto);
                var expectedUtilizador1 = logicaSistema.InsereUtilizador("Luis1", "luis1@mail.pt", "123", TipoUtilizadorEnum.Empregado);
                var expectedUtilizador2 = logicaSistema.InsereUtilizador("Luis2", "luis2@mail.pt", "456", TipoUtilizadorEnum.Empregado);
                var expectedUtilizadorLogado1 = logicaSistema.Login("luis1@mail.pt", "123");
                var expectedUtilizadorLogado2 = logicaSistema.Login("luis2@mail.pt", "456");
                var expectedSessaoMessage = $"Este ponto de venda tem uma sessão aberta para o utilizador {((Utilizador)expectedUtilizadorLogado1.Objeto).Nome}";

                //Act
                var primeiraAberturadeSessao = logicaSistema.AberturaSessao((Utilizador)expectedUtilizadorLogado1.Objeto, (PontoDeVenda)expectedPos1.Objeto);
                var tentativaDeAberturaPorOutroUtilizador = logicaSistema.AberturaSessao((Utilizador)expectedUtilizadorLogado2.Objeto, (PontoDeVenda)expectedPos1.Objeto);

                //Assert
                Assert.IsNotNull(primeiraAberturadeSessao);
                Assert.IsNotNull(tentativaDeAberturaPorOutroUtilizador);
                Assert.AreEqual(expectedSessaoMessage, tentativaDeAberturaPorOutroUtilizador.Mensagem);

            }
        }
    }
}