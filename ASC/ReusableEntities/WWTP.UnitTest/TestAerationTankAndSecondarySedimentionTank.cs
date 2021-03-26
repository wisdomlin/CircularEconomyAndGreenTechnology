using NUnit.Framework;

using System;

namespace Asc
{
    public class TestAerationTankAndSecondarySedimentionTank
    {
        [Test]
        public void UC01_Yobs()
        {
            AerationTank At = new AerationTank();
            At.Y = 0.6f;
            At.Kd = 0.06f;
            At.ThetaC = 20.8f;
            At.Yobs = At.YobsStep();
            Assert.AreEqual(0.27f, At.Yobs, 0.01);
        }

        [Test]
        public void UC02_TssIncrease()
        {
            AerationTank At = new AerationTank();
            At.Si_Bod5Soluble_MgPerLiter = 140.8f;
            At.Se_Bod5Soluble_MgPerLiter = 7.45f;
            At.Yobs = 0.27f;
            At.MLVSS_to_MLSS_Ratio = 0.8f;
            At.Pe_Q_DailyAveraged_CubicMeterPerDay = 5964.1f;
            At.TssIncreaseStep();
            Assert.AreEqual(214.7f, At.TvsIncrease_KgPerDay, 0.1);
            Assert.AreEqual(268.4f, At.TssIncrease_KgPerDay, 0.1);
        }

        [Test]
        public void UC03_TssWasteSludge()
        {
            // �o��êd�q
            AerationTank At = new AerationTank();
            At.Pe_Tss_MgPerDay = 480 * UnitsConversion.Kg_To_Mg;
            At.TssIncrease_KgPerDay = 268.4f;
            At.Pe_Q_DailyAveraged_CubicMeterPerDay = 5964.1f;

            float Concentration_WasteSludge_MgPerLiter = 8000;    // mg/L (Goal)
            float Se_Tss_MgPerLiter = 20;  //  mg/L

            float Q_WasteSludge = LinearSystemSolver(
                At.Pe_Tss_MgPerDay,
                At.TssIncrease_KgPerDay,
                Se_Tss_MgPerLiter,
                At.Pe_Q_DailyAveraged_CubicMeterPerDay,
                Concentration_WasteSludge_MgPerLiter);

            float Tss_WasteSludge
                = Q_WasteSludge
                * Concentration_WasteSludge_MgPerLiter
                * UnitsConversion.Mg_To_Kg
                / UnitsConversion.L_To_CubicMeter;
            Assert.AreEqual(78.8f, Q_WasteSludge, 0.1);
            Assert.AreEqual(630.7f, Tss_WasteSludge, 0.1);
        }

