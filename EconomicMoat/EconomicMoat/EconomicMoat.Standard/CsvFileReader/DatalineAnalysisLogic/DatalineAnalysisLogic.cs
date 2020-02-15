using System;
using System.Data;
using System.Text;

namespace EconomicMoat.Standard
{
    public class DatalineAnalysisLogic
    {
        public DatalineEntityFormat Def;
        public DataTable dtAnalysisResultFormat;
        internal DataRow drAnalysisResult;

        public DatalineAnalysisLogic()
        {
            dtAnalysisResultFormat = new DataTable();
            // 決定自身分析結果資料表之 Columns Here: 

            // --------------------------------------
            drAnalysisResult = dtAnalysisResultFormat.NewRow();

        }

        internal virtual void CustomizedAnalyze(string[] LineSplits)
        {
            DataRow drAnalysisResult = dtAnalysisResultFormat.NewRow();

            // Do Customized Analysis Here

            // Store drAnalysisResult Finally
            StoreAnalysisResult(drAnalysisResult);
        }

        internal void StoreAnalysisResult(DataRow drAnalysisResult)
        {
            dtAnalysisResultFormat.Rows.Add(drAnalysisResult);

            // Show Analysis Result
            StringBuilder sb = new StringBuilder();
            foreach (var item in drAnalysisResult.ItemArray)
            {
                sb.Append(item);
                sb.Append(',');
            }
            sb.AppendLine();
            Console.WriteLine(sb.ToString());
        }
    }
}
