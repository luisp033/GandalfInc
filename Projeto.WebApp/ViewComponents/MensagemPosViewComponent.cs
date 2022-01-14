using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Projeto.WebApp.Models;

namespace Projeto.WebApp.ViewComponents
{

    [Authorize]
    public class MensagemPosViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult((IViewComponentResult)View("MensagemPos"));

        }

    }
}
