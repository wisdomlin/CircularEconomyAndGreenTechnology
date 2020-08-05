using NUnit.Framework;
using CircularCities.Standard;
using System;

namespace CircularCities.ModuleTest
{
    public class TestPumpingStation
    {
        [Test]
        public void UC01_FlowRateAndPower()
        {
            // 設計準則
            float PeakQ_CubicMeterPerDay = 20000f;

            // 計算程序
            // 1 選用 [抽水機] 之 [流量]、[功率]
            float PumpNumberBackUp = 1;     // [泵浦數] 備用
            float PumpNumberOnUse = 3;      // [泵浦數] 使用

            // [流量] 需求
            float Q_RequiredPerPump_CubicMeterPerDay = PeakQ_CubicMeterPerDay / PumpNumberOnUse;
            float Q_RequiredPerPump_CubicMeterPerHour = Q_RequiredPerPump_CubicMeterPerDay * UnitsConversion.CMD_to_CMH;
            float Q_RequiredPerPump_CubicMeterPerSecond = Q_RequiredPerPump_CubicMeterPerDay * UnitsConversion.CMD_to_CMS;
            Assert.AreEqual(6667f, Q_RequiredPerPump_CubicMeterPerDay, 1);
            Assert.AreEqual(277.77f, Q_RequiredPerPump_CubicMeterPerHour, 0.01);
            Assert.AreEqual(0.0772, Q_RequiredPerPump_CubicMeterPerSecond, 0.0001);

            // [流量] 選用
            float Q_ChosenPerPump_CubicMeterPerHour = 280;
            float Q_ChosenPerPump_CubicMeterPerSecond = Q_ChosenPerPump_CubicMeterPerHour * UnitsConversion.CMH_to_CMS;
            Assert.GreaterOrEqual(Q_ChosenPerPump_CubicMeterPerSecond, Q_RequiredPerPump_CubicMeterPerSecond);
            float Err_Q = Math.Abs(Q_ChosenPerPump_CubicMeterPerSecond - Q_RequiredPerPump_CubicMeterPerSecond) / Q_RequiredPerPump_CubicMeterPerSecond;
            Assert.LessOrEqual(Err_Q, 0.01);

            // [功率] 需求
            float TDH_Meter = 20;   // 總揚程 (Total Dynamic Head)
            float SpecificWeight_KgfPerCubicMeter = 1006;     // 被揚水液體之 [比重量]
            float PumpEfficient = 0.60f;    // 抽水機效率 %
            float P_RequiredPerPump_HorsePower
                = Q_ChosenPerPump_CubicMeterPerSecond
                * TDH_Meter
                * SpecificWeight_KgfPerCubicMeter
                / UnitsConversion.KgfMeterPerSecond_To_Hp
                / PumpEfficient;
            Assert.AreEqual(34.7753, P_RequiredPerPump_HorsePower, 0.0001);

            // [功率] 選用
            float P_ChosenPerPump_HorsePower = 35;
            Assert.GreaterOrEqual(P_ChosenPerPump_HorsePower, P_RequiredPerPump_HorsePower);
            float Err_P = Math.Abs(P_ChosenPerPump_HorsePower - P_RequiredPerPump_HorsePower) / P_RequiredPerPump_HorsePower;
            Assert.LessOrEqual(Err_P, 0.01);
        }

        [Test]
        public void UC02_TotalDynamicHead()
        {
            // 2. 抽水機總揚程
            // 吸水管管徑
            float V_MeterPerSecond = 2.0f;
            float Q_ChosenPerPump_CubicMeterPerSecond = 0.0778f;
            float Diameter_PipeInf = MathF.Pow(4 * Q_ChosenPerPump_CubicMeterPerSecond / (float) Math.PI / V_MeterPerSecond, 0.5f);
            Assert.AreEqual(0.22, Diameter_PipeInf, 0.01);

            // 



        }
    }

}