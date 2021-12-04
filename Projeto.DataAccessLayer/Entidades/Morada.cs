using System.ComponentModel.DataAnnotations;

namespace Projeto.DataAccessLayer.Entidades
{
    public class Morada : Entidade
    {
     
        [MaxLength(255)]
        public string Endereco { get; set; } //rua das casas    n 25
        [MaxLength(7)]
        public string CodigoPostal { get; set; } //2830
        [MaxLength(100)]
        public string Localidade { get; set; } //barreiro
        public string Observacoes { get; set; } //Falar com a vizinha da frente 
    }
}