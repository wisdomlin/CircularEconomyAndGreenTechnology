using System;
using System.Data;
using System.Text;

namespace EconomicMoat.Standard
{
    public class DatalineAnalysisLogic
    {
        public DatalineEntityFormat Def;
        public DataTable dtAnalysisResultFormat;

        public DatalineAnalysisLogic()
        {
            // Define dtAnalysisResultFormat.Columns Start
            dtAnalysisResultFormat = new DataTable();

            // Define dtAnalysisResultFormat.Columns End

            // Customize Analysis Setup Start

            // Customize Analysis Setup End
        }

        internal virtual void CustomizedAnalyze(string[] LineSplits)
        {
            // Preparation Start
            DataRow drAnalysisResult = dtAnalysisResultFormat.NewRow();
            Def.LineSplits = LineSplits;
            // Preparation End

            // Generate Dataline Analysis Result Start


            // Generate Dataline Analysis Result End

            // Store Dataline Analysis Result Start
            StoreAnalysisResult(drAnalysisResult);
            // Store Dataline Analysis Result End
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
            //sb.AppendLine();
            Console.WriteLine(sb.ToString());
        }
    }
}
