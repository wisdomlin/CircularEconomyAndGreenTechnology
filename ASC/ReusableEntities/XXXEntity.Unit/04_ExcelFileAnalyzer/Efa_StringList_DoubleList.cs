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
    public class Efa_StringList_DoubleList
    {
        public string SheetName;
        public string FilePath;

        public List<string> List_X = new List<string>();
        public List<double> List_Y = new List<double>();

        public void CreateExcel()
        {
            IWorkbook workbook = new XSSFWorkbook();
            try
            {
                ISheet sheet = workbook.CreateSheet(SheetName);
                var RowIter = 0;
                var ColIter = 0;

                // X Row
                RowIter = 0;
                ColIter = 0;
                var row = sheet.CreateRow(RowIter);
                foreach (string X in List_X)
                {
                    row.CreateCell(ColIter).SetCellValue(X);
                    ColIter++;
                }

                // Y Row
                RowIter++;
                ColIter = 0;
                row = sheet.CreateRow(RowIter);
                foreach (double Y in List_Y)
                {
                    row.CreateCell(ColIter).SetCellValue(Y);
                    ColIter++;
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
