
using NUnit.Framework;
using System;
using System.Data;
using System.IO;

namespace Asc
{
    class Test_Uc_Cpa
    {
        [Test]
        public void Test_Uc_Cpa_01_GlobalPrice()
        {
            // TODO: This Use Case should reuse Uc_Cpa

            // Arrange
            DACF_Fao Dacf = new DACF_Fao();
            Dacf.FaoFilePath = @"D:\EuroStat\FrPrice\Food_price_indices_data_jul20.csv";

            // Act
            bool result;
            result = Dacf.UseCsvFileAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.UseSingularSpectrumAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.UseOutlierTrimmingAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.UseChangePointAnalyzer();
            Assert.IsTrue(result);
        }
                
        [Test]
        public void Test_Uc_Cpa_02_FrPrice_Serial()
        {
            // Arrange
            Uc_Cpa_Serial Uc_Cpa = new Uc_Cpa_Serial();

            // Data Folder Path
            Uc_Cpa.RawFolderPath = @"D:\EuroStat\FrPrice\";
            Uc_Cpa.MetaFolderPath = @"D:\Meta\DACF_EuroStat\";
            Uc_Cpa.ResultFolderPath = @"D:\Result\";
            Uc_Cpa.RawFileName = "prc_fsc_idx_1_Data_ACP.csv";

            // Act
            bool result;
            result = Uc_Cpa.Run();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Test_Uc_Cpa_03_FrPrice_Parallel()
        {
            // Arrange
            Uc_Cpa_Parallel Uc_Cpa = new Uc_Cpa_Parallel();

            // Data Folder Path
            Uc_Cpa.RawFolderPath = @"D:\EuroStat\FrPrice\";
            Uc_Cpa.MetaFolderPath = @"D:\Meta\DACF_EuroStat\";
            Uc_Cpa.ResultFolderPath = @"D:\Result\";
            Uc_Cpa.RawFileName = "prc_fsc_idx_1_Data_ACP.csv";
            // string ResultFolderPath = @"D:\Result\";
            // Uc_Ais.CpaFilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa01" + ".csv";
            // Act
            bool result;
            result = Uc_Cpa.Run();

            // Assert
            Assert.IsTrue(result);
        }
    }
}
