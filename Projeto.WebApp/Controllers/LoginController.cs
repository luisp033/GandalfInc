using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.WebApp.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projeto.WebApp.Controllers
{
    public class LoginController : BaseController
    {
        private readonly IAutenticacao _autentica;

        public LoginController(IAutenticacao autentica, ProjetoDBContext context) : base(context)
        {
            _autentica = autentica;
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            //HttpContext.Session.SetString("MyData", "Value");
            return View();
        }

        [HttpGet]
        public IActionResult LoginUsuario()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUsuario([Bind] Usuario usuario)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Email");

            TempData["MensagemPosErro"] = null;
            TempData["MensagemPosSucesso"] = null;

            if (ModelState.IsValid)
            {
                Resultado  loginStatus = _autentica.ValidarLogin(usuario);

                if (loginStatus.Sucesso)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Login)
                    };

                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-6.0
                    //HttpContext.Session.SetString("MyVariable","MyValor");

                    await HttpContext.SignInAsync(principal);

                    if (((Utilizador)loginStatus.Objeto).Tipo.TipoId == (int)TipoUtilizadorEnum.Gerente)
                    {
                        return RedirectToAction("Index", "Gestao", new { Area = "Gestao" } );
                    }
                    else if (((Utilizador)loginStatus.Objeto).Tipo.TipoId == (int)TipoUtilizadorEnum.Empregado)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        await HttpContext.SignOutAsync();
                        TempData["LoginUsuarioFalhou"] = $"Tipo de Utilizador desconhecido {((Utilizador)loginStatus.Objeto).Tipo}.";
                        return View();
                    }


                    //if (User.Identity.IsAuthenticated)
                    //    return RedirectToAction("UsuarioHome", "Usuario");
                    //else
                    //{
                    //    return RedirectToAction("LoginUsuario", "Login");
                    //}
                }
                else
                {
                    TempData["LoginUsuarioFalhou"] = "O login Falhou. Informe as credenciais corretas " + User.Identity.Name;
                    return View();
                }
            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}