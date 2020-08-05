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
            // �]�p�ǫh
            float PeakQ_CubicMeterPerDay = 20000f;

            // �p��{��
            // 1 ��� [�����] �� [�y�q]�B[�\�v]
            float PumpNumberBackUp = 1;     // [������] �ƥ�
            float PumpNumberOnUse = 3;      // [������] �ϥ�

            // [�y�q] �ݨD
            float Q_RequiredPerPump_CubicMeterPerDay = PeakQ_CubicMeterPerDay / PumpNumberOnUse;
            float Q_RequiredPerPump_CubicMeterPerHour = Q_RequiredPerPump_CubicMeterPerDay * UnitsConversion.CMD_to_CMH;
            float Q_RequiredPerPump_CubicMeterPerSecond = Q_RequiredPerPump_CubicMeterPerDay * UnitsConversion.CMD_to_CMS;
            Assert.AreEqual(6667f, Q_RequiredPerPump_CubicMeterPerDay, 1);
            Assert.AreEqual(277.77f, Q_RequiredPerPump_CubicMeterPerHour, 0.01);
            Assert.AreEqual(0.0772, Q_RequiredPerPump_CubicMeterPerSecond, 0.0001);

            // [�y�q] ���
            float Q_ChosenPerPump_CubicMeterPerHour = 280;
            float Q_ChosenPerPump_CubicMeterPerSecond = Q_ChosenPerPump_CubicMeterPerHour * UnitsConversion.CMH_to_CMS;
            Assert.GreaterOrEqual(Q_ChosenPerPump_CubicMeterPerSecond, Q_RequiredPerPump_CubicMeterPerSecond);
            float Err_Q = Math.Abs(Q_ChosenPerPump_CubicMeterPerSecond - Q_RequiredPerPump_CubicMeterPerSecond) / Q_RequiredPerPump_CubicMeterPerSecond;
            Assert.LessOrEqual(Err_Q, 0.01);

            // [�\�v] �ݨD
            float TDH_Meter = 20;   // �`���{ (Total Dynamic Head)
            float SpecificWeight_KgfPerCubicMeter = 1006;     // �Q�����G�餧 [�񭫶q]
            float PumpEfficient = 0.60f;    // ������Ĳv %
            float P_RequiredPerPump_HorsePower
                = Q_ChosenPerPump_CubicMeterPerSecond
                * TDH_Meter
                * SpecificWeight_KgfPerCubicMeter
                / UnitsConversion.KgfMeterPerSecond_To_Hp
                / PumpEfficient;
            Assert.AreEqual(34.7753, P_RequiredPerPump_HorsePower, 0.0001);

            // [�\�v] ���
            float P_ChosenPerPump_HorsePower = 35;
            Assert.GreaterOrEqual(P_ChosenPerPump_HorsePower, P_RequiredPerPump_HorsePower);
            float Err_P = Math.Abs(P_ChosenPerPump_HorsePower - P_RequiredPerPump_HorsePower) / P_RequiredPerPump_HorsePower;
            Assert.LessOrEqual(Err_P, 0.01);
        }

        [Test]
        public void UC02_TotalDynamicHead()
        {
            // 2. ������`���{
            // �l���޺ޮ|
            float V_MeterPerSecond = 2.0f;
            float Q_ChosenPerPump_CubicMeterPerSecond = 0.0778f;
            float Diameter_PipeInf = MathF.Pow(4 * Q_ChosenPerPump_CubicMeterPerSecond / (float) Math.PI / V_MeterPerSecond, 0.5f);
            Assert.AreEqual(0.22, Diameter_PipeInf, 0.01);

            // 



        }
    }

}