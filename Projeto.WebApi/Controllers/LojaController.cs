using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto.BusinessLogicLayer;
using Projeto.DataAccessLayer;
using Projeto.DataAccessLayer.Entidades;
using Projeto.WebApi.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.WebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LojaController : ControllerBase
    {

        ProjetoDBContext ctx;

        public LojaController()
        {
            ctx = new ProjetoDBContext();
        }

        /// <summary>
        /// Get all Lojas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Loja> GetAllLojas()
        {
            LogicaSistema sistema = new LogicaSistema(ctx);
            return sistema.GetAllLojas();
        }

        /// <summary>
        /// Get loja by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public Loja ObtemLojaPorNome(Guid id)
        {
            LogicaSistema sistema = new LogicaSistema(ctx);
            return (Loja)(sistema.ObtemLoja(id).Objeto);
        }

        [HttpPost]
        public Guid PostNovaLoja(string nome) {

            LogicaSistema sistema = new LogicaSistema(ctx);
            var newLoja = new Loja {
                Nome = nome
            };
            var result = sistema.InsereLoja(nome, null, null, null, null);

            return ((Loja)result.Objeto).Identificador;
        }

        [HttpDelete]
        public bool DeleteLoja(Guid id) 
        {
            LogicaSistema sistema = new LogicaSistema(ctx);
            var result = sistema.ApagaLoja(id);
            return result.Sucesso;
        }

        [HttpPut]
        public bool UpdateLoja(Guid id, LojaDto loja)
        {
            LogicaSistema sistema = new LogicaSistema(ctx);
            var result = sistema.AlteraLoja(id,loja.Nome,null,null,null,null);
            return result.Sucesso;
        }


    }
}
