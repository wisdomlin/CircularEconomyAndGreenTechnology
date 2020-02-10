using System;
using System.Collections.Generic;
using System.Text;

namespace BiologicalProcessing.Standard
{
    public static class WastewaterGenerator
    {
        public const double BOD_Default = 200;
        public const double COD_Default = 500;
        public const double SS_Default = 200;
        public const double N_T_Default = 40;
        public const double N_Kj_Default = 40;
        public const double P_T_Default = 10;

        public static double Unknown_Value_by_Random(int min = 100, int max = 150)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static Wastewater CreateCommercialWastewater(string country = "US")
        {
            Wastewater w = new Wastewater();
            return w;
        }

        public static Wastewater CreateIndustrialWastewater(string country = "US")
        {
            Wastewater w = new Wastewater();
            return w;
        }

        public static Wastewater CreateDomesticWastewater(string country = "US")
        {
            Wastewater w = new Wastewater();
            w.organic_compounds = 100;
            w.inorganic_compounds = 100;
            w.nutrient = 100;
            w.heavy_metals = 100;
            w.TSS = 100;
            w.pathogens = 100;

            Random random = new Random();
            if (country == "US")
            {
                w.BOD = 200;
                w.COD = 500;
                w.SS = 200;
                w.N_T_Field = 40;
                w.P_T = 10;
            }
            else if (country == "TW")
            {
                w.BOD = random.Next(100, 150);
                w.COD = random.Next(200, 400);
                w.SS = 200;
                w.N_T_Field = random.Next(30, 40);
                w.P_T = random.Next(3, 10);
            }
            else if (country == "NL")   // i.e., Dutch.
            {
                w.BOD = 174;
                w.COD = 471;
                w.SS = SS_Default;
                w.N_T_Field = 44;
                w.P_T = 6.7;
                w.TSS = 223;
            }
            else
            {
                w.BOD = BOD_Default;
                w.COD = COD_Default;
                w.SS = SS_Default;
                w.N_T_Field = N_T_Default;
                w.P_T = P_T_Default;

                w.Phosphate_P = Unknown_Value_by_Random();
            }
            return w;
        }
    }
}
