using NUnit.Framework;
using CircularCities.Standard;
using System;

namespace CircularCities.ModuleTest
{
    public class TestSludgeDewateringMachine
    {
        [Test]
        public void UC01_WaterContent85()
        {
            // ���]����
            float SludgeWaterContentRate = 0.85f;     // % [�êd�t���v]
            float SludgeCaptureRate = 0.95f;           // % [�êd�����v]
            float SludgeAfterDewatering_SpecificWeight_KgPerLiter = 1.060f;    // [�����êd��]

            float PolymerPercentage_in_SludgeBeforeDewatering = 0.003f;  // 0.3%

            // �ۥ[ = 100% (���t)
            float PolymerPercentage_for_SludgeAfterDewatering = 0.75f;          // 75%
            float PolymerPercentage_for_DewateringRecyclingWater = 0.25f;       // 25%

            float FlushTime_HourPerDay = 0.5f;
            float FlushWaterQuantity_CubicMeterPerHourPerBed = 12.0f;
            float DisinfectionTankWaterQuality = 0f;
            float FlushWaterQuality = DisinfectionTankWaterQuality;

            // SludgeBeforeDewatering (Quantity & Quality)
            float SludgeAfterDigestion_Q_CubicMeterPerDay = 14.0f;

            // [���ƫ�êd TSS] = [���Ʀ��� TSS] - [���Ʀ��W��G TSS]
            float DigestionTank_Tss_KgPerDay = 870f;
            float DigestionTankSupernatant_Tss_KgPerDay = 12.1f;
            float SludgeBeforeDewatering_Tss_KgPerDay
                = DigestionTank_Tss_KgPerDay - DigestionTankSupernatant_Tss_KgPerDay;
            Assert.AreEqual(858.0f, SludgeBeforeDewatering_Tss_KgPerDay, 0.1);

            // [���ƫ�êd BOD] = [���Ʀ��� BOD] - [���Ʀ��W��G BOD]
            float DigestionTank_Bod5_KgPerDay = 334f;
            float DigestionTankSupernatant_Bod5_KgPerDay = 1.8f;
            float SludgeBeforeDewatering_Bod5_KgPerDay
                = DigestionTank_Bod5_KgPerDay - DigestionTankSupernatant_Bod5_KgPerDay;
            Assert.AreEqual(332.3f, SludgeBeforeDewatering_Bod5_KgPerDay, 0.1);

            // SludgeAfterDewatering (Quantity & Quality)
            // [�����êd TSS] 
            //  = [���ƫ�êd TSS] 
            //     * ([�êd�����v] + [Polymer �� ���ƫ�êd TSS �����] * [Polymer ���t�b �����êd TSS �������])
            float SludgeAfterDewatering_Tss_KgPerDay
                = SludgeBeforeDewatering_Tss_KgPerDay
                * (SludgeCaptureRate + PolymerPercentage_in_SludgeBeforeDewatering * PolymerPercentage_for_SludgeAfterDewatering);
            Assert.AreEqual(817.0f, SludgeAfterDewatering_Tss_KgPerDay, 0.1);

            // [�����êd Bod5]
            //  = [���ƫ�êd Bod5] 
            //     * ([�êd�����v])
            float SludgeAfterDewatering_Bod5_KgPerDay
                = SludgeBeforeDewatering_Bod5_KgPerDay
                * SludgeCaptureRate;
            Assert.AreEqual(315.6f, SludgeAfterDewatering_Bod5_KgPerDay, 0.1);

            // [�����êd Q] (CMD)
            //  = [�����êd TSS] (kg / day)
            //  / [�����êd��] (kg / L) (�C�����n���ä����A�s�b�h�֭��q���êd)
            //  / [�����êd��n�@��] (L / L) (�C�����n���ä����A�s�b�h����n���êd)
            //  * [L_to_CubicMeter]
            float SludgeAfterDewatering_VolumeConcentration_LiterPerLiter
                = 1 - SludgeWaterContentRate;
            float SludgeAfterDewatering_Q_CubicMeterPerDay
                = SludgeAfterDewatering_Tss_KgPerDay
                / SludgeAfterDewatering_SpecificWeight_KgPerLiter
                / SludgeAfterDewatering_VolumeConcentration_LiterPerLiter
                * UnitsConversion.L_To_CubicMeter;
            Assert.AreEqual(5.1f, SludgeAfterDewatering_Q_CubicMeterPerDay, 0.1);




        }

