
using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Produto : Entidade
    {

        [Required, MaxLength(255)]
        public string Nome { get; set; }
        [Required]
        public CategoriaProduto Categoria { get; set; }
        [Required]
        public MarcaProduto Marca { get; set; }
        [Required]
        public decimal PrecoUnitario { get; set; }
        [MaxLength(255)]
        public string Ean { get; set; }

        public byte[] ImageData { get; set; }


    }
}
