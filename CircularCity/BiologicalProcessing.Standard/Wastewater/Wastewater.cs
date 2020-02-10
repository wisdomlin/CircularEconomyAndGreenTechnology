using System;

namespace BiologicalProcessing.Standard
{
    public class Wastewater
    {
        public Wastewater()
        {

        }

        /// <summary>
        /// Dissolved Organic Carbon
        /// </summary>
        public double DOC; 

        public double organic_compounds;
        public double inorganic_compounds;
        public double nutrient;
        public double heavy_metals;
        /// <summary>
        /// Total Suspended Solids, or Suspended Solids
        /// </summary>
        public double TSS;
        public double SS;

        public double pathogens;

        public double BOD;
        public double COD;

        public double Phosphate_P;
        public double P_T;

        public double nitrate_nitrogen;  // NO3-N
        public double nitrite_nitrogen;  // NO2-N
        public double ammonia_nitrogen;  // NH4 + NH3
        public double organically_bonded_nitrogen;
        public double N_T_Field;
        public double N_Kj_Field;

        /// <summary>
        /// Total Nitrogen (TN) Property by Sum Up
        /// </summary>
        public double N_T_Property
        {
            get
            {
                return
                  nitrate_nitrogen +
                  nitrite_nitrogen +
                  ammonia_nitrogen +
                  organically_bonded_nitrogen;
            }
        }
        /// <summary>
        /// TKN (Total Kjeldahl Nitrogen) Property by Sum Up
        /// </summary>
        public double N_Kj_Property
        {
            get
            {
                return
                  ammonia_nitrogen +
                  organically_bonded_nitrogen;
            }
        }

        
    }
}
