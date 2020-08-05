using System;
using System.Collections.Generic;
using System.Text;

namespace CircularCities.Standard
{
    public static class UnitsConversion
    {
        public const float CubicMeter_To_L = 1000f;     // (L / CubicMeter)，即 1 CubicMeter = 1000 L
        public const float L_To_CubicMeter = 0.001f;    // (CubicMeter / L)，即 1 L = 0.001 CubicMeter

        public const float CMD_to_1000CMD = 0.001f;     // (1000 CMD / CMD)，即 1 [CMD] = 0.001 [1000 CMD]

        // CMD_to_CMS
        public const float CMD_to_CMS = 0.000011574074f;        // (CMS / CMD)，即 1 [CMD] = 0.000011574074 [CMS]
        public const float CMS_to_CMD = 0.001f;                 // (CMD / CMS)，即 1 [CMS] = 86400 [CMD]

        // CMD_to_CMH
        public const float CMD_to_CMH = 0.041666666667f;        // (CMH / CMD)，即 1 [CMD] = 0.041666666667 [CMH]
        public const float CMH_to_CMD = 24f;                    // (CMD / CMH)，即 1 [CMH] = 24 [CMD]

        // CMH_to_CMS
        public const float CMH_to_CMS = 0.000277777778f;        // (CMS / CMH)，即 1 [CMH] = 0.000277777778 [CMS]
        public const float CMS_to_CMH = 3600f;                // (CMH / CMS)，即 1 [CMS] = 3600 [CMH]

        public const float Mg_To_Kg = 0.000001f;        // (Kg / Mg)，即 1 Mg = 0.000001 Kg
        public const float Kg_To_Mg = 1000000f;         // (Mg / Kg)，即 1 Kg = 1000000 Mg

        // 1 hp = 75 (kgf·m)/(s) Metric horsepower
        public const float KgfMeterPerSecond_To_Hp = 75f;
    }
}
