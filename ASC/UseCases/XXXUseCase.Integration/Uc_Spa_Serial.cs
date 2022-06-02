using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Asc
{
    public class Uc_Spa_Serial
    {
        // Variables that need to be set from outside
        public string[] SID_Array;
        public string SID_Prefix;
        public string RawFolderPath;
        public string MetaFolderPath;
        public string ResultFolderPath;

        // Internal Objects
        public CsvFileAnalyzer Cfa;
        private Dal_EcadWeather Dal;


        /// <summary>
        /// Use Case' Standard Interface
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            Stopwatch sw = new Stopwatch();
            bool result = true;

            sw.Start();
            result &= this.UseCsvFileAnalyzer();
            sw.Stop();
            Console.WriteLine($"UseCsvFileAnalyzer {sw.ElapsedMilliseconds}ms");

            sw.Restart();
            result &= this.IntraRawSpikeAnalyzer();
            sw.Stop();
            Console.WriteLine($"IntraRawSpikeAnalyzer {sw.ElapsedMilliseconds}ms");

            sw.Restart();
            result &= this.InterIntegratedSpikeAnalyzer();
            sw.Stop();
            Console.WriteLine($"InterIntegratedSpikeAnalyzer {sw.ElapsedMilliseconds}ms");

            return result;
        }

        public Uc_Spa_Serial()
        {

        }

        // For Cfa Use  // TODO: Generalize for general Spa?
        private ConcurrentDictionary<string, List<string>> Dic_STAID_List = new ConcurrentDictionary<string, List<string>>();
        private ConcurrentDictionary<string, List<string>> Dic_SOUID_List = new ConcurrentDictionary<string, List<string>>();
        private ConcurrentDictionary<string, List<string>> Dic_DATE_List = new ConcurrentDictionary<string, List<string>>();
        private ConcurrentDictionary<string, List<double>> Dic_Val_List = new ConcurrentDictionary<string, List<double>>();
        private ConcurrentDictionary<string, List<string>> Dic_Q_Val_List = new ConcurrentDictionary<string, List<string>>();

        public bool UseCsvFileAnalyzer()
        {
            bool result = true;
            foreach (string id in SID_Array)
            {
                // 1. Creation Management
                int HeaderLineStartAt = 21;
                int DataLinesStartAt = 22;
                int FooterLinesCount = 0;
                CsvFileStructure Cfs = new CsvFileStructure(HeaderLineStartAt, DataLinesStartAt, FooterLinesCount);

                char[] Delimiters = new char[] { ',', ' ' };
                DatalineEntityFormat Def = new Def_TG(Delimiters);

                Dal = new Dal_EcadWeather(Def);

                string sID = SID_Prefix + "_STAID" + id.PadLeft(6, '0');
                string FilePath = RawFolderPath + sID + ".txt";
                Cfa = new CsvFileAnalyzer(Cfs, Dal, FilePath);

                // 3. Read Csv File
                result &= Cfa.ReadCsvFile();

                // TODO: Encapsulte by Class
                Dic_STAID_List.TryAdd(sID, Dal.STAID);
                Dic_SOUID_List.TryAdd(sID, Dal.SOUID);
                Dic_DATE_List.TryAdd(sID, Dal.DATE);
                Dic_Val_List.TryAdd(sID, Dal.Val);
                Dic_Q_Val_List.TryAdd(sID, Dal.Q_Val);

                // Store as Meta
                StoreArrayAsMetaCsv(
                    Dal.STAID.ToArray(), Dal.DATE.ToArray(), Dal.Val.ToArray(), Dal.Q_Val.ToArray(),
                    "Raw_" + sID);        // "RawPrecip_" + sID);   // sID already contains prefix
                // TODO: fix the Meta filename used by other functions
            }

            return result;
        }

        public bool IntraRawSpikeAnalyzer()
        {
            try
            {
                foreach (KeyValuePair<string, List<double>> entry in Dic_Val_List)
                {
                    SpikeAnalyzer Spa = new SpikeAnalyzer();
                    string sID = entry.Key;
                    Spa._InputDataPath = MetaFolderPath + @"Cfa\" + "Raw_" + sID + ".txt";
                    Spa._OutputDataPath = MetaFolderPath + @"Spa\" + "Spa_" + sID + ".csv";
                    // "SpaPrecip_" + sID + ".csv"; // sID already contains prefix
                    // TODO: fix the Meta filename used by other functions

                    Spa._hasHeader = false;
                    Spa._separatorChar = ',';
                    Spa.Confidence = 95;
                    Spa._docsize = entry.Value.Count;   // No Use for now. Total Days, roughly 5475 days for 2005-2019 (15 years*365=5475)
                    Spa.SlidingWindowDivided = 92;    // How many spikes you want to detect in whole period? (15y * 12 spikes per year)
                    // One Window per Season (31+30+31=92) or Half Year (30*6=180) or Year (30*12=360)
                    Spa.RunAnalysis();
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
            SortedDictionary<string, double> Dic_SpikeCounts = new SortedDictionary<string, double>();

            // Y2005M01 ~ Y2019M12
            for (int Y = 2005; Y <= 2019; Y++)
            {
                for (int M = 1; M <= 12; M++)
                {
                    string key = Y.ToString("0000") + M.ToString("00");
                    Dic_SpikeCounts.Add(key, new double());
                }
            }

            // Read Spa files for each weather station
            string[] files = Directory.GetFiles(
                MetaFolderPath + @"Spa\", "*.csv",
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
                            // -9999 的 Spike 拿掉 (by Q_XX)
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

                            // Suspended: 連續或接近的 Spike 整合 (True Spikes)
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
            Efa.FilePath = ResultFolderPath + "Spa\\" + "Result_Spa_" + SID_Prefix + ".xlsx";
            Efa.SheetName = "Spa";
            Efa.List_X = List_Date;
            Efa.List_Y = List_SpikeCount;
            Efa.CreateExcel();

            // Save as CSV also (for Integrated Analysis metadata)
            string FilePath = ResultFolderPath + "Spa\\" + "Result_Spa_" + SID_Prefix + ".csv";
            StoreArrayAsResultCsv(List_Date.ToArray(), List_SpikeCount.ToArray(), FilePath);

            return res;
        }

        private void StoreArrayAsResultCsv(string[] DateArr, double[] IndexArr, string FilePath)
        {
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

        private void StoreArrayAsMetaCsv(
            string[] STAIDArr, string[] DATEArr, double[] ValArr, string[] Q_Arr, string FileName)
        {
            string FilePath = MetaFolderPath + @"Cfa\" + FileName + ".txt";
            FileInfo FI = new FileInfo(FilePath);
            FI.Directory.Create();  // If the directory already exists, this method does nothing.
            using (var file = new StreamWriter(FilePath, false))
            {
                int len = ValArr.Length;
                for (int i = 0; i < len; i++)
                {
                    file.WriteLine(string.Format("{0},{1},{2},{3}", STAIDArr[i], DATEArr[i], ValArr[i], Q_Arr[i]));
                }
            }
        }
    }
}
