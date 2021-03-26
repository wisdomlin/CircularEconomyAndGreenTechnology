using NUnit.Framework;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System;

using Asc;

namespace Asc
{
    class TestExcelAnalyzer
    {
        //[Test]
        //public void UC01_HSSF_xls()
        //{

        //    //CreateExcel Ea = new CreateExcel();
        //    //Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
        //    //            + "Result\\Result_Summary\\" + "Result_Auto_Data" + ".xls";
        //    //Ea.SheetName = "Product";
        //    //Ea.DataToWrite = new AdventureWorksEntities().dicListFpi;


        //    IWorkbook workbook = new HSSFWorkbook();
        //    try
        //    {
        //        using (var adventureWorks = new AdventureWorksEntities())
        //        {
        //            ISheet sheet = workbook.CreateSheet("Product");

        //            var RowIter = 0;
        //            var ColIter = 0;
        //            foreach (KeyValuePair<string, List<double>> entry in adventureWorks.dicListFpi)
        //            {
        //                // Data Lines
        //                var row = sheet.CreateRow(RowIter);
        //                ColIter = 0;
        //                foreach (var val in entry.Value)
        //                {
        //                    row.CreateCell(ColIter).SetCellValue(adventureWorks.dicListDate[entry.Key][ColIter]);
        //                    ColIter++;
        //                }
        //                RowIter++;

        //                row = sheet.CreateRow(RowIter);
        //                ColIter = 0;
        //                foreach (var val in entry.Value)
        //                {
        //                    row.CreateCell(ColIter).SetCellValue(val);
        //                    ColIter++;
        //                }
        //                RowIter++;
        //            }

        //            // Save Workbook
        //            string FilePath = AppDomain.CurrentDomain.BaseDirectory
        //                + "Result\\Result_Summary\\" + "Result_Auto_Data" + ".xls";
        //            FileInfo FI = new FileInfo(FilePath);
        //            FI.Directory.Create();  // If the directory already exists, this method does nothing.
        //            FileStream file = new FileStream(FilePath, FileMode.Create);
        //            workbook.Write(file);
        //            file.Close();
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        workbook.Close();
        //    }
        //}

        [Test]
        public void UC02_XSSF_xlsx()
        {
            Efa_Dic_StringList_DoubleList_Fpi Ea = new Efa_Dic_StringList_DoubleList_Fpi();
            Ea.FilePath = AppDomain.CurrentDomain.BaseDirectory
                        + "Result\\Result_Summary\\" + "Result_Auto_Data" + ".xlsx";
            Ea.SheetName = "Product";

            Ea.dicListFpi.TryAdd("Food", new List<double>() { 1.1, 2.2, 3.3 });
            Ea.dicListFpi.TryAdd("Bread and cereals", new List<double>() { 4.4, 5.5, 6.6 });
            Ea.dicListFpi.TryAdd("Bread", new List<double>() { 7.7, 8.8, 9.9 });

            Ea.dicListDate.TryAdd("Food", new List<string>() { "2005M01", "2005M02", "2005M03" });
            Ea.dicListDate.TryAdd("Bread and cereals", new List<string>() { "2005M01", "2005M02", "2005M03" });
            Ea.dicListDate.TryAdd("Bread", new List<string>() { "2005M01", "2005M02", "2005M03" });

            Ea.CreateExcel();
        }
    }
}
