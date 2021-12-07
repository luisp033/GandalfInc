using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Venda 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Identificador { get; set; }
        public PontoDeVendaSessao PontoDeVendaSessao { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime? DataHoraVenda { get; set; }
        public int NumeroSerie { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public decimal ValorPagamento { get; set; }

    }
}
