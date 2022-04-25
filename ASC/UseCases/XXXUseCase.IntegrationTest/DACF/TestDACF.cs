
using NUnit.Framework;
using System;
using System.Data;
using System.IO;

namespace Asc
{
    class TestDACF
    {
        [Test]
        public void UC01_TestDACF_GlobalPrice()
        {
            // Arrange
            DACF_Fao Dacf = new DACF_Fao();
            Dacf.FaoFilePath = AppDomain.CurrentDomain.BaseDirectory
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
        public void UC02_TestDACF_FrTg()
        {
            // 1. 
            Uc_Spa_Old Dacf = new Uc_Spa_Old();
            bool result;

            // 2. 
            result = Dacf.UseCsvFileAnalyzer(); // -9999 先不篩
            Assert.IsTrue(result);

            // 3. 
            result = Dacf.IntraRawSpikeAnalyzer();
            Assert.IsTrue(result);

            // 4. 
            result = Dacf.InterIntegratedSpikeAnalyzer();
            Assert.IsTrue(result);
        }

        [Test]
        public void UC03_TestDACF_FrRr()
        {
            // Arrange
            Uc_Spa Uc_Spa = new Uc_Spa();

            // Data Folder Path
            Uc_Spa.RawFolderPath = @"D:\ECAD\ECA_blend_rr\";
            Uc_Spa.MetaFolderPath = @"D:\Meta\DACF_FrPrecip\";
            Uc_Spa.ResultFolderPath = @"D:\Result\";

            // Precip Stations in French
            Uc_Spa.SID_Prefix = "Rr";
            Uc_Spa.SID_Array = new string[] {
                "31",
                "32",
                "33",
                "34",
                "36",
                "37",
                "39",
                "322",
                "323",
                "434",
                "736",
                "737",
                "738",
                "739",
                "740",
                "742",
                "745",
                "749",
                "750",
                "755",
                "756",
                "757",
                "758",
                "759",
                "761",
                "764",
                "767",
                "770",
                "771",
                "773",
                "774",
                "776",
                "778",
                "781",
                "785",
                "786",
                "787",
                "790",
                "792",
                "793",
                "796",
                "804",
                "2184",
                "2190",
                "2192",
                "2195",
                "2196",
                "2199",
                "2200",
                "2203",
                "2205",
                "2207",
                "2209",
                "11243",
                "11244",
                "11245",
                "11246",
                "11247",
                "11248",
                "11249",
                "21359",
                "21360"
              };

            // Act
            bool result;
            result = Uc_Spa.Run();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void UC04_TestDACF_FrPrice()
        {
            // Arrange
            Uc_Cpa Dacf = new Uc_Cpa();

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
        public void UC05_TestDACF_FrAisTemp()
        {
            // Arrange
            Uc_Ais Dacf = new Uc_Ais();

            bool result;
            string ResultFolderPath = @"D:\Result\";
            Dacf.CpaFilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa01" + ".csv";
            Dacf.SpaFilePath = ResultFolderPath + @"Spa\" + "Result_Spa_Tg" + ".csv";
            Dacf.AisFilePath = ResultFolderPath + @"Ais\" + "Result_Ais_Tg" + ".csv";

            result = Dacf.IntegratedAnalysis();
            Assert.IsTrue(result);
        }
        
        [Test]
        public void UC06_TestDACF_FrAisPrecip()
        {
            // Arrange
            Uc_Ais Dacf = new Uc_Ais();

            bool result;
            string ResultFolderPath = @"D:\Result\";
            Dacf.CpaFilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa01" + ".csv";
            Dacf.SpaFilePath = ResultFolderPath + @"Spa\" + "Result_Spa_Rr" + ".csv";
            Dacf.AisFilePath = ResultFolderPath + @"Ais\" + "Result_Ais_Rr" + ".csv";

            result = Dacf.IntegratedAnalysis();
            Assert.IsTrue(result);
        }

        [Test]
        public void UC07_TestDACF_TwTemp()
        {
            // Arrange
            DACF_TwTemp Dacf = new DACF_TwTemp();
            Dacf.DateTime_Start = DateTime.Now.ToString("yyyyMMdd-HHmm");   // 2008/1/28
            //Dacf.FilePath = AppDomain.CurrentDomain.BaseDirectory
            //+ @"DACF\Data\TG_STAID" + value.PadLeft(6, '0') + ".txt";
            //Dacf.FilePath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\" +
            //    "TG_STAID" + value.PadLeft(6, '0') + ".txt";

            // Act
            bool result;
            result = Dacf.UseCsvFileAnalyzer(); // -9999 先不篩
            Assert.IsTrue(result);

            //result = Dacf.UseSingularSpectrumAnalyzer();
            //Assert.IsTrue(result);

            //result = Dacf.UseOutlierTrimmingAnalyzer();
            //Assert.IsTrue(result);

            result = Dacf.IntraRawSpikeAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.InterIntegratedSpikeAnalyzer();
            Assert.IsTrue(result);

        }
        
        [Test]
        public void UC08_TestDACF_TwPrecip()
        {
            // Arrange
            Uc_Spa Dacf = new Uc_Spa();

            // Act
            bool result;
            result = Dacf.UseCsvFileAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.IntraRawSpikeAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.InterIntegratedSpikeAnalyzer();
            Assert.IsTrue(result);

        }

        [Test]
        public void UC09_TestDACF_TwPrice()
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
