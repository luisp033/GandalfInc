using Projeto.DataAccessLayer.Entidades;
using Projeto.DataAccessLayer.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto.BusinessLogicLayer
{
    public partial class LogicaSistema
    {
        public Resultado InsereMarca(string nome, string origem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {
                MarcaProduto marca = new MarcaProduto
                {
                    Nome = nome,
                    Origem = origem
                };

                unitOfWork.MarcaProdutos.Add(marca);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Marca não inserida");
                }

                return new Resultado(true, "Marca inserida com sucesso", marca);
            }
        }

        public List<MarcaProduto> GetAllMarcas()
        {
            using (var unitOfWork = new UnitOfWork(_context))
            {
                var result = unitOfWork.MarcaProdutos.GetAll().ToList();
                return result;
            }
        }


        public Resultado ObtemMarca(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var marca = unitOfWork.MarcaProdutos.Get(identificador);

                return new Resultado(true, "Marca lida com sucesso", marca);
            }
        }

        public Resultado AlteraMarca(Guid identificador, string nome, string origem)
        {

            #region Validacoes
            if (String.IsNullOrEmpty(nome))
            {
                return new Resultado(false, "Nome obrigatório");
            }
            #endregion

            using (var unitOfWork = new UnitOfWork(_context))
            {

                var marca = unitOfWork.MarcaProdutos.Get(identificador);
                if (marca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }

                marca.Nome = nome;
                marca.Origem = origem;

                unitOfWork.MarcaProdutos.Update(marca);
                var affected = unitOfWork.Complete();

                if (affected == 0)
                {
                    return new Resultado(false, "Marca não alterada");
                }

                return new Resultado(true, "Marca alterada com sucesso", marca);
            }
        }

        public Resultado ApagaMarca(Guid identificador)
        {

            using (var unitOfWork = new UnitOfWork(_context))
            {
                var marca = unitOfWork.MarcaProdutos.Get(identificador);
                if (marca == null)
                {
                    return new Resultado(false, "Marca inexistente");
                }

                unitOfWork.MarcaProdutos.Remove(marca);
                var affected = unitOfWork.Complete();
                if (affected == 0)
                {
                    return new Resultado(false, "Marca não removida");
                }

                return new Resultado(true, "Marca removida com sucesso");
            }
        }

    }
}
