
using NUnit.Framework;
using System;
using System.Data;
using System.IO;

namespace Asc
{
    class TestDACF
    {
        [Test]
        public void UC01_()
        {
            // Arrange
            DACF Dacf = new DACF();

            // Act
            Dacf.IntegrationScenario();

            //// Assert
            //AssertIntegrationTestOutputs();


            //CpaSubSys Cpa = new CpaSubSys();
            //Cpa.GetRawData();
            //Cpa.CallDecomposition();
            //Cpa.PassErrorSeriesAndCallOti();
            //Cpa.GetOutputResult();

        }
    }
}