        private float LinearSystemSolver(
            float Pe_Tss_MgPerDay,
            float IncTss_KgPerDay,
            float Se_Tss_MgPerLiter,
            float Pe_Q_CubicMeterPerDay,
            float Ss_MgPerLiter)
        {
            //float a = Pe_Tss_MgPerDay * UnitsConversion.Mg_To_Kg;
            //float b = IncTss_KgPerDay;
            //float c = Se_Tss_MgPerLiter / 1000;
            //float d = Pe_Q_CubicMeterPerDay;
            //float e = Ss_MgPerLiter * UnitsConversion.Mg_To_Kg / UnitsConversion.L_To_CubicMeter;            

            // �K�n 1 
            // �]�� [�o��êd�� TSS �q] = [�i�y TSS �q]  + [�W�[ TSS �q] - [�X�y TSS �q]�A
            // ���M [�i�y TSS �q] �Y [Pe_Tss_MgPerDay]�A[�W�[ TSS �q] �Y [IncTss_KgPerDay]�A
            // ���O [�X�y TSS �q] �Y [Se_Tss_MgPerLiter] �ݭn�ϥ� [Ss_Q_CubicMeterPerDay] (������) ������ഫ
            // �ҥH �ݭn�A�@�� [Ss_Q_CubicMeterPerDay] �����Y���~��D�ѡC
            // �K�n 2
            // �w�� [�o��êd�@��] = [�o��êd�� TSS �q] / [�o��êd���y�q]
            // �䤤 [�o��êd���y�q] �Y [Ss_Q_CubicMeterPerDay]
            // ���� [�o��êd�� TSS �q] = [�o��êd�@��] * [�o��êd���y�q]
            // �K�n 3
            // ���F�K�n 1 �M�K�n 2�A�N���ѥX [�o��êd���y�q]�A�A�ѥX [�o��êd�� TSS �q]

            //// 1 [�o��êd�� TSS �q] = [�i�y TSS �q]  + [�W�[ TSS �q] - [�X�y TSS �q]
            //// 1.1 [�i�y TSS �q] �ഫ�� KgPerDay
            //float Pe_Tss_KgPerDay = Pe_Tss_MgPerDay * UnitsConversion.Mg_To_Kg;
            //// 1.2 [�W�[ TSS �q] �w�g�O KgPerDay
            //// 1.3 [�X�y TSS �q] �ഫ�� KgPerDay
            //float Ss_Q_CubicMeterPerDay;
            //float Se_Tss_KgPerDay 
            //    = Se_Tss_MgPerLiter * (Pe_Q_CubicMeterPerDay - Ss_Q_CubicMeterPerDay)
            //    * UnitsConversion.CubicMeter_To_L * UnitsConversion.Mg_To_Kg;
            //// 1.4 [�o��êd�� TSS �q] = [�i�y TSS �q]  + [�W�[ TSS �q] - [�X�y TSS �q]
            //float Ss_Tss_KgPerDay = Pe_Tss_KgPerDay + IncTss_KgPerDay - Se_Tss_KgPerDay;

            //// 2 [�o��êd�@��] = [�o��êd�� TSS �q] / [�o��êd���y�q]
            //Ss_MgPerLiter 
            //    = Ss_Tss_KgPerDay / Ss_Q_CubicMeterPerDay
            //    * UnitsConversion.Kg_To_Mg / UnitsConversion.CubicMeter_To_L;
            //// 2.1 ����
            //Ss_Tss_KgPerDay
            //   = Ss_MgPerLiter * Ss_Q_CubicMeterPerDay
            //   * UnitsConversion.Mg_To_Kg / UnitsConversion.L_To_CubicMeter;

            //// 3 �p�X 1 �M 2
            //// 3.1 [�� 1] = [�� 2]
            //Pe_Tss_KgPerDay + IncTss_KgPerDay - Se_Tss_KgPerDay
            //   = Ss_MgPerLiter * Ss_Q_CubicMeterPerDay
            //   * UnitsConversion.Mg_To_Kg / UnitsConversion.L_To_CubicMeter;
            //// 3.2 �إN��
            //float a = Pe_Tss_MgPerDay;
            //float b = IncTss_KgPerDay;
            //float c = Se_Tss_MgPerLiter;
            //float d = Pe_Q_CubicMeterPerDay;
            //float e = Ss_MgPerLiter;
            //float Q = Ss_Q_CubicMeterPerDay;
            //float k1 = UnitsConversion.Mg_To_Kg;
            //float k2 = UnitsConversion.CubicMeter_To_L;
            //float k3 = UnitsConversion.L_To_CubicMeter;
            //// 3.3 ��²
            //[a * k1] + b - c * [(d - Q) * k2 * k1]
            //    = e * Q * k1 / k3;
            //// 3.4 ���}
            //a * k1 + b - c * d * k2 * k1 + c * Q * k2 * k1 = e * Q * k1 / k3
            //// 3.5 �D Q 
            //Q = (a * k1 + b - c * d * k2 * k1) / (e * k1 / k3 - c * k2 * k1)

            // 3.6 �ϥα��ɵ��G
            float a = Pe_Tss_MgPerDay;
            float b = IncTss_KgPerDay;
            float c = Se_Tss_MgPerLiter;
            float d = Pe_Q_CubicMeterPerDay;
            float e = Ss_MgPerLiter;
            float Q;    // Ss_Q_CubicMeterPerDay;
            float k1 = UnitsConversion.Mg_To_Kg;
            float k2 = UnitsConversion.CubicMeter_To_L;
            float k3 = UnitsConversion.L_To_CubicMeter;

            Q = (a * k1 + b - c * d * k2 * k1) / (e * k1 / k3 - c * k2 * k1);
            return Q;
        }

