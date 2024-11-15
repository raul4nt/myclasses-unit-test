using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace MyClassesTest
{
    [TestClass]
    public class StringAssertClassTest
    {
        [TestMethod]
        [Owner("Raul")]
        public void ContainsTest()
        {
            string str1 = "Raul Silveira";
            string str2 = "Silveira";

            StringAssert.Contains(str1, str2);
            // verifica se a str1 contem a str2(que neste caso, sim).
        }

        [TestMethod]
        [Owner("Raul")]
        public void StartsWithTest()
        {
            string str1 = "Todos Caixa Alta";
            string str2 = "Todos Caixa";

            StringAssert.StartsWith(str1, str2);
            // verifica se a str1 começa com o q esta contido na str2(sim, esta).
        }

        [TestMethod]
        [Owner("Raul")]
        public void IsAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");
            // regex que procura por pelo menos um item com caixa alta
            // neste caso, todos os nossos itens("todo caixa") estao em caixa baixa

            StringAssert.Matches("todos caixa", reg);
        }

        [TestMethod]
        [Owner("Raul")]
        public void IsNotAllLowerCaseTest()
        {
            Regex reg = new Regex(@"^([^A-Z])+$");
            
            // mesmo regex, mas agora quero verificar se NAO É lower case, entao ao inves sde 
            // matches uso doesnotmatch

            StringAssert.DoesNotMatch("Todos caixa", reg);
        }




    }
}

