using EconomicMoat.Standard;
using NUnit.Framework;
using System;
using System.Data;

namespace EconomicMoat.UnitTest
{
    class CsvFileReader_CsvFileStructure
    {
        string Path;
        CsvFileReader Cfr;
        CsvFileStructure Cfs;

        [SetUp]
        public void Setup()
        {
            Path = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\TG_STAID000001.txt";
            Cfr = new CsvFileReader();
            Cfr.FilePath = Path;
            Cfr.Delimiters = new Char[] { ',', ' ' };

            Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 21;
            Cfs.DataLinesStartAt = 22;
            Cfs.FooterLinesCount = 0;

            Cfr.Cfs = Cfs;
        }

        [Test]
        public void TestReadFullFile()
        {
            Cfr.Dal = new DatalineAnalysisLogic();
            Cfr.Dal.Def = new DatalineEntityFormat();
            bool result = Cfr.ReadFullFile();
            Assert.IsTrue(result);
        }

        [Test]
        public void TestFindDatesBeyondTgThreshold()
        {
            Cfr.Dal = new Dal_TgAbsMaxAlarm();
            //Cfr.Dal.SetThreshold(330);
            Cfr.Dal.Def = new Def_TG();
            bool result = Cfr.ReadFullFile();
            Assert.IsTrue(result);
        }

        [Test]
        public void TestReadAllTgFiles()
        {
            //cfr.Dlf = new DataLinesFormatTg();
            //bool result = Cfr.ReadAllTgFiles();
            //Assert.IsTrue(result);
        }
    }
}
