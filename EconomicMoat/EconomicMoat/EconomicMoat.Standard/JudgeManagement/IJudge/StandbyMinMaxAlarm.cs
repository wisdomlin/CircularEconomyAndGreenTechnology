﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EconomicMoat.Standard
{   
    public class StandbyMinMaxAlarm : IJudge
    {
        private SpecValue SV;
        private SpecValue AL;
        private SpecValue AH;

        public StandbyMinMaxAlarm(SpecValue SV, SpecValue AL, SpecValue AH)
        {
            this.SV = SV;
            this.AL = AL;
            this.AH = AH;
        }

        public bool Judge(PresentValue PV)
        {
            throw new NotImplementedException();
        }
    }
}
