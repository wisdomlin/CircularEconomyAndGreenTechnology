using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class StandbyMaxAlarm : IJudge
    {
        private SpecValue SV;
        private SpecValue AH;

        public StandbyMaxAlarm(SpecValue SV, SpecValue AH)
        {
            this.SV = SV;
            this.AH = AH;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
