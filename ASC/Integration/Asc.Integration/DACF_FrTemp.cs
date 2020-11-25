using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Asc
{
    public class DACF_FrTemp
    {
        public string FilePath;
        public string[] FileNames;
        public CsvFileAnalyzer Cfa;
        private Dal_FrTemp Dal;

        public string DateTime_Start;

        public DACF_FrTemp()
        {
            Dic_trendTempArr = new ConcurrentDictionary<string, double[]>();
            Dic_noiseTempArr = new ConcurrentDictionary<string, double[]>();

            Dic_FpiLower = new ConcurrentDictionary<string, double>();
            Dic_FpiUpper = new ConcurrentDictionary<string, double>();

            FileNames = new string[] {
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
                "784",
                "786",
                "793",
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
                "11249"
              };
        }

        // CFA
        private ConcurrentDictionary<string, List<string>> Dic_STAID_List = new ConcurrentDictionary<string, List<string>>();
        private ConcurrentDictionary<string, List<string>> Dic_SOUID_List = new ConcurrentDictionary<string, List<string>>();
        private ConcurrentDictionary<string, List<string>> Dic_DATE_List = new ConcurrentDictionary<string, List<string>>();
        private ConcurrentDictionary<string, List<double>> Dic_TG_List = new ConcurrentDictionary<string, List<double>>();
        private ConcurrentDictionary<string, List<string>> Dic_Q_TG_List = new ConcurrentDictionary<string, List<string>>();

        public bool InterIntegratedSpikeAnalyzer()
        {
            bool res = true;
            // -9999 的 Spike 拿掉

            // Spike 整合指標，by 月份統計

            // Suspended: 連續或接近的 Spike 整合(True Spikes)

            return res;
        }

        // SSA
        private ConcurrentDictionary<string, double[]> Dic_trendTempArr;
        private ConcurrentDictionary<string, double[]> Dic_noiseTempArr;
        // OTA
        private ConcurrentDictionary<string, double> Dic_FpiLower;
        private ConcurrentDictionary<string, double> Dic_FpiUpper;

        public bool UseCsvFileAnalyzer()
        {
            bool result = true;
            foreach (string value in FileNames)
            {
                // 1. Creation Management
                Cfa = new CsvFileAnalyzer();
                string sID = "TG_STAID" + value.PadLeft(6, '0');
                Cfa.FilePath = @"C:\Workspace\Publications\EIA\Model\ECA_blend_tg\" + sID + ".txt";
                Cfa.Delimiters = new char[] { ',', ' ' };

                CsvFileStructure Cfs = new CsvFileStructure();
                Cfs.HeaderLineStartAt = 21;
                Cfs.DataLinesStartAt = 22;
                Cfs.FooterLinesCount = 0;

                Dal = new Dal_FrTemp();
                DatalineEntityAndFormat Def = new DatalineEntityAndFormat();

                // 2. Dependency Management
                Cfa.Cfs = Cfs;
                Dal.Def = Def;
                Cfa.Dal = Dal;

                // 3. Read Csv File
                result &= Cfa.ReadCsvFile();

                // TODO: Encapsulte by Class
                Dic_STAID_List.TryAdd(sID, Dal.STAID);
                Dic_SOUID_List.TryAdd(sID, Dal.SOUID);
                Dic_DATE_List.TryAdd(sID, Dal.DATE);
                Dic_TG_List.TryAdd(sID, Dal.TG);
                Dic_Q_TG_List.TryAdd(sID, Dal.Q_TG);

                // Store as Meta
                StoreArrayAsMetaCsv(Dal.STAID.ToArray(), Dal.DATE.ToArray(), Dal.TG.ToArray(), Dal.Q_TG.ToArray(), "RawTemp_" + sID);
            }

            // 4. Store as Excel 
            //Efa_xlsx_DicDoubleList Ea = new Efa_xlsx_DicDoubleList();
            //Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
            //            + "Result\\Result_Summary\\" + "Result_Auto_Data_Original" + ".xlsx";
            //Ea.SheetName = "Original";
            //Ea.dicListDate = Dal.dicListDate;
            //Ea.dicListFpi = Dal.dicListFpi;
            //Ea.CreateExcel();

            return result;
        }

        public bool UseSingularSpectrumAnalyzer()
        {
            bool result = true;
            try
            {
                foreach (KeyValuePair<string, List<double>> entry in Dic_TG_List)
                {
                    // do something with entry.Value or entry.Key
                    SingularSpectrumAnalyzer SsaTemp = new SingularSpectrumAnalyzer();
                    SsaTemp.SetAddSequences(entry.Value.ToArray());
                    SsaTemp.SetWindow(3);
                    SsaTemp.SetAlgoTopKDirect(1);
                    SsaTemp.AnalyzeSequence(out double[] trendTempArr, out double[] noiseTempArr);
                    Dic_trendTempArr.TryAdd(entry.Key, trendTempArr);
                    Dic_noiseTempArr.TryAdd(entry.Key, noiseTempArr);
                    //StoreArrayAsMetaCsv(Dic_DATE_List[entry.Key].ToArray(), trendTempArr, "trendTemp_" + entry.Key.ToString());
                    //StoreArrayAsResultCsv(Dal.dicListDate[entry.Key].ToArray(), trendFpiArr, "Trend\\" + "trendFpi_" + entry.Key.ToString());
                    //StoreArrayAsResultCsv(Dal.dicListDate[entry.Key].ToArray(), noiseFpiArr, "Noise\\" + "noiseFpi_" + entry.Key.ToString());
                }

                //Efa_xlsx_DicDoubleArrTemp Ea = new Efa_xlsx_DicDoubleArrTemp();
                //Ea.dicListDate = Dic_DATE_List;

                //Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
                //            + "Result\\Result_Summary\\" + "Result_Auto_Data_Trend_Temp" + ".xlsx";
                //Ea.SheetName = "Trend";
                //Ea.dicArrData = Dic_trendTempArr;
                //Ea.CreateExcel();

                //Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
                //            + "Result\\Result_Summary\\" + "Result_Auto_Data_Noise_Temp" + ".xlsx";
                //Ea.SheetName = "Noise";
                //Ea.dicArrData = Dic_noiseTempArr;
                //Ea.CreateExcel();
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool UseOutlierTrimmingAnalyzer()
        {
            bool result = true;

            foreach (KeyValuePair<string, double[]> entry in Dic_noiseTempArr)
            {
                // do something with entry.Value or entry.Key
                OutlierTrimmingAnalyzer OtaFpi = new OutlierTrimmingAnalyzer();
                result &= OtaFpi.SetOriginalSeriesAndDoAnalysis(entry.Value);
                OtaFpi.GetConfidenceIntervals(out double LowerFpi, out double UpperFpi);
                Dic_FpiLower.TryAdd(entry.Key, LowerFpi);
                Dic_FpiUpper.TryAdd(entry.Key, UpperFpi);
                //StoreArrayAsResultCsv(LowerFpi, UpperFpi, "Ota\\" + "OtaFpi_" + entry.Key);
            }

            Efa_xlsx_DicDouble Ea = new Efa_xlsx_DicDouble();
            Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
                        + "Result\\Result_Summary\\" + "Result_Auto_Data_Ota" + ".xlsx";
            Ea.SheetName = "Ota";
            Ea.Dic_FpiLower = Dic_FpiLower;
            Ea.Dic_FpiUpper = Dic_FpiUpper;
            Ea.CreateExcel();

            return result;
        }

        public bool IntraRawSpikeAnalyzer()
        {
            try
            {
                foreach (KeyValuePair<string, List<double>> entry in Dic_TG_List)
                {
                    SpikeAnalyzer SpaTemp = new SpikeAnalyzer();
                    SpaTemp._dataPath = AppDomain.CurrentDomain.BaseDirectory
                        + @"Meta\" + "RawTemp_" + entry.Key + ".txt";
                    SpaTemp._hasHeader = false;
                    SpaTemp._separatorChar = ',';
                    SpaTemp._docName = "SpaTemp_" + entry.Key;
                    SpaTemp.Confidence = 95;

                    SpaTemp._docsize = entry.Value.Count;   // Total Days, roughly 5386 days for 2005-2019 (15 years)
                    SpaTemp.SlidingWindowDivided = 92;      // One Window per Season (31+30+31=92)

                    SpaTemp.DateTime_Start = DateTime_Start;
                    SpaTemp.RunAnalysis();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void StoreArrayAsResultCsv(double Index1, double Index2, string FileName)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory
                + @"Result\" + DateTime.Now.ToString("yyyyMMdd-HHmm") + "\\" + FileName + ".csv";
            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                file.WriteLine(string.Format("{0},{1}", Index1, Index2));
            }
        }

        private void StoreArrayAsResultCsv(string[] DateArr, double[] IndexArr, string FileName)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory
                + @"Result\" + DateTime.Now.ToString("yyyyMMdd-HHmm") + "\\" + FileName + ".csv";
            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                int len = IndexArr.Length;
                for (int i = 0; i < len; i++)
                {
                    file.WriteLine(string.Format("{0},{1}", DateArr[i], IndexArr[i]));
                }
            }
        }

        private void StoreArrayAsMetaCsv(string[] STAIDArr, string[] DATEArr, double[] TGArr, string[] Q_TGArr, string FileName)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory
                + @"Meta\" + FileName + ".txt";
            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                int len = TGArr.Length;
                for (int i = 0; i < len; i++)
                {
                    file.WriteLine(string.Format("{0},{1},{2},{3}", STAIDArr[i], DATEArr[i], TGArr[i], Q_TGArr[i]));
                }
            }
        }
    }
}
