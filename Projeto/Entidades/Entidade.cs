using System;

namespace Projeto.Lib.Entidades
{
    public abstract class Entidade : Controlo
    {
        public Guid Identificador { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
    }
}