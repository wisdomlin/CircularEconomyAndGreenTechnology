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
            // 廢棄污泥量
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

            // 摘要 1 
            // 因為 [廢棄污泥中 TSS 量] = [進流 TSS 量]  + [增加 TSS 量] - [出流 TSS 量]，
            // 雖然 [進流 TSS 量] 即 [Pe_Tss_MgPerDay]，[增加 TSS 量] 即 [IncTss_KgPerDay]，
            // 但是 [出流 TSS 量] 即 [Se_Tss_MgPerLiter] 需要使用 [Ss_Q_CubicMeterPerDay] (未知項) 做單位轉換
            // 所以 需要再一個 [Ss_Q_CubicMeterPerDay] 之關係式才能求解。
            // 摘要 2
            // 已知 [廢棄污泥濃度] = [廢棄污泥中 TSS 量] / [廢棄污泥之流量]
            // 其中 [廢棄污泥之流量] 即 [Ss_Q_CubicMeterPerDay]
            // 移項 [廢棄污泥中 TSS 量] = [廢棄污泥濃度] * [廢棄污泥之流量]
            // 摘要 3
            // 有了摘要 1 和摘要 2，就先解出 [廢棄污泥之流量]，再解出 [廢棄污泥中 TSS 量]

            //// 1 [廢棄污泥中 TSS 量] = [進流 TSS 量]  + [增加 TSS 量] - [出流 TSS 量]
            //// 1.1 [進流 TSS 量] 轉換為 KgPerDay
            //float Pe_Tss_KgPerDay = Pe_Tss_MgPerDay * UnitsConversion.Mg_To_Kg;
            //// 1.2 [增加 TSS 量] 已經是 KgPerDay
            //// 1.3 [出流 TSS 量] 轉換為 KgPerDay
            //float Ss_Q_CubicMeterPerDay;
            //float Se_Tss_KgPerDay 
            //    = Se_Tss_MgPerLiter * (Pe_Q_CubicMeterPerDay - Ss_Q_CubicMeterPerDay)
            //    * UnitsConversion.CubicMeter_To_L * UnitsConversion.Mg_To_Kg;
            //// 1.4 [廢棄污泥中 TSS 量] = [進流 TSS 量]  + [增加 TSS 量] - [出流 TSS 量]
            //float Ss_Tss_KgPerDay = Pe_Tss_KgPerDay + IncTss_KgPerDay - Se_Tss_KgPerDay;

            //// 2 [廢棄污泥濃度] = [廢棄污泥中 TSS 量] / [廢棄污泥之流量]
            //Ss_MgPerLiter 
            //    = Ss_Tss_KgPerDay / Ss_Q_CubicMeterPerDay
            //    * UnitsConversion.Kg_To_Mg / UnitsConversion.CubicMeter_To_L;
            //// 2.1 移項
            //Ss_Tss_KgPerDay
            //   = Ss_MgPerLiter * Ss_Q_CubicMeterPerDay
            //   * UnitsConversion.Mg_To_Kg / UnitsConversion.L_To_CubicMeter;

            //// 3 聯合 1 和 2
            //// 3.1 [式 1] = [式 2]
            //Pe_Tss_KgPerDay + IncTss_KgPerDay - Se_Tss_KgPerDay
            //   = Ss_MgPerLiter * Ss_Q_CubicMeterPerDay
            //   * UnitsConversion.Mg_To_Kg / UnitsConversion.L_To_CubicMeter;
            //// 3.2 建代號
            //float a = Pe_Tss_MgPerDay;
            //float b = IncTss_KgPerDay;
            //float c = Se_Tss_MgPerLiter;
            //float d = Pe_Q_CubicMeterPerDay;
            //float e = Ss_MgPerLiter;
            //float Q = Ss_Q_CubicMeterPerDay;
            //float k1 = UnitsConversion.Mg_To_Kg;
            //float k2 = UnitsConversion.CubicMeter_To_L;
            //float k3 = UnitsConversion.L_To_CubicMeter;
            //// 3.3 化簡
            //[a * k1] + b - c * [(d - Q) * k2 * k1]
            //    = e * Q * k1 / k3;
            //// 3.4 乘開
            //a * k1 + b - c * d * k2 * k1 + c * Q * k2 * k1 = e * Q * k1 / k3
            //// 3.5 求 Q 
            //Q = (a * k1 + b - c * d * k2 * k1) / (e * k1 / k3 - c * k2 * k1)

            // 3.6 使用推導結果
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

            // 廢棄污泥中的 BOD 量 (固體部分)
            float BOD5WasteSludge_Solid
                = At.Tss_WasteSludge
                * BiodegradableRatioOfSludge
                * Biodegradable_To_BODu
                * BODu_To_BOD5; // Kg/d
            Assert.AreEqual(395.8, BOD5WasteSludge_Solid, 0.1);

            // 廢棄污泥中的 BOD 量 (液體部分)
            At.Q_WasteSludge = 78.8f;
            At.Se_Bod5Soluble_MgPerLiter = 7.45f;
            float BOD5WasteSludge_Liquid
                = At.Q_WasteSludge * At.Se_Bod5Soluble_MgPerLiter
                * UnitsConversion.Mg_To_Kg * UnitsConversion.CubicMeter_To_L;
            Assert.AreEqual(0.58f, BOD5WasteSludge_Liquid, 0.01);

            // 廢棄污泥中的總 BOD 量
            float BOD5WasteSludge = BOD5WasteSludge_Solid + BOD5WasteSludge_Liquid;
            Assert.AreEqual(396.44f, BOD5WasteSludge, 0.01);
        }

        [Test]
        public void UC05_SeEffQualityAndQuantity()
        {
            // 二沉池出流水量與水質
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
        //    Pst.Pst_Tss_RemovalRate = 0.60f;    // (即 60%)
        //    Pst.Pst_Bod5_RemovalRate = 0.30f;   // (即 30%)
        //    Pst.Ps_SpecificWeight_KgPerLiter = 1.002f;         // (kg / L)
        //    Pst.Ps_VolumeConcentration_LiterPerLiter = 0.02f;  // (即 2 %)(L / L)

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
        //    Pst.Pst_Tss_RemovalRate = 0.60f;    // (即 60%)
        //    Pst.Pst_Bod5_RemovalRate = 0.30f;   // (即 30%)
        //    Pst.Ps_SpecificWeight_KgPerLiter = 1.002f;         // (kg / L)
        //    Pst.Ps_VolumeConcentration_LiterPerLiter = 0.02f;  // (即 2 %)(L / L)

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