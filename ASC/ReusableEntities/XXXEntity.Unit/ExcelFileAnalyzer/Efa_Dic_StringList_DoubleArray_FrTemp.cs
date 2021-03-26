using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;
using NPOI.HSSF.Util;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Asc
{
    public class Efa_Dic_StringList_DoubleArray_FrTemp
    {
        public string SheetName;
        public string FilePath;

        public ConcurrentDictionary<string, List<string>> dicListDate = new ConcurrentDictionary<string, List<string>>();
        public ConcurrentDictionary<string, double[]> dicArrData = new ConcurrentDictionary<string, double[]>();

        public int RowIterStartToUse;

        public void CreateExcel()
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                ISheet sheet = workbook.CreateSheet(SheetName);
                ICellStyle cellStyle = workbook.CreateCellStyle();
                IDataFormat format = workbook.CreateDataFormat();
                cellStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

                var RowIter = 0;
                var ColIter = 0;

                foreach (KeyValuePair<string, double[]> entry
                    in dicArrData.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value))
                {
                    // Header Line
                    var row = sheet.CreateRow(RowIter);
                    ColIter = 0;
                    row.CreateCell(ColIter).SetCellValue(entry.Key);
                    RowIter++;

                    // Data Lines
                    row = sheet.CreateRow(RowIter);
                    ColIter = 0;
                    foreach (var val in entry.Value)
                    {
                        DateTime t = ParseStringToDateTime(dicListDate[entry.Key][ColIter]);
                        NPOI.SS.UserModel.ICell cell = row.CreateCell(ColIter);
                        cell.SetCellValue(t);
                        cell.CellStyle = cellStyle;
                        ColIter++;
                    }
                    RowIter++;

                    row = sheet.CreateRow(RowIter);
                    ColIter = 0;
                    foreach (var val in entry.Value)
                    {
                        row.CreateCell(ColIter).SetCellValue(val);
                        ColIter++;
                    }
                    RowIter++;
                }

                // Save Workbook
                FileInfo FI = new FileInfo(FilePath);
                FI.Directory.Create();  // If the directory already exists, this method does nothing.
                FileStream file = new FileStream(FilePath, FileMode.Create);
                workbook.Write(file);
                file.Close();
                RowIterStartToUse = RowIter;
            }
            catch
            {
                throw;
            }
            finally
            {
                workbook.Close();
            }
        }

        private DateTime ParseStringToDateTime(string s)
        {
            DateTime result = DateTime.ParseExact(s, "yyyyMMdd", CultureInfo.InvariantCulture);
            return result;
        }
    }
}
