using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class MarcaProduto : Entidade
    {
        [Required, MaxLength(255)]
        public string Nome { get; set; }
        [MaxLength(255)]
        public string Origem { get; set; }   
    }
}
