
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Asc
{
    class TestCsvFileAnalyzer
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

        [Test]
        public void UC06_AnalyzeEuroStatFrenchCsv()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.FilePath = AppDomain.CurrentDomain.BaseDirectory
                    + @"CsvFileAnalyzer\Data\" + @"prc_fsc_idx_1_Data_ACP.csv";
            Cfr.Delimiters = new char[] { '\t' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 1;
            Cfs.DataLinesStartAt = 2;
            Cfs.FooterLinesCount = 0;

            DatalineAnalysisLogic Dal = new Dal_EuroStatFrench();
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
        public void UC07_ReadPM2_5_Csv_20180101()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.FilePath = @"D:\EPA_IoT_Station_Data\2018\201801\" + @"epa_micro_20180101.csv";
            Cfr.Delimiters = new char[] { '\t', ',' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 1;
            Cfs.DataLinesStartAt = 2;
            Cfs.FooterLinesCount = 0;

            Dal_EPA_IoT_Station Dal = new Dal_EPA_IoT_Station();
            DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

            List<string> DeviceIdList = CreateDeviceIdList();
            List<string> DeviceTypeList = CreateDeviceTypeList();
            List<string> TankIdList = CreateTankIdList(DeviceIdList, DeviceTypeList);
            Dictionary<string, List<string>> DeviceIdList_byType = GetDeviceIdList_byType();
            CreateQmtAndPst(TankIdList,
                out Dictionary<string, QuickMixTank> Dic_Qmt,
                out Dictionary<string, PrimarySedimentationTank> Dic_Pst);

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Dal.Dic_Qmt = Dic_Qmt;
            Dal.DeviceIdList = DeviceIdList;
            Dal.DeviceTypeList = DeviceTypeList;
            Dal.DeviceIdList_byType = DeviceIdList_byType;
            Dal.useDeviceIdList = false;
            Cfr.Dal = Dal;

            // 3. Read Csv File
            bool result = Cfr.ReadCsvFile();
            Assert.IsTrue(result);

            // Prepare Result Folder Path
            string ResultFolder = @"D:\Result\EPAIoT_station_Taichung_Result";
            string TestMethod = TestContext.CurrentContext.Test.Name;
            string TestTime = DateTime.Now.ToString("yyyyMMdd-HHmmss");

            // Write File Headers for Dic_Qmt Result
            string ResultFileName = "Dic_Qmt";
            string ResultFilePath = ResultFolder + "\\" + TestMethod + "\\" + TestTime + "\\" + ResultFileName + ".csv";
            FileInfo FI = new FileInfo(ResultFilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(ResultFilePath, false))
            {
                file.WriteLine(string.Format("{0},{1},{2}",
                    "TankType",
                    "DataAmount",
                    "DataAvg"));
            }

            // Write File for each Dic_Qmt content
            foreach (KeyValuePair<string, QuickMixTank> entry in Dic_Qmt)
            {
                entry.Value.ComputeAvg();
                using (var file = new StreamWriter(ResultFilePath, true))
                {
                    file.WriteLine(string.Format("{0},{1},{2}",
                        entry.Value.TankID,
                        entry.Value.DataAmount,
                        entry.Value.DataAvg));
                }
            }

            // Write File for each Dic_Qmt content
            ResultFileName = "Hour_01_AllArea";
            ResultFilePath = ResultFolder + "\\" + TestMethod + "\\" + TestTime + "\\" + ResultFileName + ".csv";
            FI = new FileInfo(ResultFilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(ResultFilePath, true))
            {
                QuickMixTank Qmt = Dic_Qmt[ResultFileName];
                var TsAndDt = Qmt.Ts.Zip(Qmt.List_Dt, (n, w) => new { TS = n, DT = w });
                foreach (var nw in TsAndDt)
                {
                    file.WriteLine(string.Format("{0},{1}", nw.TS, nw.DT.ToString("yyyy/MM/dd HH:mm")));
                }
            }

            // Assert: x6 (年、季、月、週、日、時) for all stations (not only Taichung)
            Assert.AreEqual(24.46821841, Dic_Qmt["Month_01_AllArea"].DataAvg, 0.1);
            Assert.AreEqual(24.46821841, Dic_Qmt["Week_01_AllArea"].DataAvg, 0.1);
            Assert.AreEqual(24.46821841, Dic_Qmt["Day_01_AllArea"].DataAvg, 0.1);
            Assert.AreEqual(20.13677468, Dic_Qmt["Hour_01_AllArea"].DataAvg, 0.1);

            Assert.AreEqual(5308, Dic_Qmt["Hour_01_AllArea"].DataAmount);
            Assert.AreEqual(128801, Dic_Qmt["Month_01_AllArea"].DataAmount);
        }

        [Test]
        public void UC08_AggregateFromQmtToPst()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.FilePath = @"D:\EPA_IoT_Station_Data\2018\201801\" + @"epa_micro_20180101.csv";
            Cfr.Delimiters = new char[] { '\t', ',', '\"' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 1;
            Cfs.DataLinesStartAt = 2;
            Cfs.FooterLinesCount = 0;

            Dal_EPA_IoT_Station Dal = new Dal_EPA_IoT_Station();
            DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

            List<string> DeviceIdList = CreateDeviceIdList();
            List<string> DeviceTypeList = CreateDeviceTypeList();
            List<string> TankIdList = CreateTankIdList(DeviceIdList, DeviceTypeList);
            Dictionary<string, List<string>> DeviceIdList_byType = GetDeviceIdList_byType();
            CreateQmtAndPst(TankIdList,
                out Dictionary<string, QuickMixTank> Dic_Qmt,
                out Dictionary<string, PrimarySedimentationTank> Dic_Pst);

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Dal.Dic_Qmt = Dic_Qmt;
            Dal.DeviceIdList = DeviceIdList;
            Dal.DeviceTypeList = DeviceTypeList;
            Dal.DeviceIdList_byType = DeviceIdList_byType;
            Dal.useDeviceIdList = false;
            Cfr.Dal = Dal;

            // Debug Use
            string ResultFolder = @"D:\Result\EPAIoT_station_Taichung_Result";
            string TestMethod = TestContext.CurrentContext.Test.Name;
            string TestTime = DateTime.Now.ToString("yyyyMMdd-HHmmss");

            string ResultFileName = "Dic_Qmt";
            string ResultFilePath = ResultFolder + "\\" + TestMethod + "\\" + TestTime + "\\" + ResultFileName + ".csv";
            FileInfo FI = new FileInfo(ResultFilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(ResultFilePath, false))
            {
                file.WriteLine(string.Format("{0},{1},{2}", "TankType", "DataAmount", "DataAvg"));
            }

            // 3. Read Csv File and Distribute by Dataline Analysis Logic (20180101)
            bool result = Cfr.ReadCsvFile();
            Assert.IsTrue(result);

            // ComputeAvg & Aggregate (1st Wave)
            foreach (KeyValuePair<string, QuickMixTank> entry in Dic_Qmt)
            {
                // ComputeAvg
                entry.Value.ComputeAvg();
                // Aggregate
                Dic_Pst.TryGetValue(entry.Key, out PrimarySedimentationTank Pst);
                Pst.Aggregate(entry.Value.DataAmount, entry.Value.DataAvg);
                // Reset
                entry.Value.Reset();
            }

            // Prepare 


            // 時間模式檢驗 for all stations (not only Taichung)
            // DataAvg
            Assert.AreEqual(24.46821841, Dic_Pst["Month_01_AllArea"].DataAvg, 0.1);
            Assert.AreEqual(24.46821841, Dic_Pst["Week_01_AllArea"].DataAvg, 0.1);
            Assert.AreEqual(24.46821841, Dic_Pst["Day_01_AllArea"].DataAvg, 0.1);
            Assert.AreEqual(20.13677468, Dic_Pst["Hour_01_AllArea"].DataAvg, 0.1);
            // DataAmount
            Assert.AreEqual(5308, Dic_Pst["Hour_01_AllArea"].DataAmount);
            Assert.AreEqual(128801, Dic_Pst["Month_01_AllArea"].DataAmount);
            // 初值為零
            Assert.AreEqual(0, Dic_Pst["Day_02_AllArea"].DataAvg, 0.1);

            // 空間模式檢驗 for all stations (not only Taichung)
            // Amount = 460, Avg = 22.91086957
            Assert.AreEqual(20.9623431, Dic_Pst["Y2018_6182023037"].DataAvg, 0.1);
            Assert.AreEqual(20.9623431, Dic_Pst["Y2018S04_6182023037"].DataAvg, 0.1);

            // 3. Read Csv File and Distribute by Dataline Analysis Logic (20180102)
            Cfr.FilePath = @"D:\EPA_IoT_Station_Data\2018\201801\" + @"epa_micro_20180102.csv";
            result = Cfr.ReadCsvFile();
            Assert.IsTrue(result);

            // ComputeAvg & Aggregate (2nd Wave)
            foreach (KeyValuePair<string, QuickMixTank> entry in Dic_Qmt)
            {
                // ComputeAvg
                entry.Value.ComputeAvg();

                // Aggregate
                Dic_Pst.TryGetValue(entry.Key, out PrimarySedimentationTank Pst);
                Pst.Aggregate(entry.Value.DataAmount, entry.Value.DataAvg);
                // Reset
                entry.Value.Reset();
            }

            // 時間模式檢驗 for all stations (not only Taichung)
            Assert.AreEqual(25.29205887, Dic_Pst["Month_01_AllArea"].DataAvg, 0.1);     // changed
            Assert.AreEqual(278235, Dic_Pst["Month_01_AllArea"].DataAmount);            // changed
            Assert.AreEqual(24.46821841, Dic_Pst["Week_01_AllArea"].DataAvg, 0.1);     // unchanged
            Assert.AreEqual(128801, Dic_Pst["Week_01_AllArea"].DataAmount);            // unchanged
            Assert.AreEqual(24.46821841, Dic_Pst["Day_01_AllArea"].DataAvg, 0.1);     // unchanged
            Assert.AreEqual(128801, Dic_Pst["Day_01_AllArea"].DataAmount);            // unchanged
            Assert.AreEqual(25.91038130, Dic_Pst["Hour_01_AllArea"].DataAvg, 0.1);     // changed
            Assert.AreEqual(10779, Dic_Pst["Hour_01_AllArea"].DataAmount);             // changed

            // 初值不為零
            // Amount = 149434, Avg = 26.00214811
            Assert.AreEqual(26.00214811, Dic_Pst["Day_02_AllArea"].DataAvg, 0.1);
            Assert.AreEqual(149434, Dic_Pst["Day_02_AllArea"].DataAmount);

            // 空間模式檢驗 for all stations (not only Taichung)
            // N1=460, G1=22.91086957, N2=450, G2=14.22222222, N'=910, G'=18.61428572
            Assert.AreEqual(26.88691099, Dic_Pst["Y2018_6182023037"].DataAvg, 0.1);
            Assert.AreEqual(26.88691099, Dic_Pst["Y2018S04_6182023037"].DataAvg, 0.1);
        }

        [Test]
        public void UC09_Aggregate201801_OneMonth()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.Delimiters = new char[] { '\t', ',', '\"' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 1;
            Cfs.DataLinesStartAt = 2;
            Cfs.FooterLinesCount = 0;

            Dal_EPA_IoT_Station Dal = new Dal_EPA_IoT_Station();
            DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

            List<string> DeviceIdList = CreateDeviceIdList();
            List<string> DeviceTypeList = CreateDeviceTypeList();
            List<string> TankIdList = CreateTankIdList(DeviceIdList, DeviceTypeList);
            Dictionary<string, List<string>> DeviceIdList_byType = GetDeviceIdList_byType();
            CreateQmtAndPst(TankIdList,
                out Dictionary<string, QuickMixTank> Dic_Qmt,
                out Dictionary<string, PrimarySedimentationTank> Dic_Pst);

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Dal.Dic_Qmt = Dic_Qmt;
            Dal.DeviceIdList = DeviceIdList;
            Dal.DeviceTypeList = DeviceTypeList;
            Dal.DeviceIdList_byType = DeviceIdList_byType;
            Dal.useDeviceIdList = true;
            Cfr.Dal = Dal;

            // All the CSV files in specified directory, including its sub-directories.
            string[] files =
                Directory.GetFiles(@"D:\EPA_IoT_Station_Data\2018\201801\", "*.csv", SearchOption.AllDirectories);
            string TestStartTime = DateTime.Now.ToString("yyyyMMdd-HHmm");
            foreach (string InputFilePath in files)
            {
                Cfr.FilePath = InputFilePath;

                // Output File for Pst (Debug: Print each iteration Pst, Release: Print only final iteration Pst)
                // ResultFilePath = TestMethod / TestTime / InputFileName
                string ResultFilePath = @"D:\Result\EPAIoT_station_Taichung_Result\" +
                    TestContext.CurrentContext.Test.Name + @"\" +
                    TestStartTime + @"\" +
                    "Pst_" + Path.GetFileName(InputFilePath);
                FileInfo FI = new FileInfo(ResultFilePath);
                FI.Directory.Create();  // If the directory already exists, this method does nothing.
                using (var file = new StreamWriter(ResultFilePath, false))
                {
                    file.WriteLine(string.Format("{0},{1},{2}", "TankType", "DataAmount", "DataAvg"));
                }

                // 3. Read Csv File and Distribute by Dataline Analysis Logic
                bool result = Cfr.ReadCsvFile();
                Assert.IsTrue(result);

                // ComputeAvg & Aggregate
                foreach (KeyValuePair<string, QuickMixTank> entry in Dic_Qmt)
                {
                    // ComputeAvg
                    entry.Value.ComputeAvg();
                    // Aggregate
                    Dic_Pst.TryGetValue(entry.Key, out PrimarySedimentationTank Pst);
                    Pst.Aggregate(entry.Value.DataAmount, entry.Value.DataAvg);
                    // Print Tank (Debug here)
                    PrintPst(Pst, ResultFilePath);
                    // Reset
                    entry.Value.Reset();
                }
            }
        }

        [Test]
        public void UC10_Aggregate2018_OneYear()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.Delimiters = new char[] { '\t', ',', '\"' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 1;
            Cfs.DataLinesStartAt = 2;
            Cfs.FooterLinesCount = 0;

            Dal_EPA_IoT_Station Dal = new Dal_EPA_IoT_Station();
            DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

            List<string> DeviceIdList = CreateDeviceIdList();
            List<string> DeviceTypeList = CreateDeviceTypeList();
            List<string> TankIdList = CreateTankIdList(DeviceIdList, DeviceTypeList);
            Dictionary<string, List<string>> DeviceIdList_byType = GetDeviceIdList_byType();

            bool Res_CreateQmtAndPst = CreateQmtAndPst(TankIdList,
                out Dictionary<string, QuickMixTank> Dic_Qmt,
                out Dictionary<string, PrimarySedimentationTank> Dic_Pst);
            Assert.IsTrue(Res_CreateQmtAndPst);

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Dal.Dic_Qmt = Dic_Qmt;
            Dal.DeviceIdList = DeviceIdList;
            Dal.DeviceTypeList = DeviceTypeList;
            Dal.DeviceIdList_byType = DeviceIdList_byType;
            Dal.useDeviceIdList = true;
            Cfr.Dal = Dal;

            // Iterate all CSV files in specified directory, including its sub-directories.
            string[] files =
                Directory.GetFiles(@"D:\EPA_IoT_Station_Data\2018\", "*.csv", SearchOption.AllDirectories);
            foreach (string InputFilePath in files)
            {
                Cfr.FilePath = InputFilePath;

                // 3. Read Csv File and Distribute by Dataline Analysis Logic
                bool Res_ReadCsvFile = Cfr.ReadCsvFile();
                Assert.IsTrue(Res_ReadCsvFile);

                // ComputeAvg & Aggregate
                foreach (KeyValuePair<string, QuickMixTank> entry in Dic_Qmt)
                {
                    // ComputeAvg
                    entry.Value.ComputeAvg();
                    // Aggregate
                    Dic_Pst.TryGetValue(entry.Key, out PrimarySedimentationTank Pst);
                    Pst.Aggregate(entry.Value.DataAmount, entry.Value.DataAvg);
                    // Reset
                    entry.Value.Reset();
                }
            }

            // Create File Header for [月x1 + 週x1 + 日x1 + 時x1 + 年x3 + 季x12]
            string ResultFolder = @"D:\Result\EPAIoT_station_Taichung_Result";
            string TestMethod = TestContext.CurrentContext.Test.Name;
            string TestTime = DateTime.Now.ToString("yyyyMMdd-HHmm");
            List<string> TankIdList_forOutput = CreateTankIdList_TemporalMode();
            TankIdList_forOutput.AddRange(CreateTankIdList_SpatialMode());
            foreach (string entry in TankIdList_forOutput)
            {
                string TankId = entry.Split('_')[0];
                string ResultFilePath = ResultFolder + "\\" + TestMethod + "\\" + TestTime + "\\" + TankId + ".csv";
                FileInfo FI = new FileInfo(ResultFilePath);
                FI.Directory.Create();  // If the directory already exists, this method does nothing.
                using (var file = new StreamWriter(ResultFilePath, false))
                {
                    file.WriteLine(string.Format("{0},{1},{2}", "TankId", "DataAmount", "DataAvg"));
                }
            }

            // Create File Content for [月x1 + 週x1 + 日x1 + 時x1 + 年x3 + 季x12]
            foreach (KeyValuePair<string, PrimarySedimentationTank> entry in Dic_Pst)
            {
                // [FileName]_[RowId], e.g., [Y2018S04]_[DeviceId], [Week]_[01_DeviceType], ...
                string TankId = entry.Key.Split('_')[0];
                string ResultFilePath = ResultFolder + "\\" + TestMethod + "\\" + TestTime + "\\" + TankId + ".csv";
                PrintPst(entry.Value, ResultFilePath);
            }
        }

        [Test]
        public void UC11_Aggregate2018to2020_ThreeYears()
        {
            // 1. Creation Management
            CsvFileAnalyzer Cfr = new CsvFileAnalyzer();
            Cfr.Delimiters = new char[] { '\t', ',', '\"' };

            CsvFileStructure Cfs = new CsvFileStructure();
            Cfs.HeaderLineStartAt = 1;
            Cfs.DataLinesStartAt = 2;
            Cfs.FooterLinesCount = 0;

            Dal_EPA_IoT_Station Dal = new Dal_EPA_IoT_Station();
            DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

            List<string> DeviceIdList = CreateDeviceIdList();
            List<string> DeviceTypeList = CreateDeviceTypeList();
            List<string> TankIdList = CreateTankIdList(DeviceIdList, DeviceTypeList);
            Dictionary<string, List<string>> DeviceIdList_byType = GetDeviceIdList_byType();

            bool Res_CreateQmtAndPst = CreateQmtAndPst(TankIdList,
                out Dictionary<string, QuickMixTank> Dic_Qmt,
                out Dictionary<string, PrimarySedimentationTank> Dic_Pst);
            Assert.IsTrue(Res_CreateQmtAndPst);

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Dal.Dic_Qmt = Dic_Qmt;
            Dal.DeviceIdList = DeviceIdList;
            Dal.DeviceTypeList = DeviceTypeList;
            Dal.DeviceIdList_byType = DeviceIdList_byType;
            Dal.useDeviceIdList = true;
            Cfr.Dal = Dal;

            // Iterate all CSV files in specified directory, including its sub-directories.
            string[] files =
                Directory.GetFiles(@"D:\EPA_IoT_Station_Data\", "*.csv", SearchOption.AllDirectories);
            foreach (string InputFilePath in files)
            {
                Cfr.FilePath = InputFilePath;

                // 3. Read Csv File and Distribute by Dataline Analysis Logic
                bool Res_ReadCsvFile = Cfr.ReadCsvFile();
                Assert.IsTrue(Res_ReadCsvFile, "InputFilePath: " + InputFilePath);

                // ComputeAvg & Aggregate
                foreach (KeyValuePair<string, QuickMixTank> entry in Dic_Qmt)
                {
                    // ComputeAvg
                    entry.Value.ComputeAvg();
                    // Aggregate
                    Dic_Pst.TryGetValue(entry.Key, out PrimarySedimentationTank Pst);
                    Pst.Aggregate(entry.Value.DataAmount, entry.Value.DataAvg);
                    // Reset
                    entry.Value.Reset();
                }
            }

            // Create File Header for [月x1 + 週x1 + 日x1 + 時x1 + 年x3 + 季x12]
            string ResultFolder = @"D:\Result\EPAIoT_station_Taichung_Result";
            string TestMethod = TestContext.CurrentContext.Test.Name;
            string TestTime = DateTime.Now.ToString("yyyyMMdd-HHmm");
            List<string> TankIdList_forOutput = CreateTankIdList_TemporalMode();
            TankIdList_forOutput.AddRange(CreateTankIdList_SpatialMode());
            foreach (string entry in TankIdList_forOutput)
            {
                string TankId = entry.Split('_')[0];
                string ResultFilePath = ResultFolder + "\\" + TestMethod + "\\" + TestTime + "\\" + TankId + ".csv";
                FileInfo FI = new FileInfo(ResultFilePath);
                FI.Directory.Create();  // If the directory already exists, this method does nothing.
                using (var file = new StreamWriter(ResultFilePath, false))
                {
                    file.WriteLine(string.Format("{0},{1},{2}", "TankId", "DataAmount", "DataAvg"));
                }
            }

            // Create File Content for [月x1 + 週x1 + 日x1 + 時x1 + 年x3 + 季x12]
            foreach (KeyValuePair<string, PrimarySedimentationTank> entry in Dic_Pst)
            {
                // [FileName]_[RowId], e.g., [Y2018S04]_[DeviceId], [Week]_[01_DeviceType], 
                string TankId = entry.Key.Split('_')[0];
                string ResultFilePath = ResultFolder + "\\" + TestMethod + "\\" + TestTime + "\\" + TankId + ".csv";
                PrintPst(entry.Value, ResultFilePath);
            }
        }

        private Dictionary<string, List<string>> GetDeviceIdList_byType()
        {
            Dictionary<string, List<string>> DeviceIdList_byType = new Dictionary<string, List<string>>();
            #region Add "TrafficArea"
            List<string> TrafficArea = new List<string>();
            TrafficArea.Add("7782112149");
            TrafficArea.Add("10249876890");
            TrafficArea.Add("10267398180");
            TrafficArea.Add("10244913793");
            TrafficArea.Add("10253565369");
            TrafficArea.Add("10245703313");
            TrafficArea.Add("10253915817");
            TrafficArea.Add("10251436077");
            TrafficArea.Add("10246934296");
            TrafficArea.Add("10257280434");
            TrafficArea.Add("10240008501");
            TrafficArea.Add("10242171932");
            TrafficArea.Add("10246874791");
            TrafficArea.Add("10253879246");
            TrafficArea.Add("10241142002");
            TrafficArea.Add("10245151362");
            TrafficArea.Add("10246506970");
            TrafficArea.Add("10254380805");
            TrafficArea.Add("10252223623");
            TrafficArea.Add("10256417782");
            TrafficArea.Add("10257725115");
            TrafficArea.Add("10239120368");
            TrafficArea.Add("10248997728");
            TrafficArea.Add("10250759781");
            TrafficArea.Add("10244605822");
            TrafficArea.Add("10244577830");
            TrafficArea.Add("10249717168");
            TrafficArea.Add("10248800420");
            TrafficArea.Add("10252616931");
            TrafficArea.Add("10249134433");
            TrafficArea.Add("10250452920");
            TrafficArea.Add("10251021017");
            TrafficArea.Add("10250355744");
            TrafficArea.Add("10247617538");
            TrafficArea.Add("10249566412");
            TrafficArea.Add("10251754410");
            TrafficArea.Add("10249482907");
            TrafficArea.Add("10250204781");
            TrafficArea.Add("10252908808");
            TrafficArea.Add("10245084314");
            TrafficArea.Add("10239097872");
            TrafficArea.Add("10238440342");
            TrafficArea.Add("10236803538");
            TrafficArea.Add("10233740567");
            TrafficArea.Add("10236609821");
            TrafficArea.Add("10232560258");
            TrafficArea.Add("10258359938");
            TrafficArea.Add("10236336459");
            TrafficArea.Add("10238792140");
            TrafficArea.Add("10237567392");
            TrafficArea.Add("10232839597");
            TrafficArea.Add("10234365835");
            TrafficArea.Add("10233817722");
            TrafficArea.Add("10235893007");
            TrafficArea.Add("10234586373");
            TrafficArea.Add("10238312092");
            TrafficArea.Add("10232916032");
            TrafficArea.Add("10233088203");
            TrafficArea.Add("10234710685");
            TrafficArea.Add("10234625609");
            TrafficArea.Add("10250510364");
            TrafficArea.Add("10238965139");
            TrafficArea.Add("10236206370");
            TrafficArea.Add("10238020632");
            TrafficArea.Add("10249603842");
            TrafficArea.Add("10233623195");
            TrafficArea.Add("10238849376");
            TrafficArea.Add("10232760701");
            TrafficArea.Add("10252847747");
            TrafficArea.Add("10233456215");
            TrafficArea.Add("10237701460");
            TrafficArea.Add("10233263561");
            TrafficArea.Add("10234968107");
            TrafficArea.Add("10254501820");
            TrafficArea.Add("10246444737");
            TrafficArea.Add("10247786889");
            TrafficArea.Add("6232140628");
            TrafficArea.Add("6244972103");
            TrafficArea.Add("6239260498");
            TrafficArea.Add("6229853346");
            TrafficArea.Add("6233189651");
            TrafficArea.Add("6232992646");
            TrafficArea.Add("6232779192");
            TrafficArea.Add("6227110910");
            TrafficArea.Add("6227323190");
            TrafficArea.Add("6231946778");
            TrafficArea.Add("6228310872");
            TrafficArea.Add("6241910435");
            TrafficArea.Add("6238532767");
            TrafficArea.Add("6242322884");
            TrafficArea.Add("6243075267");
            TrafficArea.Add("6235562361");
            TrafficArea.Add("6232477761");
            TrafficArea.Add("6240599138");
            TrafficArea.Add("6227537963");
            TrafficArea.Add("6227842173");
            TrafficArea.Add("10236581498");
            DeviceIdList_byType.Add("TrafficArea", TrafficArea);
            #endregion

            #region Add "IndustrialArea"
            List<string> IndustrialArea = new List<string>();
            IndustrialArea.Add("6240072114");
            IndustrialArea.Add("10248126724");
            IndustrialArea.Add("10239386051");
            IndustrialArea.Add("10240872207");
            IndustrialArea.Add("10240416140");
            IndustrialArea.Add("10243136164");
            IndustrialArea.Add("10246102258");
            IndustrialArea.Add("10241218475");
            IndustrialArea.Add("10242646750");
            IndustrialArea.Add("10256580700");
            IndustrialArea.Add("10242598362");
            IndustrialArea.Add("10240275398");
            IndustrialArea.Add("10242340321");
            IndustrialArea.Add("10240629640");
            IndustrialArea.Add("10242009511");
            IndustrialArea.Add("10243540697");
            IndustrialArea.Add("10244311954");
            IndustrialArea.Add("10241347900");
            IndustrialArea.Add("10244491842");
            IndustrialArea.Add("10241766010");
            IndustrialArea.Add("10241966726");
            IndustrialArea.Add("10242429139");
            IndustrialArea.Add("10243225590");
            IndustrialArea.Add("10247011851");
            IndustrialArea.Add("10244025543");
            IndustrialArea.Add("6207564493");
            IndustrialArea.Add("10240960298");
            IndustrialArea.Add("10257145180");
            IndustrialArea.Add("6204640213");
            IndustrialArea.Add("10242803915");
            IndustrialArea.Add("10239290211");
            IndustrialArea.Add("10242974229");
            IndustrialArea.Add("10248655362");
            IndustrialArea.Add("10251875550");
            IndustrialArea.Add("10246046479");
            IndustrialArea.Add("10267581123");
            IndustrialArea.Add("10245663645");
            IndustrialArea.Add("10266614326");
            IndustrialArea.Add("10254401338");
            IndustrialArea.Add("10246630667");
            IndustrialArea.Add("10253642943");
            IndustrialArea.Add("10237145655");
            IndustrialArea.Add("10264481763");
            IndustrialArea.Add("10265478203");
            IndustrialArea.Add("10256390029");
            IndustrialArea.Add("10262050526");
            IndustrialArea.Add("10263486603");
            IndustrialArea.Add("10259705029");
            IndustrialArea.Add("10261653315");
            IndustrialArea.Add("10262181091");
            IndustrialArea.Add("10263029046");
            IndustrialArea.Add("10257821935");
            IndustrialArea.Add("10258635925");
            IndustrialArea.Add("10262725307");
            IndustrialArea.Add("10264355041");
            IndustrialArea.Add("10266129234");
            IndustrialArea.Add("10263278503");
            IndustrialArea.Add("10245503162");
            IndustrialArea.Add("10265554161");
            IndustrialArea.Add("10256072648");
            IndustrialArea.Add("10265334020");
            IndustrialArea.Add("10258908074");
            IndustrialArea.Add("10249335573");
            IndustrialArea.Add("10259840993");
            IndustrialArea.Add("10259456945");
            IndustrialArea.Add("10263710892");
            IndustrialArea.Add("10244299340");
            IndustrialArea.Add("10264535085");
            IndustrialArea.Add("10263169109");
            IndustrialArea.Add("10262686650");
            IndustrialArea.Add("10262855425");
            IndustrialArea.Add("10257920204");
            IndustrialArea.Add("10259029279");
            IndustrialArea.Add("10262448890");
            IndustrialArea.Add("10261891910");
            IndustrialArea.Add("10245361644");
            IndustrialArea.Add("10248706692");
            IndustrialArea.Add("10250954893");
            IndustrialArea.Add("10261747402");
            IndustrialArea.Add("10252417543");
            IndustrialArea.Add("10234160359");
            IndustrialArea.Add("10264964139");
            IndustrialArea.Add("10261434653");
            IndustrialArea.Add("10242745932");
            IndustrialArea.Add("10238653354");
            IndustrialArea.Add("10237815893");
            IndustrialArea.Add("10243968317");
            IndustrialArea.Add("10265935756");
            IndustrialArea.Add("10244713183");
            IndustrialArea.Add("10233184179");
            IndustrialArea.Add("10258079140");
            IndustrialArea.Add("10263302988");
            IndustrialArea.Add("10259984638");
            IndustrialArea.Add("10258288573");
            IndustrialArea.Add("10265869456");
            IndustrialArea.Add("10261973167");
            IndustrialArea.Add("10262249504");
            IndustrialArea.Add("10264023529");
            IndustrialArea.Add("10257544322");
            IndustrialArea.Add("10267145946");
            IndustrialArea.Add("10238163008");
            IndustrialArea.Add("10265011477");
            IndustrialArea.Add("10240329053");
            IndustrialArea.Add("10247502151");
            IndustrialArea.Add("10247438786");
            IndustrialArea.Add("10237688498");
            IndustrialArea.Add("10241061603");
            IndustrialArea.Add("10259624194");
            IndustrialArea.Add("10236095866");
            IndustrialArea.Add("10263665206");
            IndustrialArea.Add("10237013988");
            IndustrialArea.Add("10247920216");
            IndustrialArea.Add("10258176549");
            IndustrialArea.Add("10262553855");
            IndustrialArea.Add("10236748581");
            IndustrialArea.Add("10241583401");
            IndustrialArea.Add("10264272682");
            IndustrialArea.Add("10235001897");
            IndustrialArea.Add("10250060633");
            IndustrialArea.Add("10266897572");
            IndustrialArea.Add("10246373093");
            IndustrialArea.Add("10250185638");
            IndustrialArea.Add("10267016765");
            IndustrialArea.Add("10249995574");
            IndustrialArea.Add("10252517759");
            IndustrialArea.Add("10252310112");
            IndustrialArea.Add("10250860650");
            IndustrialArea.Add("10253017242");
            IndustrialArea.Add("10233340370");
            IndustrialArea.Add("10238558174");
            IndustrialArea.Add("10265231825");
            IndustrialArea.Add("10235324367");
            IndustrialArea.Add("10258712314");
            IndustrialArea.Add("10259259031");
            IndustrialArea.Add("10258500246");
            IndustrialArea.Add("10253260031");
            IndustrialArea.Add("10265624958");
            IndustrialArea.Add("10260043742");
            IndustrialArea.Add("10259339656");
            IndustrialArea.Add("10265172676");
            IndustrialArea.Add("10264146075");
            IndustrialArea.Add("10265793653");
            IndustrialArea.Add("10236997574");
            IndustrialArea.Add("10263808764");
            IndustrialArea.Add("10246250679");
            IndustrialArea.Add("10251231255");
            IndustrialArea.Add("10258416364");
            IndustrialArea.Add("6208491748");
            IndustrialArea.Add("6206552528");
            IndustrialArea.Add("6201524114");
            IndustrialArea.Add("6172927386");
            IndustrialArea.Add("6212006825");
            IndustrialArea.Add("6171278726");
            IndustrialArea.Add("6201335822");
            IndustrialArea.Add("6170988978");
            IndustrialArea.Add("6226666876");
            IndustrialArea.Add("6222977480");
            IndustrialArea.Add("6218588677");
            IndustrialArea.Add("6244368394");
            IndustrialArea.Add("6226375779");
            IndustrialArea.Add("6235437869");
            IndustrialArea.Add("6171173697");
            IndustrialArea.Add("6173816125");
            IndustrialArea.Add("6170644194");
            IndustrialArea.Add("6178996233");
            IndustrialArea.Add("6218689018");
            IndustrialArea.Add("6219878466");
            IndustrialArea.Add("6233351858");
            IndustrialArea.Add("6175361960");
            IndustrialArea.Add("6222332931");
            IndustrialArea.Add("6181754015");
            IndustrialArea.Add("6171874334");
            IndustrialArea.Add("6177453601");
            IndustrialArea.Add("6221617715");
            IndustrialArea.Add("6174764830");
            IndustrialArea.Add("6232673827");
            IndustrialArea.Add("6211182359");
            IndustrialArea.Add("6211248136");
            IndustrialArea.Add("6179004739");
            IndustrialArea.Add("6222150001");
            IndustrialArea.Add("6171999223");
            IndustrialArea.Add("6210741194");
            IndustrialArea.Add("6176606512");
            IndustrialArea.Add("6177854105");
            IndustrialArea.Add("6173704280");
            IndustrialArea.Add("6207293578");
            IndustrialArea.Add("6206690053");
            IndustrialArea.Add("6208392234");
            IndustrialArea.Add("6219001111");
            IndustrialArea.Add("6175154889");
            IndustrialArea.Add("6208073033");
            IndustrialArea.Add("6220532668");
            IndustrialArea.Add("6221036526");
            IndustrialArea.Add("6207422912");
            IndustrialArea.Add("6208106918");
            IndustrialArea.Add("6203850001");
            IndustrialArea.Add("6235757154");
            IndustrialArea.Add("6206820413");
            IndustrialArea.Add("6221298468");
            IndustrialArea.Add("6176281540");
            IndustrialArea.Add("6208886319");
            IndustrialArea.Add("6208778561");
            IndustrialArea.Add("6206486190");
            IndustrialArea.Add("6181939798");
            IndustrialArea.Add("6207993456");
            IndustrialArea.Add("6207773627");
            IndustrialArea.Add("6207364725");
            IndustrialArea.Add("6244708588");
            IndustrialArea.Add("6223072367");
            IndustrialArea.Add("6206204805");
            IndustrialArea.Add("6204894886");
            IndustrialArea.Add("6206704365");
            IndustrialArea.Add("6211524198");
            IndustrialArea.Add("6210524986");
            IndustrialArea.Add("6212562290");
            IndustrialArea.Add("6211037997");
            IndustrialArea.Add("6212945325");
            IndustrialArea.Add("6212397163");
            IndustrialArea.Add("6210183557");
            IndustrialArea.Add("6210498840");
            IndustrialArea.Add("6181356136");
            IndustrialArea.Add("6181100138");
            IndustrialArea.Add("6179199537");
            IndustrialArea.Add("6178700795");
            IndustrialArea.Add("6204564670");
            IndustrialArea.Add("6177966845");
            IndustrialArea.Add("6203931876");
            IndustrialArea.Add("6202062910");
            IndustrialArea.Add("6176870215");
            IndustrialArea.Add("6202118053");
            IndustrialArea.Add("6201492301");
            IndustrialArea.Add("6204337453");
            IndustrialArea.Add("6176132315");
            IndustrialArea.Add("6204095761");
            IndustrialArea.Add("6171517419");
            IndustrialArea.Add("6204462904");
            IndustrialArea.Add("6178374774");
            IndustrialArea.Add("6202288936");
            IndustrialArea.Add("6204710281");
            IndustrialArea.Add("6665272821");
            IndustrialArea.Add("6202383967");
            IndustrialArea.Add("6170479504");
            IndustrialArea.Add("6180944395");
            IndustrialArea.Add("6205010682");
            IndustrialArea.Add("6202987339");
            IndustrialArea.Add("6202881113");
            IndustrialArea.Add("6209655053");
            IndustrialArea.Add("6201080763");
            IndustrialArea.Add("6201125435");
            IndustrialArea.Add("6202590634");
            IndustrialArea.Add("6202680053");
            IndustrialArea.Add("6202438562");
            IndustrialArea.Add("6203405861");
            IndustrialArea.Add("6179285675");
            IndustrialArea.Add("6205259163");
            IndustrialArea.Add("6205107975");
            IndustrialArea.Add("6204262565");
            IndustrialArea.Add("6205323118");
            IndustrialArea.Add("6178045816");
            IndustrialArea.Add("6179839647");
            IndustrialArea.Add("6171080763");
            IndustrialArea.Add("6201252883");
            IndustrialArea.Add("6181554044");
            IndustrialArea.Add("6181235830");
            IndustrialArea.Add("6180327867");
            IndustrialArea.Add("6175537920");
            IndustrialArea.Add("6201633906");
            IndustrialArea.Add("6172045596");
            IndustrialArea.Add("6175415111");
            IndustrialArea.Add("6200907176");
            IndustrialArea.Add("6170511673");
            IndustrialArea.Add("6172251404");
            IndustrialArea.Add("6172819559");
            IndustrialArea.Add("6203372597");
            IndustrialArea.Add("6202746480");
            IndustrialArea.Add("6235398605");
            IndustrialArea.Add("6226884006");
            IndustrialArea.Add("6211482058");
            IndustrialArea.Add("6238991310");
            IndustrialArea.Add("6238049673");
            IndustrialArea.Add("6228812932");
            IndustrialArea.Add("6237927372");
            IndustrialArea.Add("6236408387");
            IndustrialArea.Add("6245647067");
            IndustrialArea.Add("6237387071");
            IndustrialArea.Add("6241043227");
            IndustrialArea.Add("6234848409");
            IndustrialArea.Add("6233471223");
            IndustrialArea.Add("6238127527");
            IndustrialArea.Add("6241569662");
            IndustrialArea.Add("6234614695");
            IndustrialArea.Add("6240368695");
            IndustrialArea.Add("6230941521");
            IndustrialArea.Add("6239453250");
            IndustrialArea.Add("6240930004");
            IndustrialArea.Add("6233857572");
            IndustrialArea.Add("6236081087");
            IndustrialArea.Add("6245586887");
            IndustrialArea.Add("6237506236");
            IndustrialArea.Add("6226939495");
            IndustrialArea.Add("6229082773");
            IndustrialArea.Add("6237781455");
            IndustrialArea.Add("6246054126");
            IndustrialArea.Add("6225566514");
            IndustrialArea.Add("6228950270");
            IndustrialArea.Add("6228636232");
            IndustrialArea.Add("6230240450");
            IndustrialArea.Add("6237189035");
            IndustrialArea.Add("6224914687");
            IndustrialArea.Add("6238399852");
            IndustrialArea.Add("6243834493");
            IndustrialArea.Add("6235229032");
            IndustrialArea.Add("6226554446");
            IndustrialArea.Add("6236799288");
            IndustrialArea.Add("6242469253");
            IndustrialArea.Add("6238210158");
            IndustrialArea.Add("6218254567");
            IndustrialArea.Add("6237668574");
            IndustrialArea.Add("6228405280");
            IndustrialArea.Add("6218402962");
            IndustrialArea.Add("6232254541");
            IndustrialArea.Add("6225011500");
            IndustrialArea.Add("6221487384");
            IndustrialArea.Add("6219695422");
            IndustrialArea.Add("6173626634");
            IndustrialArea.Add("6222043482");
            IndustrialArea.Add("6170369574");
            IndustrialArea.Add("6213243025");
            IndustrialArea.Add("6181695067");
            IndustrialArea.Add("6281023536");
            IndustrialArea.Add("6220269632");
            IndustrialArea.Add("6209992732");
            IndustrialArea.Add("6237212914");
            IndustrialArea.Add("6300997376");
            IndustrialArea.Add("6221347298");
            IndustrialArea.Add("6244091139");
            IndustrialArea.Add("6181882927");
            IndustrialArea.Add("6179654387");
            IndustrialArea.Add("6231453744");
            IndustrialArea.Add("6240442620");
            IndustrialArea.Add("6172367777");
            IndustrialArea.Add("6243423203");
            IndustrialArea.Add("6173541534");
            IndustrialArea.Add("6225659069");
            IndustrialArea.Add("6241698404");
            IndustrialArea.Add("6227290571");
            IndustrialArea.Add("6218929645");
            IndustrialArea.Add("6210078747");
            IndustrialArea.Add("6212480146");
            IndustrialArea.Add("6177265783");
            IndustrialArea.Add("6179327308");
            IndustrialArea.Add("6176546298");
            IndustrialArea.Add("6174523355");
            IndustrialArea.Add("6218757213");
            IndustrialArea.Add("6234034768");
            IndustrialArea.Add("6172581936");
            IndustrialArea.Add("6170777118");
            IndustrialArea.Add("6734009828");
            IndustrialArea.Add("6178576524");
            IndustrialArea.Add("6229304336");
            IndustrialArea.Add("10259529107");
            IndustrialArea.Add("10239486340");
            IndustrialArea.Add("10264666051");
            IndustrialArea.Add("6243669136");
            IndustrialArea.Add("6219218058");
            IndustrialArea.Add("6173343636");
            IndustrialArea.Add("6176407315");
            IndustrialArea.Add("10251343896");
            IndustrialArea.Add("10257682951");
            IndustrialArea.Add("6245846003");
            IndustrialArea.Add("6219300143");
            IndustrialArea.Add("6222621233");
            IndustrialArea.Add("6182023037");
            IndustrialArea.Add("6180518061");
            IndustrialArea.Add("6213148077");
            IndustrialArea.Add("6220907808");
            IndustrialArea.Add("6217593017");
            IndustrialArea.Add("6217629621");
            IndustrialArea.Add("6212848475");
            IndustrialArea.Add("6173021158");
            IndustrialArea.Add("6180077214");
            IndustrialArea.Add("6172485084");
            IndustrialArea.Add("10257043484");
            IndustrialArea.Add("10245424335");
            IndustrialArea.Add("6180470321");
            IndustrialArea.Add("6211763679");
            IndustrialArea.Add("6237059567");
            IndustrialArea.Add("6217949795");
            IndustrialArea.Add("6239096570");
            IndustrialArea.Add("6180601972");
            IndustrialArea.Add("6207044520");
            IndustrialArea.Add("6240656770");
            IndustrialArea.Add("6208517888");
            IndustrialArea.Add("6245270680");
            IndustrialArea.Add("6207890116");
            IndustrialArea.Add("6208299987");
            IndustrialArea.Add("6206950058");
            IndustrialArea.Add("6207138487");
            IndustrialArea.Add("6177725880");
            IndustrialArea.Add("6170852684");
            IndustrialArea.Add("6207679133");
            IndustrialArea.Add("6204138710");
            IndustrialArea.Add("6206340296");
            IndustrialArea.Add("6208697466");
            IndustrialArea.Add("6203673165");
            IndustrialArea.Add("6181454800");
            IndustrialArea.Add("6203223552");
            IndustrialArea.Add("6182185799");
            IndustrialArea.Add("6174400378");
            IndustrialArea.Add("6204979816");
            IndustrialArea.Add("6178439669");
            IndustrialArea.Add("6201952137");
            IndustrialArea.Add("6203127880");
            IndustrialArea.Add("6203519167");
            IndustrialArea.Add("6201856818");
            IndustrialArea.Add("6177160756");
            IndustrialArea.Add("6203775652");
            IndustrialArea.Add("6175817505");
            IndustrialArea.Add("6178877334");
            IndustrialArea.Add("6178145504");
            IndustrialArea.Add("6175789606");
            IndustrialArea.Add("6203040092");
            IndustrialArea.Add("6176318024");
            IndustrialArea.Add("6180852322");
            IndustrialArea.Add("6175052291");
            IndustrialArea.Add("6174615384");
            IndustrialArea.Add("6229412989");
            IndustrialArea.Add("6239132534");
            IndustrialArea.Add("6210933544");
            IndustrialArea.Add("6211840796");
            IndustrialArea.Add("6211353090");
            IndustrialArea.Add("6213053800");
            IndustrialArea.Add("6218889355");
            IndustrialArea.Add("6220629506");
            IndustrialArea.Add("6217487544");
            IndustrialArea.Add("6175645216");
            IndustrialArea.Add("6239765735");
            IndustrialArea.Add("6235074938");
            IndustrialArea.Add("6238885367");
            IndustrialArea.Add("6233970390");
            IndustrialArea.Add("6245332461");
            IndustrialArea.Add("6221795973");
            IndustrialArea.Add("6176999336");
            IndustrialArea.Add("6229244764");
            IndustrialArea.Add("6220040748");
            IndustrialArea.Add("6219776330");
            IndustrialArea.Add("6172629251");
            IndustrialArea.Add("6173195849");
            IndustrialArea.Add("6238654030");
            IndustrialArea.Add("6236238291");
            IndustrialArea.Add("6229712569");
            IndustrialArea.Add("6232887063");
            IndustrialArea.Add("6222895282");
            IndustrialArea.Add("6230680423");
            IndustrialArea.Add("6243536602");
            IndustrialArea.Add("6220466572");
            IndustrialArea.Add("6221578795");
            IndustrialArea.Add("6174191442");
            IndustrialArea.Add("6171766699");
            IndustrialArea.Add("6231743614");
            IndustrialArea.Add("6242672729");
            IndustrialArea.Add("6217883961");
            IndustrialArea.Add("6174854904");
            IndustrialArea.Add("6219114065");
            IndustrialArea.Add("6224264022");
            IndustrialArea.Add("6230309523");
            IndustrialArea.Add("6176075694");
            IndustrialArea.Add("6221183541");
            IndustrialArea.Add("6177020493");
            IndustrialArea.Add("6233027183");
            IndustrialArea.Add("6218105858");
            IndustrialArea.Add("6176760340");
            IndustrialArea.Add("6231147598");
            IndustrialArea.Add("6212250069");
            IndustrialArea.Add("6182299831");
            IndustrialArea.Add("6228134874");
            IndustrialArea.Add("6212726347");
            IndustrialArea.Add("6213450153");
            IndustrialArea.Add("6210817291");
            IndustrialArea.Add("6223146608");
            IndustrialArea.Add("6221860040");
            DeviceIdList_byType.Add("IndustrialArea", IndustrialArea);
            #endregion

            #region Add "ResidentialArea"
            List<string> ResidentialArea = new List<string>();
            ResidentialArea.Add("10241484288");
            ResidentialArea.Add("10266340000");
            ResidentialArea.Add("10242247858");
            ResidentialArea.Add("10252783870");
            ResidentialArea.Add("10253128546");
            ResidentialArea.Add("10249276644");
            ResidentialArea.Add("10266411032");
            ResidentialArea.Add("10248417464");
            ResidentialArea.Add("10243642553");
            ResidentialArea.Add("10245812926");
            ResidentialArea.Add("10246721324");
            ResidentialArea.Add("10245924980");
            ResidentialArea.Add("10239544549");
            ResidentialArea.Add("10239626131");
            ResidentialArea.Add("10255540934");
            ResidentialArea.Add("10243467794");
            ResidentialArea.Add("10260613719");
            ResidentialArea.Add("10240176513");
            ResidentialArea.Add("10247230633");
            ResidentialArea.Add("10251957562");
            ResidentialArea.Add("10241807377");
            ResidentialArea.Add("10266991225");
            ResidentialArea.Add("10256611580");
            ResidentialArea.Add("10257360104");
            ResidentialArea.Add("10267779953");
            ResidentialArea.Add("10243744003");
            ResidentialArea.Add("10260760011");
            ResidentialArea.Add("10244839650");
            ResidentialArea.Add("10249086546");
            ResidentialArea.Add("10254114735");
            ResidentialArea.Add("10243827947");
            ResidentialArea.Add("10239973579");
            ResidentialArea.Add("10251503446");
            ResidentialArea.Add("10239759109");
            ResidentialArea.Add("10266506754");
            ResidentialArea.Add("10244122238");
            ResidentialArea.Add("10254626174");
            ResidentialArea.Add("10257473582");
            ResidentialArea.Add("10267299982");
            ResidentialArea.Add("10245263284");
            ResidentialArea.Add("10264820527");
            ResidentialArea.Add("10259106423");
            ResidentialArea.Add("10266015972");
            ResidentialArea.Add("10260213368");
            ResidentialArea.Add("10260933084");
            ResidentialArea.Add("10255043733");
            ResidentialArea.Add("10263576512");
            ResidentialArea.Add("10256112887");
            ResidentialArea.Add("10260113433");
            ResidentialArea.Add("10254800374");
            ResidentialArea.Add("10264702299");
            ResidentialArea.Add("10254700957");
            ResidentialArea.Add("10266701841");
            ResidentialArea.Add("10267486598");
            ResidentialArea.Add("10260516075");
            ResidentialArea.Add("10256253497");
            ResidentialArea.Add("10263920604");
            ResidentialArea.Add("10262386419");
            ResidentialArea.Add("10261392289");
            ResidentialArea.Add("10262984815");
            ResidentialArea.Add("10254930071");
            ResidentialArea.Add("10260366775");
            ResidentialArea.Add("10258864685");
            ResidentialArea.Add("10260881033");
            ResidentialArea.Add("10256701032");
            ResidentialArea.Add("10248376647");
            ResidentialArea.Add("10251642794");
            ResidentialArea.Add("10240539651");
            ResidentialArea.Add("10253379919");
            ResidentialArea.Add("10241612373");
            ResidentialArea.Add("10247146639");
            ResidentialArea.Add("10243381987");
            ResidentialArea.Add("10256911387");
            ResidentialArea.Add("10256854452");
            ResidentialArea.Add("10250619145");
            ResidentialArea.Add("10240764842");
            ResidentialArea.Add("10251161256");
            ResidentialArea.Add("10254028197");
            ResidentialArea.Add("10267634930");
            ResidentialArea.Add("10266249460");
            ResidentialArea.Add("10239828865");
            ResidentialArea.Add("10248039486");
            ResidentialArea.Add("10235201894");
            ResidentialArea.Add("10237237284");
            ResidentialArea.Add("10248293521");
            ResidentialArea.Add("10254288683");
            ResidentialArea.Add("10247853515");
            ResidentialArea.Add("10252121215");
            ResidentialArea.Add("10253451685");
            ResidentialArea.Add("10238283857");
            ResidentialArea.Add("10237325556");
            ResidentialArea.Add("10235407215");
            ResidentialArea.Add("10232655835");
            ResidentialArea.Add("10234255865");
            ResidentialArea.Add("10234070928");
            ResidentialArea.Add("10236436247");
            ResidentialArea.Add("10235945446");
            ResidentialArea.Add("10237478630");
            ResidentialArea.Add("10236151834");
            ResidentialArea.Add("10235599348");
            ResidentialArea.Add("10233901196");
            ResidentialArea.Add("10234821436");
            ResidentialArea.Add("10237967492");
            ResidentialArea.Add("10235132922");
            ResidentialArea.Add("10234405930");
            ResidentialArea.Add("6231656272");
            ResidentialArea.Add("6231098742");
            ResidentialArea.Add("6180170091");
            ResidentialArea.Add("6236980012");
            ResidentialArea.Add("6181070276");
            ResidentialArea.Add("6219436037");
            ResidentialArea.Add("6244630424");
            ResidentialArea.Add("6219908917");
            ResidentialArea.Add("6173911150");
            ResidentialArea.Add("6210315358");
            ResidentialArea.Add("6245011123");
            ResidentialArea.Add("6234476267");
            ResidentialArea.Add("6239694957");
            ResidentialArea.Add("6240121958");
            ResidentialArea.Add("6234382586");
            ResidentialArea.Add("6236647522");
            ResidentialArea.Add("6242741149");
            ResidentialArea.Add("6241169523");
            ResidentialArea.Add("6241489399");
            ResidentialArea.Add("6232078920");
            ResidentialArea.Add("6230508072");
            ResidentialArea.Add("6227051815");
            ResidentialArea.Add("6232386523");
            ResidentialArea.Add("6245123633");
            ResidentialArea.Add("6241808941");
            ResidentialArea.Add("6230748013");
            ResidentialArea.Add("6210658833");
            ResidentialArea.Add("6177340025");
            ResidentialArea.Add("6174945966");
            ResidentialArea.Add("6180255516");
            ResidentialArea.Add("6225736125");
            ResidentialArea.Add("6178641010");
            ResidentialArea.Add("6172101879");
            ResidentialArea.Add("6180731755");
            ResidentialArea.Add("6174298410");
            ResidentialArea.Add("6201786838");
            ResidentialArea.Add("6179529044");
            ResidentialArea.Add("6179469631");
            ResidentialArea.Add("6242078378");
            ResidentialArea.Add("6177503710");
            ResidentialArea.Add("6230007561");
            ResidentialArea.Add("6242910593");
            ResidentialArea.Add("6179753931");
            ResidentialArea.Add("6213353995");
            ResidentialArea.Add("6220874631");
            ResidentialArea.Add("6275437912");
            ResidentialArea.Add("6244566439");
            ResidentialArea.Add("6231365445");
            ResidentialArea.Add("6235624366");
            ResidentialArea.Add("6240276717");
            ResidentialArea.Add("6231529004");
            ResidentialArea.Add("6232549562");
            ResidentialArea.Add("6240735130");
            ResidentialArea.Add("6244469967");
            ResidentialArea.Add("6234730726");
            ResidentialArea.Add("6243271703");
            ResidentialArea.Add("6237402965");
            ResidentialArea.Add("6239902461");
            ResidentialArea.Add("6225110909");
            ResidentialArea.Add("6233672784");
            ResidentialArea.Add("10247325283");
            ResidentialArea.Add("10248521745");
            ResidentialArea.Add("10252051252");
            ResidentialArea.Add("6234902334");
            ResidentialArea.Add("6243944350");
            ResidentialArea.Add("10253760218");
            ResidentialArea.Add("10243057549");
            ResidentialArea.Add("6242281657");
            ResidentialArea.Add("6224828663");
            ResidentialArea.Add("6210256605");
            ResidentialArea.Add("6219549704");
            ResidentialArea.Add("6222795523");
            ResidentialArea.Add("6222561082");
            ResidentialArea.Add("6227666850");
            ResidentialArea.Add("6173417043");
            ResidentialArea.Add("6171499477");
            ResidentialArea.Add("6174022245");
            ResidentialArea.Add("6229543551");
            ResidentialArea.Add("6174382248");
            ResidentialArea.Add("6178223087");
            ResidentialArea.Add("6171610758");
            ResidentialArea.Add("6175910936");
            ResidentialArea.Add("6175293430");
            ResidentialArea.Add("6171387123");
            ResidentialArea.Add("6227498199");
            ResidentialArea.Add("6179956220");
            ResidentialArea.Add("6173244138");
            ResidentialArea.Add("6177683510");
            ResidentialArea.Add("6172711849");
            ResidentialArea.Add("6211660429");
            ResidentialArea.Add("6240867207");
            ResidentialArea.Add("6211949360");
            ResidentialArea.Add("6222282508");
            ResidentialArea.Add("6217782347");
            ResidentialArea.Add("6222453027");
            ResidentialArea.Add("6213788350");
            ResidentialArea.Add("6213547179");
            ResidentialArea.Add("6220337631");
            ResidentialArea.Add("6220132030");
            ResidentialArea.Add("6221910304");
            ResidentialArea.Add("6212618457");
            ResidentialArea.Add("6220766553");
            ResidentialArea.Add("6225205864");
            ResidentialArea.Add("6212185966");
            ResidentialArea.Add("6218071331");
            ResidentialArea.Add("6213695310");
            ResidentialArea.Add("6218341831");
            ResidentialArea.Add("6225852947");
            ResidentialArea.Add("6226165270");
            ResidentialArea.Add("6243766237");
            ResidentialArea.Add("6244296191");
            ResidentialArea.Add("6242520428");
            ResidentialArea.Add("6243133200");
            ResidentialArea.Add("6245902302");
            ResidentialArea.Add("6227705118");
            ResidentialArea.Add("6231259381");
            ResidentialArea.Add("6243305808");
            ResidentialArea.Add("6226030691");
            ResidentialArea.Add("6225336176");
            ResidentialArea.Add("6236827013");
            ResidentialArea.Add("6227912497");
            ResidentialArea.Add("6239354315");
            ResidentialArea.Add("6236397244");
            ResidentialArea.Add("6233212145");
            ResidentialArea.Add("6225437402");
            ResidentialArea.Add("6233704190");
            ResidentialArea.Add("6228525814");
            ResidentialArea.Add("6228023475");
            ResidentialArea.Add("6241268571");
            ResidentialArea.Add("6236182798");
            ResidentialArea.Add("6228251924");
            ResidentialArea.Add("6228758161");
            ResidentialArea.Add("6245738858");
            ResidentialArea.Add("6238720128");
            ResidentialArea.Add("6235816058");
            ResidentialArea.Add("6230147248");
            ResidentialArea.Add("6235163418");
            ResidentialArea.Add("6229916701");
            ResidentialArea.Add("6238474325");
            DeviceIdList_byType.Add("ResidentialArea", ResidentialArea);
            #endregion
            return DeviceIdList_byType;
        }

        private void PrintPst(PrimarySedimentationTank Pst, string FilePath)
        {
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine(string.Format("{0},{1},{2}", "Pst_" + Pst.TankId, Pst.DataAmount, Pst.DataAvg));
            }
        }

        private void PrintQmt(QuickMixTank Qmt, string FilePath)
        {
            using (var file = new StreamWriter(FilePath, true))
            {
                file.WriteLine(string.Format("{0},{1},{2}", "Qmt_" + Qmt.TankID, Qmt.DataAmount, Qmt.DataAvg));
            }

            // Debug Use
            string QmtFilePath = @"D:\Result\EPAIoT_station_Taichung_Result\" +
                    DateTime.Now.ToString("yyyyMMdd-HHmmss") + "_Qmt_" + Qmt.TankID + ".csv";
            FileInfo FI = new FileInfo(QmtFilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(QmtFilePath, true))
            {
                var TsAndDt = Qmt.Ts.Zip(Qmt.List_Dt, (n, w) => new { TS = n, DT = w });
                foreach (var nw in TsAndDt)
                {
                    file.WriteLine(string.Format("{0},{1}", nw.TS, nw.DT.ToString("yyyy/MM/dd HH:mm")));
                }
            }
        }

        private bool isToPrint(string key)
        {
            bool Res = false;
            if (key == "MM01")
                Res = true;
            else if (key == "ww01")
                Res = true;
            else if (key == "dd01")
                Res = true;
            else if (key == "HH01")
                Res = true;
            else if (key == "dd02")
                Res = true;
            else if (key == "Y2018_6182023037")
                Res = true;
            else if (key == "Y2018S04_6182023037")
                Res = true;
            return Res;
        }

        private List<string> CreateDeviceIdList()
        {
            List<string> List_Device = new List<string>();
            List_Device.Add("6240072114");
            List_Device.Add("10248126724");
            List_Device.Add("10239386051");
            List_Device.Add("10240872207");
            List_Device.Add("10240416140");
            List_Device.Add("10243136164");
            List_Device.Add("10246102258");
            List_Device.Add("7782112149");
            List_Device.Add("10241218475");
            List_Device.Add("10242646750");
            List_Device.Add("10241484288");
            List_Device.Add("10256580700");
            List_Device.Add("10242598362");
            List_Device.Add("10240275398");
            List_Device.Add("10242340321");
            List_Device.Add("10266340000");
            List_Device.Add("10242247858");
            List_Device.Add("10249876890");
            List_Device.Add("10252783870");
            List_Device.Add("10253128546");
            List_Device.Add("10267398180");
            List_Device.Add("10249276644");
            List_Device.Add("10244913793");
            List_Device.Add("10253565369");
            List_Device.Add("10240629640");
            List_Device.Add("10242009511");
            List_Device.Add("10243540697");
            List_Device.Add("10244311954");
            List_Device.Add("10241347900");
            List_Device.Add("10244491842");
            List_Device.Add("10241766010");
            List_Device.Add("10241966726");
            List_Device.Add("10242429139");
            List_Device.Add("10243225590");
            List_Device.Add("10247011851");
            List_Device.Add("10244025543");
            List_Device.Add("6207564493");
            List_Device.Add("10245703313");
            List_Device.Add("10253915817");
            List_Device.Add("10266411032");
            List_Device.Add("10251436077");
            List_Device.Add("10248417464");
            List_Device.Add("10246934296");
            List_Device.Add("10243642553");
            List_Device.Add("10245812926");
            List_Device.Add("10246721324");
            List_Device.Add("10245924980");
            List_Device.Add("10257280434");
            List_Device.Add("10240008501");
            List_Device.Add("10239544549");
            List_Device.Add("10239626131");
            List_Device.Add("10255540934");
            List_Device.Add("10243467794");
            List_Device.Add("10260613719");
            List_Device.Add("10240176513");
            List_Device.Add("10247230633");
            List_Device.Add("10251957562");
            List_Device.Add("10241807377");
            List_Device.Add("10266991225");
            List_Device.Add("10256611580");
            List_Device.Add("10257360104");
            List_Device.Add("10267779953");
            List_Device.Add("10240960298");
            List_Device.Add("10257145180");
            List_Device.Add("10243744003");
            List_Device.Add("10260760011");
            List_Device.Add("10244839650");
            List_Device.Add("10249086546");
            List_Device.Add("10254114735");
            List_Device.Add("10243827947");
            List_Device.Add("10239973579");
            List_Device.Add("10251503446");
            List_Device.Add("10239759109");
            List_Device.Add("10266506754");
            List_Device.Add("10242171932");
            List_Device.Add("10244122238");
            List_Device.Add("10246874791");
            List_Device.Add("10253879246");
            List_Device.Add("10254626174");
            List_Device.Add("10241142002");
            List_Device.Add("10257473582");
            List_Device.Add("10245151362");
            List_Device.Add("10267299982");
            List_Device.Add("10245263284");
            List_Device.Add("10264820527");
            List_Device.Add("10259106423");
            List_Device.Add("10266015972");
            List_Device.Add("10260213368");
            List_Device.Add("10260933084");
            List_Device.Add("10255043733");
            List_Device.Add("10263576512");
            List_Device.Add("10256112887");
            List_Device.Add("10260113433");
            List_Device.Add("10254800374");
            List_Device.Add("10264702299");
            List_Device.Add("10254700957");
            List_Device.Add("10266701841");
            List_Device.Add("10267486598");
            List_Device.Add("10260516075");
            List_Device.Add("10261021464");
            List_Device.Add("10260428098");
            List_Device.Add("10261240251");
            List_Device.Add("10261571438");
            List_Device.Add("10261172732");
            List_Device.Add("10256253497");
            List_Device.Add("10263920604");
            List_Device.Add("10262386419");
            List_Device.Add("10261392289");
            List_Device.Add("10262984815");
            List_Device.Add("10254930071");
            List_Device.Add("10260366775");
            List_Device.Add("10258864685");
            List_Device.Add("10260881033");
            List_Device.Add("6204640213");
            List_Device.Add("10246506970");
            List_Device.Add("10256701032");
            List_Device.Add("10248376647");
            List_Device.Add("10251642794");
            List_Device.Add("10254380805");
            List_Device.Add("10252223623");
            List_Device.Add("10256417782");
            List_Device.Add("10257725115");
            List_Device.Add("10242803915");
            List_Device.Add("10240539651");
            List_Device.Add("10253379919");
            List_Device.Add("10239290211");
            List_Device.Add("10239120368");
            List_Device.Add("10242974229");
            List_Device.Add("10241612373");
            List_Device.Add("10248997728");
            List_Device.Add("10247146639");
            List_Device.Add("10243381987");
            List_Device.Add("10250759781");
            List_Device.Add("10244605822");
            List_Device.Add("10256911387");
            List_Device.Add("10244577830");
            List_Device.Add("10248655362");
            List_Device.Add("10251875550");
            List_Device.Add("10246046479");
            List_Device.Add("10267581123");
            List_Device.Add("10245663645");
            List_Device.Add("10266614326");
            List_Device.Add("10254401338");
            List_Device.Add("10246630667");
            List_Device.Add("10253642943");
            List_Device.Add("10237145655");
            List_Device.Add("10264481763");
            List_Device.Add("10265478203");
            List_Device.Add("10256390029");
            List_Device.Add("10262050526");
            List_Device.Add("10263486603");
            List_Device.Add("10259705029");
            List_Device.Add("10261653315");
            List_Device.Add("10262181091");
            List_Device.Add("10263029046");
            List_Device.Add("10256854452");
            List_Device.Add("10257821935");
            List_Device.Add("10258635925");
            List_Device.Add("10262725307");
            List_Device.Add("10264355041");
            List_Device.Add("10266129234");
            List_Device.Add("10263278503");
            List_Device.Add("10245503162");
            List_Device.Add("10265554161");
            List_Device.Add("10256072648");
            List_Device.Add("10265334020");
            List_Device.Add("10258908074");
            List_Device.Add("10249335573");
            List_Device.Add("10259840993");
            List_Device.Add("10259456945");
            List_Device.Add("10263710892");
            List_Device.Add("10244299340");
            List_Device.Add("10264535085");
            List_Device.Add("10263169109");
            List_Device.Add("10262686650");
            List_Device.Add("10262855425");
            List_Device.Add("10257920204");
            List_Device.Add("10259029279");
            List_Device.Add("10262448890");
            List_Device.Add("10261891910");
            List_Device.Add("10245361644");
            List_Device.Add("10250619145");
            List_Device.Add("10240764842");
            List_Device.Add("10249717168");
            List_Device.Add("10248800420");
            List_Device.Add("10252616931");
            List_Device.Add("10251161256");
            List_Device.Add("10249134433");
            List_Device.Add("10250452920");
            List_Device.Add("10251021017");
            List_Device.Add("10254028197");
            List_Device.Add("10250355744");
            List_Device.Add("10247617538");
            List_Device.Add("10267634930");
            List_Device.Add("10249566412");
            List_Device.Add("10251754410");
            List_Device.Add("10249482907");
            List_Device.Add("10250204781");
            List_Device.Add("10252908808");
            List_Device.Add("10245084314");
            List_Device.Add("10266249460");
            List_Device.Add("10239828865");
            List_Device.Add("10248039486");
            List_Device.Add("10235201894");
            List_Device.Add("10237237284");
            List_Device.Add("10248293521");
            List_Device.Add("10254288683");
            List_Device.Add("10247853515");
            List_Device.Add("10252121215");
            List_Device.Add("10253451685");
            List_Device.Add("10238283857");
            List_Device.Add("10239097872");
            List_Device.Add("10238440342");
            List_Device.Add("10237325556");
            List_Device.Add("10248706692");
            List_Device.Add("10250954893");
            List_Device.Add("10261747402");
            List_Device.Add("10252417543");
            List_Device.Add("10234160359");
            List_Device.Add("10264964139");
            List_Device.Add("10261434653");
            List_Device.Add("10242745932");
            List_Device.Add("10238653354");
            List_Device.Add("10237815893");
            List_Device.Add("10243968317");
            List_Device.Add("10265935756");
            List_Device.Add("10244713183");
            List_Device.Add("10233184179");
            List_Device.Add("10258079140");
            List_Device.Add("10263302988");
            List_Device.Add("10259984638");
            List_Device.Add("10258288573");
            List_Device.Add("10265869456");
            List_Device.Add("10261973167");
            List_Device.Add("10262249504");
            List_Device.Add("10264023529");
            List_Device.Add("10257544322");
            List_Device.Add("10267145946");
            List_Device.Add("10238163008");
            List_Device.Add("10265011477");
            List_Device.Add("10240329053");
            List_Device.Add("10247502151");
            List_Device.Add("10247438786");
            List_Device.Add("10237688498");
            List_Device.Add("10241061603");
            List_Device.Add("10259624194");
            List_Device.Add("10236095866");
            List_Device.Add("10263665206");
            List_Device.Add("10237013988");
            List_Device.Add("10247920216");
            List_Device.Add("10258176549");
            List_Device.Add("10262553855");
            List_Device.Add("10236748581");
            List_Device.Add("10241583401");
            List_Device.Add("10264272682");
            List_Device.Add("10235001897");
            List_Device.Add("10236803538");
            List_Device.Add("10255703917");
            List_Device.Add("10233740567");
            List_Device.Add("10255827480");
            List_Device.Add("10236609821");
            List_Device.Add("10255679846");
            List_Device.Add("10255363386");
            List_Device.Add("10255426539");
            List_Device.Add("10232560258");
            List_Device.Add("10255279876");
            List_Device.Add("10258359938");
            List_Device.Add("10236336459");
            List_Device.Add("10238792140");
            List_Device.Add("10237567392");
            List_Device.Add("10232839597");
            List_Device.Add("10234365835");
            List_Device.Add("10233817722");
            List_Device.Add("10235893007");
            List_Device.Add("10234586373");
            List_Device.Add("10238312092");
            List_Device.Add("10232916032");
            List_Device.Add("10233088203");
            List_Device.Add("10234710685");
            List_Device.Add("10235407215");
            List_Device.Add("10232655835");
            List_Device.Add("10234255865");
            List_Device.Add("10250060633");
            List_Device.Add("10266897572");
            List_Device.Add("10246373093");
            List_Device.Add("10250185638");
            List_Device.Add("10234625609");
            List_Device.Add("10267016765");
            List_Device.Add("10234070928");
            List_Device.Add("10250510364");
            List_Device.Add("10238965139");
            List_Device.Add("10249995574");
            List_Device.Add("10236436247");
            List_Device.Add("10236206370");
            List_Device.Add("10238020632");
            List_Device.Add("10252517759");
            List_Device.Add("10249603842");
            List_Device.Add("10233623195");
            List_Device.Add("10252310112");
            List_Device.Add("10238849376");
            List_Device.Add("10250860650");
            List_Device.Add("10253017242");
            List_Device.Add("10235945446");
            List_Device.Add("10232760701");
            List_Device.Add("10237478630");
            List_Device.Add("10236151834");
            List_Device.Add("10235599348");
            List_Device.Add("10252847747");
            List_Device.Add("10233340370");
            List_Device.Add("10233456215");
            List_Device.Add("10237701460");
            List_Device.Add("10233263561");
            List_Device.Add("10233901196");
            List_Device.Add("10234821436");
            List_Device.Add("10237967492");
            List_Device.Add("10234968107");
            List_Device.Add("10235132922");
            List_Device.Add("10238558174");
            List_Device.Add("10234405930");
            List_Device.Add("10265231825");
            List_Device.Add("10235324367");
            List_Device.Add("10258712314");
            List_Device.Add("10259259031");
            List_Device.Add("10258500246");
            List_Device.Add("10253260031");
            List_Device.Add("10254501820");
            List_Device.Add("10265624958");
            List_Device.Add("10260043742");
            List_Device.Add("10259339656");
            List_Device.Add("10265172676");
            List_Device.Add("10264146075");
            List_Device.Add("10265793653");
            List_Device.Add("10236997574");
            List_Device.Add("10263808764");
            List_Device.Add("10246250679");
            List_Device.Add("10251231255");
            List_Device.Add("10246444737");
            List_Device.Add("10247786889");
            List_Device.Add("10258416364");
            List_Device.Add("6208491748");
            List_Device.Add("6206552528");
            List_Device.Add("6231656272");
            List_Device.Add("6231098742");
            List_Device.Add("6201524114");
            List_Device.Add("6172927386");
            List_Device.Add("6180170091");
            List_Device.Add("6236980012");
            List_Device.Add("6181070276");
            List_Device.Add("6219436037");
            List_Device.Add("6212006825");
            List_Device.Add("6244630424");
            List_Device.Add("6171278726");
            List_Device.Add("6201335822");
            List_Device.Add("6170988978");
            List_Device.Add("6219908917");
            List_Device.Add("6173911150");
            List_Device.Add("6226666876");
            List_Device.Add("6210315358");
            List_Device.Add("6222977480");
            List_Device.Add("6218588677");
            List_Device.Add("6244368394");
            List_Device.Add("6226375779");
            List_Device.Add("6235437869");
            List_Device.Add("6171173697");
            List_Device.Add("6232140628");
            List_Device.Add("6244972103");
            List_Device.Add("6173816125");
            List_Device.Add("6170644194");
            List_Device.Add("6178996233");
            List_Device.Add("6218689018");
            List_Device.Add("6219878466");
            List_Device.Add("6233351858");
            List_Device.Add("6175361960");
            List_Device.Add("6222332931");
            List_Device.Add("6239260498");
            List_Device.Add("6229853346");
            List_Device.Add("6233189651");
            List_Device.Add("6232992646");
            List_Device.Add("6181754015");
            List_Device.Add("6171874334");
            List_Device.Add("6177453601");
            List_Device.Add("6232779192");
            List_Device.Add("6221617715");
            List_Device.Add("6174764830");
            List_Device.Add("6232673827");
            List_Device.Add("6227110910");
            List_Device.Add("6211182359");
            List_Device.Add("6227323190");
            List_Device.Add("6211248136");
            List_Device.Add("6179004739");
            List_Device.Add("6222150001");
            List_Device.Add("6171999223");
            List_Device.Add("6210741194");
            List_Device.Add("6176606512");
            List_Device.Add("6177854105");
            List_Device.Add("6231946778");
            List_Device.Add("6228310872");
            List_Device.Add("6241910435");
            List_Device.Add("6238532767");
            List_Device.Add("6242322884");
            List_Device.Add("6243075267");
            List_Device.Add("6235562361");
            List_Device.Add("6232477761");
            List_Device.Add("6173704280");
            List_Device.Add("6240599138");
            List_Device.Add("6227537963");
            List_Device.Add("6207293578");
            List_Device.Add("6206690053");
            List_Device.Add("6208392234");
            List_Device.Add("6219001111");
            List_Device.Add("6175154889");
            List_Device.Add("6208073033");
            List_Device.Add("6220532668");
            List_Device.Add("6221036526");
            List_Device.Add("6207422912");
            List_Device.Add("6208106918");
            List_Device.Add("6203850001");
            List_Device.Add("6235757154");
            List_Device.Add("6206820413");
            List_Device.Add("6221298468");
            List_Device.Add("6176281540");
            List_Device.Add("6208886319");
            List_Device.Add("6208778561");
            List_Device.Add("6206486190");
            List_Device.Add("6181939798");
            List_Device.Add("6207993456");
            List_Device.Add("6207773627");
            List_Device.Add("6207364725");
            List_Device.Add("6244708588");
            List_Device.Add("6223072367");
            List_Device.Add("6206204805");
            List_Device.Add("6204894886");
            List_Device.Add("6206704365");
            List_Device.Add("6245011123");
            List_Device.Add("6211524198");
            List_Device.Add("6234476267");
            List_Device.Add("6239694957");
            List_Device.Add("6240121958");
            List_Device.Add("6234382586");
            List_Device.Add("6236647522");
            List_Device.Add("6242741149");
            List_Device.Add("6241169523");
            List_Device.Add("6241489399");
            List_Device.Add("6232078920");
            List_Device.Add("6210524986");
            List_Device.Add("6212562290");
            List_Device.Add("6211037997");
            List_Device.Add("6230508072");
            List_Device.Add("6227051815");
            List_Device.Add("6212945325");
            List_Device.Add("6232386523");
            List_Device.Add("6212397163");
            List_Device.Add("6210183557");
            List_Device.Add("6210498840");
            List_Device.Add("6245123633");
            List_Device.Add("6181356136");
            List_Device.Add("6181100138");
            List_Device.Add("6179199537");
            List_Device.Add("6178700795");
            List_Device.Add("6204564670");
            List_Device.Add("6177966845");
            List_Device.Add("6203931876");
            List_Device.Add("6202062910");
            List_Device.Add("6176870215");
            List_Device.Add("6202118053");
            List_Device.Add("6201492301");
            List_Device.Add("6204337453");
            List_Device.Add("6176132315");
            List_Device.Add("6204095761");
            List_Device.Add("6171517419");
            List_Device.Add("6204462904");
            List_Device.Add("6178374774");
            List_Device.Add("6202288936");
            List_Device.Add("6204710281");
            List_Device.Add("6665272821");
            List_Device.Add("6202383967");
            List_Device.Add("6170479504");
            List_Device.Add("6180944395");
            List_Device.Add("6205010682");
            List_Device.Add("6202987339");
            List_Device.Add("6202881113");
            List_Device.Add("6209655053");
            List_Device.Add("6201080763");
            List_Device.Add("6201125435");
            List_Device.Add("6202590634");
            List_Device.Add("6202680053");
            List_Device.Add("6202438562");
            List_Device.Add("6203405861");
            List_Device.Add("6179285675");
            List_Device.Add("6205259163");
            List_Device.Add("6205107975");
            List_Device.Add("6204262565");
            List_Device.Add("6241808941");
            List_Device.Add("6205323118");
            List_Device.Add("6178045816");
            List_Device.Add("6179839647");
            List_Device.Add("6171080763");
            List_Device.Add("6201252883");
            List_Device.Add("6181554044");
            List_Device.Add("6181235830");
            List_Device.Add("6180327867");
            List_Device.Add("6175537920");
            List_Device.Add("6201633906");
            List_Device.Add("6172045596");
            List_Device.Add("6175415111");
            List_Device.Add("6200907176");
            List_Device.Add("6170511673");
            List_Device.Add("6172251404");
            List_Device.Add("6172819559");
            List_Device.Add("6203372597");
            List_Device.Add("6202746480");
            List_Device.Add("6230748013");
            List_Device.Add("6210658833");
            List_Device.Add("6177340025");
            List_Device.Add("6174945966");
            List_Device.Add("6180255516");
            List_Device.Add("6225736125");
            List_Device.Add("6178641010");
            List_Device.Add("6235398605");
            List_Device.Add("6172101879");
            List_Device.Add("6180731755");
            List_Device.Add("6174298410");
            List_Device.Add("6201786838");
            List_Device.Add("6226884006");
            List_Device.Add("6179529044");
            List_Device.Add("6179469631");
            List_Device.Add("6242078378");
            List_Device.Add("6177503710");
            List_Device.Add("6230007561");
            List_Device.Add("6242910593");
            List_Device.Add("6179753931");
            List_Device.Add("6213353995");
            List_Device.Add("6220874631");
            List_Device.Add("6211482058");
            List_Device.Add("6275437912");
            List_Device.Add("6238991310");
            List_Device.Add("6244566439");
            List_Device.Add("6238049673");
            List_Device.Add("6228812932");
            List_Device.Add("6237927372");
            List_Device.Add("6236408387");
            List_Device.Add("6245647067");
            List_Device.Add("6237387071");
            List_Device.Add("6231365445");
            List_Device.Add("6241043227");
            List_Device.Add("6234848409");
            List_Device.Add("6233471223");
            List_Device.Add("6238127527");
            List_Device.Add("6241569662");
            List_Device.Add("6234614695");
            List_Device.Add("6240368695");
            List_Device.Add("6230941521");
            List_Device.Add("6239453250");
            List_Device.Add("6240930004");
            List_Device.Add("6233857572");
            List_Device.Add("6236081087");
            List_Device.Add("6245586887");
            List_Device.Add("6237506236");
            List_Device.Add("6226939495");
            List_Device.Add("6229082773");
            List_Device.Add("6237781455");
            List_Device.Add("6246054126");
            List_Device.Add("6225566514");
            List_Device.Add("6228950270");
            List_Device.Add("6228636232");
            List_Device.Add("6230240450");
            List_Device.Add("6237189035");
            List_Device.Add("6224914687");
            List_Device.Add("6238399852");
            List_Device.Add("6243834493");
            List_Device.Add("6235229032");
            List_Device.Add("6226554446");
            List_Device.Add("6236799288");
            List_Device.Add("6242469253");
            List_Device.Add("6238210158");
            List_Device.Add("6235624366");
            List_Device.Add("6240276717");
            List_Device.Add("6231529004");
            List_Device.Add("6232549562");
            List_Device.Add("6278607506");
            List_Device.Add("6218254567");
            List_Device.Add("6237668574");
            List_Device.Add("6234242613");
            List_Device.Add("6230439113");
            List_Device.Add("6278885733");
            List_Device.Add("6245479430");
            List_Device.Add("6236560599");
            List_Device.Add("6228405280");
            List_Device.Add("6218402962");
            List_Device.Add("6232254541");
            List_Device.Add("6239538314");
            List_Device.Add("6229663074");
            List_Device.Add("6225011500");
            List_Device.Add("6242884116");
            List_Device.Add("6221487384");
            List_Device.Add("6219695422");
            List_Device.Add("6173626634");
            List_Device.Add("6222043482");
            List_Device.Add("6170369574");
            List_Device.Add("6213243025");
            List_Device.Add("6181695067");
            List_Device.Add("6281023536");
            List_Device.Add("6220269632");
            List_Device.Add("6209992732");
            List_Device.Add("6237212914");
            List_Device.Add("6300997376");
            List_Device.Add("6221347298");
            List_Device.Add("6240735130");
            List_Device.Add("6227842173");
            List_Device.Add("6244091139");
            List_Device.Add("6244469967");
            List_Device.Add("6181882927");
            List_Device.Add("6179654387");
            List_Device.Add("6231453744");
            List_Device.Add("6240442620");
            List_Device.Add("6172367777");
            List_Device.Add("6243423203");
            List_Device.Add("6234730726");
            List_Device.Add("6173541534");
            List_Device.Add("6243271703");
            List_Device.Add("6225659069");
            List_Device.Add("6241698404");
            List_Device.Add("6237402965");
            List_Device.Add("6227290571");
            List_Device.Add("6218929645");
            List_Device.Add("6210078747");
            List_Device.Add("6212480146");
            List_Device.Add("6177265783");
            List_Device.Add("6179327308");
            List_Device.Add("6176546298");
            List_Device.Add("6174523355");
            List_Device.Add("6218757213");
            List_Device.Add("6234034768");
            List_Device.Add("6239902461");
            List_Device.Add("6172581936");
            List_Device.Add("6170777118");
            List_Device.Add("6225110909");
            List_Device.Add("6734009828");
            List_Device.Add("6178576524");
            List_Device.Add("6229304336");
            List_Device.Add("6233672784");
            List_Device.Add("10247325283");
            List_Device.Add("10248521745");
            List_Device.Add("10252051252");
            List_Device.Add("10259529107");
            List_Device.Add("10239486340");
            List_Device.Add("10264666051");
            List_Device.Add("10255128236");
            List_Device.Add("10255916864");
            List_Device.Add("6243669136");
            List_Device.Add("6219218058");
            List_Device.Add("6234902334");
            List_Device.Add("6173343636");
            List_Device.Add("6176407315");
            List_Device.Add("10251343896");
            List_Device.Add("10257682951");
            List_Device.Add("6245846003");
            List_Device.Add("6219300143");
            List_Device.Add("6222621233");
            List_Device.Add("6182023037");
            List_Device.Add("6180518061");
            List_Device.Add("6213148077");
            List_Device.Add("6243944350");
            List_Device.Add("6220907808");
            List_Device.Add("6217593017");
            List_Device.Add("6217629621");
            List_Device.Add("6212848475");
            List_Device.Add("6173021158");
            List_Device.Add("6180077214");
            List_Device.Add("6172485084");
            List_Device.Add("10257043484");
            List_Device.Add("10253760218");
            List_Device.Add("10243057549");
            List_Device.Add("10236581498");
            List_Device.Add("10245424335");
            List_Device.Add("6180470321");
            List_Device.Add("6211763679");
            List_Device.Add("6237059567");
            List_Device.Add("6217949795");
            List_Device.Add("6239096570");
            List_Device.Add("6180601972");
            List_Device.Add("6242281657");
            List_Device.Add("6207044520");
            List_Device.Add("6240656770");
            List_Device.Add("6208517888");
            List_Device.Add("6245270680");
            List_Device.Add("6207890116");
            List_Device.Add("6208299987");
            List_Device.Add("6206950058");
            List_Device.Add("6207138487");
            List_Device.Add("6177725880");
            List_Device.Add("6170852684");
            List_Device.Add("6207679133");
            List_Device.Add("6204138710");
            List_Device.Add("6206340296");
            List_Device.Add("6208697466");
            List_Device.Add("6224828663");
            List_Device.Add("6203673165");
            List_Device.Add("6181454800");
            List_Device.Add("6203223552");
            List_Device.Add("6182185799");
            List_Device.Add("6174400378");
            List_Device.Add("6204979816");
            List_Device.Add("6178439669");
            List_Device.Add("6201952137");
            List_Device.Add("6203127880");
            List_Device.Add("6203519167");
            List_Device.Add("6201856818");
            List_Device.Add("6177160756");
            List_Device.Add("6203775652");
            List_Device.Add("6175817505");
            List_Device.Add("6178877334");
            List_Device.Add("6178145504");
            List_Device.Add("6175789606");
            List_Device.Add("6203040092");
            List_Device.Add("6176318024");
            List_Device.Add("6180852322");
            List_Device.Add("6175052291");
            List_Device.Add("6174615384");
            List_Device.Add("6210256605");
            List_Device.Add("6219549704");
            List_Device.Add("6222795523");
            List_Device.Add("6222561082");
            List_Device.Add("6227666850");
            List_Device.Add("6173417043");
            List_Device.Add("6171499477");
            List_Device.Add("6229412989");
            List_Device.Add("6174022245");
            List_Device.Add("6229543551");
            List_Device.Add("6174382248");
            List_Device.Add("6178223087");
            List_Device.Add("6171610758");
            List_Device.Add("6239132534");
            List_Device.Add("6175910936");
            List_Device.Add("6175293430");
            List_Device.Add("6171387123");
            List_Device.Add("6227498199");
            List_Device.Add("6179956220");
            List_Device.Add("6173244138");
            List_Device.Add("6177683510");
            List_Device.Add("6172711849");
            List_Device.Add("6210933544");
            List_Device.Add("6211660429");
            List_Device.Add("6240867207");
            List_Device.Add("6211949360");
            List_Device.Add("6222282508");
            List_Device.Add("6211840796");
            List_Device.Add("6217782347");
            List_Device.Add("6222453027");
            List_Device.Add("6213788350");
            List_Device.Add("6213547179");
            List_Device.Add("6220337631");
            List_Device.Add("6220132030");
            List_Device.Add("6221910304");
            List_Device.Add("6212618457");
            List_Device.Add("6220766553");
            List_Device.Add("6211353090");
            List_Device.Add("6225205864");
            List_Device.Add("6212185966");
            List_Device.Add("6218071331");
            List_Device.Add("6213053800");
            List_Device.Add("6213695310");
            List_Device.Add("6218341831");
            List_Device.Add("6218889355");
            List_Device.Add("6220629506");
            List_Device.Add("6217487544");
            List_Device.Add("6175645216");
            List_Device.Add("6239765735");
            List_Device.Add("6235074938");
            List_Device.Add("6238885367");
            List_Device.Add("6233970390");
            List_Device.Add("6245332461");
            List_Device.Add("6225852947");
            List_Device.Add("6226165270");
            List_Device.Add("6243766237");
            List_Device.Add("6244296191");
            List_Device.Add("6242520428");
            List_Device.Add("6243133200");
            List_Device.Add("6221795973");
            List_Device.Add("6226788097");
            List_Device.Add("6226450855");
            List_Device.Add("6231842508");
            List_Device.Add("6176999336");
            List_Device.Add("6235957093");
            List_Device.Add("6229163449");
            List_Device.Add("6244809410");
            List_Device.Add("6225905675");
            List_Device.Add("6278517688");
            List_Device.Add("6233520485");
            List_Device.Add("6237815360");
            List_Device.Add("6241752560");
            List_Device.Add("6229244764");
            List_Device.Add("6220040748");
            List_Device.Add("6219776330");
            List_Device.Add("6172629251");
            List_Device.Add("6173195849");
            List_Device.Add("6238654030");
            List_Device.Add("6236238291");
            List_Device.Add("6278772730");
            List_Device.Add("6242163948");
            List_Device.Add("6278430579");
            List_Device.Add("6241360449");
            List_Device.Add("6229712569");
            List_Device.Add("6232887063");
            List_Device.Add("6222895282");
            List_Device.Add("6230680423");
            List_Device.Add("6243536602");
            List_Device.Add("6220466572");
            List_Device.Add("6221578795");
            List_Device.Add("6174191442");
            List_Device.Add("6226245536");
            List_Device.Add("6245902302");
            List_Device.Add("6227705118");
            List_Device.Add("6231259381");
            List_Device.Add("6171766699");
            List_Device.Add("6231743614");
            List_Device.Add("6242672729");
            List_Device.Add("6217883961");
            List_Device.Add("6174854904");
            List_Device.Add("6219114065");
            List_Device.Add("6224264022");
            List_Device.Add("6230309523");
            List_Device.Add("6176075694");
            List_Device.Add("6221183541");
            List_Device.Add("6243305808");
            List_Device.Add("6177020493");
            List_Device.Add("6233027183");
            List_Device.Add("6226030691");
            List_Device.Add("6225336176");
            List_Device.Add("6218105858");
            List_Device.Add("6176760340");
            List_Device.Add("6231147598");
            List_Device.Add("6212250069");
            List_Device.Add("6182299831");
            List_Device.Add("6228134874");
            List_Device.Add("6212726347");
            List_Device.Add("6236827013");
            List_Device.Add("6213450153");
            List_Device.Add("6210817291");
            List_Device.Add("6223146608");
            List_Device.Add("6227912497");
            List_Device.Add("6239354315");
            List_Device.Add("6236397244");
            List_Device.Add("6233212145");
            List_Device.Add("6225437402");
            List_Device.Add("6233704190");
            List_Device.Add("6228525814");
            List_Device.Add("6228023475");
            List_Device.Add("6241268571");
            List_Device.Add("6236182798");
            List_Device.Add("6228251924");
            List_Device.Add("6228758161");
            List_Device.Add("6245738858");
            List_Device.Add("6238720128");
            List_Device.Add("6235816058");
            List_Device.Add("6230147248");
            List_Device.Add("6235163418");
            List_Device.Add("6229916701");
            List_Device.Add("6238474325");
            List_Device.Add("6221860040");
            return List_Device;
        }
        private List<string> CreateDeviceTypeList()
        {
            List<string> List_Device = new List<string>();
            // 全部+交通+工業+社區
            List_Device.Add("AllArea");
            List_Device.Add("TrafficArea");
            List_Device.Add("IndustrialArea");
            List_Device.Add("ResidentialArea");
            return List_Device;
        }

        private List<string> CreateTankIdList_TemporalMode()
        {
            List<string> TankIdList = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                string key = "Month_" + i.ToString("00");
                TankIdList.Add(key);
            }
            for (int i = 0; i <= 6; i++)
            {
                string key = "Week_" + i.ToString("00");
                TankIdList.Add(key);
            }
            for (int i = 1; i <= 31; i++)
            {
                string key = "Day_" + i.ToString("00");
                TankIdList.Add(key);
            }
            for (int i = 0; i <= 23; i++)
            {
                string key = "Hour_" + i.ToString("00");
                TankIdList.Add(key);
            }
            return TankIdList;
        }
        private List<string> CreateTankIdList_SpatialMode()
        {
            List<string> TankIdList = new List<string>();
            // 年均值
            for (int i = 2018; i <= 2020; i++)
            {
                string key = "Y" + i.ToString("0000");
                TankIdList.Add(key);
            }
            // 季均值
            for (int i = 2018; i <= 2020; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    string key = "Y" + i.ToString("0000") + "S" + j.ToString("00");
                    TankIdList.Add(key);
                }
            }
            return TankIdList;
        }

        private List<string> CreateTankIdList(List<string> DeviceIdList, List<string> DeviceTypeList)
        {
            List<string> TankIdList = new List<string>();

            // Add TankIdList TemporalMode
            List<string> TankIdList_TemporalMode = CreateTankIdList_TemporalMode();
            foreach (string DeviceType in DeviceTypeList)
            {
                foreach (string TankId in TankIdList_TemporalMode)
                {
                    string key = TankId + "_" + DeviceType;
                    TankIdList.Add(key);
                }
            }

            // Add TankIdList SpatialMode
            List<string> TankIdList_SpatialMode = CreateTankIdList_SpatialMode();
            foreach (string DeviceId in DeviceIdList)
            {
                foreach (string TankId in TankIdList_SpatialMode)
                {
                    string key = TankId + "_" + DeviceId;
                    TankIdList.Add(key);
                }
            }
            return TankIdList;
        }

        private bool CreateQmtAndPst(List<string> TankIdList,
            out Dictionary<string, QuickMixTank> Dic_Qmt,
            out Dictionary<string, PrimarySedimentationTank> Dic_Pst)
        {
            Dic_Qmt = new Dictionary<string, QuickMixTank>();
            Dic_Pst = new Dictionary<string, PrimarySedimentationTank>();

            try
            {
                foreach (string TankId in TankIdList)
                {
                    Dic_Qmt.Add(TankId, new QuickMixTank(TankId));
                    Dic_Pst.Add(TankId, new PrimarySedimentationTank(TankId));
                }
                return true;
            }
            catch (Exception e)
            {
                //  Block of code to handle errors
                return false;
            }
        }

        private string GetConfigFolder()
        {
            string ProjectFolderPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            return ProjectFolderPath + @"CsvFileReader\CsvFileReaderConfig\";
        }
    }
}
