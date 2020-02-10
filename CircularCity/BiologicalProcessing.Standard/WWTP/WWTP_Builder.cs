using System;
using System.Collections.Generic;
using System.Text;

namespace BiologicalProcessing.Standard
{
    public static class WWTP_Builder
    {
        public static WWTP CreateWWTP(int PE = 100000)
        {
            WWTP w = new WWTP();
            w.PopulationEquivalent = PE;
            return w;
        }
    }
}
