using Projeto.Lib.Entidades.Pessoas;
using System;

namespace Projeto.Lib.Faturacao
{
    public class PontoDeVenda
    {
        public Guid Identificador { get; set; }
        public Loja Loja { get; set; }
    }
}
