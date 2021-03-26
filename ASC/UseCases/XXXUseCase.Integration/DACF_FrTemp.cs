﻿using System;
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
                }
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

            Efa_Dic_Double_Double_Ota Ea = new Efa_Dic_Double_Double_Ota();
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
                        + @"Meta\DACF_FrTemp\" + @"Cfa\" + "RawTemp_" + entry.Key + ".txt";
                    SpaTemp._hasHeader = false;
                    SpaTemp._separatorChar = ',';
                    SpaTemp._docName = "SpaTemp_" + entry.Key;
                    SpaTemp.Confidence = 95;

                    SpaTemp._docsize = entry.Value.Count;   // No Use for now. Total Days, roughly 5475 days for 2005-2019 (15 years*365=5475)
                    SpaTemp.SlidingWindowDivided = 92;    // How many spikes you want to detect in whole period? (15y * 12 spikes per year)
                    // One Window per Season (31+30+31=92) or Half Year (30*6=180) or Year (30*12=360)

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

        public bool InterIntegratedSpikeAnalyzer()
        {
            bool res = true;

            // Create Tanks
            //List<string> TankIdList = new List<string>();
            SortedDictionary<string, double> Dic_SpikeCounts = new SortedDictionary<string, double>();
            //List<Double> List_SpikeCounts = new List<double>();

            // Y2005M01 ~ Y2019M12
            for (int Y = 2005; Y <= 2019; Y++)
            {
                for (int M = 1; M <= 12; M++)
                {
                    string key = Y.ToString("0000") + M.ToString("00");
                    Dic_SpikeCounts.Add(key, new double());
                }
            }
            //TankFactory.CreatePst(TankIdList, out Dictionary<string, PrimarySedimentationTank> Dic_Pst);


            // Read Spa files for each climate station
            string[] files =
                Directory.GetFiles(
                    AppDomain.CurrentDomain.BaseDirectory + @"Meta\DACF_FrTemp\" + @"Spa\", "*.csv",
                    SearchOption.AllDirectories);
            foreach (string InputFilePath in files)
            {
                //bool res = true;
                int LineIndex = 0;
                string Line = "";
                char[] Delimiters = new char[] { '\t' };

                try
                {
                    using (FileStream fs = File.Open(InputFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (BufferedStream bs = new BufferedStream(fs))
                    using (StreamReader sr = new StreamReader(bs))
                    {
                        //if (Cfs.FooterLinesCount > 0)
                        //    FileTotalLinesCount = File.ReadLines(FilePath).Count();
                        while ((Line = sr.ReadLine()) != null)
                        {
                            // Skip First Line
                            LineIndex++;
                            if (LineIndex == 1)
                                continue;

                            string[] Splits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                            // -9999 的 Spike 拿掉 (by Q_TG)
                            if (Splits[2] != "0")
                                continue;
                            // Spike 整合指標: by 月份統計 (時間模式快混池)
                            else
                            {
                                // DistributeToPst();
                                // According to Splits[1],
                                // TODO: need to do format checking
                                string year = Splits[1].Substring(0, 4);
                                string month = Splits[1].Substring(4, 2);
                                string key = year + month;

                                //Dic_SpikeCounts.AddOrUpdate(key, 0, (k, oldValue) => oldValue + 1);
                                //Dic_SpikeCounts.
                                if (Dic_SpikeCounts.TryGetValue(key, out double Cnt))
                                {
                                    Dic_SpikeCounts[key] = ++Cnt;
                                }
                                else
                                {
                                    // Exception (It's impossible not found)
                                    throw new Exception();
                                }

                                // TODO: check performance
                                //if (Dic_Pst.TryGetValue(key, out PrimarySedimentationTank Pst))
                                //{
                                //    // Add
                                //    Pst.Aggregate(1, 0);
                                //}
                                //else
                                //{
                                //    // Exception (It's impossible not found)
                                //    throw new Exception();
                                //}
                            }

                            // Suspended: 連續或接近的 Spike 整合(True Spikes)
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("InputFilePath: " + InputFilePath +
                        "\tLineIndex: " + LineIndex.ToString() + "\tLine: " + Line.ToString());
                    res = false;
                }
                finally
                {
                    // Do nothing
                }
            }

            // Write Integrated Analysis Result to one File Format
            // Efa Input Format: List<string>, List<double>
            List<string> List_Date = new List<string>();
            List<double> List_SpikeCount = new List<double>();

            // Sort Dic_SpikeCounts by Keys

            foreach (KeyValuePair<string, double> entry in Dic_SpikeCounts)
            {
                List_Date.Add(DateTime.ParseExact(entry.Key, "yyyyMM", null).ToString("yyyy-MM"));
                List_SpikeCount.Add(entry.Value);
            }

            Efa_StringList_DoubleList Efa = new Efa_StringList_DoubleList();
            Efa.FilePath = AppDomain.CurrentDomain.BaseDirectory
                        + "Result\\Result_Summary\\" + "Result_Auto_Data_Spa" + ".xlsx";
            Efa.SheetName = "Spa";
            Efa.List_X = List_Date;
            Efa.List_Y = List_SpikeCount;
            Efa.CreateExcel();

            // Save as CSV also (for Integrated Analysis metadata)
            string FilePath = AppDomain.CurrentDomain.BaseDirectory
                + "Result\\Result_Summary\\" + "Result_Auto_Data_Spa" + ".csv";
            StoreArrayAsResultCsv(List_Date.ToArray(), List_SpikeCount.ToArray(), FilePath);


            return res;
        }


        private void StoreArrayAsResultCsv(double Index1, double Index2, string FilePath)
        {
            //string FilePath = AppDomain.CurrentDomain.BaseDirectory
            //    + @"Result\" + DateTime.Now.ToString("yyyyMMdd-HHmm") + "\\" + FileName + ".csv";

            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                file.WriteLine(string.Format("{0},{1}", Index1, Index2));
            }
        }

        private void StoreArrayAsResultCsv(string[] DateArr, double[] IndexArr, string FilePath)
        {
            //string FilePath = AppDomain.CurrentDomain.BaseDirectory
            //    + @"Result\" + DateTime.Now.ToString("yyyyMMdd-HHmm") + "\\" + FileName + ".csv";

            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                int s_cnt = 1;
                foreach (string s in DateArr)
                {
                    if (s_cnt < DateArr.Length)
                        file.Write(string.Format("{0},", s));
                    else
                        file.WriteLine(string.Format("{0}", s));
                    s_cnt++;
                }
                int d_cnt = 1;
                foreach (double d in IndexArr)
                {
                    if (d_cnt < IndexArr.Length)
                        file.Write(string.Format("{0},", d));
                    else
                        file.WriteLine(string.Format("{0}", d));
                    d_cnt++;
                }
            }
        }

        private void StoreArrayAsMetaCsv(string[] STAIDArr, string[] DATEArr, double[] TGArr, string[] Q_TGArr, string FileName)
        {
            string FilePath = AppDomain.CurrentDomain.BaseDirectory
                + @"Meta\DACF_FrTemp\" + @"Cfa\" + FileName + ".txt";
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