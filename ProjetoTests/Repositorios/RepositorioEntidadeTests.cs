using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Lib.Entidades.Pessoas;
using Projeto.Lib.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Lib.Repositorios.Tests
{
    [TestClass()]
    public class RepositorioEntidadeTests
    {
        [TestMethod()]
        public void DeveCriarEntidadeTest()
        {
            //Arrange
            var repo = new RepositorioEntidade();
            var cliente = new Cliente();
            //Act
            repo.Criar(cliente);
            repo.Criar(cliente);
            var quantidade = repo.ObterTodos().Count;


            //Assert
            Assert.AreEqual(2, quantidade);

        }

        [TestMethod()]
        public void DeveObterPorIdentificadorTest()
        {
            //Arrange
            /*
                053e42d3-f520-4699-b0f7-8c72c9a53c50
                1f454dd3-dc3d-4889-a2bf-65ba6580de83
            */
            var repo = new RepositorioEntidade();
            var identificadorCliente = new Guid("053e42d3-f520-4699-b0f7-8c72c9a53c50");

            var cliente = new Cliente { Identificador = new Guid("053e42d3-f520-4699-b0f7-8c72c9a53c50") };
            var cliente2 = new Cliente { Identificador = new Guid("1f454dd3-dc3d-4889-a2bf-65ba6580de83") };
            //Act
            repo.Criar(cliente);
            repo.Criar(cliente2);

            var obtido = repo.ObterPorIdentificador(identificadorCliente);

            //Assert
            Assert.IsInstanceOfType(obtido,typeof(Cliente));
            Assert.AreEqual(obtido.Identificador.ToString(), cliente.Identificador.ToString());


        }



        //[DataTestMethod()]
        //[DataRow(new Guid("053e42d3-f520-4699-b0f7-8c72c9a53c50"))]
        //[DataRow("1f454dd3-dc3d-4889-a2bf-65ba6580de83")]
        //public void DeveObterPorIdentificador2Test(Guid guid)
        //{
        //    //Arrange
        //    /*
        //        053e42d3-f520-4699-b0f7-8c72c9a53c50
        //        1f454dd3-dc3d-4889-a2bf-65ba6580de83
        //    */
        //    var repo = new RepositorioEntidade();
        //    var identificadorCliente = guid;

        //    var cliente = new Cliente { Identificador = guid };
        //    //Act
        //    repo.Criar(cliente);

        //    var obtido = repo.ObterPorIdentificador(identificadorCliente);

        //    //Assert
        //    Assert.IsInstanceOfType(obtido,typeof(Cliente));
        //    Assert.Equals(obtido.Identificador, cliente.Identificador);


        //}


    }
}