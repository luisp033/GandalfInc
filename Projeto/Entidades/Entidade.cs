using System;

namespace Projeto.Lib.Entidades
{
    public abstract class Entidade 
    {
        public Guid Identificador { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

        public Entidade()
        {
            Identificador = Guid.NewGuid();

            DataUltimaAlteracao = DateTime.Now;

            Ativo = true;
        }

    }
}