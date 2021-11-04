using Projeto.Lib.Entidades.Pessoas;

namespace Projeto.Lib.Entidades.Produtos
{
    public class Produto : Entidade
    {
        public decimal PrecoUnitario { get; set; }
        public string Fabricante { get; set; }
        public Fornecedor Fornecedor { get; set; }
        public CategoriaProduto CategoriaProduto { get; set; }
        public string ReferenciaInternacionalProduto { get; set; } //EAN
        public string NumeroSerie { get; set; }
    }
}
