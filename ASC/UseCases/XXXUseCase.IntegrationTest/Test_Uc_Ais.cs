
using NUnit.Framework;
using System;
using System.Data;
using System.IO;

namespace Asc
{
    class Test_Uc_Ais
    {
        [Test]
        public void Test_Uc_Ais_FrTg()
        {
            // Arrange
            Uc_Ais Uc_Ais = new Uc_Ais();
            string ResultFolderPath = @"D:\Result\";
            Uc_Ais.CpaFilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa01" + ".csv";
            Uc_Ais.SpaFilePath = ResultFolderPath + @"Spa\" + "Result_Spa_Tg" + ".csv";
            Uc_Ais.AisFilePath = ResultFolderPath + @"Ais\" + "Result_Ais_Tg" + ".csv";

            // Act
            bool result;
            result = Uc_Ais.Run();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_Uc_Ais_FrRr()
        {
            // Arrange
            Uc_Ais Uc_Ais = new Uc_Ais();
            string ResultFolderPath = @"D:\Result\";
            Uc_Ais.CpaFilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa01" + ".csv";
            Uc_Ais.SpaFilePath = ResultFolderPath + @"Spa\" + "Result_Spa_Rr" + ".csv";
            Uc_Ais.AisFilePath = ResultFolderPath + @"Ais\" + "Result_Ais_Rr" + ".csv";

            // Act
            bool result;
            result = Uc_Ais.Run();

            // Assert
            Assert.IsTrue(result);
        }
    }
}
