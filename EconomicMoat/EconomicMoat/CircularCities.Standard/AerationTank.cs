using System;
using System.Collections.Generic;
using System.Text;

namespace CircularCities.Standard
{
    public class AerationTank
    {
        #region PrimaryEffluent
        public float Pe_Q_DailyAveraged_CubicMeterPerDay;
        public float Pe_Q_DailyAveraged_LiterPerDay;
        public float Pe_Tss_MgPerDay;
        public float Pe_Tss_MgPerLiter;
        public float Pe_Bod5_MgPerDay;
        public float Pe_Bod5_MgPerLiter;
        #endregion

        #region Yobs (BOD5 to Tvs)
        /// <summary>
        /// 生長係數 Y (kgVss/kgBod5)
        /// </summary>
        public float Y;
        /// <summary>
        /// 內生期衰退係數 Kd (Day^-1)
        /// </summary>
        public float Kd;
        /// <summary>
        /// 平均細胞停留時間 ThetaC (Day)
        /// </summary>
        public float ThetaC;

        /// <summary>
        /// 污泥表象生物轉化率 Yobs (修正 Y) (kgVss/kgBod5)
        /// </summary>
        public float Yobs;
        public float YobsStep()
        {
            Yobs = Y / (1 + Kd * ThetaC);
            return Yobs;
        }
        #endregion

        #region TssIncrease (Tvs to Tss)
        /// <summary>
        /// 進流可溶解 BOD5 (mg/L)
        /// </summary>
        public float Si_Bod5Soluble_MgPerLiter;
        /// <summary>
        /// 出流可溶解 BOD5 (mg/L)
        /// </summary>
        public float Se_Bod5Soluble_MgPerLiter;
        public float TvsIncrease_KgPerDay;
        public float TssIncrease_KgPerDay;
        public float MLVSS_to_MLSS_Ratio;
        public float Tss_WasteSludge;
        public float Q_WasteSludge;

        public void TssIncreaseStep()
        {
            TvsIncrease_KgPerDay
                = Pe_Q_DailyAveraged_CubicMeterPerDay
                * UnitsConversion.CubicMeter_To_L
                * (Si_Bod5Soluble_MgPerLiter - Se_Bod5Soluble_MgPerLiter)
                * UnitsConversion.Mg_To_Kg
                * Yobs;

            TssIncrease_KgPerDay
                = TvsIncrease_KgPerDay
                / MLVSS_to_MLSS_Ratio;
        }
        #endregion

        #region Waste Sludge


        #endregion


    }
}
