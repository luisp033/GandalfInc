using System;

namespace Projeto.DataAccessLayer.Entidades
{
    public abstract class Entidade 
    {
        public Guid Identificador { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

        protected Entidade()
        {
            Identificador = Guid.NewGuid();

            DataUltimaAlteracao = DateTime.Now;

            Ativo = true;
        }

    }
}