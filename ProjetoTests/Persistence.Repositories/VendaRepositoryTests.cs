using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Enumerados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.DataAccessLayer.Persistence.Repositories.Tests
{
    [TestClass()]
    public class VendaRepositoryTests
    {
        [TestMethod()]
        public void VendaRepository_InsertGetAndFindDBTest()
        {
            //Teste with a Dabase in sqlite ...
            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange

                var unitOfWork = new UnitOfWork(contexto);
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

                var actualVenda = unitOfWork.Vendas.GetAll();

                Assert.IsNotNull(actualVenda);
                Assert.AreEqual(1, actualVenda.Count());
                Assert.AreEqual(2, actualVenda.First().DetalheVendas.Count());
                Assert.IsNull(actualVenda.First().DataHoraVenda);
                Assert.AreEqual("Luis", actualVenda.First().Cliente.Nome);
                Assert.AreEqual((int)TipoPagamentoEnum.Dinheiro, actualVenda.First().TipoPagamento.Id);

            }
        }


        [TestMethod()]
        public void VendaRepository_AddRangeAndGetAllDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
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

                var expectedDetalhe1 = new List<DetalheVenda>
                {
                    new DetalheVenda{ 
                        Desconto = 0,
                        PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 0m,
                        Estoque = expectedEstoque1,
                    }
                };
                var expectedDetalhe2 = new List<DetalheVenda>
                {
                    new DetalheVenda{
                        Desconto = 50.0m,
                        PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 50.0m,
                        Estoque = expectedEstoque2,
                    }
                };
                unitOfWork.DetalheVendas.AddRange(expectedDetalhe1);
                unitOfWork.DetalheVendas.AddRange(expectedDetalhe2);

                unitOfWork.Complete();

                var tipoPagamentoDinheiro = unitOfWork.TipoPagamentos.Find(x => x.Id == (int)TipoPagamentoEnum.Dinheiro).FirstOrDefault();

                var expected = new List<Venda>() {
                    new Venda
                    {
                        PontoDeVendaSessao = expectedPontoDeVendaSessao,
                        Cliente = expectedCliente,
                        TipoPagamento = tipoPagamentoDinheiro,
                        DetalheVendas = expectedDetalhe1
                    },
                    new Venda
                    {
                        PontoDeVendaSessao = expectedPontoDeVendaSessao,
                        Cliente = expectedCliente,
                        TipoPagamento = tipoPagamentoDinheiro,
                        DetalheVendas = expectedDetalhe2
                    }
                };
                unitOfWork.Vendas.AddRange(expected);
                unitOfWork.Complete();

                //act
                var actual = unitOfWork.DetalheVendas.GetAll();

                //assert
                Assert.IsNotNull(actual);
                Assert.AreEqual(2, actual.Count());

            }
        }

        [TestMethod()]
        public void VendaRepository_RemoveDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();
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

                var expectedDetalhe1 = new List<DetalheVenda>
                {
                    new DetalheVenda{
                        Desconto = 0,
                        PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 0m,
                        Estoque = expectedEstoque1,
                    }
                };
                var expectedDetalhe2 = new List<DetalheVenda>
                {
                    new DetalheVenda{
                        Desconto = 50.0m,
                        PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 50.0m,
                        Estoque = expectedEstoque2,
                    }
                };
                unitOfWork.DetalheVendas.AddRange(expectedDetalhe1);
                unitOfWork.DetalheVendas.AddRange(expectedDetalhe2);

                unitOfWork.Complete();

                var tipoPagamentoDinheiro = unitOfWork.TipoPagamentos.Find(x => x.Id == (int)TipoPagamentoEnum.Dinheiro).FirstOrDefault();

                var expected = new List<Venda>() {
                    new Venda
                    {
                        Identificador = guid,
                        PontoDeVendaSessao = expectedPontoDeVendaSessao,
                        Cliente = expectedCliente,
                        TipoPagamento = tipoPagamentoDinheiro,
                        DetalheVendas = expectedDetalhe1
                    },
                    new Venda
                    {
                        PontoDeVendaSessao = expectedPontoDeVendaSessao,
                        Cliente = expectedCliente,
                        TipoPagamento = tipoPagamentoDinheiro,
                        DetalheVendas = expectedDetalhe2
                    }
                };
                unitOfWork.Vendas.AddRange(expected);
                unitOfWork.Complete();

                var actualBefore = unitOfWork.Vendas.GetAll();

                //act
                var firstElement = unitOfWork.Vendas.Get(guid);
                unitOfWork.Vendas.Remove(firstElement);
                unitOfWork.Complete();
                var actualFirstDelete = unitOfWork.Vendas.GetAll();

                unitOfWork.Vendas.RemoveRange(actualFirstDelete);
                unitOfWork.Complete();
                var actualSecondDelete = unitOfWork.Vendas.GetAll();

                //assert
                Assert.AreEqual(2, actualBefore.Count());
                Assert.AreEqual(1, actualFirstDelete.Count());
                Assert.AreEqual(0, actualSecondDelete.Count());

            }
        }

        [TestMethod()]
        public void VendaRepository_UpdateDBTest()
        {

            //Teste with a Dabase in sqlite ...

            using (var contexto = new DataAccessLayer.ProjetoDBContext(DataBaseType.Sqlite))
            {
                //arrange
                var unitOfWork = new UnitOfWork(contexto);
                var guid = Guid.NewGuid();
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

                var expectedDetalhe1 = new List<DetalheVenda>
                {
                    new DetalheVenda{
                        Desconto = 0,
                        PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 0m,
                        Estoque = expectedEstoque1,
                    }
                };
                var expectedDetalhe2 = new List<DetalheVenda>
                {
                    new DetalheVenda{
                        Desconto = 50.0m,
                        PrecoFinal = expectedEstoque2.Produto.PrecoUnitario - 50.0m,
                        Estoque = expectedEstoque2,
                    }
                };
                unitOfWork.DetalheVendas.AddRange(expectedDetalhe1);
                unitOfWork.DetalheVendas.AddRange(expectedDetalhe2);

                unitOfWork.Complete();

                var tipoPagamentoDinheiro = unitOfWork.TipoPagamentos.Find(x => x.Id == (int)TipoPagamentoEnum.Dinheiro).FirstOrDefault();

                var expected = new List<Venda>() {
                    new Venda
                    {
                        Identificador = guid,
                        PontoDeVendaSessao = expectedPontoDeVendaSessao,
                        Cliente = expectedCliente,
                        TipoPagamento = tipoPagamentoDinheiro,
                        DetalheVendas = expectedDetalhe1
                    },
                    new Venda
                    {
                        PontoDeVendaSessao = expectedPontoDeVendaSessao,
                        Cliente = expectedCliente,
                        TipoPagamento = tipoPagamentoDinheiro,
                        DetalheVendas = expectedDetalhe2
                    }
                };
                unitOfWork.Vendas.AddRange(expected);
                unitOfWork.Complete();

                var actualBefore = unitOfWork.Vendas.GetAll();

                var dadoEsperado = 88;

                //act
                var firstElement = unitOfWork.Vendas.Get(guid);
                firstElement.NumeroSerie = dadoEsperado;
                unitOfWork.Vendas.Update(firstElement);
                unitOfWork.Complete();

                firstElement = unitOfWork.Vendas.Get(guid);
                var actualFirstUpdate = unitOfWork.Vendas.GetAll();

                foreach (var item in actualFirstUpdate)
                {
                    item.NumeroSerie = dadoEsperado;
                }
                unitOfWork.Vendas.UpdateRange(actualFirstUpdate);
                unitOfWork.Complete();
                var actualSecondUpdate = unitOfWork.Vendas.GetAll();


                var dadosAlterados = actualSecondUpdate.Count(x => x.NumeroSerie == dadoEsperado);

                //assert
                Assert.AreEqual(2, actualBefore.Count());
                Assert.AreEqual(dadoEsperado, firstElement.NumeroSerie);
                Assert.AreEqual(2, dadosAlterados);


            }
        }


    }




}