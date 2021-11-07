using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Lib.Entidades.Produtos;
using Projeto.Lib.Faturacao;
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
        public void DeveImpedirVendaItemForaDoEstoqueTest()
        {
            //Arrange

            var estoque = new Estoque
            {
                Produtos = new List<Produto>()
            };

            var comp1 = new Produto { Nome = "Computador" };
            var comp2 = new Produto { Nome = "Computador" };
            var comp3 = new Produto { Nome = "Computador" };
            var comp4 = new Produto { Nome = "Computador" };
            var smart1 = new Produto { Nome = "Smartphone XIAOMI" };

            estoque.Produtos.Add(comp1);
            estoque.Produtos.Add(comp2);
            estoque.Produtos.Add(comp3);
            estoque.Produtos.Add(comp4);
            estoque.Produtos.Add(smart1);


            var vendaParaoJoao = new Venda();
            vendaParaoJoao.DetalheVendas = new List<DetalheVenda>();

            var detalheVenda = new DetalheVenda
            {
                Produto = comp1
            };
            var detalheVenda2 = new DetalheVenda
            {
                Produto = comp2
            };
            
            vendaParaoJoao.DetalheVendas.Add(detalheVenda);
            vendaParaoJoao.DetalheVendas.Add(detalheVenda2);

            //Act

            //var VendaPossivel = estoque.ValidarDisponibilidade()



            //Assert


        }



    }
}