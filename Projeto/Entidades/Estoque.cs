using Projeto.Lib.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.Lib.Entidades
{
    public class Estoque : Entidade
    {

        private readonly RepositorioProduto repositorioProduto;
        public Estoque(RepositorioProduto repoProduto)
        {
            repositorioProduto = repoProduto;
        }

        public string Ean { get; set; }
        public DateTime DataEntrada { get; set; }
        public DateTime? DataVenda { get; set; }
        public Guid? IdentificadorVenda { get; set; }


        //public List<Produto> Produtos { get; set; }

        //public bool ValidarDisponibilidade(Dictionary<Produto, int> quantidadeItens) 
        //{

        //    foreach (var item in quantidadeItens.Keys)
        //    {

        //        var quantidadeEmEstoque = Produtos.Where(x => x.Nome == item.Nome).Count();
        //        var valorSolicitado = 0;

        //        quantidadeItens.TryGetValue(item, out valorSolicitado);

        //        if (valorSolicitado > quantidadeEmEstoque)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;

        //}

        public override string ToString()
        {
            var nomeProduto = repositorioProduto.ObterPorEan(Ean)?.Nome;
            return $"Ean: {Ean} - DataEntrada: {DataEntrada} - Data Saida:{DataVenda} - Venda: {IdentificadorVenda} - Nome Produto: {nomeProduto} ";
        }

    }
}
