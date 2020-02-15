using System;
using System.Data;
using System.Text;

namespace EconomicMoat.Standard
{
    public class Dal_TgAbsMaxAlarm : DatalineAnalysisLogic
    {
        public string sAlarmHigh;

        public Dal_TgAbsMaxAlarm()
        {
            dtAnalysisResultFormat = new DataTable();
            // 決定自身分析結果資料表之 Columns Here: 
            dtAnalysisResultFormat.Columns.Add("DATE");
            dtAnalysisResultFormat.Columns.Add("TG");
            // --------------------------------------
            drAnalysisResult = dtAnalysisResultFormat.NewRow();

            // Customized Analysis Setup
            // TODO: SetThreshold()
            sAlarmHigh = "230";
        }

        internal override void CustomizedAnalyze(string[] LineSplits)
        {
            Def.LineSplits = LineSplits;
            string ValueType = Def.GetValueType("TG");
            string ValueString = Def.GetValueString("TG");

            PresentValue PV = new PresentValue(ValueType, ValueString);
            SpecValue AlarmHigh = new SpecValue(ValueType, sAlarmHigh);

            AbsMaxJudge absMaxJudge = new AbsMaxJudge(AlarmHigh);   // TODO: Create by Factory to decouple Modules
            bool JudgeResult = absMaxJudge.Judge(PV);

            if (JudgeResult)
            {
                drAnalysisResult["DATE"] = Def.GetValueString("DATE");
                drAnalysisResult["TG"] = Def.GetValueString("TG");
            }

            // Store drAnalysisResult Finally
            StoreAnalysisResult(drAnalysisResult);
        }
    }
}
