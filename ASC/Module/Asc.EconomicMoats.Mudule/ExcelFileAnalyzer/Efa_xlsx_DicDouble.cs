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

namespace Asc
{
    public class Efa_xlsx_DicDouble
    {
        public string SheetName;
        public string FilePath;

        public ConcurrentDictionary<string, double> Dic_FpiLower = new ConcurrentDictionary<string, double>();
        public ConcurrentDictionary<string, double> Dic_FpiUpper = new ConcurrentDictionary<string, double>();

        public void CreateExcel()
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                ISheet sheet = workbook.CreateSheet(SheetName);

                var RowIter = 0;
                var ColIter = 0;

                // Header Line - Food
                var row = sheet.CreateRow(RowIter);
                ColIter = 0;
                row.CreateCell(ColIter).SetCellValue("Food");
                RowIter += 2;

                // Data Lines - Food
                row = sheet.CreateRow(RowIter);
                ColIter = 0;

                Dic_FpiLower.TryGetValue("Food", out double FpiLower);
                row.CreateCell(ColIter).SetCellValue(FpiLower);
                ColIter++;
                row.CreateCell(ColIter).SetCellValue(FpiLower);
                ColIter++;

                Dic_FpiUpper.TryGetValue("Food", out double FpiUpper);
                row.CreateCell(ColIter).SetCellValue(FpiUpper);
                ColIter++;
                row.CreateCell(ColIter).SetCellValue(FpiUpper);
                ColIter++;

                RowIter++;


                foreach (KeyValuePair<string, double> entry
                    in Dic_FpiLower.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value))
                {
                    if (entry.Key == "Food")
                        continue;

                    // Header Line - Except Food
                    row = sheet.CreateRow(RowIter);
                    ColIter = 0;
                    row.CreateCell(ColIter).SetCellValue(entry.Key);
                    RowIter += 2;

                    // Data Lines - Except Food
                    row = sheet.CreateRow(RowIter);
                    ColIter = 0;

                    Dic_FpiLower.TryGetValue(entry.Key, out double _FpiLower);
                    row.CreateCell(ColIter).SetCellValue(_FpiLower);
                    ColIter++;
                    row.CreateCell(ColIter).SetCellValue(_FpiLower);
                    ColIter++;

                    Dic_FpiUpper.TryGetValue(entry.Key, out double _FpiUpper);
                    row.CreateCell(ColIter).SetCellValue(_FpiUpper);
                    ColIter++;
                    row.CreateCell(ColIter).SetCellValue(_FpiUpper);
                    ColIter++;

                    RowIter++;
                }

                // Save Workbook
                FileInfo FI = new FileInfo(FilePath);
                FI.Directory.Create();  // If the directory already exists, this method does nothing.
                FileStream file = new FileStream(FilePath, FileMode.Create);
                workbook.Write(file);
                file.Close();
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

        public void UpdateExcel()
        {
            IWorkbook workBook;
            using (var fs = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                if (Path.GetExtension(FilePath).ToLower() == ".xls")
                {
                    workBook = new HSSFWorkbook(fs);
                }
                else
                {
                    workBook = new XSSFWorkbook(fs);
                }
            }
        }
    }
}
