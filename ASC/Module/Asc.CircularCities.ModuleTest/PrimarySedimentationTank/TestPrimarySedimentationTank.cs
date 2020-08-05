using NUnit.Framework;
using CircularCities.Standard;
using System;

namespace CircularCities.ModuleTest
{
    public class TestPrimarySedimentationTank
    {
        [Test]
        public void UC01_PrimaryInfluent()
        {
            Pst_C01 C01 = new Pst_C01();
            PrimaryInfluent Pi = new PrimaryInfluent();
            C01.SetPiQualityAndQuantity(Pi);

            Assert.AreEqual(1200000000f, Pi.Tss_MgPerDay);
            Assert.AreEqual(1200f, Pi.Tss_KgPerDay);
            Assert.AreEqual(1200000000f, Pi.Bod5_MgPerDay);
            Assert.AreEqual(1200f, Pi.Bod5_KgPerDay);
        }

        [Test]
        public void UC02_PstFlowsInFromPi()
        {
            Pst_C01 Pst_C01 = new Pst_C01();
            PrimaryInfluent Pi = new PrimaryInfluent();
            Pst_C01.SetPiQualityAndQuantity(Pi);

            PrimarySedimentationTank Pst = new PrimarySedimentationTank();
            Pst.FlowsInFrom(Pi);

            Assert.AreEqual(Pi.Q_DailyAveraged_CubicMeterPerDay, Pst.Pi_Q_DailyAveraged_CubicMeterPerDay);
            Assert.AreEqual(Pi.Tss_MgPerLiter, Pst.Pi_Tss_MgPerLiter);
            Assert.AreEqual(Pi.Tss_MgPerDay, Pst.Pi_Tss_MgPerDay);
            Assert.AreEqual(Pi.Tss_KgPerDay, Pst.Pi_Tss_KgPerDay);
            Assert.AreEqual(Pi.Bod5_MgPerLiter, Pst.Pi_Bod5_MgPerLiter);
            Assert.AreEqual(Pi.Bod5_MgPerDay, Pst.Pi_Bod5_MgPerDay);
            Assert.AreEqual(Pi.Bod5_KgPerDay, Pst.Pi_Bod5_KgPerDay);
        }

        [Test]
        public void UC03_PstRemovalCapability()
        {
            Pst_C01 C01 = new Pst_C01();
            PrimaryInfluent Pi = new PrimaryInfluent();
            C01.SetPiQualityAndQuantity(Pi);

            PrimarySedimentationTank Pst = new PrimarySedimentationTank();
            C01.SetPstCapability(Pst);

            Pst.FlowsInFrom(Pi);
            Pst.TreatmentProcess();

            Assert.AreEqual(720f, Pst.Ps_Tss_KgPerDay);
            Assert.AreEqual(360f, Pst.Ps_Bod5_KgPerDay);
            Assert.AreEqual(35.9f, Pst.Ps_Q_DailyAveraged_CubicMeterPerDay, 0.1);

            Assert.AreEqual(5964.1f, Pst.Pe_Q_DailyAveraged_CubicMeterPerDay, 0.1);
            Assert.AreEqual(5964100f, Pst.Pe_Q_DailyAveraged_LiterPerDay, 100);
            Assert.AreEqual(80.5f, Pst.Pe_Tss_MgPerLiter, 0.1);
            Assert.AreEqual(140.8f, Pst.Pe_Bod5_MgPerLiter, 0.1);
        }

        [Test]
        public void UC04_PstFlowsOutToPsAndPe()
        {
            Pst_C01 Pst_C01 = new Pst_C01();
            PrimaryInfluent Pi = new PrimaryInfluent();
            Pst_C01.SetPiQualityAndQuantity(Pi);
            //Pi.SetQualityAndQuantity(Pi_C01);

            PrimarySedimentationTank Pst = new PrimarySedimentationTank();
            Pst_C01.SetPstCapability(Pst);
            //Pst.SetCapability(C01);
            Pst.FlowsInFrom(Pi);
            Pst.TreatmentProcess();

            PrimarySludge Ps = new PrimarySludge();
            Pst.FlowsOutTo(Ps);

            PrimaryEffluent Pe = new PrimaryEffluent();
            Pst.FlowsOutTo(Pe);

            //TODO: Encapsulate C01 Class to Stable All UC Tests
            Assert.AreEqual(720f, Ps.Tss_KgPerDay);
            Assert.AreEqual(360f, Ps.Bod5_KgPerDay);
            Assert.AreEqual(35.9f, Ps.Q_DailyAveraged_CubicMeterPerDay, 0.1);

            Assert.AreEqual(5964.1f, Pe.Q_DailyAveraged_CubicMeterPerDay, 0.1);
            Assert.AreEqual(80.5f, Pe.Tss_MgPerLiter, 0.1);
            Assert.AreEqual(140.8f, Pe.Bod5_MgPerLiter, 0.1);


            Assert.AreEqual(720f, Ps.Tss_KgPerDay);
            Assert.AreEqual(360f, Ps.Bod5_KgPerDay);
            Assert.AreEqual(35.9f, Ps.Q_DailyAveraged_CubicMeterPerDay, 0.1);

            Assert.AreEqual(5964.1f, Pe.Q_DailyAveraged_CubicMeterPerDay, 0.1);
            Assert.AreEqual(80.5f, Pe.Tss_MgPerLiter, 0.1);
            Assert.AreEqual(140.8f, Pe.Bod5_MgPerLiter, 0.1);
        }
    }
}