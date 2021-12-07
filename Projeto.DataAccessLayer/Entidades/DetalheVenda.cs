
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.DataAccessLayer.Entidades
{
    public class DetalheVenda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Identificador { get; set; }
        public Venda Venda { get; set; }
        public Estoque EstoqueProduto { get; set; }
        public decimal Desconto { get; set; }
        public decimal PrecoFinal { get; set; }

    }
}