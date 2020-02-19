using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class RelMaxAlarm : IJudge
    {
        private SpecValue SV;
        private SpecValue AH;

        public RelMaxAlarm(SpecValue SV, SpecValue AH)
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
