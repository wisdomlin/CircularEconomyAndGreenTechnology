using System;

namespace CircularCities.Standard
{
    public class PrimarySedimentationTank
    {
        #region Removal: PrimaryEffluent / PrimarySludge
        public float Pst_Tss_RemovalRate;
        public float Pst_Bod5_RemovalRate;
        #endregion

        #region Removal: PrimarySludge
        public float Ps_SpecificWeight_KgPerLiter;
        public float Ps_VolumeConcentration_LiterPerLiter;
        #endregion

        #region PrimaryInfluent
        public float Pi_Q_DailyAveraged_CubicMeterPerDay;
        public float Pi_Tss_MgPerLiter;
        public float Pi_Tss_MgPerDay;
        public float Pi_Tss_KgPerDay;
        public float Pi_Bod5_MgPerLiter;
        public float Pi_Bod5_MgPerDay;
        public float Pi_Bod5_KgPerDay;
        #endregion

        #region PrimarySludge
        public float Ps_Tss_KgPerDay;
        public float Ps_Bod5_KgPerDay;
        public float Ps_Q_DailyAveraged_CubicMeterPerDay;
        #endregion

        #region PrimaryEffluent
        public float Pe_Q_DailyAveraged_CubicMeterPerDay;
        public float Pe_Q_DailyAveraged_LiterPerDay;
        public float Pe_Tss_MgPerDay;
        public float Pe_Tss_MgPerLiter;
        public float Pe_Bod5_MgPerDay;
        public float Pe_Bod5_MgPerLiter;
        #endregion

        public void FlowsInFrom(PrimaryInfluent Pi)
        {
            Pi_Q_DailyAveraged_CubicMeterPerDay = Pi.Q_DailyAveraged_CubicMeterPerDay;
            Pi_Tss_MgPerLiter = Pi.Tss_MgPerLiter;
            Pi_Tss_MgPerDay = Pi.Tss_MgPerDay;
            Pi_Tss_KgPerDay = Pi.Tss_KgPerDay;
            Pi_Bod5_MgPerLiter = Pi.Bod5_MgPerLiter;
            Pi_Bod5_MgPerDay = Pi.Bod5_MgPerDay;
            Pi_Bod5_KgPerDay = Pi.Bod5_KgPerDay;
        }

        public void TreatmentProcess()
        {
            C01_PsStep();
            C01_PeStep();
        }
        
        public void FlowsOutTo(PrimarySludge Ps)
        {
            Ps.Tss_KgPerDay =
                Ps_Tss_KgPerDay;
            Ps.Bod5_KgPerDay =
                Ps_Bod5_KgPerDay;
            Ps.Q_DailyAveraged_CubicMeterPerDay =
                Ps_Q_DailyAveraged_CubicMeterPerDay;
        }
        
        private void C01_PsStep()
        {
            Ps_Tss_KgPerDay
                = Pi_Tss_KgPerDay
                * Pst_Tss_RemovalRate;

            Ps_Bod5_KgPerDay
                = Pi_Bod5_KgPerDay
                * Pst_Bod5_RemovalRate;

            Ps_Q_DailyAveraged_CubicMeterPerDay
                = Ps_Tss_KgPerDay
                * (1 / Ps_SpecificWeight_KgPerLiter)
                * (1 / Ps_VolumeConcentration_LiterPerLiter)
                * UnitsConversion.L_To_CubicMeter;
        }

        private void C01_PeStep()
        {
            Pe_Q_DailyAveraged_CubicMeterPerDay
                = Pi_Q_DailyAveraged_CubicMeterPerDay
                - Ps_Q_DailyAveraged_CubicMeterPerDay;

            Pe_Q_DailyAveraged_LiterPerDay
                = Pe_Q_DailyAveraged_CubicMeterPerDay
                * UnitsConversion.CubicMeter_To_L;

            Pe_Tss_MgPerDay
                = Pi_Tss_KgPerDay
                * (1 - Pst_Tss_RemovalRate)
                * UnitsConversion.Kg_To_Mg;

            Pe_Tss_MgPerLiter
                = Pe_Tss_MgPerDay
                / Pe_Q_DailyAveraged_LiterPerDay;

            Pe_Bod5_MgPerDay
                = Pi_Bod5_KgPerDay
                * (1 - Pst_Bod5_RemovalRate)
                * UnitsConversion.Kg_To_Mg;

            Pe_Bod5_MgPerLiter
                = Pe_Bod5_MgPerDay
                / Pe_Q_DailyAveraged_LiterPerDay;
        }

        public void FlowsOutTo(PrimaryEffluent Pe)
        {
            Pe.Q_DailyAveraged_CubicMeterPerDay = Pe_Q_DailyAveraged_CubicMeterPerDay;
            Pe.Q_DailyAveraged_LiterPerDay = Pe_Q_DailyAveraged_LiterPerDay;
            Pe.Tss_MgPerDay = Pe_Tss_MgPerDay;
            Pe.Tss_MgPerLiter = Pe_Tss_MgPerLiter;
            Pe.Bod5_MgPerDay = Pe_Bod5_MgPerDay;
            Pe.Bod5_MgPerLiter = Pe_Bod5_MgPerLiter;
        }

    }
}
