using Projeto.Lib.Entidades;
using Projeto.Lib.Infraestrutura;
using Projeto.Lib.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projeto.Lib.Faturacao
{
    public class Venda //: IImpressora
    {
        private readonly RepositorioEstoque repositorioEstoque;
        private readonly RepositorioProduto repositorioProduto;

        public Venda(RepositorioEstoque repoEstoque, RepositorioProduto repoProduto)
        {
            Identificador = Guid.NewGuid();
            repositorioEstoque = repoEstoque;
            repositorioProduto = repoProduto;
        }

        public Guid Identificador { get; set; }
        public PontoDeVenda PontoDeVenda { get; set; }
        public Utilizador Vendedor { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime? DataHoraVenda { get; set; }
        public int NumeroSerie { get; set; }
        public TipoPagamento? TipoPagamento { get; set; }
        public decimal ValorPagamento { get; set; }

        private List<DetalheVenda> detalheVenda;
        public List<DetalheVenda> DetalheVenda
        {
            get
            {
                if (detalheVenda == null)
                {
                    detalheVenda = new List<DetalheVenda>();
                }
                return detalheVenda;
            }
            set { detalheVenda = value; }
        }

        public Guid IniciarNovaVenda(PontoDeVenda pontodeVenda, Utilizador vendedor)
        {

            PontoDeVenda = pontodeVenda;
            Vendedor = vendedor;

            return Identificador;
        }

        public string AdicionarProduto(string ean, int quantidade)
        {
            if (TipoPagamento.HasValue)
            {
                return $"Compra já terminada não pode efetuar operações";
            }

            // Bloquear produto do estoque
            var listaEstoque = repositorioEstoque.BloquearProdutosDoEstoqueParaVenda(ean, quantidade, Identificador);

            if (listaEstoque == null)
            {
                return $"Produto(s) não disponíveis para venda";
            }

            // Adicionar a nossa lista de detalhe
            var produto = repositorioProduto.ObterPorEan(listaEstoque[0].Ean);
            var desconto = 0;
            foreach (var item in listaEstoque)
            {
                DetalheVenda.Add(new DetalheVenda() { Desconto = 0.0m, Produto = produto, PrecoFinal = produto.PrecoUnitario - desconto , NumeroSerie = item.NumeroSerie, EstoqueIdentificador = item.Identificador});
            }
            return $"Produto(s) adicionado(s) com sucesso";
        }

        public string RemoverProduto(Guid estoqueIdentificador)
        {
            if (TipoPagamento.HasValue)
            {
                return $"Compra já terminada não pode efetuar operações";
            }

            var produtoParaApagar = DetalheVenda.FirstOrDefault(x => x.EstoqueIdentificador == estoqueIdentificador);

            if (produtoParaApagar == null)
            {
                return $"Produto não disponível para remoção";
            }

            // Desbloquear produto do estoque
            var remocaoComSucesso = repositorioEstoque.DesbloquearProdutosDoEstoqueEmVenda(produtoParaApagar.EstoqueIdentificador);

            if (!remocaoComSucesso)
            {
                return $"Não foi possível remover o produto - Contacte IT";
            }

            DetalheVenda.Remove(produtoParaApagar);

            return $"Produto(s) removido(s) com sucesso";
        }


        public string CancelarVenda()
        {
            if (TipoPagamento.HasValue)
            {
                return $"Compra já terminada não pode efetuar operações";
            }

            var listaParaApagar = DetalheVenda.Select(x => x.EstoqueIdentificador).ToList();
            foreach (var item in listaParaApagar)
            {
                RemoverProduto(item);
            }
            return $"Todos os produtos foram removidos";
        }

        public string FinalizarPagamento(TipoPagamento tipoPagamento, Cliente cliente) 
        {

            if (TipoPagamento.HasValue)
            {
                return $"Compra já terminada não pode efetuar operações";
            }

            Cliente = cliente;
            TipoPagamento = tipoPagamento;
            ValorPagamento = DetalheVenda.Sum(x => x.PrecoFinal);
            DataHoraVenda = DateTime.Now;
            repositorioEstoque.DesativarProdutosPagos(Identificador);

            return $"Compra efetuada com sucesso";
        }



        // Gerar Recibo

        //public void GerarRecibo()
        //{
        //    var sb = new StringBuilder();
        //    sb.AppendLine($"Fatura Recibo FRD {DataHoraVenda.Year}/{NumeroSerie}");
        //    sb.AppendLine($"Loja: {PontoDeVenda.Loja.NumeroFiscal} - Ponto de Venda: {PontoDeVenda.Identificador} ");
        //    sb.AppendLine($"Loja: {PontoDeVenda.Loja.Morada}");
        //    sb.AppendLine($"Vendedor: {Vendedor.Nome} Identificador: {Vendedor.Identificador} ");
        //    sb.AppendLine($"Data da Fatura/Recibo: {DataHoraVenda} ");
        //    sb.AppendLine($"Tipo Pagamento: {TipoPagamento} "); //TODO: TRocar enumerador por string
        //    sb.AppendLine($"Valor Pagamento: {ValorPagamento} ");
        //    foreach (var item in DetalheVendas)
        //    {
        //        sb.AppendLine($"Nome: {item.Produto.Nome}  - Valor Unitario: {item.Produto.PrecoUnitario} -  Desconto : {item.Desconto} - PrecoFinal : {item.PrecoFinal}");
        //    }

        //    //Poderíamos escrever usando a notação LINQ
        //    //DetalheVenda.Produtos.Select(x => sb.AppendLine($"Nome: {x.Nome}  - Valor Unitario: {x.PrecoUnitario} - Número de Série: {x.NumeroSerie}"))
        //}

        public override string ToString()
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendLine($"Venda\nId: {Identificador} Loja: {PontoDeVenda?.Loja?.Nome} POS: {PontoDeVenda.Nome} Vendedor: {Vendedor.Nome}");
            foreach (var item in DetalheVenda)
            {
                msg.AppendLine($"\t\tProduto{item.Produto.Ean} - {item.Produto.Nome} - preço final {item.PrecoFinal} Serie nr. {item.NumeroSerie} ");
            }

            return msg.ToString();
        }

    }
}
