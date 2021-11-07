namespace Projeto.Lib.Entidades.Pessoas
{
    public abstract class Pessoa : Entidade
    {

        public string Nome { get; set; }
        public string NumeroFiscal { get; set; }
        public Morada MoradaEntrega { get; set; }
        public Morada MoradaFaturacao { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public Pessoa()
        {
            MoradaEntrega = new Morada();
        }

    }
}