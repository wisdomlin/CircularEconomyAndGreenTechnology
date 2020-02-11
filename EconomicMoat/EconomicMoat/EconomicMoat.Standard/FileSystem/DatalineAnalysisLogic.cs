using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EconomicMoat.Standard
{
    public class DatalineAnalysisLogic
    {
        public DataLinesFormat dlf;
        public DataTable dtAnalysisResult;

        public DatalineAnalysisLogic()
        {
            dtAnalysisResult = new DataTable();
            dtAnalysisResult.Columns.Add("DATE");
            dtAnalysisResult.Columns.Add("TG");
        }

        internal void CustomizedAnalyze(string[] splits)
        {
            Int16 Tg = dlf.Parse(splits, "TG");
            Int16 Threshold = 230; // TODO: To be Spec
            bool Res = (Tg >= Threshold);   // TODO: To be Judge
            if (Res)
            {
                DataRow newRow = dtAnalysisResult.NewRow();
                newRow["DATE"] = splits[dlf.GetFieldStringIndex("DATE")];
                newRow["TG"] = splits[dlf.GetFieldStringIndex("TG")];
            }
        }
    }
}
