using EconomicMoats.Standard;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace EconomicMoat.ModuleTest
{
    class TestCsvFileAnalyzerMudule
    {
        [Test]
        public void UC01_ReadTgCsvAndBaseAnalysis()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.FilePath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\TG_STAID000001.txt";
            Cfr.Delimiters = new char[] { ',', ' ', '\t' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 21;
            Cfs.DataLinesStartAt = 22;
            Cfs.FooterLinesCount = 0;

            DatalineAnalysisLogic Dal = new DatalineAnalysisLogic();
            DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Cfr.Dal = Dal;

            // 3. Read Csv File
            bool result = Cfr.ReadCsvFile();
            Assert.IsTrue(result);
        }

        [Test]
        public void UC02_ReadTgCsvAndCustomizeAnalysis()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.FilePath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\TG_STAID000001.txt";
            Cfr.Delimiters = new char[] { ',', ' ', '\t' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 21;
            Cfs.DataLinesStartAt = 22;
            Cfs.FooterLinesCount = 0;

            DatalineAnalysisLogic Dal = new Dal_TgAbsMaxAlarm();
            DatalineEntityAndFormat Def = new Def_TG();

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Cfr.Dal = Dal;

            // 3. Read Csv File
            bool result = Cfr.ReadCsvFile();
            Assert.IsTrue(result);
        }

        [Test]
        public void UC03_ReadTenCsvFiles()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.FilePath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\TG_STAID000001.txt";
            Cfr.Delimiters = new char[] { ',', ' ', '\t' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 21;
            Cfs.DataLinesStartAt = 22;
            Cfs.FooterLinesCount = 0;

            DatalineAnalysisLogic Dal = new Dal_TgAbsMaxAlarm();
            DatalineEntityAndFormat Def = new Def_TG();

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Cfr.Dal = Dal;

            // 3. Read Csv Files
            // Find all files in a folder
            string FolderPath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg";
            DirectoryInfo d = new DirectoryInfo(FolderPath);

            int i = 0;
            foreach (FileInfo file in d.GetFiles("TG_*.txt"))
            {
                // Do something for each file
                string FilePath = file.FullName;
                Cfr.SetFilePath(FilePath);
                bool result = Cfr.ReadCsvFile();
                Assert.IsTrue(result);

                i++;
                if (i >= 10)
                    break;
            }
        }

        [Test]
        public void UC04_ReadFpiCsvAndCustomizeAnalysis()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.FilePath = @"C:\Workspace\Branches\CircularEconomyAndGreenTechnology\EconomicMoat\EconomicMoat\EconomicMoats.ModuleTest\SSA\Food_price_indices_data_jul20.csv";
            Cfr.Delimiters = new char[] { ',', ' ', '\t' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 3;
            Cfs.DataLinesStartAt = 5;
            Cfs.FooterLinesCount = 0;

            DatalineAnalysisLogic Dal = new Dal_FaoFpi();
            DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Cfr.Dal = Dal;

            // 3. Read Csv File
            bool result = Cfr.ReadCsvFile();
            Assert.IsTrue(result);

            // 4. 
        }

        [Test]
        public void UC05_AnalyzeFaoCsvAndSeparateFiles()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.FilePath = @"C:\Workspace\Branches\CircularEconomyAndGreenTechnology\EconomicMoat\EconomicMoat\EconomicMoats.ModuleTest\SSA\Food_price_indices_data_jul20.csv";
            Cfr.Delimiters = new char[] { ',', ' ', '\t' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 3;
            Cfs.DataLinesStartAt = 5;
            Cfs.FooterLinesCount = 0;

            DatalineAnalysisLogic Dal = new Dal_FaoFpi();
            DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Cfr.Dal = Dal;

            // 3. Read Csv File
            bool result = Cfr.ReadCsvFile();
            Assert.IsTrue(result);
        }

        private string GetConfigFolder()
        {
            string ProjectFolderPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            return ProjectFolderPath + @"CsvFileReader\CsvFileReaderConfig\";
        }
    }
}
