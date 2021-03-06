using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer.Tests
{
    [TestClass()]
    public class LogicaSistemaTests
    {
        #region Estoques
        [TestMethod()]
        public void InsereEstoqueTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                var produtoTeste = logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var expectedQtd = 5;

                //Act
                var resultadoOK = logicaSistema.InsereEstoque(((Produto)produtoTeste.Objeto).Identificador, expectedQtd);
                var resultadoNOK = logicaSistema.InsereEstoque(((Produto)produtoTeste.Objeto).Identificador, 0);

                //Assert
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsTrue(resultadoOK.Objeto is List<Estoque>);
                Assert.AreEqual(expectedQtd, ((List<Estoque>)resultadoOK.Objeto).Count());
            }
        }

        [TestMethod()]
        public void ApagaEstoqueTest()
        {

            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                var produtoTeste = logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var inserted = logicaSistema.InsereEstoque(((Produto)produtoTeste.Objeto).Identificador, 1);
                var expectedId = ((List<Estoque>)inserted.Objeto)[0].Identificador;

                //Act
                var resultadoOK = logicaSistema.ApagaEstoque(expectedId);
                var resultadoNOK = logicaSistema.ApagaEstoque(expectedId);

                //Assert
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);

            }
        }

        [TestMethod()]
        public void ObtemEstoqueTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                var produtoTeste = logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var inserted = logicaSistema.InsereEstoque(((Produto)produtoTeste.Objeto).Identificador, 1);
                var expectedId = ((List<Estoque>)inserted.Objeto)[0].Identificador;

                //Act
                var actual = logicaSistema.ObtemEstoque(expectedId);
                var actualId = ((Estoque)actual.Objeto).Identificador;

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedId, actualId);

            }
        }

        [TestMethod()]
        public void AlteraEstoqueTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                var produtoTeste = logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var inserted = logicaSistema.InsereEstoque(((Produto)produtoTeste.Objeto).Identificador, 1);
                var expectedId = ((List<Estoque>)inserted.Objeto)[0].Identificador;

                //Act
                var resultadoOK = logicaSistema.AlteraEstoque(expectedId, "Serie Alterado");
                var resultadoNOK = logicaSistema.AlteraEstoque(Guid.NewGuid(), "Serie Alterado2");

                var actual = logicaSistema.ObtemEstoque(expectedId);

                //Assert
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.AreEqual("Serie Alterado", ((Estoque)actual.Objeto).NumeroSerie);
            }
        }

        [TestMethod()]
        public void GetAllEstoqueTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                var produtoTeste = logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var inserted = logicaSistema.InsereEstoque(((Produto)produtoTeste.Objeto).Identificador, 10);

                //Act
                var actual = logicaSistema.GetAllEstoque();

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(10, actual.Count());
            }
        }

        #endregion

        #region Produtos

        [TestMethod()]
        public void InsereProdutoTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid  expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;

                //Act
                var resultadoOK = logicaSistema.InsereProduto(expectedNome,expectedcategoria,expectedMarca,expectedEan,expectedPreco);
                var resultadoNOKSemNome = logicaSistema.InsereProduto(null, expectedcategoria, expectedMarca, expectedEan, expectedPreco);

                //Assert
                Assert.IsFalse(resultadoNOKSemNome.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsTrue(resultadoOK.Objeto is Produto);
            }
        }

        [TestMethod()]
        public void ApagaProdutoTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                var inserted = logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var expectedId = ((Produto)inserted.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.ApagaProduto(expectedId);
                var resultadoNOK = logicaSistema.ApagaProduto(expectedId);

                //Assert
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);

            }
        }

        [TestMethod()]
        public void ObtemProdutoTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                var inserted = logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var expectedId = ((Produto)inserted.Objeto).Identificador;

                //Act
                var actual = logicaSistema.ObtemProduto(expectedId);
                var actualId = ((Produto)actual.Objeto).Identificador;

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedId, actualId);

            }
        }

        [TestMethod()]
        public void ObtemProdutosPorCategoriaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria1 = logicaSistema.InsereCategoria("TV", 1);
                var categoria2 = logicaSistema.InsereCategoria("PHONE", 2);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria1 = ((CategoriaProduto)categoria1.Objeto).Identificador;
                Guid expectedcategoria2 = ((CategoriaProduto)categoria2.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                logicaSistema.InsereProduto(expectedNome, expectedcategoria1, expectedMarca, expectedEan, expectedPreco);
                logicaSistema.InsereProduto(expectedNome, expectedcategoria1, expectedMarca, expectedEan, expectedPreco);
                logicaSistema.InsereProduto(expectedNome, expectedcategoria2, expectedMarca, expectedEan, expectedPreco);

                //Act
                var actual = logicaSistema.ObtemProdutosPorCategoria(expectedcategoria1);

                //Assert
                Assert.AreEqual(2, ((List<Produto>)actual.Objeto).Count);
            }
        }

        [TestMethod()]
        public void AlteraProdutoTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                var inserted = logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var expectedId = ((Produto)inserted.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.AlteraProduto(expectedId, "Nome Alterado", expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var resultadoNOK = logicaSistema.AlteraProduto(Guid.NewGuid(), expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                var actual = logicaSistema.ObtemProduto(expectedId);

                //Assert
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.AreEqual("Nome Alterado", ((Produto)actual.Objeto).Nome);
            }
        }

        [TestMethod()]
        public void GetAllProdutoTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                var marca = logicaSistema.InsereMarca("Sony", "Japão");
                var categoria = logicaSistema.InsereCategoria("TV", 1);
                string expectedNome = "Produto Teste";
                Guid expectedcategoria = ((CategoriaProduto)categoria.Objeto).Identificador;
                Guid expectedMarca = ((MarcaProduto)marca.Objeto).Identificador;
                string expectedEan = "123";
                decimal expectedPreco = 9.9m;
                logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);
                logicaSistema.InsereProduto(expectedNome, expectedcategoria, expectedMarca, expectedEan, expectedPreco);

                //Act
                var actual = logicaSistema.GetAllProdutos();

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());
            }
        }

        #endregion

        #region Marcas

        [TestMethod()]
        public void InsereMarcaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                string expectedOrigem = "Oeste";

                //Act
                var resultadoOK = logicaSistema.InsereMarca(expectedNome, expectedOrigem);
                var resultadoNOKSemNome = logicaSistema.InsereMarca(null, expectedOrigem);

                //Assert
                Assert.IsFalse(resultadoNOKSemNome.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsTrue(resultadoOK.Objeto is MarcaProduto);
            }
        }

        [TestMethod()]
        public void ApagaMarcaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                string expectedOrigem = "Oeste";
                var inserted = logicaSistema.InsereMarca(expectedNome, expectedOrigem);
                var expectedId = ((MarcaProduto)inserted.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.ApagaMarca(expectedId);
                var resultadoNOK = logicaSistema.ApagaMarca(expectedId);

                //Assert
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);

            }
        }

        [TestMethod()]
        public void ObtemMarcaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                string expectedOrigem = "Oeste";
                var inserted = logicaSistema.InsereMarca(expectedNome, expectedOrigem);
                var expectedId = ((MarcaProduto)inserted.Objeto).Identificador;

                //Act
                var actual = logicaSistema.ObtemMarca(expectedId);
                var actualId = ((MarcaProduto)actual.Objeto).Identificador;

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedId, actualId);

            }
        }

        [TestMethod()]
        public void AlteraMarcaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                string expectedOrigem = "Oeste";
                var inserted = logicaSistema.InsereMarca(expectedNome, expectedOrigem);
                var expectedId = ((MarcaProduto)inserted.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.AlteraMarca(expectedId, "Nome Alterado", "Origem Alterada");
                var resultadoNOK = logicaSistema.AlteraMarca(Guid.NewGuid(), expectedNome, expectedOrigem);
                var actual = logicaSistema.ObtemMarca(expectedId);

                //Assert
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.AreEqual("Nome Alterado", ((MarcaProduto)actual.Objeto).Nome);
                Assert.AreEqual("Origem Alterada", ((MarcaProduto)actual.Objeto).Origem);
            }
        }

        [TestMethod()]
        public void GetAllMarcaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                string expectedOrigem = "Oeste";
                logicaSistema.InsereMarca(expectedNome, expectedOrigem);
                logicaSistema.InsereMarca(expectedNome, expectedOrigem);

                //Act
                var actual = logicaSistema.GetAllMarcas();

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());
            }
        }

        #endregion

        #region Categorias

        [TestMethod()]
        public void InsereCategoriaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                int expectedOrdem = 0;

                //Act
                var resultadoOK = logicaSistema.InsereCategoria(expectedNome, expectedOrdem);
                var resultadoNOKSemNome = logicaSistema.InsereCategoria(null, expectedOrdem);

                //Assert
                Assert.IsFalse(resultadoNOKSemNome.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsTrue(resultadoOK.Objeto is CategoriaProduto);
            }
        }

        [TestMethod()]
        public void ApagaCategoriaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                int expectedOrdem = 0;
                var inserted = logicaSistema.InsereCategoria(expectedNome, expectedOrdem);
                var expectedId = ((CategoriaProduto)inserted.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.ApagaCategoria(expectedId);
                var resultadoNOK = logicaSistema.ApagaCategoria(expectedId);

                //Assert
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);

            }
        }

        [TestMethod()]
        public void ObtemCategoriaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                int expectedOrdem = 0;
                var inserted = logicaSistema.InsereCategoria(expectedNome, expectedOrdem);
                var expectedId = ((CategoriaProduto)inserted.Objeto).Identificador;

                //Act
                var actual = logicaSistema.ObtemCategoria(expectedId);
                var actualId = ((CategoriaProduto)actual.Objeto).Identificador;

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedId, actualId);

            }
        }

        [TestMethod()]
        public void AlteraCategoriaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                int expectedOrdem = 0;
                var inserted = logicaSistema.InsereCategoria(expectedNome, expectedOrdem);
                var expectedId = ((CategoriaProduto)inserted.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.AlteraCategoria(expectedId, "Nome Alterado", 5);
                var resultadoNOK = logicaSistema.AlteraCategoria(Guid.NewGuid(), expectedNome, 0);
                var actual= logicaSistema.ObtemCategoria(expectedId);

                //Assert
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.AreEqual("Nome Alterado", ((CategoriaProduto)actual.Objeto).Nome);
                Assert.AreEqual(5, ((CategoriaProduto)actual.Objeto).OrdemApresentacao);
            }
        }

        [TestMethod()]
        public void GetAllCategoriaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Frutas";
                int expectedOrdem = 0;
                logicaSistema.InsereCategoria(expectedNome, expectedOrdem);
                logicaSistema.InsereCategoria(expectedNome, expectedOrdem);

                //Act
                var actual = logicaSistema.GetAllCategorias();

                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());
            }
        }

        #endregion

        #region Lojas

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
        public void ApagaLojaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Loja Teste";
                string expectedEmail = "Luis@mail.pt";
                string expectedTelefone = "960123456";
                string expectedNumeroFiscal = "123456789";
                Morada expectedMorada = new Morada() { Endereco = "Rua do la vai", CodigoPostal = "1234567", Localidade = "Lisboa" };
                var expectedLoja = logicaSistema.InsereLoja(expectedNome, expectedNumeroFiscal, expectedEmail, expectedTelefone, expectedMorada);
                var expectedId = ((Loja)expectedLoja.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.ApagaLoja(expectedId);
                var resultadoNOK = logicaSistema.ApagaLoja(expectedId);

                //Assert
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);

            }
        }

        [TestMethod()]
        public void ObtemLojaTest()
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
                var expectedLoja = logicaSistema.InsereLoja(expectedNome, expectedNumeroFiscal, expectedEmail, expectedTelefone, espectedMorada);
                var expectedId = ((Loja)expectedLoja.Objeto).Identificador;

                //Act
                var actualLoja = logicaSistema.ObtemLoja(expectedId);
                var actualId = ((Loja)actualLoja.Objeto).Identificador;


                //Assert
                Assert.IsNotNull(actualLoja);
                Assert.AreEqual(expectedId, actualId);

            }
        }

        [TestMethod()]
        public void AlteraLojaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Loja Teste";
                string expectedEmail = "Luis@mail.pt";
                string expectedTelefone = "960123456";
                string expectedNumeroFiscal = "123456789";
                Morada expectedMorada = new Morada() { Endereco = "Rua do la vai", CodigoPostal = "1234567", Localidade = "Lisboa" };
                var expectedLoja = logicaSistema.InsereLoja(expectedNome, expectedNumeroFiscal, expectedEmail, expectedTelefone, expectedMorada);
                var expectedId = ((Loja)expectedLoja.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.AlteraLoja(expectedId, "Loja Teste Alterado", expectedNumeroFiscal, expectedEmail, expectedTelefone, expectedMorada);
                var resultadoNOK = logicaSistema.AlteraLoja(Guid.NewGuid(), expectedNome, expectedNumeroFiscal, expectedEmail, expectedTelefone, expectedMorada);
                var actualLoja = logicaSistema.ObtemLoja(expectedId);

                //Assert
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.AreEqual("Loja Teste Alterado", ((Loja)actualLoja.Objeto).Nome);
            }
        }

        [TestMethod()]
        public void GetAllLojasTest()
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
                logicaSistema.InsereLoja(expectedNome, expectedNumeroFiscal, expectedEmail, expectedTelefone, espectedMorada);
                logicaSistema.InsereLoja(expectedNome, expectedNumeroFiscal, expectedEmail, expectedTelefone, espectedMorada);

                //Act
                var lojas = logicaSistema.GetAllLojas();

                //Assert
                Assert.IsNotNull(lojas);
                Assert.AreEqual(2, lojas.Count());
            }
        }
        #endregion

        #region Utilizadores

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
        public void ApagaUtilizadorTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Luis";
                string expectedEmail = "Luis@mail.pt";
                string expectedSenha = "456";

                var expectedUtilizador = logicaSistema.InsereUtilizador(expectedNome, expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);
                var expectedId = ((Utilizador)expectedUtilizador.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.ApagaUtilizador(expectedId);
                var resultadoNOK = logicaSistema.ApagaUtilizador(expectedId);

                //Assert
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);

            }
        }

        [TestMethod()]
        public void ObtemUtilizadorTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Luis";
                string expectedEmail = "Luis@mail.pt";
                string expectedSenha = "456";

                var expectedUtilizador = logicaSistema.InsereUtilizador(expectedNome, expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);
                var expectedId = ((Utilizador)expectedUtilizador.Objeto).Identificador;

                //Act
                var actualUtilizador = logicaSistema.ObtemUtilizador(expectedId);
                var actualId = ((Utilizador)actualUtilizador.Objeto).Identificador;


                //Assert
                Assert.IsNotNull(actualUtilizador);
                Assert.AreEqual(expectedId, actualId);

            }
        }

        [TestMethod()]
        public void AlteraUtilizadorTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Luis";
                string expectedEmail = "Luis@mail.pt";
                string expectedSenha = "456";

                var expectedUtilizador = logicaSistema.InsereUtilizador(expectedNome, expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);
                var expectedId = ((Utilizador)expectedUtilizador.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.AlteraUtilizador(expectedId, "Utilizador Teste Alterado", expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);
                var resultadoNOK = logicaSistema.AlteraUtilizador(Guid.NewGuid(), "Utilizador Teste Alterado", expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);
                var actualUtilizador = logicaSistema.ObtemUtilizador(expectedId);

                //Assert
                Assert.IsTrue(resultadoOK.Sucesso);
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.AreEqual("Utilizador Teste Alterado", ((Utilizador)actualUtilizador.Objeto).Nome);
            }
        }

        [TestMethod()]
        public void GetAllUtilizadoresTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Luis";
                string expectedEmail = "Luis@mail.pt";
                string expectedSenha = "456";

                var expectedUtilizador1 = logicaSistema.InsereUtilizador(expectedNome, expectedEmail, expectedSenha, TipoUtilizadorEnum.Gerente);
                var expectedUtilizador2 = logicaSistema.InsereUtilizador(expectedNome, expectedEmail, expectedSenha, TipoUtilizadorEnum.Empregado);

                //Act
                var utilizadores = logicaSistema.GetAllUtilizadores();

                //Assert
                Assert.IsNotNull(utilizadores);
                Assert.AreEqual(2, utilizadores.Count());
            }
        }

        #endregion

        #region Logins
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

                var expectedLoja = logicaSistema.InsereLoja("Loja 1", null, null, null, null);
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
        #endregion

        #region Pontos de Venda

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
        public void GetAllPontoDeVendasTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Pos Teste";
                var expectedLojaCreated = logicaSistema.InsereLoja("Loja do teste", null, null, null, null);

                //Act
                logicaSistema.InserePontoDeVenda(expectedNome, (Loja)expectedLojaCreated.Objeto);
                logicaSistema.InserePontoDeVenda(expectedNome, (Loja)expectedLojaCreated.Objeto);

                //Act
                var pontoDeVendas = logicaSistema.GetAllPontoDeVendas();

                //Assert
                Assert.IsNotNull(pontoDeVendas);
                Assert.AreEqual(2, pontoDeVendas.Count());
            }
        }

        [TestMethod()]
        public void ObtemPontoDeVendaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Pos Teste";
                var expectedLojaCreated = logicaSistema.InsereLoja("Loja do teste", null, null, null, null);
                var expectedPontoDeVenda = logicaSistema.InserePontoDeVenda(expectedNome, (Loja)expectedLojaCreated.Objeto);
                var expectedId = ((PontoDeVenda)expectedPontoDeVenda.Objeto).Identificador;

                //Act
                var actual = logicaSistema.ObtemPontoDeVenda(expectedId);
                var actualId = ((PontoDeVenda)actual.Objeto).Identificador;


                //Assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(expectedId, actualId);

            }
        }

        [TestMethod()]
        public void AlteraPontoDeVendaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Pos Teste";
                var expectedLojaCreated = logicaSistema.InsereLoja("Loja do teste", null, null, null, null);
                var expectedPontoDeVenda = logicaSistema.InserePontoDeVenda(expectedNome, (Loja)expectedLojaCreated.Objeto);
                var expectedId = ((PontoDeVenda)expectedPontoDeVenda.Objeto).Identificador;

                //Act
                var actualOK = logicaSistema.AlteraPontoDeVenda(expectedId, "POS Teste Alterado", ((Loja)expectedLojaCreated.Objeto).Identificador);
                var actualNOK = logicaSistema.AlteraPontoDeVenda(Guid.NewGuid(), "Erro", ((Loja)expectedLojaCreated.Objeto).Identificador);
                var actualUtilizador = logicaSistema.ObtemUtilizador(expectedId);

                //Assert
                Assert.IsTrue(actualOK.Sucesso);
                Assert.IsFalse(actualNOK.Sucesso);
                Assert.AreEqual("POS Teste Alterado", ((PontoDeVenda)actualOK.Objeto).Nome);
            }
        }

        [TestMethod()]
        public void ApagaPontoDeVendaTest()
        {
            using (var contexto = new ProjetoDBContext(DataBaseType.Sqlite))
            {
                //Arrange
                var logicaSistema = new LogicaSistema(contexto);

                string expectedNome = "Pos Teste";
                var expectedLojaCreated = logicaSistema.InsereLoja("Loja do teste", null, null, null, null);
                var expectedPontoDeVenda = logicaSistema.InserePontoDeVenda(expectedNome, (Loja)expectedLojaCreated.Objeto);
                var expectedId = ((PontoDeVenda)expectedPontoDeVenda.Objeto).Identificador;

                //Act
                var resultadoOK = logicaSistema.ApagaPontoDeVenda(expectedId);
                var resultadoNOK = logicaSistema.ApagaPontoDeVenda(expectedId);

                //Assert
                Assert.IsFalse(resultadoNOK.Sucesso);
                Assert.IsTrue(resultadoOK.Sucesso);

            }
        }

        #endregion

    }
}