        [Test]
        public void UC01_WaterContent75()
        {
            // ���]����
            float SludgeWaterContentRate = 0.75f;     // % [�êd�t���v]
            float SludgeCaptureRate = 0.95f;           // % [�êd�����v]
            float SludgeAfterDewatering_SpecificWeight_KgPerLiter = 1.060f;    // [�����êd��]

            float PolymerPercentage_in_SludgeBeforeDewatering = 0.003f;  // 0.3%

            // �ۥ[ = 100% (���t)
            float PolymerPercentage_for_SludgeAfterDewatering = 0.75f;          // 75%
            float PolymerPercentage_for_DewateringRecyclingWater = 0.25f;       // 25%

            float FlushTime_HourPerDay = 0.5f;
            float FlushWaterQuantity_CubicMeterPerHourPerBed = 12.0f;
            float DisinfectionTankWaterQuality = 0f;
            float FlushWaterQuality = DisinfectionTankWaterQuality;

            // SludgeBeforeDewatering (Quantity & Quality)
            float SludgeAfterDigestion_Q_CubicMeterPerDay = 14.0f;

            // [���ƫ�êd TSS] = [���Ʀ��� TSS] - [���Ʀ��W��G TSS]
            float DigestionTank_Tss_KgPerDay = 870f;
            float DigestionTankSupernatant_Tss_KgPerDay = 12.1f;
            float SludgeBeforeDewatering_Tss_KgPerDay
                = DigestionTank_Tss_KgPerDay - DigestionTankSupernatant_Tss_KgPerDay;
            Assert.AreEqual(858.0f, SludgeBeforeDewatering_Tss_KgPerDay, 0.1);

            // [���ƫ�êd BOD] = [���Ʀ��� BOD] - [���Ʀ��W��G BOD]
            float DigestionTank_Bod5_KgPerDay = 334f;
            float DigestionTankSupernatant_Bod5_KgPerDay = 1.8f;
            float SludgeBeforeDewatering_Bod5_KgPerDay
                = DigestionTank_Bod5_KgPerDay - DigestionTankSupernatant_Bod5_KgPerDay;
            Assert.AreEqual(332.3f, SludgeBeforeDewatering_Bod5_KgPerDay, 0.1);

            // SludgeAfterDewatering (Quantity & Quality)
            // [�����êd TSS] 
            //  = [���ƫ�êd TSS] 
            //     * ([�êd�����v] + [Polymer �� ���ƫ�êd TSS �����] * [Polymer ���t�b �����êd TSS �������])
            float SludgeAfterDewatering_Tss_KgPerDay
                = SludgeBeforeDewatering_Tss_KgPerDay
                * (SludgeCaptureRate + PolymerPercentage_in_SludgeBeforeDewatering * PolymerPercentage_for_SludgeAfterDewatering);
            Assert.AreEqual(817.0f, SludgeAfterDewatering_Tss_KgPerDay, 0.1);

            // [�����êd Bod5]
            //  = [���ƫ�êd Bod5] 
            //     * ([�êd�����v])
            float SludgeAfterDewatering_Bod5_KgPerDay
                = SludgeBeforeDewatering_Bod5_KgPerDay
                * SludgeCaptureRate;
            Assert.AreEqual(315.6f, SludgeAfterDewatering_Bod5_KgPerDay, 0.1);

            // [�����êd Q] (CMD)
            //  = [�����êd TSS] (kg / day)
            //  / [�����êd��] (kg / L) (�C�����n���ä����A�s�b�h�֭��q���êd)
            //  / [�����êd��n�@��] (L / L) (�C�����n���ä����A�s�b�h����n���êd)
            //  * [L_to_CubicMeter]
            float SludgeAfterDewatering_VolumeConcentration_LiterPerLiter
                = 1 - SludgeWaterContentRate;
            float SludgeAfterDewatering_Q_CubicMeterPerDay
                = SludgeAfterDewatering_Tss_KgPerDay
                / SludgeAfterDewatering_SpecificWeight_KgPerLiter
                / SludgeAfterDewatering_VolumeConcentration_LiterPerLiter
                * UnitsConversion.L_To_CubicMeter;
            Assert.AreEqual(3.08f, SludgeAfterDewatering_Q_CubicMeterPerDay, 0.01);
        }

        [Test]
        public void UC02_()
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
        public void UC03_()
        {

        }


        [Test]
        public void UC04_()
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
        public void UC05_()
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