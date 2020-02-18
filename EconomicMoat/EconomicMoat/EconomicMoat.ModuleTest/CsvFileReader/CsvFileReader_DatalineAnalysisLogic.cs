using EconomicMoat.Standard;
using NUnit.Framework;
using System;
using System.Data;

namespace EconomicMoat.ModuleTest
{
    class CsvFileReader_DatalineAnalysisLogic
    {
        [Test]
        public void UC01_HowToCustomizedAnalyzeEachDataline()
        {
            // Part I. CsvFileReader_CsvFileStructure
            // 1. Prepare CsvFileStructure
            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 21;
            Cfs.DataLinesStartAt = 22;
            Cfs.FooterLinesCount = 0;

            // 2. Prepare CsvFileReader
            CsvFileReader Cfr = new CsvFileReader();
            Cfr.FilePath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\TG_STAID000001.txt";
            Cfr.Delimiters = new Char[] { ',', ' ' };

            // 3. CsvFileReader uses CsvFileStructure
            Cfr.Cfs = Cfs;

            // Part III. CsvFileReader_DatalineAnalysisLogic
            // Part II. DatalineAnalysisLogic_DatalineEntityFormat
            // 1.             
            DatalineEntityFormat Def = new Def_TG();

            DatalineAnalysisLogic Dal = new Dal_TgAbsMaxAlarm();
            Dal.Def = Def;

            Cfr.Dal = Dal;

            bool result = Cfr.ReadFullFile();
            Assert.IsTrue(result);
        }

        //[Test]
        //public void TestFindDatesBeyondTgThreshold()
        //{
        //    Cfr.Dal = new Dal_TgAbsMaxAlarm();
        //    //Cfr.Dal.SetThreshold(330);
        //    Cfr.Dal.Def = new Def_TG();
        //    bool result = Cfr.ReadFullFile();
        //    Assert.IsTrue(result);
        //}

        //[Test]
        //public void TestReadAllTgFiles()
        //{
        //    //cfr.Dlf = new DataLinesFormatTg();
        //    //bool result = Cfr.ReadAllTgFiles();
        //    //Assert.IsTrue(result);
        //}
    }
}
