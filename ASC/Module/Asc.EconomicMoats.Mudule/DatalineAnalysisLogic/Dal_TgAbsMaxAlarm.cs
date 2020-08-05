using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EconomicMoats.Standard
{
    /// <summary>
    /// Select Datalines whose TG > AbsMaxAlarm
    /// </summary>
    public class Dal_TgAbsMaxAlarm : DatalineAnalysisLogic
    {
        public Dal_TgAbsMaxAlarm()
        {
            // Define dtAnalysisResultFormat.Columns in Subclass.
            dtAnalysisResultFormat.Columns.Add("STAID");
            dtAnalysisResultFormat.Columns.Add("SOUID");
            dtAnalysisResultFormat.Columns.Add("DATE");
            dtAnalysisResultFormat.Columns.Add("TG");
            dtAnalysisResultFormat.Columns.Add("Q_TG");
            // Customize Analysis Setup in Subclass.

        }

        internal override void CustomizedAnalyze(string Line)
        {
            // Preparation Start
            //DataRow dicAnalysisResult = dtAnalysisResultFormat.NewRow();
            Dictionary<string, string> dicAnalysisResult = new Dictionary<string, string>();
            string[] LineSplits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
            Def.LineSplits = LineSplits;
            // Preparation End

            // Generate Dataline Analysis Result Start
            // 1. Prepare PresentValue
            string ValueType = Def.GetValueType("TG");
            string ValueString = Def.GetValueString("TG");
            GenericValue PV = new GenericValue(ValueType, ValueString);

            // 2. Prepare IJudge
            string sAlarmHigh = "230";  // TODO: Get SpecValue Dynamically
            GenericValue AlarmHigh = new GenericValue(ValueType, sAlarmHigh);
            AbsMaxAlarm absMaxAlarm = new AbsMaxAlarm(AlarmHigh);   // TODO: Create by Factory to decouple Modules

            // 3. Do IJudge.Judge(PresentValue)
            bool JudgeResult = absMaxAlarm.Judge(PV);
            if (JudgeResult)
            {
                //drAnalysisResult["STAID"] = Def.GetValueString("STAID");
                //drAnalysisResult["SOUID"] = Def.GetValueString("SOUID");
                //drAnalysisResult["DATE"] = Def.GetValueString("DATE");
                //drAnalysisResult["TG"] = Def.GetValueString("TG");
                //drAnalysisResult["Q_TG"] = Def.GetValueString("Q_TG");
                dicAnalysisResult.Add("STAID", Def.GetValueString("STAID"));
                dicAnalysisResult.Add("SOUID", Def.GetValueString("SOUID"));
                dicAnalysisResult.Add("DATE", Def.GetValueString("DATE"));
                dicAnalysisResult.Add("TG", Def.GetValueString("TG"));
                dicAnalysisResult.Add("Q_TG", Def.GetValueString("Q_TG"));
            }
            // Generate Dataline Analysis Result End

            // Store Dataline Analysis Result Start
            if (JudgeResult)
            {
                StoreAnalysisResult(dicAnalysisResult);
            }
            // Store Dataline Analysis Result End
        }

        internal void StoreAnalysisResult(Dictionary<string, string> dicAnalysisResult)
        {
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("{@value1}", dicAnalysisResult);
        }
    }
}
