﻿using System;
using System.Data;
using System.Text;

namespace EconomicMoat.Standard
{
    /// <summary>
    /// Select Datalines whose TG > AbsMaxAlarm
    /// </summary>
    public class Dal_TgAbsMaxAlarm : DatalineAnalysisLogic
    {
        public Dal_TgAbsMaxAlarm()
        {
            // Define dtAnalysisResultFormat.Columns in Subclass.
            dtAnalysisResultFormat.Columns.Add("DATE");
            dtAnalysisResultFormat.Columns.Add("TG");

            // Customize Analysis Setup in Subclass.

        }

        internal override void CustomizedAnalyze(string[] LineSplits)
        {
            // Preparation Start
            DataRow drAnalysisResult = dtAnalysisResultFormat.NewRow();
            Def.LineSplits = LineSplits;
            // Preparation End

            // Generate Dataline Analysis Result Start
            // 1. Prepare PresentValue
            string ValueType = Def.GetValueType("TG");
            string ValueString = Def.GetValueString("TG");
            PresentValue PV = new PresentValue(ValueType, ValueString);

            // 2. Prepare IJudge
            string sAlarmHigh = "230";  // TODO: Get SpecValue Dynamically
            SpecValue AlarmHigh = new SpecValue(ValueType, sAlarmHigh);
            AbsMaxAlarm absMaxAlarm = new AbsMaxAlarm(AlarmHigh);   // TODO: Create by Factory to decouple Modules

            // 3. Do IJudge.Judge(PresentValue)
            bool JudgeResult = absMaxAlarm.Judge(PV);
            if (JudgeResult)
            {
                drAnalysisResult["DATE"] = Def.GetValueString("DATE");
                drAnalysisResult["TG"] = Def.GetValueString("TG");
            }
            // Generate Dataline Analysis Result End

            // Store Dataline Analysis Result Start
            if (JudgeResult)
            {
                StoreAnalysisResult(drAnalysisResult);
            }
            // Store Dataline Analysis Result End
        }
    }
}