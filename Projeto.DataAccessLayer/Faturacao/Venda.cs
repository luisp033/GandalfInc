using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Projeto.DataAccessLayer.Faturacao
{
    public class Venda 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        //public override string ToString()
        //{
        //    StringBuilder msg = new StringBuilder();
        //    msg.AppendLine($"Venda\nId: {Identificador} Loja: {PontoDeVenda?.Loja?.Nome} POS: {PontoDeVenda.Nome} Vendedor: {Vendedor.Nome}");
        //    foreach (var item in DetalheVenda)
        //    {
        //        msg.AppendLine($"\t\tProduto{item.Produto.Ean} - {item.Produto.Nome} - preço final {item.PrecoFinal} Serie nr. {item.NumeroSerie} ");
        //    }

        //    return msg.ToString();
        //}

    }
}
