using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Asc
{
    /// <summary>
    /// Select Datalines whose TG > AbsMaxAlarm
    /// </summary>
    public class Dal_TgAbsMaxAlarmConsumer : DatalineAnalysisLogic
    {
        public Dal_TgAbsMaxAlarmConsumer(DatalineEntityFormat _Def) : base(_Def)
        {
            // Define dtAnalysisResultFormat.Columns in Subclass.
            dtAnalysisResultFormat.Columns.Add("STAID");
            dtAnalysisResultFormat.Columns.Add("SOUID");
            dtAnalysisResultFormat.Columns.Add("DATE");
            dtAnalysisResultFormat.Columns.Add("TG");
            dtAnalysisResultFormat.Columns.Add("Q_TG");
            // Customize Analysis Setup in Subclass.

        }

        public async Task CustomizedAnalyze_AsyncSendReceive(BufferBlock<string> source)
        {
            // ---------------------------------------
            // Preparation
            // ---------------------------------------
            //Dictionary<string, string> dicAnalysisResult = new Dictionary<string, string>();

            // Read from the source buffer until the source buffer has no available output data.
            while (await source.OutputAvailableAsync())
            {
                string Line;
                while (source.TryReceive(out Line))
                {
                    #region Process Data Line
                    string[] LineSplits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                    Def.LineSplits = LineSplits;

                    // ---------------------------------------
                    // Generate Dataline Analysis Result
                    // ---------------------------------------
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
                    Dictionary<string, string> dicAnalysisResult = new Dictionary<string, string>();
                    if (JudgeResult)
                    {                        
                        dicAnalysisResult.Add("STAID", Def.GetValueString("STAID"));
                        dicAnalysisResult.Add("SOUID", Def.GetValueString("SOUID"));
                        dicAnalysisResult.Add("DATE", Def.GetValueString("DATE"));
                        dicAnalysisResult.Add("TG", Def.GetValueString("TG"));
                        dicAnalysisResult.Add("Q_TG", Def.GetValueString("Q_TG"));
                    }

                    // ---------------------------------------
                    // Store Dataline Analysis Result
                    // ---------------------------------------
                    if (JudgeResult)
                    {
                        StoreAnalysisResult(dicAnalysisResult);
                    }
                    #endregion
                }
            }
        }

        internal override void CustomizedAnalyze(string Line)
        {
            // ---------------------------------------
            // Preparation
            // ---------------------------------------
            Dictionary<string, string> dicAnalysisResult = new Dictionary<string, string>();

            string[] LineSplits = Line.Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
            Def.LineSplits = LineSplits;

            // ---------------------------------------
            // Generate Dataline Analysis Result
            // ---------------------------------------
            // 1. Prepare PresentValue
            string ValueType = Def.GetValueType("TG");
            // TODO: Should be replaced by CsvFileFormat.GetValueAddress()
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
                dicAnalysisResult.Add("STAID", Def.GetValueString("STAID"));
                dicAnalysisResult.Add("SOUID", Def.GetValueString("SOUID"));
                dicAnalysisResult.Add("DATE", Def.GetValueString("DATE"));
                dicAnalysisResult.Add("TG", Def.GetValueString("TG"));
                dicAnalysisResult.Add("Q_TG", Def.GetValueString("Q_TG"));
            }

            // ---------------------------------------
            // Store Dataline Analysis Result
            // ---------------------------------------
            if (JudgeResult)
            {
                StoreAnalysisResult(dicAnalysisResult);
            }
        }

        internal void StoreAnalysisResult(Dictionary<string, string> dicAnalysisResult)
        {
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            logger.Info("{@value1}", dicAnalysisResult);
        }
    }
}
