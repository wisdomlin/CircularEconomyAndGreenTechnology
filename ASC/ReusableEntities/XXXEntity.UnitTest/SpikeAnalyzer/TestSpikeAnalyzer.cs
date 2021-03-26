

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Asc
{
    class TestSpikeAnalyzer
    {
        [Test]
        public void UC01_TestSpa_Temp()
        {
            SpikeAnalyzer Spa = new SpikeAnalyzer();
            Spa._dataPath = @"C:\Workspace\Branches\CircularEconomyAndGreenTechnology\ASC\Module\Asc.EconomicMoats.ModuleTest\SpikeAnalyzer\Data\TG_STAID000032.txt";
            Spa._docsize = 5387;
            Spa.RunAnalysis();
        }
    }
}
