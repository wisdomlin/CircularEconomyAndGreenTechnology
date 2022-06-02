using System;
using System.Data;
using System.Text;

namespace Asc
{
    public class DatalineAnalysisLogic
    {
        internal DatalineEntityFormat Def;

        public DataTable dtAnalysisResultFormat;
        
        public DatalineAnalysisLogic(DatalineEntityFormat _Def)
        {
            Def = _Def;

            dtAnalysisResultFormat = new DataTable();
            // Define dtAnalysisResultFormat.Columns in Subclass.

            // Customize Analysis Setup in Subclass.

        }

        internal virtual void CustomizedAnalyze(string Dataline)
        {
            string[] LineSplits = Dataline.Split(Def.Delimiters, StringSplitOptions.RemoveEmptyEntries);

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

        internal string[] ProcessHeaderLine(string Line)
        {
            return Line.Split(Def.Delimiters, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