        [Test]
        public void UC04_BOD5WasteSludge()
        {
            AerationTank At = new AerationTank();
            At.Tss_WasteSludge = 630.7f;

            float BiodegradableRatioOfSludge = 0.65f;
            float Biodegradable_To_BODu = 1.42f;
            float BODu_To_BOD5 = 0.68f;

            // �o��êd���� BOD �q (�T�鳡��)
            float BOD5WasteSludge_Solid
                = At.Tss_WasteSludge
                * BiodegradableRatioOfSludge
                * Biodegradable_To_BODu
                * BODu_To_BOD5; // Kg/d
            Assert.AreEqual(395.8, BOD5WasteSludge_Solid, 0.1);

            // �o��êd���� BOD �q (�G�鳡��)
            At.Q_WasteSludge = 78.8f;
            At.Se_Bod5Soluble_MgPerLiter = 7.45f;
            float BOD5WasteSludge_Liquid
                = At.Q_WasteSludge * At.Se_Bod5Soluble_MgPerLiter
                * UnitsConversion.Mg_To_Kg * UnitsConversion.CubicMeter_To_L;
            Assert.AreEqual(0.58f, BOD5WasteSludge_Liquid, 0.01);

            // �o��êd�����` BOD �q
            float BOD5WasteSludge = BOD5WasteSludge_Solid + BOD5WasteSludge_Liquid;
            Assert.AreEqual(396.44f, BOD5WasteSludge, 0.01);
        }

        [Test]
        public void UC05_SeEffQualityAndQuantity()
        {
            // �G�I���X�y���q�P����
            float Se_Tss_MgPerLiter = 20;  //  mg/L
            float Se_Bod5_MgPerLiter = 20;  //  mg/L
            
            AerationTank At = new AerationTank();
            At.Q_WasteSludge = 78.8f;
            At.Pe_Q_DailyAveraged_CubicMeterPerDay = 5964.1f;

            float Q_Eff = At.Pe_Q_DailyAveraged_CubicMeterPerDay - At.Q_WasteSludge;
            Assert.AreEqual(5885.3f, Q_Eff, 0.1);
            float TSS_Eff_KgPerDay
                = Se_Tss_MgPerLiter * Q_Eff * UnitsConversion.Mg_To_Kg * UnitsConversion.CubicMeter_To_L;
            Assert.AreEqual(117.8f, TSS_Eff_KgPerDay, 0.1);
            float Bod_Eff_KgPerDay
                = Se_Bod5_MgPerLiter * Q_Eff * UnitsConversion.Mg_To_Kg * UnitsConversion.CubicMeter_To_L;
            Assert.AreEqual(117.8f, Bod_Eff_KgPerDay, 0.1);
        }

        //[Test]
        //public void UC02_PstFlowsInFromPi()
        //{
        //    PrimaryInfluent Pi = new PrimaryInfluent();
        //    Pi.C01_PiQualityAndQuantity();
        //    Pi.C01_PiFieldsUpdateProcess();

        //    PrimarySedimentationTank Pst = new PrimarySedimentationTank();
        //    Pst.FlowsInFrom(Pi);

