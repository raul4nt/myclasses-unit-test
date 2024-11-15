using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;

namespace MyClassesTest
{
    [TestClass]
    public class AssertClassTest
    {
        #region AreEqual/AreNotEqual tests
        [TestMethod]
        [Owner("Raul")]
        public void AreEqualTest()
        {
            string str1 = "Raul";
            string str2 = "Raul";

            Assert.AreEqual(str1, str2);
            // são equivalentes ou nao?
        }

        [TestMethod]
        [Owner("Raul")]
        [ExpectedException(typeof(AssertFailedException))]
        // exceção de assert falho
        public void AreEqualCaseSensitiveText()
        {
            string str1 = "Raul";
            string str2 = "raul";

            Assert.AreEqual(str1, str2, false);
            // colocamos false no final = espera que levante a exceção
            // se colocarmos true, verificamos se ambos são sensitive
            // se colocarmos false, nao faz essa verificação
            // ou seja, quando colocamros false a nossa ideia é que nao passe o teste mesmo
        }

        [TestMethod]
        [Owner("Raul")]
        public void AreNotEqualTest()
        {
            string str1 = "Raul";
            string str2 = "Cláudio";

            Assert.AreNotEqual(str1, str2);
            // nao sao equivalentes ou sao?

        }
        #endregion

        #region AreSame/AreNotSame Tests

        [TestMethod]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x; 

            Assert.AreSame(x, y);
            // verifica se são o mesmo objeto(neste caso, são)
        }

        [TestMethod]
        public void AreNotSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            Assert.AreNotSame(x, y);
            // verifica se não são o mesmo objeto(nesse caso, não são)
        }

        #endregion
    }
}
