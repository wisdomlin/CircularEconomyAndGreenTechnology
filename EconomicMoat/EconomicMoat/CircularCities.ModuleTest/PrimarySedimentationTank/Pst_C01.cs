using System;
using System.Collections.Generic;
using System.Text;

namespace CircularCities.Standard
{
    public class Pst_C01
    {
        float Ps_Tss_KgPerDay = 720f;
        float Ps_Bod5_KgPerDay = 360f;
        float Ps_Q_DailyAveraged_CubicMeterPerDay = 35.9f;
        double delta_Ps_Q_DailyAveraged_CubicMeterPerDay = 0.1;


        float Pe_Q_DailyAveraged_CubicMeterPerDay = 5964.1f;
        double delta_Q_DailyAveraged_CubicMeterPerDay = 0.1;
        float Pe_Tss_MgPerLiter = 80.5f;
        double delta_Tss_MgPerLiter = 0.1;
        float Pe_Bod5_MgPerLiter = 140.8f;
        double delta_Bod5_MgPerLiter = 0.1;

        /// <summary>
        /// Customer 01
        /// </summary>
        public void SetPiQualityAndQuantity(PrimaryInfluent Pi)
        {
            Pi.Tss_MgPerLiter = 200;
            Pi.Bod5_MgPerLiter = 200;
            Pi.Q_DailyAveraged_CubicMeterPerDay = 6000;
            PiFieldsUpdateProcess(Pi);
        }

        /// <summary>
        /// Customer 01
        /// </summary>
        public void PiFieldsUpdateProcess(PrimaryInfluent Pi)
        {
            Pi.Tss_MgPerDay
                = Pi.Tss_MgPerLiter
                * Pi.Q_DailyAveraged_CubicMeterPerDay
                * UnitsConversion.CubicMeter_To_L;
            Pi.Tss_KgPerDay
                = Pi.Tss_MgPerDay
                * UnitsConversion.Mg_To_Kg;
            Pi.Bod5_MgPerDay
                = Pi.Bod5_MgPerLiter
                * Pi.Q_DailyAveraged_CubicMeterPerDay
                * UnitsConversion.CubicMeter_To_L;
            Pi.Bod5_KgPerDay
                = Pi.Bod5_MgPerDay
                * UnitsConversion.Mg_To_Kg;
        }

        public void SetPstCapability(PrimarySedimentationTank Pst)
        {
            Pst.Pst_Tss_RemovalRate = 0.60f;    // (即 60%)
            Pst.Pst_Bod5_RemovalRate = 0.30f;   // (即 30%)
            Pst.Ps_SpecificWeight_KgPerLiter = 1.002f;         // (kg / L)
            Pst.Ps_VolumeConcentration_LiterPerLiter = 0.02f;  // (即 2 %)(L / L)
        }
    }
}
