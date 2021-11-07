namespace Projeto.Lib.Entidades
{
    //Usuario ou Utilizador
    public class Utilizador : Entidade
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUtilizador Tipo { get; set; }


        public override string ToString()
        {
            return $"Utilizador: {base.Identificador} - Nome: {Nome} - Tipo:{Tipo} - Ativo:{Ativo}";
        }


    }
}
