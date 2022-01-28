using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class LojaRepositoryTests
    {
        [TestMethod()]
        public void LojaRepository_InsertDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {

                //arrange
                var id = Guid.NewGuid();
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Identificador = Guid.NewGuid(),
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

                var expected = new Loja
                {
                    Identificador = id,
                    Nome = "Loja Teste",
                    Email = "loja@teste.pt",
                    Telefone = "123456789",
                    NumeroFiscal = "123456789",
                    Responsavel = expectedUtilizador,
                    Morada = expectedMorada

                };
                var DateExpected = DateTime.Today;

                //act
                unitOfWork.Lojas.Add(expected);
                unitOfWork.Complete();
                var actual = unitOfWork.Lojas.Get(id);
                var actualFind = unitOfWork.Lojas.Find(x => x.Identificador == id).FirstOrDefault();
                var DateActual = new DateTime(actual.DataCriacao.Value.Year, actual.DataCriacao.Value.Month, actual.DataCriacao.Value.Day, 0, 0, 0);

                //assert
                Assert.IsNotNull(actual);
                Assert.IsNull(actual.DataUltimaAlteracao);
                Assert.AreEqual(DateExpected, DateActual);
                Assert.IsTrue(actual.Ativo);
                Assert.AreEqual(expected.Identificador, actual.Identificador);
                Assert.AreEqual(expected.Nome, actualFind.Nome);
                Assert.AreEqual(expected.Responsavel.Nome, expectedUtilizador.Nome);
                Assert.AreEqual(expected.Morada.Endereco, expectedMorada.Endereco);

            }
        }

        [TestMethod()]
        public void LojaRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Identificador = Guid.NewGuid(),
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

                var expected = new List<Loja>() {
                    new Loja
                    {
                        Identificador = Guid.NewGuid(),
                        Nome = "Loja Teste",
                        Email = "loja@teste.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        Responsavel = expectedUtilizador,
                        Morada = expectedMorada,
                    },
                    new Loja
                    {
                        Identificador = Guid.NewGuid(),
                        Nome = "Loja Teste",
                        Email = "loja@teste.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        Responsavel = expectedUtilizador,
                    },
                };
                unitOfWork.Lojas.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.Lojas.GetAll();

                var actualResponsaveis = actual.Count(x => x.Responsavel.Nome == "User Teste");
                var actualMoradas = actual.Count(x => x.Morada?.Endereco == expectedMorada.Endereco);

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());
                Assert.AreEqual(2, actualResponsaveis);
                Assert.AreEqual(1, actualMoradas);
            }
        }

        [TestMethod()]
        public void LojaRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Identificador = Guid.NewGuid(),
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


                var guid = Guid.NewGuid();
                var expected = new List<Loja>() {

                    new Loja
                    {
                        Identificador = guid,
                        Nome = "Loja Teste 1",
                        Email = "loja1@teste.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        Responsavel = expectedUtilizador,
                        Morada = expectedMorada,
                    },
                    new Loja
                    {
                        Identificador = Guid.NewGuid(),
                        Nome = "Loja Teste 2",
                        Email = "loja2@teste.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        Responsavel = expectedUtilizador,
                        Morada = expectedMorada,
                    },
                    new Loja
                    {
                        Identificador = Guid.NewGuid(),
                        Nome = "Loja Teste 3",
                        Email = "loja3@teste.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        Responsavel = expectedUtilizador,
                        Morada = expectedMorada,
                    },

                };

                unitOfWork.Lojas.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Lojas.GetAll();

                //act
                var firstElement = unitOfWork.Lojas.Get(guid);
                unitOfWork.Lojas.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.Lojas.GetAll();

                unitOfWork.Lojas.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.Lojas.GetAll();

                var utilizadorActual = unitOfWork.Utilizadores.GetAll();
                var moradaActual = unitOfWork.Moradas.GetAll();


                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(2, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());
                Assert.AreEqual(1, utilizadorActual.Count());
                Assert.AreEqual(1, moradaActual.Count());
            }
        }

        [TestMethod()]
        public void LojaRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var unitOfWork = new UnitOfWork(new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite)))
            {
                //arrange
                var expectedTipoUtilizador = unitOfWork.TipoUtilizadores.Find(x => x.TipoId == (int)TipoUtilizadorEnum.Empregado).First();

                var expectedUtilizador = new Utilizador
                {
                    Identificador = Guid.NewGuid(),
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

                var guid = Guid.NewGuid();
                var expected = new List<Loja>() {
                    new Loja
                    {
                        Identificador = guid,
                        Nome = "Loja Teste 1",
                        Email = "loja1@teste.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        Responsavel = expectedUtilizador,
                        Morada = expectedMorada,
                    },
                    new Loja
                    {
                        Identificador = Guid.NewGuid(),
                        Nome = "Loja Teste 2",
                        Email = "loja2@teste.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        Responsavel = expectedUtilizador,
                        Morada = expectedMorada,
                    },
                    new Loja
                    {
                        Identificador = Guid.NewGuid(),
                        Nome = "Loja Teste 3",
                        Email = "loja3@teste.pt",
                        Telefone = "123456789",
                        NumeroFiscal = "123456789",
                        Responsavel = expectedUtilizador,
                        Morada = expectedMorada,
                    },

                };

                unitOfWork.Lojas.AddRange(expected);
                unitOfWork.Complete();
                var actualBefore = unitOfWork.Lojas.GetAll();

                var nomeEsperado = "Nome Esperado";
                var dadoAlterado = "000000000";

                var DateExpected = DateTime.Today;

                //act
                var firstElement = unitOfWork.Lojas.Get(guid);
                firstElement.Nome = nomeEsperado;
                firstElement.Responsavel = null;
                firstElement.Morada = null;
                unitOfWork.Lojas.Update(firstElement);
                unitOfWork.Complete();
                firstElement = unitOfWork.Lojas.Get(guid);
                var actualFirstUpdate = unitOfWork.Lojas.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.NumeroFiscal = dadoAlterado;
                }
                unitOfWork.Lojas.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.Lojas.GetAll();


                var DateModifiedCount = actualSecondUpdate.Count(x =>
                       x.DataUltimaAlteracao.Value.Year == DateExpected.Year
                    && x.DataUltimaAlteracao.Value.Month == DateExpected.Month
                    && x.DataUltimaAlteracao.Value.Day == DateExpected.Day);

                var dadosAlterados = actualSecondUpdate.Count(x => x.NumeroFiscal == dadoAlterado);
                var actualResponsaveisCount = actualSecondUpdate.Count(x => x.Responsavel?.Nome == expectedUtilizador.Nome);
                var actualMoradasCount = actualSecondUpdate.Count(x => x.Morada?.Endereco == expectedMorada.Endereco);

                //assert
                Assert.AreEqual(3, actualBefore.Count());
                Assert.AreEqual(nomeEsperado, firstElement.Nome);
                Assert.AreEqual(3, dadosAlterados);
                Assert.AreEqual(3, DateModifiedCount);
                Assert.AreEqual(2, actualResponsaveisCount);
                Assert.AreEqual(2, actualMoradasCount);

            }
        }

        [TestMethod()]
        public void LojaRepository_GetAllLojasComPontosDeVendaTest()
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

                var expectedPontoDeVendaSessao = new PontoDeVendaSessao
                {
                    Utilizador = expectedUtilizador,
                    PontoDeVenda = expectedPos,
                    DataLogin = DateTime.Today
                };
                var DateExpected = DateTime.Today;
                unitOfWork.PontoDeVendaSessoes.Add(expectedPontoDeVendaSessao);

                var expectedMorada = new Morada
                {
                    Endereco = "Rua do Teste 1",
                    Localidade = "Odivelas",
                    CodigoPostal = "1000999",
                    Observacoes = "Observacoes para teste"
                };
                unitOfWork.Moradas.Add(expectedMorada);

                var expectedCliente = new Cliente
                {
                    Nome = "Luis",
                    DataNascimento = new DateTime(1971, 12, 17),
                    Email = "luis@mail.pt",
                    Telefone = "123456789",
                    NumeroFiscal = "123456789",
                    MoradaEntrega = expectedMorada,
                    MoradaFaturacao = expectedMorada,
                };
                unitOfWork.Clientes.Add(expectedCliente);

                var expectedCategoria = new CategoriaProduto
                {
                    Nome = "SmartPhones"
                };
                unitOfWork.CategoriaProdutos.Add(expectedCategoria);

                var expectedMarca = new MarcaProduto
                {
                    Nome = "Apple",
                    Origem = "USA"
                };
                unitOfWork.MarcaProdutos.Add(expectedMarca);

                var expectedProduto = new Produto
                {
                    Nome = "Iphone 1",
                    Categoria = expectedCategoria,
                    Marca = expectedMarca,
                    PrecoUnitario = 100,
                    Ean = "GTR5373529-56",
                };
                unitOfWork.Produtos.Add(expectedProduto);

                var expectedEstoque1 = new Estoque
                {
                    NumeroSerie = "SN/1",
                    Produto = expectedProduto,
                    DataEntrada = DateTime.Today,
                };
                unitOfWork.Estoques.Add(expectedEstoque1);
                var expectedEstoque2 = new Estoque
                {
                    NumeroSerie = "SN/2",
                    Produto = expectedProduto,
                    DataEntrada = DateTime.Today,

                };
                unitOfWork.Estoques.Add(expectedEstoque2);

                var expectedDetalhe1 = new DetalheVenda
                {
                    Desconto = 0,
                    PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 0m,
                    Estoque = expectedEstoque1,
                };
                var expectedDetalhe2 = new DetalheVenda
                {
                    Desconto = 50.0m,
                    PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 50.0m,
                    Estoque = expectedEstoque2,
                };

                unitOfWork.DetalheVendas.Add(expectedDetalhe1);
                unitOfWork.DetalheVendas.Add(expectedDetalhe2);

                unitOfWork.Complete();

                var expectedDetalheVendas = unitOfWork.DetalheVendas.GetAll();
                var tipoPagamentoDinheiro = unitOfWork.TipoPagamentos.Find(x => x.Id == (int)TipoPagamentoEnum.Dinheiro).FirstOrDefault();

                var expectedVenda = new Venda
                {
                    PontoDeVendaSessao = expectedPontoDeVendaSessao,
                    Cliente = expectedCliente,
                    TipoPagamento = tipoPagamentoDinheiro,
                    DetalheVendas = expectedDetalheVendas.ToList()

                };
                unitOfWork.Vendas.Add(expectedVenda);
                unitOfWork.Complete();

                var listaExpected = unitOfWork.Lojas.GetAllLojasComPontosDeVenda();

                Assert.AreEqual(1, listaExpected.Count());


            }
        }
    }
}