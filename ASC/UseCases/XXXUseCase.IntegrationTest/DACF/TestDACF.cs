﻿
using NUnit.Framework;
using System;
using System.Data;
using System.IO;

namespace Asc
{
    class TestDACF
    {
        [Test]
        public void UC01_TestDACF_Fao()
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
        public void UC02_TestDACF_EuroStat()
        {
            // Arrange
            DACF_EuroStat Dacf = new DACF_EuroStat();
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

        [Test]
        public void UC03_TestDACF_FrTemp()
        {
            // Arrange
            DACF_FrTemp Dacf = new DACF_FrTemp();
            Dacf.DateTime_Start = DateTime.Now.ToString("yyyyMMdd-HHmm");
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
        public void UC04_TestDACF_AisTemp()
        {
            // Arrange
            DACF_Ais Dacf = new DACF_Ais();

            bool result;
            string ResultFolderPath = @"D:\Result\";
            Dacf.CpaFilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa01" + ".csv";
            Dacf.SpaFilePath = ResultFolderPath + @"Spa\" + "Result_Spa_Tg" + ".csv";
            Dacf.AisFilePath = ResultFolderPath + @"Ais\" + "Result_Ais_Tg" + ".csv";

            result = Dacf.IntegratedAnalysis();
            Assert.IsTrue(result);
        }

        [Test]
        public void UC04_TestDACF_AisPrecip()
        {
            // Arrange
            DACF_Ais Dacf = new DACF_Ais();

            bool result;
            string ResultFolderPath = @"D:\Result\";
            Dacf.CpaFilePath = ResultFolderPath + @"Cpa\" + "Result_Cpa01" + ".csv";
            Dacf.SpaFilePath = ResultFolderPath + @"Spa\" + "Result_Spa_Rr" + ".csv";
            Dacf.AisFilePath = ResultFolderPath + @"Ais\" + "Result_Ais_Rr" + ".csv";

            result = Dacf.IntegratedAnalysis();
            Assert.IsTrue(result);
        }

        [Test]
        public void UC03_TestDACF_FrPrecip()
        {
            // Arrange
            DACF_FrPrecip Dacf = new DACF_FrPrecip();
            Dacf.DateTime_Start = DateTime.Now.ToString("yyyyMMdd-HHmm");

            // Act
            bool result;
            result = Dacf.UseCsvFileAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.IntraRawSpikeAnalyzer();
            Assert.IsTrue(result);

            result = Dacf.InterIntegratedSpikeAnalyzer();
            Assert.IsTrue(result);

        }
    }
}
