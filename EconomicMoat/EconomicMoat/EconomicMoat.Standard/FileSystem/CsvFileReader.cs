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

        public CsvFileStructure Cfs;
        public DatalineAnalysisLogic Dal;

        #region File Properties
        public string FilePath;
        public Char[] Delimiters;
        #endregion

        #region Class CONSTANTS


        #endregion

        private string[] headers;

        public CsvFileReader()
        {
            // Default Values
            FilePath = "./";
            Delimiters = new Char[] { ',', '\\', '\n', ' ', '\t' };
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

        private void ProcessForEachLine(string line, int line_index)
        {
            CsvFileStructure.Locations loc = DetermineLocationFor(line_index);
            switch (loc)
            {
                case CsvFileStructure.Locations.HeadingLines:
                    {
                        break;
                    }
                case CsvFileStructure.Locations.HeaderLines:
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

                            //DataColumn col = new DataColumn(headers[i], Type.GetType(DataTypes[i]));
                            //dtAnalysisResult.Columns.Add(col);
                            //col.AllowDBNull = true;
                            //col.Unique = false;
                        }
                        break;
                    }
                case CsvFileStructure.Locations.DataLines:
                    {
                        string[] splits = line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                        // TODO: Check (splits.Length == headers.Length)
                        Dal.CustomizedAnalyze(splits);
                        //dtAnalysisResult.Rows.Add(drAnalysisResult);
                        break;
                    }
                case CsvFileStructure.Locations.FooterLines:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private CsvFileStructure.Locations DetermineLocationFor(int line_index)
        {
            // 針對所有分界點由低到高排序 (每個分界點皆須是 Unique，不適用的分界點為 -1)
            List<Tuple<int, CsvFileStructure.Locations>> list = new List<Tuple<int, CsvFileStructure.Locations>>();
            list.Add(Tuple.Create(Cfs.HeaderLineStartAt, CsvFileStructure.Locations.HeaderLines));
            list.Add(Tuple.Create(Cfs.DataLinesStartAt, CsvFileStructure.Locations.DataLines));
            list.Add(Tuple.Create(Cfs.FooterLinesStartAt, CsvFileStructure.Locations.FooterLines));
            list.Sort((x, y) => y.Item1.CompareTo(x.Item1));

            // 依序比較是否進入該點範圍內，直到全部比較完畢
            CsvFileStructure.Locations result = list[0].Item2;
            foreach (Tuple<int, CsvFileStructure.Locations> entry in list)
            {
                if (entry.Item1 <= line_index)
                {
                    result = entry.Item2;
                }
            }
            return result;
        }

        public bool ReadAllTgFiles()
        {
            throw new NotImplementedException();
        }

    }
}
