using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Lib.Entidades.Pessoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Lib.Entidades.Pessoas.Tests
{
    [TestClass()]
    public class PessoaTests
    {
        [TestMethod()]
        public void DeveCriarCliente()
        {
            //Arrange
            var pessoa = new Cliente();
            pessoa.Nome = "Luis";
            pessoa.Ativo = true;

            //Act
            var possuiIdentificadorAtribuido = pessoa.Identificador != new Guid();
            //var possuiDataInsercaoAtribuida = pessoa.DataInsercao != new DateTime();

            //Assert
            Assert.IsNotNull(pessoa);
            //Assert.IsInstanceOfType(pessoa, typeof(Pessoa));
            Assert.IsTrue(pessoa.Ativo);
            Assert.IsTrue(possuiIdentificadorAtribuido);
            //Assert.IsTrue(possuiDataInsercaoAtribuida);

        }



    }
}