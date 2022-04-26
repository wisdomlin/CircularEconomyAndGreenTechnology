
using NUnit.Framework;
using System;
using System.Data;
using System.IO;

namespace Asc
{
    class Test_Uc_Cpa
    {
        [Test]
        public void Test_Uc_Cpa_GlobalPrice()
        {
            // Arrange
            DACF_Fao Dacf = new DACF_Fao();
            //Dacf.FaoFilePath = AppDomain.CurrentDomain.BaseDirectory
            //    + @"DACF\Data\Food_price_indices_data_jul20.csv";
            Dacf.FaoFilePath = @"D:\EuroStat\FrPrice\"
                + @"DACF\Data\Food_price_indices_data_jul20.csv";

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
        public void Test_Uc_Cpa_FrPrice()
        {
            // Arrange
            Uc_Cpa Uc_Cpa = new Uc_Cpa();

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
        public void Test_Uc_Cpa_TwPrice()
        {
            // Arrange
            Uc_Cpa Dacf = new Uc_Cpa();
            //Dacf.FilePath = AppDomain.CurrentDomain.BaseDirectory
            //    + @"DACF\Data\prc_fsc_idx_1_Data_ACP.csv";

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
    }
}