        //    Assert.AreEqual(Pi.Q_DailyAveraged_CubicMeterPerDay, Pst.Pi_Q_DailyAveraged_CubicMeterPerDay);
        //    Assert.AreEqual(Pi.Tss_MgPerLiter, Pst.Pi_Tss_MgPerLiter);
        //    Assert.AreEqual(Pi.Tss_MgPerDay, Pst.Pi_Tss_MgPerDay);
        //    Assert.AreEqual(Pi.Tss_KgPerDay, Pst.Pi_Tss_KgPerDay);
        //    Assert.AreEqual(Pi.Bod5_MgPerLiter, Pst.Pi_Bod5_MgPerLiter);
        //    Assert.AreEqual(Pi.Bod5_MgPerDay, Pst.Pi_Bod5_MgPerDay);
        //    Assert.AreEqual(Pi.Bod5_KgPerDay, Pst.Pi_Bod5_KgPerDay);
        //}

        //[Test]
        //public void UC03_PstRemovalCapability()
        //{
        //    PrimaryInfluent Pi = new PrimaryInfluent();
        //    Pi.C01_PiQualityAndQuantity();
        //    Pi.C01_PiFieldsUpdateProcess();

        //    PrimarySedimentationTank Pst = new PrimarySedimentationTank();
        //    Pst.Pst_Tss_RemovalRate = 0.60f;    // (�Y 60%)
        //    Pst.Pst_Bod5_RemovalRate = 0.30f;   // (�Y 30%)
        //    Pst.Ps_SpecificWeight_KgPerLiter = 1.002f;         // (kg / L)
        //    Pst.Ps_VolumeConcentration_LiterPerLiter = 0.02f;  // (�Y 2 %)(L / L)

        //    Pst.FlowsInFrom(Pi);
        //    Pst.TreatmentProcess();

        //    Assert.AreEqual(720f, Pst.Ps_Tss_KgPerDay);
        //    Assert.AreEqual(360f, Pst.Ps_Bod5_KgPerDay);
        //    Assert.AreEqual(35.9f, Pst.Ps_Q_DailyAveraged_CubicMeterPerDay, 0.1);

        //    Assert.AreEqual(5964.1f, Pst.Pe_Q_DailyAveraged_CubicMeterPerDay, 0.1);
        //    Assert.AreEqual(5964100f, Pst.Pe_Q_DailyAveraged_LiterPerDay, 100);
        //    Assert.AreEqual(80.5f, Pst.Pe_Tss_MgPerLiter, 0.1);
        //    Assert.AreEqual(140.8f, Pst.Pe_Bod5_MgPerLiter, 0.1);
        //}

        //[Test]
        //public void UC04_PstFlowsOutToPsAndPe()
        //{
        //    PrimaryInfluent Pi = new PrimaryInfluent();
        //    Pi.C01_PiQualityAndQuantity();
        //    Pi.C01_PiFieldsUpdateProcess();

        //    PrimarySedimentationTank Pst = new PrimarySedimentationTank();
        //    Pst.Pst_Tss_RemovalRate = 0.60f;    // (�Y 60%)
        //    Pst.Pst_Bod5_RemovalRate = 0.30f;   // (�Y 30%)
        //    Pst.Ps_SpecificWeight_KgPerLiter = 1.002f;         // (kg / L)
        //    Pst.Ps_VolumeConcentration_LiterPerLiter = 0.02f;  // (�Y 2 %)(L / L)

        //    Pst.FlowsInFrom(Pi);
        //    Pst.TreatmentProcess();

        //    PrimarySludge Ps = new PrimarySludge();
        //    Pst.FlowsOutTo(Ps);

        //    PrimaryEffluent Pe = new PrimaryEffluent();
        //    Pst.FlowsOutTo(Pe);

        //    Assert.AreEqual(720f, Ps.Tss_KgPerDay);
        //    Assert.AreEqual(360f, Ps.Bod5_KgPerDay);
        //    Assert.AreEqual(35.9f, Ps.Q_DailyAveraged_CubicMeterPerDay, 0.1);

        //    Assert.AreEqual(5964.1f, Pe.Q_DailyAveraged_CubicMeterPerDay, 0.1);
        //    Assert.AreEqual(80.5f, Pe.Tss_MgPerLiter, 0.1);
        //    Assert.AreEqual(140.8f, Pe.Bod5_MgPerLiter, 0.1);
        //}
    }
}