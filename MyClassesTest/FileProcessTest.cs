using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\BadFileName.bat";
        private string _GoodFilename;

        public TestContext TestContext { get; set; }

        #region Test Initialize e Cleanup

        [TestInitialize]
        public void TestInitialize()
            // executa antes do teste iniciar(ou na inicialização)
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
            {
                SetGoodFileName();

                if (!string.IsNullOrEmpty(_GoodFilename))
                {
                 
                    TestContext.WriteLine($"Creating File: {_GoodFilename}");
                    File.AppendAllText(_GoodFilename, "Some Text");
                }
            }
        }

        [TestCleanup]
        public void TestCleanup()
            // executa depois que o teste é finalizado
        {
            if (TestContext.TestName.StartsWith("FileNameDoesExists"))
            {
                if (!string.IsNullOrEmpty(_GoodFilename))
                {
                    TestContext.WriteLine($"Deleting File: {_GoodFilename}");
                    File.Delete(_GoodFilename);
                }
            }
        }

        #endregion

        [TestMethod]
        [Description("Check to see if a file does exist.")]
        [Owner("Thiago")]
        [Priority(0)]
        [TestCategory("NoException")]
        public void FileNameDoesExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            
            TestContext.WriteLine($"Testing File: {_GoodFilename}");
            fromCall = fp.FileExists(_GoodFilename);
            

            Assert.IsTrue(fromCall);
        }

        [TestMethod]

        public void FileNameDoesExistsSimpleMessage()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;


            TestContext.WriteLine($"Testing File: {_GoodFilename}");
            fromCall = fp.FileExists(_GoodFilename);


            Assert.IsFalse(fromCall, "File Does NOT Exist.");
            // mensagem de erro personalizada(neste caso, file does not exist)
        }

        [TestMethod]

        public void FileNameDoesExistsMessageFormatting()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;


            TestContext.WriteLine($"Testing File: {_GoodFilename}");
            fromCall = fp.FileExists(_GoodFilename);


            Assert.IsFalse(fromCall, "File '{0}' Does NOT Exist.", _GoodFilename);
            // mensagem de erro personalizada FORMATADA(neste caso, file does not exist, porem, com o caminho do arquivo ali no {0})
        }


        public void SetGoodFileName()
        {
            _GoodFilename = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFilename.Contains("[AppPath]"))
            {
                _GoodFilename = _GoodFilename.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        private const string FILE_NAME = @"FileToDeploy.txt";


        [TestMethod]
        [Owner("Raul Silveira")]
        [DeploymentItem(FILE_NAME)]
        // pra fazer o teste ele faz deploy de tudo o que ele precisava,
        // e depois que finalizou o teste, ele deleta tudo
        public void FileNameDoesExistsUsingDeploymentItem()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool fromCall;

            fileName = $@"{TestContext.DeploymentDirectory}\{FILE_NAME}";

            TestContext.WriteLine($"Checking File: {fileName}");
            fromCall = fp.FileExists(fileName);


            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Timeout(3100)]
        //[Timeout(2000)]
        //ideia de timeout: neste caso o teste falharia, pois estou
        //esperando que o teste execute em 2 segundos, mas coloquei um sleep
        //de 3 segundos, ou seja, ele nao vai demorar 2 segundos e sim 3. se eu colocasse
        //    um timeout de 5, por exemplo, daria tudo certo, pois ele executou o sleep de 3 
        //    segundos tranquilamente em 5 segundos
        public void SimulateTimeout()
        {
            System.Threading.Thread.Sleep(3000);
        }

        [TestMethod]
        [Description("Check to see if a file does NOT exist.")]
        [Owner("Pedro")]
        [Priority(0)]
        [TestCategory("NoException")]

        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Owner("Raul Silveira")]
        [Priority(1)]
        [TestCategory("Exception")]
        // esperando uma exceção específica
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();
            fp.FileExists("");
        }

        [TestMethod]
        [Owner("Joaquim")]
        [Priority(1)]
        [TestCategory("Exception")]
        //[Ignore] -> usado pra ignorar algum teste

        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();
            try
            {
                fp.FileExists("");
            }
            catch (ArgumentException)
            {
                // The test was a Sucess
                return;
            }

            Assert.Fail("Fail expected");
        }
    }
}
