using System;

namespace Projeto.Lib.Entidades
{
    public abstract class Controlo
    {
        public DateTime DataInsercao { get; set; }
        public Utilizador UtilizadorInsercao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public Utilizador UtilizadorUltimaAlteracao { get; set; }

    }
}