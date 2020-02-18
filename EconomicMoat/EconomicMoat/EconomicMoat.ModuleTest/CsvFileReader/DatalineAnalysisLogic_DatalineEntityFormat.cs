using EconomicMoat.Standard;
using NUnit.Framework;
using System;
using System.Data;

namespace EconomicMoat.ModuleTest
{
    class DatalineAnalysisLogic_DatalineEntityFormat
    {
        [Test]
        public void UC01_HowToUseDatalineEntityFormat()
        {
            string FieldName = "TG";
            DatalineEntityFormat Def = new Def_TG();

            Assert.AreEqual(3, Def.GetValueAddress(FieldName));
            Assert.AreEqual("Int16", Def.GetValueType(FieldName));
        }

        [Test]
        public void UC02_HowToUseDatalineAnalysisLogic()
        {
            // 1. Prepare DatalineEntityFormat
            string FieldName = "TG";
            DatalineEntityFormat Def = new Def_TG();

            // 2. Prepare DatalineAnalysisLogic
            //Def.LineSplits = new string[] { "1", "8906", "19180101", "-34", "0" };
            //string ValueType = Def.GetValueType("TG");
            //string ValueString = Def.GetValueString("TG");

            //PresentValue PV = new PresentValue(ValueType, ValueString);
            //SpecValue AlarmHigh = new SpecValue(ValueType, sAlarmHigh);

            //AbsMaxJudge absMaxJudge = new AbsMaxJudge(AlarmHigh);   // TODO: Create by Factory to decouple Modules
            //bool JudgeResult = absMaxJudge.Judge(PV);

            //if (JudgeResult)
            //{
            //    drAnalysisResult["DATE"] = Def.GetValueString("DATE");
            //    drAnalysisResult["TG"] = Def.GetValueString("TG");
            //}


            Assert.AreEqual(3, Def.GetValueAddress(FieldName));
            Assert.AreEqual("Int16", Def.GetValueType(FieldName));
        }
    }
}
