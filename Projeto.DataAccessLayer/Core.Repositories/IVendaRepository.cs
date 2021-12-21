﻿using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;

namespace Projeto.DataAccessLayer.Core.Repositories
{
    public interface IVendaRepository : IRepository<Venda>
    {
        Venda GetVendaEmCurso(Guid pontoVendaSessaoId);
    }
}
