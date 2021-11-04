using System;

namespace Projeto.Lib.Entidades
{
    public abstract class Entidade
    {
        public Guid Identificador { get; set; }
        public string Nome { get; set; }
        private DateTime dataAlteracao;

        public DateTime DataAlteracao
        {
            get { return dataAlteracao; }
            set { 
                if(dataAlteracao == new DateTime())
                {
                    dataAlteracao = DateTime.Now;
                }
                dataAlteracao = value; 
            }
        }
        public bool Ativo { get; set; }
    }
}