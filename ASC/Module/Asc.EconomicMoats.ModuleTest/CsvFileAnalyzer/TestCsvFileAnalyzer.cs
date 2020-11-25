
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

            Dictionary<string, QuickMixTank> Dic_Qmt = CreateQuickMixTank();
            List<string> List_deviceId = CreateList_deviceId();

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Dal.Dic_Qmt = Dic_Qmt;
            Dal.List_deviceId = List_deviceId;
            Dal.useDeviceIdList = false;
            Cfr.Dal = Dal;

            // 3. Read Csv File
            bool result = Cfr.ReadCsvFile();
            Assert.IsTrue(result);

            // EPAIoT_station_Taichung_Result
            string FilePath = @"D:\Result\EPAIoT_station_Taichung_Result\" +
                    DateTime.Now.ToString("yyyyMMdd-HHmm") + ".csv";
            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                file.WriteLine(string.Format("{0},{1},{2}",
                    "TankType",
                    "DataAmount",
                    "DataAvg"));
            }

            // If slow, convert to Parallel.ForEach
            foreach (KeyValuePair<string, QuickMixTank> entry in Dic_Qmt)
            {
                entry.Value.ComputeAvg();
                using (var file = new StreamWriter(FilePath, true))
                {
                    file.WriteLine(string.Format("{0},{1},{2}",
                        entry.Value.TankType,
                        entry.Value.DataAmount,
                        entry.Value.DataAvg));
                }
            }

            // Debug
            FilePath = @"D:\Result\EPAIoT_station_Taichung_Result\" +
                   DateTime.Now.ToString("yyyyMMdd-HHmm") + "HH01.csv";
            FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, true))
            {
                QuickMixTank Qmt = Dic_Qmt["HH01"];
                var TsAndDt = Qmt.Ts.Zip(Qmt.List_Dt, (n, w) => new { TS = n, DT = w });
                foreach (var nw in TsAndDt)
                {
                    file.WriteLine(string.Format("{0},{1}", nw.TS, nw.DT.ToString("yyyy/MM/dd HH:mm")));
                }
            }

            // 135162-134944=218 (why?)
            // x6 (年、季、月、週、日、時) for all stations (not only Taichung)
            Assert.AreEqual(24.46821841, Dic_Qmt["yyyy2018"].DataAvg, 0.1);
            Assert.AreEqual(24.46821841, Dic_Qmt["SS04"].DataAvg, 0.1);
            Assert.AreEqual(24.46821841, Dic_Qmt["MM01"].DataAvg, 0.1);
            Assert.AreEqual(24.46821841, Dic_Qmt["ww01"].DataAvg, 0.1);
            Assert.AreEqual(24.46821841, Dic_Qmt["dd01"].DataAvg, 0.1);
            Assert.AreEqual(20.13677468, Dic_Qmt["HH01"].DataAvg, 0.1);

            Assert.AreEqual(5308, Dic_Qmt["HH01"].DataAmount);
            Assert.AreEqual(128801, Dic_Qmt["MM01"].DataAmount);
        }

        [Test]
        public void UC08_AggregateFromQmtToPst()
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

            Dictionary<string, QuickMixTank> Dic_Qmt = CreateQuickMixTank();
            List<string> List_deviceId = CreateList_deviceId();
            Dictionary<string, PrimarySedimentationTank> Dic_Pst = CreatePrimarySedimentationTank(List_deviceId);

            // 2. Dependency Management
            Cfr.Cfs = Cfs;
            Dal.Def = Def;
            Dal.Dic_Qmt = Dic_Qmt;
            Dal.List_deviceId = List_deviceId;
            Dal.useDeviceIdList = true;
            Cfr.Dal = Dal;

            // 3. Read Csv File and Distribute by Dataline Analysis Logic (20180101)
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
            }

            // 3. Read Csv File and Distribute by Dataline Analysis Logic (20180102)
            Cfr.FilePath = @"D:\EPA_IoT_Station_Data\2018\201801\" + @"epa_micro_20180102.csv";
            result = Cfr.ReadCsvFile();
            Assert.IsTrue(result);


            // Result Folder for 快混池 / 初沉池
            //string FilePath = @"D:\Result\EPAIoT_station_Taichung_Result\" +
            //        DateTime.Now.ToString("yyyyMMdd-HHmm") + ".csv";
            //FileInfo FI = new FileInfo(FilePath);
            //FI.Directory.Create();  // If the directory already exists, this method does nothing.
            //using (var file = new StreamWriter(FilePath, false))
            //{
            //    file.WriteLine(string.Format("{0},{1},{2}",
            //        "TankType",
            //        "DataAmount",
            //        "DataAvg"));
            //}

            // If slow, convert to Parallel.ForEach
            foreach (KeyValuePair<string, QuickMixTank> entry in Dic_Qmt)
            {
                entry.Value.ComputeAvg();
                //using (var file = new StreamWriter(FilePath, true))
                //{
                //    file.WriteLine(string.Format("{0},{1},{2}",
                //        entry.Value.TankType,
                //        entry.Value.DataAmount,
                //        entry.Value.DataAvg));
                //}
            }
        }

        private Dictionary<string, PrimarySedimentationTank> CreatePrimarySedimentationTank(List<string> List_deviceId)
        {
            Dictionary<string, PrimarySedimentationTank> Dic_Pst = new Dictionary<string, PrimarySedimentationTank>();

            foreach (string DeviceID in List_deviceId)
            {
                // 年均值
                Dic_Pst.Add("Y2018_" + DeviceID, new PrimarySedimentationTank("Y2018_" + DeviceID));
                Dic_Pst.Add("Y2019_" + DeviceID, new PrimarySedimentationTank("Y2019_" + DeviceID));
                Dic_Pst.Add("Y2020_" + DeviceID, new PrimarySedimentationTank("Y2020_" + DeviceID));
                // 季均值
                Dic_Pst.Add("Y2018S01_" + DeviceID, new PrimarySedimentationTank("Y2018S01_" + DeviceID));
                Dic_Pst.Add("Y2018S02_" + DeviceID, new PrimarySedimentationTank("Y2018S02_" + DeviceID));
                Dic_Pst.Add("Y2018S03_" + DeviceID, new PrimarySedimentationTank("Y2018S03_" + DeviceID));
                Dic_Pst.Add("Y2018S04_" + DeviceID, new PrimarySedimentationTank("Y2018S04_" + DeviceID));
                Dic_Pst.Add("Y2019S01_" + DeviceID, new PrimarySedimentationTank("Y2019S01_" + DeviceID));
                Dic_Pst.Add("Y2019S02_" + DeviceID, new PrimarySedimentationTank("Y2019S02_" + DeviceID));
                Dic_Pst.Add("Y2019S03_" + DeviceID, new PrimarySedimentationTank("Y2019S03_" + DeviceID));
                Dic_Pst.Add("Y2019S04_" + DeviceID, new PrimarySedimentationTank("Y2019S04_" + DeviceID));
                Dic_Pst.Add("Y2020S01_" + DeviceID, new PrimarySedimentationTank("Y2020S01_" + DeviceID));
                Dic_Pst.Add("Y2020S02_" + DeviceID, new PrimarySedimentationTank("Y2020S02_" + DeviceID));
                Dic_Pst.Add("Y2020S03_" + DeviceID, new PrimarySedimentationTank("Y2020S03_" + DeviceID));
                Dic_Pst.Add("Y2020S04_" + DeviceID, new PrimarySedimentationTank("Y2020S04_" + DeviceID));
            }

            return Dic_Pst;
        }

        private List<string> CreateList_deviceId()
        {
            List<string> List_deviceId = new List<string>();

            List_deviceId.Add("6240072114");
            List_deviceId.Add("10248126724");
            List_deviceId.Add("10239386051");
            List_deviceId.Add("10240872207");
            List_deviceId.Add("10240416140");
            List_deviceId.Add("10243136164");
            List_deviceId.Add("10246102258");
            List_deviceId.Add("7782112149");
            List_deviceId.Add("10241218475");
            List_deviceId.Add("10242646750");
            List_deviceId.Add("10241484288");
            List_deviceId.Add("10256580700");
            List_deviceId.Add("10242598362");
            List_deviceId.Add("10240275398");
            List_deviceId.Add("10242340321");
            List_deviceId.Add("10266340000");
            List_deviceId.Add("10242247858");
            List_deviceId.Add("10249876890");
            List_deviceId.Add("10252783870");
            List_deviceId.Add("10253128546");
            List_deviceId.Add("10267398180");
            List_deviceId.Add("10249276644");
            List_deviceId.Add("10244913793");
            List_deviceId.Add("10253565369");
            List_deviceId.Add("10240629640");
            List_deviceId.Add("10242009511");
            List_deviceId.Add("10243540697");
            List_deviceId.Add("10244311954");
            List_deviceId.Add("10241347900");
            List_deviceId.Add("10244491842");
            List_deviceId.Add("10241766010");
            List_deviceId.Add("10241966726");
            List_deviceId.Add("10242429139");
            List_deviceId.Add("10243225590");
            List_deviceId.Add("10247011851");
            List_deviceId.Add("10244025543");
            List_deviceId.Add("6207564493");
            List_deviceId.Add("10245703313");
            List_deviceId.Add("10253915817");
            List_deviceId.Add("10266411032");
            List_deviceId.Add("10251436077");
            List_deviceId.Add("10248417464");
            List_deviceId.Add("10246934296");
            List_deviceId.Add("10243642553");
            List_deviceId.Add("10245812926");
            List_deviceId.Add("10246721324");
            List_deviceId.Add("10245924980");
            List_deviceId.Add("10257280434");
            List_deviceId.Add("10240008501");
            List_deviceId.Add("10239544549");
            List_deviceId.Add("10239626131");
            List_deviceId.Add("10255540934");
            List_deviceId.Add("10243467794");
            List_deviceId.Add("10260613719");
            List_deviceId.Add("10240176513");
            List_deviceId.Add("10247230633");
            List_deviceId.Add("10251957562");
            List_deviceId.Add("10241807377");
            List_deviceId.Add("10266991225");
            List_deviceId.Add("10256611580");
            List_deviceId.Add("10257360104");
            List_deviceId.Add("10267779953");
            List_deviceId.Add("10240960298");
            List_deviceId.Add("10257145180");
            List_deviceId.Add("10243744003");
            List_deviceId.Add("10260760011");
            List_deviceId.Add("10244839650");
            List_deviceId.Add("10249086546");
            List_deviceId.Add("10254114735");
            List_deviceId.Add("10243827947");
            List_deviceId.Add("10239973579");
            List_deviceId.Add("10251503446");
            List_deviceId.Add("10239759109");
            List_deviceId.Add("10266506754");
            List_deviceId.Add("10242171932");
            List_deviceId.Add("10244122238");
            List_deviceId.Add("10246874791");
            List_deviceId.Add("10253879246");
            List_deviceId.Add("10254626174");
            List_deviceId.Add("10241142002");
            List_deviceId.Add("10257473582");
            List_deviceId.Add("10245151362");
            List_deviceId.Add("10267299982");
            List_deviceId.Add("10245263284");
            List_deviceId.Add("10264820527");
            List_deviceId.Add("10259106423");
            List_deviceId.Add("10266015972");
            List_deviceId.Add("10260213368");
            List_deviceId.Add("10260933084");
            List_deviceId.Add("10255043733");
            List_deviceId.Add("10263576512");
            List_deviceId.Add("10256112887");
            List_deviceId.Add("10260113433");
            List_deviceId.Add("10254800374");
            List_deviceId.Add("10264702299");
            List_deviceId.Add("10254700957");
            List_deviceId.Add("10266701841");
            List_deviceId.Add("10267486598");
            List_deviceId.Add("10260516075");
            List_deviceId.Add("10261021464");
            List_deviceId.Add("10260428098");
            List_deviceId.Add("10261240251");
            List_deviceId.Add("10261571438");
            List_deviceId.Add("10261172732");
            List_deviceId.Add("10256253497");
            List_deviceId.Add("10263920604");
            List_deviceId.Add("10262386419");
            List_deviceId.Add("10261392289");
            List_deviceId.Add("10262984815");
            List_deviceId.Add("10254930071");
            List_deviceId.Add("10260366775");
            List_deviceId.Add("10258864685");
            List_deviceId.Add("10260881033");
            List_deviceId.Add("6204640213");
            List_deviceId.Add("10246506970");
            List_deviceId.Add("10256701032");
            List_deviceId.Add("10248376647");
            List_deviceId.Add("10251642794");
            List_deviceId.Add("10254380805");
            List_deviceId.Add("10252223623");
            List_deviceId.Add("10256417782");
            List_deviceId.Add("10257725115");
            List_deviceId.Add("10242803915");
            List_deviceId.Add("10240539651");
            List_deviceId.Add("10253379919");
            List_deviceId.Add("10239290211");
            List_deviceId.Add("10239120368");
            List_deviceId.Add("10242974229");
            List_deviceId.Add("10241612373");
            List_deviceId.Add("10248997728");
            List_deviceId.Add("10247146639");
            List_deviceId.Add("10243381987");
            List_deviceId.Add("10250759781");
            List_deviceId.Add("10244605822");
            List_deviceId.Add("10256911387");
            List_deviceId.Add("10244577830");
            List_deviceId.Add("10248655362");
            List_deviceId.Add("10251875550");
            List_deviceId.Add("10246046479");
            List_deviceId.Add("10267581123");
            List_deviceId.Add("10245663645");
            List_deviceId.Add("10266614326");
            List_deviceId.Add("10254401338");
            List_deviceId.Add("10246630667");
            List_deviceId.Add("10253642943");
            List_deviceId.Add("10237145655");
            List_deviceId.Add("10264481763");
            List_deviceId.Add("10265478203");
            List_deviceId.Add("10256390029");
            List_deviceId.Add("10262050526");
            List_deviceId.Add("10263486603");
            List_deviceId.Add("10259705029");
            List_deviceId.Add("10261653315");
            List_deviceId.Add("10262181091");
            List_deviceId.Add("10263029046");
            List_deviceId.Add("10256854452");
            List_deviceId.Add("10257821935");
            List_deviceId.Add("10258635925");
            List_deviceId.Add("10262725307");
            List_deviceId.Add("10264355041");
            List_deviceId.Add("10266129234");
            List_deviceId.Add("10263278503");
            List_deviceId.Add("10245503162");
            List_deviceId.Add("10265554161");
            List_deviceId.Add("10256072648");
            List_deviceId.Add("10265334020");
            List_deviceId.Add("10258908074");
            List_deviceId.Add("10249335573");
            List_deviceId.Add("10259840993");
            List_deviceId.Add("10259456945");
            List_deviceId.Add("10263710892");
            List_deviceId.Add("10244299340");
            List_deviceId.Add("10264535085");
            List_deviceId.Add("10263169109");
            List_deviceId.Add("10262686650");
            List_deviceId.Add("10262855425");
            List_deviceId.Add("10257920204");
            List_deviceId.Add("10259029279");
            List_deviceId.Add("10262448890");
            List_deviceId.Add("10261891910");
            List_deviceId.Add("10245361644");
            List_deviceId.Add("10250619145");
            List_deviceId.Add("10240764842");
            List_deviceId.Add("10249717168");
            List_deviceId.Add("10248800420");
            List_deviceId.Add("10252616931");
            List_deviceId.Add("10251161256");
            List_deviceId.Add("10249134433");
            List_deviceId.Add("10250452920");
            List_deviceId.Add("10251021017");
            List_deviceId.Add("10254028197");
            List_deviceId.Add("10250355744");
            List_deviceId.Add("10247617538");
            List_deviceId.Add("10267634930");
            List_deviceId.Add("10249566412");
            List_deviceId.Add("10251754410");
            List_deviceId.Add("10249482907");
            List_deviceId.Add("10250204781");
            List_deviceId.Add("10252908808");
            List_deviceId.Add("10245084314");
            List_deviceId.Add("10266249460");
            List_deviceId.Add("10239828865");
            List_deviceId.Add("10248039486");
            List_deviceId.Add("10235201894");
            List_deviceId.Add("10237237284");
            List_deviceId.Add("10248293521");
            List_deviceId.Add("10254288683");
            List_deviceId.Add("10247853515");
            List_deviceId.Add("10252121215");
            List_deviceId.Add("10253451685");
            List_deviceId.Add("10238283857");
            List_deviceId.Add("10239097872");
            List_deviceId.Add("10238440342");
            List_deviceId.Add("10237325556");
            List_deviceId.Add("10248706692");
            List_deviceId.Add("10250954893");
            List_deviceId.Add("10261747402");
            List_deviceId.Add("10252417543");
            List_deviceId.Add("10234160359");
            List_deviceId.Add("10264964139");
            List_deviceId.Add("10261434653");
            List_deviceId.Add("10242745932");
            List_deviceId.Add("10238653354");
            List_deviceId.Add("10237815893");
            List_deviceId.Add("10243968317");
            List_deviceId.Add("10265935756");
            List_deviceId.Add("10244713183");
            List_deviceId.Add("10233184179");
            List_deviceId.Add("10258079140");
            List_deviceId.Add("10263302988");
            List_deviceId.Add("10259984638");
            List_deviceId.Add("10258288573");
            List_deviceId.Add("10265869456");
            List_deviceId.Add("10261973167");
            List_deviceId.Add("10262249504");
            List_deviceId.Add("10264023529");
            List_deviceId.Add("10257544322");
            List_deviceId.Add("10267145946");
            List_deviceId.Add("10238163008");
            List_deviceId.Add("10265011477");
            List_deviceId.Add("10240329053");
            List_deviceId.Add("10247502151");
            List_deviceId.Add("10247438786");
            List_deviceId.Add("10237688498");
            List_deviceId.Add("10241061603");
            List_deviceId.Add("10259624194");
            List_deviceId.Add("10236095866");
            List_deviceId.Add("10263665206");
            List_deviceId.Add("10237013988");
            List_deviceId.Add("10247920216");
            List_deviceId.Add("10258176549");
            List_deviceId.Add("10262553855");
            List_deviceId.Add("10236748581");
            List_deviceId.Add("10241583401");
            List_deviceId.Add("10264272682");
            List_deviceId.Add("10235001897");
            List_deviceId.Add("10236803538");
            List_deviceId.Add("10255703917");
            List_deviceId.Add("10233740567");
            List_deviceId.Add("10255827480");
            List_deviceId.Add("10236609821");
            List_deviceId.Add("10255679846");
            List_deviceId.Add("10255363386");
            List_deviceId.Add("10255426539");
            List_deviceId.Add("10232560258");
            List_deviceId.Add("10255279876");
            List_deviceId.Add("10258359938");
            List_deviceId.Add("10236336459");
            List_deviceId.Add("10238792140");
            List_deviceId.Add("10237567392");
            List_deviceId.Add("10232839597");
            List_deviceId.Add("10234365835");
            List_deviceId.Add("10233817722");
            List_deviceId.Add("10235893007");
            List_deviceId.Add("10234586373");
            List_deviceId.Add("10238312092");
            List_deviceId.Add("10232916032");
            List_deviceId.Add("10233088203");
            List_deviceId.Add("10234710685");
            List_deviceId.Add("10235407215");
            List_deviceId.Add("10232655835");
            List_deviceId.Add("10234255865");
            List_deviceId.Add("10250060633");
            List_deviceId.Add("10266897572");
            List_deviceId.Add("10246373093");
            List_deviceId.Add("10250185638");
            List_deviceId.Add("10234625609");
            List_deviceId.Add("10267016765");
            List_deviceId.Add("10234070928");
            List_deviceId.Add("10250510364");
            List_deviceId.Add("10238965139");
            List_deviceId.Add("10249995574");
            List_deviceId.Add("10236436247");
            List_deviceId.Add("10236206370");
            List_deviceId.Add("10238020632");
            List_deviceId.Add("10252517759");
            List_deviceId.Add("10249603842");
            List_deviceId.Add("10233623195");
            List_deviceId.Add("10252310112");
            List_deviceId.Add("10238849376");
            List_deviceId.Add("10250860650");
            List_deviceId.Add("10253017242");
            List_deviceId.Add("10235945446");
            List_deviceId.Add("10232760701");
            List_deviceId.Add("10237478630");
            List_deviceId.Add("10236151834");
            List_deviceId.Add("10235599348");
            List_deviceId.Add("10252847747");
            List_deviceId.Add("10233340370");
            List_deviceId.Add("10233456215");
            List_deviceId.Add("10237701460");
            List_deviceId.Add("10233263561");
            List_deviceId.Add("10233901196");
            List_deviceId.Add("10234821436");
            List_deviceId.Add("10237967492");
            List_deviceId.Add("10234968107");
            List_deviceId.Add("10235132922");
            List_deviceId.Add("10238558174");
            List_deviceId.Add("10234405930");
            List_deviceId.Add("10265231825");
            List_deviceId.Add("10235324367");
            List_deviceId.Add("10258712314");
            List_deviceId.Add("10259259031");
            List_deviceId.Add("10258500246");
            List_deviceId.Add("10253260031");
            List_deviceId.Add("10254501820");
            List_deviceId.Add("10265624958");
            List_deviceId.Add("10260043742");
            List_deviceId.Add("10259339656");
            List_deviceId.Add("10265172676");
            List_deviceId.Add("10264146075");
            List_deviceId.Add("10265793653");
            List_deviceId.Add("10236997574");
            List_deviceId.Add("10263808764");
            List_deviceId.Add("10246250679");
            List_deviceId.Add("10251231255");
            List_deviceId.Add("10246444737");
            List_deviceId.Add("10247786889");
            List_deviceId.Add("10258416364");
            List_deviceId.Add("6208491748");
            List_deviceId.Add("6206552528");
            List_deviceId.Add("6231656272");
            List_deviceId.Add("6231098742");
            List_deviceId.Add("6201524114");
            List_deviceId.Add("6172927386");
            List_deviceId.Add("6180170091");
            List_deviceId.Add("6236980012");
            List_deviceId.Add("6181070276");
            List_deviceId.Add("6219436037");
            List_deviceId.Add("6212006825");
            List_deviceId.Add("6244630424");
            List_deviceId.Add("6171278726");
            List_deviceId.Add("6201335822");
            List_deviceId.Add("6170988978");
            List_deviceId.Add("6219908917");
            List_deviceId.Add("6173911150");
            List_deviceId.Add("6226666876");
            List_deviceId.Add("6210315358");
            List_deviceId.Add("6222977480");
            List_deviceId.Add("6218588677");
            List_deviceId.Add("6244368394");
            List_deviceId.Add("6226375779");
            List_deviceId.Add("6235437869");
            List_deviceId.Add("6171173697");
            List_deviceId.Add("6232140628");
            List_deviceId.Add("6244972103");
            List_deviceId.Add("6173816125");
            List_deviceId.Add("6170644194");
            List_deviceId.Add("6178996233");
            List_deviceId.Add("6218689018");
            List_deviceId.Add("6219878466");
            List_deviceId.Add("6233351858");
            List_deviceId.Add("6175361960");
            List_deviceId.Add("6222332931");
            List_deviceId.Add("6239260498");
            List_deviceId.Add("6229853346");
            List_deviceId.Add("6233189651");
            List_deviceId.Add("6232992646");
            List_deviceId.Add("6181754015");
            List_deviceId.Add("6171874334");
            List_deviceId.Add("6177453601");
            List_deviceId.Add("6232779192");
            List_deviceId.Add("6221617715");
            List_deviceId.Add("6174764830");
            List_deviceId.Add("6232673827");
            List_deviceId.Add("6227110910");
            List_deviceId.Add("6211182359");
            List_deviceId.Add("6227323190");
            List_deviceId.Add("6211248136");
            List_deviceId.Add("6179004739");
            List_deviceId.Add("6222150001");
            List_deviceId.Add("6171999223");
            List_deviceId.Add("6210741194");
            List_deviceId.Add("6176606512");
            List_deviceId.Add("6177854105");
            List_deviceId.Add("6231946778");
            List_deviceId.Add("6228310872");
            List_deviceId.Add("6241910435");
            List_deviceId.Add("6238532767");
            List_deviceId.Add("6242322884");
            List_deviceId.Add("6243075267");
            List_deviceId.Add("6235562361");
            List_deviceId.Add("6232477761");
            List_deviceId.Add("6173704280");
            List_deviceId.Add("6240599138");
            List_deviceId.Add("6227537963");
            List_deviceId.Add("6207293578");
            List_deviceId.Add("6206690053");
            List_deviceId.Add("6208392234");
            List_deviceId.Add("6219001111");
            List_deviceId.Add("6175154889");
            List_deviceId.Add("6208073033");
            List_deviceId.Add("6220532668");
            List_deviceId.Add("6221036526");
            List_deviceId.Add("6207422912");
            List_deviceId.Add("6208106918");
            List_deviceId.Add("6203850001");
            List_deviceId.Add("6235757154");
            List_deviceId.Add("6206820413");
            List_deviceId.Add("6221298468");
            List_deviceId.Add("6176281540");
            List_deviceId.Add("6208886319");
            List_deviceId.Add("6208778561");
            List_deviceId.Add("6206486190");
            List_deviceId.Add("6181939798");
            List_deviceId.Add("6207993456");
            List_deviceId.Add("6207773627");
            List_deviceId.Add("6207364725");
            List_deviceId.Add("6244708588");
            List_deviceId.Add("6223072367");
            List_deviceId.Add("6206204805");
            List_deviceId.Add("6204894886");
            List_deviceId.Add("6206704365");
            List_deviceId.Add("6245011123");
            List_deviceId.Add("6211524198");
            List_deviceId.Add("6234476267");
            List_deviceId.Add("6239694957");
            List_deviceId.Add("6240121958");
            List_deviceId.Add("6234382586");
            List_deviceId.Add("6236647522");
            List_deviceId.Add("6242741149");
            List_deviceId.Add("6241169523");
            List_deviceId.Add("6241489399");
            List_deviceId.Add("6232078920");
            List_deviceId.Add("6210524986");
            List_deviceId.Add("6212562290");
            List_deviceId.Add("6211037997");
            List_deviceId.Add("6230508072");
            List_deviceId.Add("6227051815");
            List_deviceId.Add("6212945325");
            List_deviceId.Add("6232386523");
            List_deviceId.Add("6212397163");
            List_deviceId.Add("6210183557");
            List_deviceId.Add("6210498840");
            List_deviceId.Add("6245123633");
            List_deviceId.Add("6181356136");
            List_deviceId.Add("6181100138");
            List_deviceId.Add("6179199537");
            List_deviceId.Add("6178700795");
            List_deviceId.Add("6204564670");
            List_deviceId.Add("6177966845");
            List_deviceId.Add("6203931876");
            List_deviceId.Add("6202062910");
            List_deviceId.Add("6176870215");
            List_deviceId.Add("6202118053");
            List_deviceId.Add("6201492301");
            List_deviceId.Add("6204337453");
            List_deviceId.Add("6176132315");
            List_deviceId.Add("6204095761");
            List_deviceId.Add("6171517419");
            List_deviceId.Add("6204462904");
            List_deviceId.Add("6178374774");
            List_deviceId.Add("6202288936");
            List_deviceId.Add("6204710281");
            List_deviceId.Add("6665272821");
            List_deviceId.Add("6202383967");
            List_deviceId.Add("6170479504");
            List_deviceId.Add("6180944395");
            List_deviceId.Add("6205010682");
            List_deviceId.Add("6202987339");
            List_deviceId.Add("6202881113");
            List_deviceId.Add("6209655053");
            List_deviceId.Add("6201080763");
            List_deviceId.Add("6201125435");
            List_deviceId.Add("6202590634");
            List_deviceId.Add("6202680053");
            List_deviceId.Add("6202438562");
            List_deviceId.Add("6203405861");
            List_deviceId.Add("6179285675");
            List_deviceId.Add("6205259163");
            List_deviceId.Add("6205107975");
            List_deviceId.Add("6204262565");
            List_deviceId.Add("6241808941");
            List_deviceId.Add("6205323118");
            List_deviceId.Add("6178045816");
            List_deviceId.Add("6179839647");
            List_deviceId.Add("6171080763");
            List_deviceId.Add("6201252883");
            List_deviceId.Add("6181554044");
            List_deviceId.Add("6181235830");
            List_deviceId.Add("6180327867");
            List_deviceId.Add("6175537920");
            List_deviceId.Add("6201633906");
            List_deviceId.Add("6172045596");
            List_deviceId.Add("6175415111");
            List_deviceId.Add("6200907176");
            List_deviceId.Add("6170511673");
            List_deviceId.Add("6172251404");
            List_deviceId.Add("6172819559");
            List_deviceId.Add("6203372597");
            List_deviceId.Add("6202746480");
            List_deviceId.Add("6230748013");
            List_deviceId.Add("6210658833");
            List_deviceId.Add("6177340025");
            List_deviceId.Add("6174945966");
            List_deviceId.Add("6180255516");
            List_deviceId.Add("6225736125");
            List_deviceId.Add("6178641010");
            List_deviceId.Add("6235398605");
            List_deviceId.Add("6172101879");
            List_deviceId.Add("6180731755");
            List_deviceId.Add("6174298410");
            List_deviceId.Add("6201786838");
            List_deviceId.Add("6226884006");
            List_deviceId.Add("6179529044");
            List_deviceId.Add("6179469631");
            List_deviceId.Add("6242078378");
            List_deviceId.Add("6177503710");
            List_deviceId.Add("6230007561");
            List_deviceId.Add("6242910593");
            List_deviceId.Add("6179753931");
            List_deviceId.Add("6213353995");
            List_deviceId.Add("6220874631");
            List_deviceId.Add("6211482058");
            List_deviceId.Add("6275437912");
            List_deviceId.Add("6238991310");
            List_deviceId.Add("6244566439");
            List_deviceId.Add("6238049673");
            List_deviceId.Add("6228812932");
            List_deviceId.Add("6237927372");
            List_deviceId.Add("6236408387");
            List_deviceId.Add("6245647067");
            List_deviceId.Add("6237387071");
            List_deviceId.Add("6231365445");
            List_deviceId.Add("6241043227");
            List_deviceId.Add("6234848409");
            List_deviceId.Add("6233471223");
            List_deviceId.Add("6238127527");
            List_deviceId.Add("6241569662");
            List_deviceId.Add("6234614695");
            List_deviceId.Add("6240368695");
            List_deviceId.Add("6230941521");
            List_deviceId.Add("6239453250");
            List_deviceId.Add("6240930004");
            List_deviceId.Add("6233857572");
            List_deviceId.Add("6236081087");
            List_deviceId.Add("6245586887");
            List_deviceId.Add("6237506236");
            List_deviceId.Add("6226939495");
            List_deviceId.Add("6229082773");
            List_deviceId.Add("6237781455");
            List_deviceId.Add("6246054126");
            List_deviceId.Add("6225566514");
            List_deviceId.Add("6228950270");
            List_deviceId.Add("6228636232");
            List_deviceId.Add("6230240450");
            List_deviceId.Add("6237189035");
            List_deviceId.Add("6224914687");
            List_deviceId.Add("6238399852");
            List_deviceId.Add("6243834493");
            List_deviceId.Add("6235229032");
            List_deviceId.Add("6226554446");
            List_deviceId.Add("6236799288");
            List_deviceId.Add("6242469253");
            List_deviceId.Add("6238210158");
            List_deviceId.Add("6235624366");
            List_deviceId.Add("6240276717");
            List_deviceId.Add("6231529004");
            List_deviceId.Add("6232549562");
            List_deviceId.Add("6278607506");
            List_deviceId.Add("6218254567");
            List_deviceId.Add("6237668574");
            List_deviceId.Add("6234242613");
            List_deviceId.Add("6230439113");
            List_deviceId.Add("6278885733");
            List_deviceId.Add("6245479430");
            List_deviceId.Add("6236560599");
            List_deviceId.Add("6228405280");
            List_deviceId.Add("6218402962");
            List_deviceId.Add("6232254541");
            List_deviceId.Add("6239538314");
            List_deviceId.Add("6229663074");
            List_deviceId.Add("6225011500");
            List_deviceId.Add("6242884116");
            List_deviceId.Add("6221487384");
            List_deviceId.Add("6219695422");
            List_deviceId.Add("6173626634");
            List_deviceId.Add("6222043482");
            List_deviceId.Add("6170369574");
            List_deviceId.Add("6213243025");
            List_deviceId.Add("6181695067");
            List_deviceId.Add("6281023536");
            List_deviceId.Add("6220269632");
            List_deviceId.Add("6209992732");
            List_deviceId.Add("6237212914");
            List_deviceId.Add("6300997376");
            List_deviceId.Add("6221347298");
            List_deviceId.Add("6240735130");
            List_deviceId.Add("6227842173");
            List_deviceId.Add("6244091139");
            List_deviceId.Add("6244469967");
            List_deviceId.Add("6181882927");
            List_deviceId.Add("6179654387");
            List_deviceId.Add("6231453744");
            List_deviceId.Add("6240442620");
            List_deviceId.Add("6172367777");
            List_deviceId.Add("6243423203");
            List_deviceId.Add("6234730726");
            List_deviceId.Add("6173541534");
            List_deviceId.Add("6243271703");
            List_deviceId.Add("6225659069");
            List_deviceId.Add("6241698404");
            List_deviceId.Add("6237402965");
            List_deviceId.Add("6227290571");
            List_deviceId.Add("6218929645");
            List_deviceId.Add("6210078747");
            List_deviceId.Add("6212480146");
            List_deviceId.Add("6177265783");
            List_deviceId.Add("6179327308");
            List_deviceId.Add("6176546298");
            List_deviceId.Add("6174523355");
            List_deviceId.Add("6218757213");
            List_deviceId.Add("6234034768");
            List_deviceId.Add("6239902461");
            List_deviceId.Add("6172581936");
            List_deviceId.Add("6170777118");
            List_deviceId.Add("6225110909");
            List_deviceId.Add("6734009828");
            List_deviceId.Add("6178576524");
            List_deviceId.Add("6229304336");
            List_deviceId.Add("6233672784");
            List_deviceId.Add("10247325283");
            List_deviceId.Add("10248521745");
            List_deviceId.Add("10252051252");
            List_deviceId.Add("10259529107");
            List_deviceId.Add("10239486340");
            List_deviceId.Add("10264666051");
            List_deviceId.Add("10255128236");
            List_deviceId.Add("10255916864");
            List_deviceId.Add("6243669136");
            List_deviceId.Add("6219218058");
            List_deviceId.Add("6234902334");
            List_deviceId.Add("6173343636");
            List_deviceId.Add("6176407315");
            List_deviceId.Add("10251343896");
            List_deviceId.Add("10257682951");
            List_deviceId.Add("6245846003");
            List_deviceId.Add("6219300143");
            List_deviceId.Add("6222621233");
            List_deviceId.Add("6182023037");
            List_deviceId.Add("6180518061");
            List_deviceId.Add("6213148077");
            List_deviceId.Add("6243944350");
            List_deviceId.Add("6220907808");
            List_deviceId.Add("6217593017");
            List_deviceId.Add("6217629621");
            List_deviceId.Add("6212848475");
            List_deviceId.Add("6173021158");
            List_deviceId.Add("6180077214");
            List_deviceId.Add("6172485084");
            List_deviceId.Add("10257043484");
            List_deviceId.Add("10253760218");
            List_deviceId.Add("10243057549");
            List_deviceId.Add("10236581498");
            List_deviceId.Add("10245424335");
            List_deviceId.Add("6180470321");
            List_deviceId.Add("6211763679");
            List_deviceId.Add("6237059567");
            List_deviceId.Add("6217949795");
            List_deviceId.Add("6239096570");
            List_deviceId.Add("6180601972");
            List_deviceId.Add("6242281657");
            List_deviceId.Add("6207044520");
            List_deviceId.Add("6240656770");
            List_deviceId.Add("6208517888");
            List_deviceId.Add("6245270680");
            List_deviceId.Add("6207890116");
            List_deviceId.Add("6208299987");
            List_deviceId.Add("6206950058");
            List_deviceId.Add("6207138487");
            List_deviceId.Add("6177725880");
            List_deviceId.Add("6170852684");
            List_deviceId.Add("6207679133");
            List_deviceId.Add("6204138710");
            List_deviceId.Add("6206340296");
            List_deviceId.Add("6208697466");
            List_deviceId.Add("6224828663");
            List_deviceId.Add("6203673165");
            List_deviceId.Add("6181454800");
            List_deviceId.Add("6203223552");
            List_deviceId.Add("6182185799");
            List_deviceId.Add("6174400378");
            List_deviceId.Add("6204979816");
            List_deviceId.Add("6178439669");
            List_deviceId.Add("6201952137");
            List_deviceId.Add("6203127880");
            List_deviceId.Add("6203519167");
            List_deviceId.Add("6201856818");
            List_deviceId.Add("6177160756");
            List_deviceId.Add("6203775652");
            List_deviceId.Add("6175817505");
            List_deviceId.Add("6178877334");
            List_deviceId.Add("6178145504");
            List_deviceId.Add("6175789606");
            List_deviceId.Add("6203040092");
            List_deviceId.Add("6176318024");
            List_deviceId.Add("6180852322");
            List_deviceId.Add("6175052291");
            List_deviceId.Add("6174615384");
            List_deviceId.Add("6210256605");
            List_deviceId.Add("6219549704");
            List_deviceId.Add("6222795523");
            List_deviceId.Add("6222561082");
            List_deviceId.Add("6227666850");
            List_deviceId.Add("6173417043");
            List_deviceId.Add("6171499477");
            List_deviceId.Add("6229412989");
            List_deviceId.Add("6174022245");
            List_deviceId.Add("6229543551");
            List_deviceId.Add("6174382248");
            List_deviceId.Add("6178223087");
            List_deviceId.Add("6171610758");
            List_deviceId.Add("6239132534");
            List_deviceId.Add("6175910936");
            List_deviceId.Add("6175293430");
            List_deviceId.Add("6171387123");
            List_deviceId.Add("6227498199");
            List_deviceId.Add("6179956220");
            List_deviceId.Add("6173244138");
            List_deviceId.Add("6177683510");
            List_deviceId.Add("6172711849");
            List_deviceId.Add("6210933544");
            List_deviceId.Add("6211660429");
            List_deviceId.Add("6240867207");
            List_deviceId.Add("6211949360");
            List_deviceId.Add("6222282508");
            List_deviceId.Add("6211840796");
            List_deviceId.Add("6217782347");
            List_deviceId.Add("6222453027");
            List_deviceId.Add("6213788350");
            List_deviceId.Add("6213547179");
            List_deviceId.Add("6220337631");
            List_deviceId.Add("6220132030");
            List_deviceId.Add("6221910304");
            List_deviceId.Add("6212618457");
            List_deviceId.Add("6220766553");
            List_deviceId.Add("6211353090");
            List_deviceId.Add("6225205864");
            List_deviceId.Add("6212185966");
            List_deviceId.Add("6218071331");
            List_deviceId.Add("6213053800");
            List_deviceId.Add("6213695310");
            List_deviceId.Add("6218341831");
            List_deviceId.Add("6218889355");
            List_deviceId.Add("6220629506");
            List_deviceId.Add("6217487544");
            List_deviceId.Add("6175645216");
            List_deviceId.Add("6239765735");
            List_deviceId.Add("6235074938");
            List_deviceId.Add("6238885367");
            List_deviceId.Add("6233970390");
            List_deviceId.Add("6245332461");
            List_deviceId.Add("6225852947");
            List_deviceId.Add("6226165270");
            List_deviceId.Add("6243766237");
            List_deviceId.Add("6244296191");
            List_deviceId.Add("6242520428");
            List_deviceId.Add("6243133200");
            List_deviceId.Add("6221795973");
            List_deviceId.Add("6226788097");
            List_deviceId.Add("6226450855");
            List_deviceId.Add("6231842508");
            List_deviceId.Add("6176999336");
            List_deviceId.Add("6235957093");
            List_deviceId.Add("6229163449");
            List_deviceId.Add("6244809410");
            List_deviceId.Add("6225905675");
            List_deviceId.Add("6278517688");
            List_deviceId.Add("6233520485");
            List_deviceId.Add("6237815360");
            List_deviceId.Add("6241752560");
            List_deviceId.Add("6229244764");
            List_deviceId.Add("6220040748");
            List_deviceId.Add("6219776330");
            List_deviceId.Add("6172629251");
            List_deviceId.Add("6173195849");
            List_deviceId.Add("6238654030");
            List_deviceId.Add("6236238291");
            List_deviceId.Add("6278772730");
            List_deviceId.Add("6242163948");
            List_deviceId.Add("6278430579");
            List_deviceId.Add("6241360449");
            List_deviceId.Add("6229712569");
            List_deviceId.Add("6232887063");
            List_deviceId.Add("6222895282");
            List_deviceId.Add("6230680423");
            List_deviceId.Add("6243536602");
            List_deviceId.Add("6220466572");
            List_deviceId.Add("6221578795");
            List_deviceId.Add("6174191442");
            List_deviceId.Add("6226245536");
            List_deviceId.Add("6245902302");
            List_deviceId.Add("6227705118");
            List_deviceId.Add("6231259381");
            List_deviceId.Add("6171766699");
            List_deviceId.Add("6231743614");
            List_deviceId.Add("6242672729");
            List_deviceId.Add("6217883961");
            List_deviceId.Add("6174854904");
            List_deviceId.Add("6219114065");
            List_deviceId.Add("6224264022");
            List_deviceId.Add("6230309523");
            List_deviceId.Add("6176075694");
            List_deviceId.Add("6221183541");
            List_deviceId.Add("6243305808");
            List_deviceId.Add("6177020493");
            List_deviceId.Add("6233027183");
            List_deviceId.Add("6226030691");
            List_deviceId.Add("6225336176");
            List_deviceId.Add("6218105858");
            List_deviceId.Add("6176760340");
            List_deviceId.Add("6231147598");
            List_deviceId.Add("6212250069");
            List_deviceId.Add("6182299831");
            List_deviceId.Add("6228134874");
            List_deviceId.Add("6212726347");
            List_deviceId.Add("6236827013");
            List_deviceId.Add("6213450153");
            List_deviceId.Add("6210817291");
            List_deviceId.Add("6223146608");
            List_deviceId.Add("6227912497");
            List_deviceId.Add("6239354315");
            List_deviceId.Add("6236397244");
            List_deviceId.Add("6233212145");
            List_deviceId.Add("6225437402");
            List_deviceId.Add("6233704190");
            List_deviceId.Add("6228525814");
            List_deviceId.Add("6228023475");
            List_deviceId.Add("6241268571");
            List_deviceId.Add("6236182798");
            List_deviceId.Add("6228251924");
            List_deviceId.Add("6228758161");
            List_deviceId.Add("6245738858");
            List_deviceId.Add("6238720128");
            List_deviceId.Add("6235816058");
            List_deviceId.Add("6230147248");
            List_deviceId.Add("6235163418");
            List_deviceId.Add("6229916701");
            List_deviceId.Add("6238474325");
            List_deviceId.Add("6221860040");

            return List_deviceId;
        }

        private Dictionary<string, QuickMixTank> CreateQuickMixTank()
        {
            Dictionary<string, QuickMixTank> Dic_Qmt = new Dictionary<string, QuickMixTank>();

            Dic_Qmt.Add("MM01", new QuickMixTank("MM01"));
            Dic_Qmt.Add("MM02", new QuickMixTank("MM02"));
            Dic_Qmt.Add("MM03", new QuickMixTank("MM03"));
            Dic_Qmt.Add("MM04", new QuickMixTank("MM04"));
            Dic_Qmt.Add("MM05", new QuickMixTank("MM05"));
            Dic_Qmt.Add("MM06", new QuickMixTank("MM06"));
            Dic_Qmt.Add("MM07", new QuickMixTank("MM07"));
            Dic_Qmt.Add("MM08", new QuickMixTank("MM08"));
            Dic_Qmt.Add("MM09", new QuickMixTank("MM09"));
            Dic_Qmt.Add("MM10", new QuickMixTank("MM10"));
            Dic_Qmt.Add("MM11", new QuickMixTank("MM11"));
            Dic_Qmt.Add("MM12", new QuickMixTank("MM12"));
            Dic_Qmt.Add("ww01", new QuickMixTank("ww01"));
            Dic_Qmt.Add("ww02", new QuickMixTank("ww02"));
            Dic_Qmt.Add("ww03", new QuickMixTank("ww03"));
            Dic_Qmt.Add("ww04", new QuickMixTank("ww04"));
            Dic_Qmt.Add("ww05", new QuickMixTank("ww05"));
            Dic_Qmt.Add("ww06", new QuickMixTank("ww06"));
            Dic_Qmt.Add("ww07", new QuickMixTank("ww07"));
            Dic_Qmt.Add("dd01", new QuickMixTank("dd01"));
            Dic_Qmt.Add("dd02", new QuickMixTank("dd02"));
            Dic_Qmt.Add("dd03", new QuickMixTank("dd03"));
            Dic_Qmt.Add("dd04", new QuickMixTank("dd04"));
            Dic_Qmt.Add("dd05", new QuickMixTank("dd05"));
            Dic_Qmt.Add("dd06", new QuickMixTank("dd06"));
            Dic_Qmt.Add("dd07", new QuickMixTank("dd07"));
            Dic_Qmt.Add("dd08", new QuickMixTank("dd08"));
            Dic_Qmt.Add("dd09", new QuickMixTank("dd09"));
            Dic_Qmt.Add("dd10", new QuickMixTank("dd10"));
            Dic_Qmt.Add("dd11", new QuickMixTank("dd11"));
            Dic_Qmt.Add("dd12", new QuickMixTank("dd12"));
            Dic_Qmt.Add("dd13", new QuickMixTank("dd13"));
            Dic_Qmt.Add("dd14", new QuickMixTank("dd14"));
            Dic_Qmt.Add("dd15", new QuickMixTank("dd15"));
            Dic_Qmt.Add("dd16", new QuickMixTank("dd16"));
            Dic_Qmt.Add("dd17", new QuickMixTank("dd17"));
            Dic_Qmt.Add("dd18", new QuickMixTank("dd18"));
            Dic_Qmt.Add("dd19", new QuickMixTank("dd19"));
            Dic_Qmt.Add("dd20", new QuickMixTank("dd20"));
            Dic_Qmt.Add("dd21", new QuickMixTank("dd21"));
            Dic_Qmt.Add("dd22", new QuickMixTank("dd22"));
            Dic_Qmt.Add("dd23", new QuickMixTank("dd23"));
            Dic_Qmt.Add("dd24", new QuickMixTank("dd24"));
            Dic_Qmt.Add("dd25", new QuickMixTank("dd25"));
            Dic_Qmt.Add("dd26", new QuickMixTank("dd26"));
            Dic_Qmt.Add("dd27", new QuickMixTank("dd27"));
            Dic_Qmt.Add("dd28", new QuickMixTank("dd28"));
            Dic_Qmt.Add("dd29", new QuickMixTank("dd29"));
            Dic_Qmt.Add("dd30", new QuickMixTank("dd30"));
            Dic_Qmt.Add("dd31", new QuickMixTank("dd31"));
            Dic_Qmt.Add("HH00", new QuickMixTank("HH00"));
            Dic_Qmt.Add("HH01", new QuickMixTank("HH01"));
            Dic_Qmt.Add("HH02", new QuickMixTank("HH02"));
            Dic_Qmt.Add("HH03", new QuickMixTank("HH03"));
            Dic_Qmt.Add("HH04", new QuickMixTank("HH04"));
            Dic_Qmt.Add("HH05", new QuickMixTank("HH05"));
            Dic_Qmt.Add("HH06", new QuickMixTank("HH06"));
            Dic_Qmt.Add("HH07", new QuickMixTank("HH07"));
            Dic_Qmt.Add("HH08", new QuickMixTank("HH08"));
            Dic_Qmt.Add("HH09", new QuickMixTank("HH09"));
            Dic_Qmt.Add("HH10", new QuickMixTank("HH10"));
            Dic_Qmt.Add("HH11", new QuickMixTank("HH11"));
            Dic_Qmt.Add("HH12", new QuickMixTank("HH12"));
            Dic_Qmt.Add("HH13", new QuickMixTank("HH13"));
            Dic_Qmt.Add("HH14", new QuickMixTank("HH14"));
            Dic_Qmt.Add("HH15", new QuickMixTank("HH15"));
            Dic_Qmt.Add("HH16", new QuickMixTank("HH16"));
            Dic_Qmt.Add("HH17", new QuickMixTank("HH17"));
            Dic_Qmt.Add("HH18", new QuickMixTank("HH18"));
            Dic_Qmt.Add("HH19", new QuickMixTank("HH19"));
            Dic_Qmt.Add("HH20", new QuickMixTank("HH20"));
            Dic_Qmt.Add("HH21", new QuickMixTank("HH21"));
            Dic_Qmt.Add("HH22", new QuickMixTank("HH22"));
            Dic_Qmt.Add("HH23", new QuickMixTank("HH23"));
            Dic_Qmt.Add("yyyy2018", new QuickMixTank("yyyy2018"));
            Dic_Qmt.Add("yyyy2019", new QuickMixTank("yyyy2019"));
            Dic_Qmt.Add("yyyy2020", new QuickMixTank("yyyy2020"));
            Dic_Qmt.Add("SS01", new QuickMixTank("SS01"));
            Dic_Qmt.Add("SS02", new QuickMixTank("SS02"));
            Dic_Qmt.Add("SS03", new QuickMixTank("SS03"));
            Dic_Qmt.Add("SS04", new QuickMixTank("SS04"));

            return Dic_Qmt;
        }

        private string GetConfigFolder()
        {
            string ProjectFolderPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));
            return ProjectFolderPath + @"CsvFileReader\CsvFileReaderConfig\";
        }
    }
}
