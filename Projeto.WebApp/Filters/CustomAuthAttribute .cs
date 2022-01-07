using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using System;

namespace Projeto.WebApp.Filters
{

    public class ValidaTipoFilterAttribute : Attribute, IAuthorizationFilter
    {
        public TipoUtilizadorEnum Tipo { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Vai ter de obter os dados do utilizador logado
            LogicaSistema sistema = new LogicaSistema(new ProjetoDBContext());

             var tipo = (TipoUtilizadorEnum)(sistema.ObtemTipoUtilizadorByEmail(context.HttpContext.User.Identity.Name).Objeto);

            if (Tipo != tipo)
            {
                context.Result = new RedirectResult("/Home/Index");
            }

            //throw new NotImplementedException();
        }


        //public void OnActionExecuting(ActionExecutingContext context)
        //{




        //    if (Tipo == TipoUtilizadorEnum.Gerente) 
        //    { 
            
        //    }
        //}
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }


    }
}
