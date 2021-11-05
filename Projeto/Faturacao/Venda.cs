using Projeto.Lib.Entidades;
using Projeto.Lib.Entidades.Pessoas;
using Projeto.Lib.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Lib.Faturacao
{
    public class Venda : IImpressora
    {
        public Utilizador Vendedor { get; set; }
        public Cliente Cliente { get; set; }
        public PontoDeVenda PontoDeVenda { get; set; }
        public DateTime DataHoraVenda { get; set; }
        public int NumeroSerie { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
        public List<DetalheVenda> DetalheVendas { get; set; }  

        public void GerarRecibo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Fatura Recibo FRD {DataHoraVenda.Year}/{NumeroSerie}");
            sb.AppendLine($"Loja: {PontoDeVenda.Loja.NumeroFiscal} - Ponto de Venda: {PontoDeVenda.Identificador} ");
            sb.AppendLine($"Loja: {PontoDeVenda.Loja.Morada}");
            sb.AppendLine($"Vendedor: {Vendedor.Nome} Identificador: {Vendedor.Identificador} ");
            sb.AppendLine($"Data da Fatura/Recibo: {DataHoraVenda} ");
            sb.AppendLine($"Tipo Pagamento: {TipoPagamento} "); //TODO: TRocar enumerador por string
            sb.AppendLine($"Valor Pagamento: {ValorPagamento} ");
            foreach (var item in DetalheVendas)
            {
                sb.AppendLine($"Nome: {item.Produto.Nome}  - Valor Unitario: {item.Produto.PrecoUnitario} -  Desconto : {item.Desconto} - PrecoFinal : {item.PrecoFinal}");
            }

            //Poderíamos escrever usando a notação LINQ
            //DetalheVenda.Produtos.Select(x => sb.AppendLine($"Nome: {x.Nome}  - Valor Unitario: {x.PrecoUnitario} - Número de Série: {x.NumeroSerie}"))
        }
    }
}
