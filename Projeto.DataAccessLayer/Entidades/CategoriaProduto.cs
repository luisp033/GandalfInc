using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class CategoriaProduto : Entidade
    {
        [Required,MaxLength(255)]
        public string Nome { get; set; }
        public int OrdemApresentacao { get; set; }
    }
}
