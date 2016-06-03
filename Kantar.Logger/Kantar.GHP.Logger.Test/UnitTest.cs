using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kantar.GHP.Logger.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void DebugLevelLog()
        {
            //Arrange
            ILogger log = new FileLogger(typeof(UnitTest));

            //Act
            log.Log(LoggingLevel.Debug, "Test Debug Message info");

            //Assert
        }

        [TestMethod]
        public void INFOLevelLog()
        {
            //Arrange
            ILogger log = new FileLogger(typeof(UnitTest));

            //Act
            log.Log(LoggingLevel.Info, "Test INFO Message info");

            //Assert
        }

        [TestMethod]
        public void WARNLevelLog()
        {
            //Arrange
            ILogger log = new FileLogger(typeof(UnitTest));

            //Act
            log.Log(LoggingLevel.Warning, "Test WARN Message info");

            //Assert
        }

        [TestMethod]
        public void ERRORLevelLog()
        {
            //Arrange
            ILogger log = new FileLogger(typeof(UnitTest));

            //Act
            log.Log(LoggingLevel.Error, "Test ERROR Message info");

            //Assert
        }

        [TestMethod]
        public void FATALLevelLog()
        {
            //Arrange
            ILogger log = new FileLogger(typeof(UnitTest));

            //Act
            log.Log(LoggingLevel.Fatal, "Test FATAL Message info");

            //Assert
        }
    }
}
