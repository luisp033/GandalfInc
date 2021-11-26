
using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Produto : Entidade
    {
        public string Ean { get; set; }

        [Required, MaxLength(255)]
        public string Nome { get; set; }
        public CategoriaProduto Categoria{ get; set; }
        public MarcaProduto Marca { get; set; }
        public decimal PrecoUnitario { get; set; }

        //public override string ToString()
        //{
        //    return $"Ean: {Ean} - Nome: {Nome} - Categoria: {Categoria?.Nome} - Marca: {Marca?.Nome} - Preço: {PrecoUnitario}";
        //}

    }
}
