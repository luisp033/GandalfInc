using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Projeto.Tests
{
    /// <summary>
    /// Classe apenas para exemplo. Deve ser descartada
    /// </summary>
    [TestClass]
    public class CalculadoraTests
    {
        [TestMethod]
        public void SomarTest()
        {
            //arrange
            var a = 1;
            var b = 1;

            //act
            var actual = Calculadora.Somar(a, b);

            //assert
            Assert.AreEqual(2, actual);
        }

        /// <summary>
        /// Exemplo de testes com múltiplos valores. Vou explicar hoje
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        [DataTestMethod]
        [DataRow(1, 1, 2, DisplayName = "Primeiro Caso")]
        [DataRow(0, 1, 1, DisplayName = "Segundo Caso")]
        [DataRow(1, 2, 3, DisplayName = "Terceiro Caso")]
        public void SomarDataRowTest(int a, int b, int c)
        {
            //act
            var actual = Calculadora.Somar(a, b);

            //assert
            Assert.AreEqual(c, actual);
        }
    }
}