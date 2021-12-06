using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Faturacao;
using Projeto.DataAccessLayer.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.BusinessLogicLayer
{
    public class LogicaVenda
    {
        private readonly Venda venda = null;
        private readonly RepositorioEstoque repositorioEstoque;
        private readonly RepositorioProduto repositorioProduto;

        public LogicaVenda(Venda venda,RepositorioEstoque repoEstoque, RepositorioProduto repoProduto)
        {
            this.venda = venda;
        }

        //public Guid IniciarNovaVenda(PontoDeVenda pontodeVenda, Utilizador vendedor)
        //{

        //    venda.PontoDeVenda = pontodeVenda;
        //    venda.Vendedor = vendedor;

        //    return venda.Identificador;
        //}

        public string AdicionarProduto(string ean, int quantidade)
        {
            //if (venda.TipoPagamento.HasValue)
            //{
            //    return $"Compra já terminada não pode efetuar operações";
            //}

            //// Bloquear produto do estoque
            //var listaEstoque = repositorioEstoque.BloquearProdutosDoEstoqueParaVenda(ean, quantidade, venda.Identificador);

            //if (listaEstoque == null)
            //{
            //    return $"Produto(s) não disponíveis para venda";
            //}

            //// Adicionar a nossa lista de detalhe
            //var produto = repositorioProduto.ObterPorEan(listaEstoque[0].Ean);
            //var desconto = 0;
            //foreach (var item in listaEstoque)
            //{
            //    venda.DetalheVenda.Add(new DetalheVenda() { Desconto = 0.0m, Produto = produto, PrecoFinal = produto.PrecoUnitario - desconto, NumeroSerie = item.NumeroSerie, EstoqueIdentificador = item.Identificador });
            //}
            return $"Produto(s) adicionado(s) com sucesso";
        }

        public string RemoverProduto(Guid estoqueIdentificador)
        {
            //if (venda.TipoPagamento.HasValue)
            //{
            //    return $"Compra já terminada não pode efetuar operações";
            //}

            //var produtoParaApagar = venda.DetalheVenda.FirstOrDefault(x => x.EstoqueIdentificador == estoqueIdentificador);

            //if (produtoParaApagar == null)
            //{
            //    return $"Produto não disponível para remoção";
            //}

            //// Desbloquear produto do estoque
            //var remocaoComSucesso = repositorioEstoque.DesbloquearProdutosDoEstoqueEmVenda(produtoParaApagar.EstoqueIdentificador);

            //if (!remocaoComSucesso)
            //{
            //    return $"Não foi possível remover o produto - Contacte IT";
            //}

            //venda.DetalheVenda.Remove(produtoParaApagar);

            return $"Produto(s) removido(s) com sucesso";
        }
        public string CancelarVenda()
        {
            //if (venda.TipoPagamento.HasValue)
            //{
            //    return $"Compra já terminada não pode efetuar operações";
            //}

            //var listaParaApagar = venda.DetalheVenda.Select(x => x.EstoqueIdentificador).ToList();
            //foreach (var item in listaParaApagar)
            //{
            //    RemoverProduto(item);
            //}
            return $"Todos os produtos foram removidos";
        }

        public string FinalizarPagamento(TipoPagamento tipoPagamento, Cliente cliente)
        {
            //if (venda.TipoPagamento.HasValue)
            //{
            //    return $"Compra já terminada não pode efetuar operações";
            //}

            //venda.Cliente = cliente;
            //venda.TipoPagamento = tipoPagamento;
            //venda.ValorPagamento = venda.DetalheVenda.Sum(x => x.PrecoFinal);
            //venda.DataHoraVenda = DateTime.Now;
            //repositorioEstoque.DesativarProdutosPagos(venda.Identificador);

            return $"Compra efetuada com sucesso";
        }

        //public void GerarRecibo()
        //{
        //    var sb = new StringBuilder();
        //    sb.AppendLine($"Fatura Recibo FRD {venda.DataHoraVenda.Value.Year}/{venda.NumeroSerie}");
        //    sb.AppendLine($"Loja: {venda.PontoDeVenda.Loja.NumeroFiscal} - Ponto de Venda: {venda.PontoDeVenda.Identificador} ");
        //    sb.AppendLine($"Loja: {venda.PontoDeVenda.Loja.Morada}");
        //    sb.AppendLine($"Vendedor: {venda.Vendedor.Nome} Identificador: {venda.Vendedor.Identificador} ");
        //    sb.AppendLine($"Data da Fatura/Recibo: {venda.DataHoraVenda} ");
        //    sb.AppendLine($"Tipo Pagamento: {venda.TipoPagamento} ");
        //    sb.AppendLine($"Valor Pagamento: {venda.ValorPagamento} ");
        //    foreach (var item in venda.DetalheVenda)
        //    {
        //        sb.AppendLine($"Id: {item.EstoqueIdentificador} Nome: {item.Produto.Nome}  - Valor Unitario: {item.Produto.PrecoUnitario} -  Desconto : {item.Desconto} - PrecoFinal : {item.PrecoFinal}");
        //    }

        //    Console.WriteLine(sb);

        //    //Poderíamos escrever usando a notação LINQ
        //    //DetalheVenda.Produtos.Select(x => sb.AppendLine($"Nome: {x.Nome}  - Valor Unitario: {x.PrecoUnitario} - Número de Série: {x.NumeroSerie}"))
        //}

    }
}
