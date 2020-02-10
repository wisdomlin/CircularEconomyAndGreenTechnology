using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace EconomicMoat.Standard
{
    /// <summary>
    /// -1 if no Header Line
    /// N if Header Line is at Nth line. (N starts from 1)
    /// </summary>
    public class CsvFileReader
    {
        #region File Properties
        public string FilePath;
        public Char[] Delimiters;

        public int HeadingLinesStartAt;
        public int HeaderLineStartAt;
        public int DataLinesStartAt;
        public int FooterLinesStartAt;
        #endregion

        #region Class CONSTANTS
        /// <summary>
        /// Constant Value = -1
        /// </summary>
        public int COUNT_UNKNOWN = -1;
        /// <summary>
        /// Constant Value = -1
        /// </summary>
        public int NOT_APPLIED = -1;
        #endregion

        public DataLinesFormat Dlf;


        private string[] headers;
        private DataTable table;

        public CsvFileReader()
        {
            table = new DataTable();

            // Default Values
            FilePath = "./";
            Delimiters = new Char[] { ',', '\\', '\n', ' ', '\t' };
            HeadingLinesStartAt = NOT_APPLIED;
            HeaderLineStartAt = 1;
            DataLinesStartAt = 2;
            FooterLinesStartAt = NOT_APPLIED;
        }

        public bool ReadAllTgFiles()
        {
            throw new NotImplementedException();
        }

        public bool FindDatesBeyondTgThreshold(int Threshold)
        {
            bool res = true;
            try
            {
                // TODO: Assign FindDatesBeyondThreshold() for ProcessDataLines()
                Dlf.AnalysisLogicAsThreshold = Threshold;
                ReadFullFile();
            }
            catch
            {
                res = false;
            }
            finally
            {

            }
            return res;
        }

        public bool ReadFullFile()
        {
            bool res = true;
            try
            {
                // [using statement] for better memory management
                using (FileStream fs = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    int line_index = 1;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        ProcessForEachLine(line, line_index);
                        line_index++;
                    }
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                // Do nothing
            }
            return res;
        }

        private enum Locations { HeadingLines, HeaderLines, DataLines, FooterLines }
        private void ProcessForEachLine(string line, int line_index)
        {
            Locations loc = DetermineLocationFor(line_index);
            switch (loc)
            {
                case Locations.HeadingLines:
                    {
                        break;
                    }
                case Locations.HeaderLines:
                    {
                        headers = line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                        string[] DataTypes = {
                            "System.UInt16",
                            "System.UInt16",
                            "System.DateTime",
                            "System.Int16",
                            "System.SByte" };

                        for (int i = 0; i < headers.Length; i++)
                        {

                            DataColumn col = new DataColumn(headers[i], Type.GetType(DataTypes[i]));
                            table.Columns.Add(col);
                            col.AllowDBNull = true;
                            col.Unique = false;
                        }
                        break;
                    }
                case Locations.DataLines:
                    {
                        string[] splits = line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                        // TODO: Check (splits.Length == headers.Length)
                        //DataLinesFormat.TryParseAndAnalysisDataLines(splits);
                        Dal.Analyze(int n, string splits[n], out DataRow drAnalysisResult);
                        Dt.add(drAnalysisResult);

                        break;
                    }
                case Locations.FooterLines:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private Locations DetermineLocationFor(int line_index)
        {
            // 針對所有分界點由低到高排序 (每個分界點皆須是 Unique，不適用的分界點為 -1)
            List<Tuple<int, Locations>> list = new List<Tuple<int, Locations>>();
            list.Add(Tuple.Create(HeadingLinesStartAt, Locations.HeadingLines));
            list.Add(Tuple.Create(HeaderLineStartAt, Locations.HeaderLines));
            list.Add(Tuple.Create(DataLinesStartAt, Locations.DataLines));
            list.Add(Tuple.Create(FooterLinesStartAt, Locations.FooterLines));
            list.Sort((x, y) => y.Item1.CompareTo(x.Item1));

            // 依序比較是否進入該點範圍內，直到全部比較完畢
            Locations result = list[0].Item2;
            foreach (Tuple<int, Locations> entry in list)
            {
                if (entry.Item1 <= line_index)
                {
                    result = entry.Item2;
                }
            }
            return result;
        }
    }
}
