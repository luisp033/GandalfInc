using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Lib.Entidades;
using Projeto.Lib.Faturacao;
using Projeto.Lib.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Lib.Faturacao.Tests
{
    [TestClass()]
    public class VendaTests
    {
        [TestMethod()]
        public void DeveIniciarNovaVendaComDadosDoPontoDeVendaEVendedorTest()
        {
            //Arrange
            RepositorioUtilizador utilizadores = new RepositorioUtilizador();
            RepositorioLoja lojas = new RepositorioLoja(utilizadores);
            RepositorioPontoDeVenda pontosDeVenda = new RepositorioPontoDeVenda(utilizadores, lojas);
            RepositorioCategoriaProduto categorias = new RepositorioCategoriaProduto();
            RepositorioMarcaProduto marcas = new RepositorioMarcaProduto();
            RepositorioProduto produtos = new RepositorioProduto(categorias,marcas);
            RepositorioEstoque estoques = new RepositorioEstoque(produtos);
            Venda venda = new Venda(estoques, produtos);

            var pontoDeVendaSelecionado = pontosDeVenda.ObterPorNomeActivo("POS 1");
            var utilizadorLogado = pontosDeVenda.Login(pontoDeVendaSelecionado,"elsa@mail.pt","1234");

            //Act
            var novaVenda = venda.IniciarNovaVenda(pontoDeVendaSelecionado, utilizadorLogado);

            //Assert
            Assert.IsNotNull(utilizadorLogado);
            Assert.IsNotNull(novaVenda);
            Assert.AreEqual(utilizadorLogado.Email,venda.Vendedor.Email);
            Assert.AreEqual(pontoDeVendaSelecionado.Identificador, venda.PontoDeVenda.Identificador);

            System.Diagnostics.Debug.WriteLine(venda);
        }

        [TestMethod()]
        public void AdicionarProdutosAVenda()
        {
            //Arrange
            RepositorioUtilizador utilizadores = new RepositorioUtilizador();
            RepositorioLoja lojas = new RepositorioLoja(utilizadores);
            RepositorioPontoDeVenda pontosDeVenda = new RepositorioPontoDeVenda(utilizadores, lojas);
            RepositorioCategoriaProduto categorias = new RepositorioCategoriaProduto();
            RepositorioMarcaProduto marcas = new RepositorioMarcaProduto();
            RepositorioProduto produtos = new RepositorioProduto(categorias, marcas);
            RepositorioEstoque estoques = new RepositorioEstoque(produtos);
            Venda venda = new Venda(estoques, produtos);

            var pontoDeVendaSelecionado = pontosDeVenda.ObterPorNomeActivo("POS 1");
            var utilizadorLogado = pontosDeVenda.Login(pontoDeVendaSelecionado, "elsa@mail.pt", "1234");
            var novaVenda = venda.IniciarNovaVenda(pontoDeVendaSelecionado, utilizadorLogado);

            //Act
            venda.AdicionarProduto("10001", 2);
            venda.AdicionarProduto("10001", 2);

            var produtosAdicionados = venda.DetalheVenda.Count;

            //Assert
            Assert.AreEqual(4,produtosAdicionados);

            System.Diagnostics.Debug.WriteLine(venda);
        }

        [TestMethod()]
        public void RemoverProdutodaVenda()
        {
            //Arrange
            RepositorioUtilizador utilizadores = new RepositorioUtilizador();
            RepositorioLoja lojas = new RepositorioLoja(utilizadores);
            RepositorioPontoDeVenda pontosDeVenda = new RepositorioPontoDeVenda(utilizadores, lojas);
            RepositorioCategoriaProduto categorias = new RepositorioCategoriaProduto();
            RepositorioMarcaProduto marcas = new RepositorioMarcaProduto();
            RepositorioProduto produtos = new RepositorioProduto(categorias, marcas);
            RepositorioEstoque estoques = new RepositorioEstoque(produtos);
            Venda venda = new Venda(estoques, produtos);

            var pontoDeVendaSelecionado = pontosDeVenda.ObterPorNomeActivo("POS 1");
            var utilizadorLogado = pontosDeVenda.Login(pontoDeVendaSelecionado, "elsa@mail.pt", "1234");
            var novaVenda = venda.IniciarNovaVenda(pontoDeVendaSelecionado, utilizadorLogado);

            //Act
            venda.AdicionarProduto("10001", 2);
            var primeiroProduto = venda.DetalheVenda.FirstOrDefault();
            venda.RemoverProduto(primeiroProduto.EstoqueIdentificador);
            var produtosNaVendaDetalhe = venda.DetalheVenda.Count;
            var estoqueLiberto = estoques.ObterPorIdentificador(primeiroProduto.EstoqueIdentificador).DataVenda == null;

            //Assert
            Assert.AreEqual(1,produtosNaVendaDetalhe);
            Assert.IsTrue(estoqueLiberto);


            System.Diagnostics.Debug.WriteLine(venda);
        }

        [TestMethod()]
        public void DeveLimparELibertarTodosOsItemsDeUmaCompra()
        {
            //Arrange
            RepositorioUtilizador utilizadores = new RepositorioUtilizador();
            RepositorioLoja lojas = new RepositorioLoja(utilizadores);
            RepositorioPontoDeVenda pontosDeVenda = new RepositorioPontoDeVenda(utilizadores, lojas);
            RepositorioCategoriaProduto categorias = new RepositorioCategoriaProduto();
            RepositorioMarcaProduto marcas = new RepositorioMarcaProduto();
            RepositorioProduto produtos = new RepositorioProduto(categorias, marcas);
            RepositorioEstoque estoques = new RepositorioEstoque(produtos);
            Venda venda = new Venda(estoques, produtos);

            var pontoDeVendaSelecionado = pontosDeVenda.ObterPorNomeActivo("POS 1");
            var utilizadorLogado = pontosDeVenda.Login(pontoDeVendaSelecionado, "elsa@mail.pt", "1234");
            var novaVenda = venda.IniciarNovaVenda(pontoDeVendaSelecionado, utilizadorLogado);

            //Act
            venda.AdicionarProduto("10001", 2);
            venda.CancelarVenda();
            var produtosNaVendaDetalhe = venda.DetalheVenda.Count;
            var estoqueLiberto = estoques.ObterListaPorEan("10001")?.Count(x=>x.DataVenda == null);

            //Assert
            Assert.AreEqual(0,produtosNaVendaDetalhe);
            Assert.AreEqual(5,estoqueLiberto);


            System.Diagnostics.Debug.WriteLine(venda);
        }
    }
}