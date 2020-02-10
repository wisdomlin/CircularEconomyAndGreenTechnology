using System;
using System.Collections.Generic;
using System.Text;

namespace BiologicalProcessing.Standard
{
    public class WWTP
    {
        public int PopulationEquivalent;

        public void Minimize_Discharged_Pollutant(Wastewater w)
        {
            w.organic_compounds = 0;
            w.inorganic_compounds = 0;
            w.nutrient = 0;
            w.heavy_metals = 0;
            w.TSS = 0;
            w.pathogens = 0;

            w.BOD = 0;
            w.COD = 0;

            w.nitrate_nitrogen = 0;
            w.nitrite_nitrogen = 0;
            w.ammonia_nitrogen = 0;
            w.organically_bonded_nitrogen = 0;
            w.N_T_Field = 0;
            w.N_Kj_Field = 0;

            w.P_T = 0;
        }
    }
}
