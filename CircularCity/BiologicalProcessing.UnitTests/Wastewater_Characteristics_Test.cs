using NUnit.Framework;
using BiologicalProcessing.Standard;
using System;

namespace BiologicalProcessing.UnitTests
{
    [TestFixture]
    public class Wastewater_Characteristics_Test
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Wastewater_Composition_Domestic_General()
        {
            Wastewater wastewater = WastewaterGenerator.CreateDomesticWastewater();
            Assert.IsNotNull(wastewater.organic_compounds);
            Assert.IsNotNull(wastewater.inorganic_compounds);
            Assert.IsNotNull(wastewater.nutrient);
            Assert.IsNotNull(wastewater.heavy_metals);
            Assert.IsNotNull(wastewater.TSS);
            Assert.IsNotNull(wastewater.pathogens);
        }

        [Test]
        public void Wastewater_Treatment_Effluent_Requirement_General()
        {
            Wastewater wastewater = WastewaterGenerator.CreateDomesticWastewater();
            WWTP WWTP = WWTP_Builder.CreateWWTP();
            WWTP.Minimize_Discharged_Pollutant(wastewater);
            Assert.Less(wastewater.organic_compounds, 1);
            Assert.Less(wastewater.inorganic_compounds, 1);
            Assert.Less(wastewater.nutrient, 1);
            Assert.Less(wastewater.heavy_metals, 1);
            Assert.Less(wastewater.TSS, 1);
            Assert.Less(wastewater.pathogens, 1);
        }

        [Test]
        public void Wastewater_Treatment_Effluent_Requirement_Netherlands_2001()
        {
            Wastewater wastewater = WastewaterGenerator.CreateDomesticWastewater("NL");
            WWTP wwtp = WWTP_Builder.CreateWWTP();
            wwtp.Minimize_Discharged_Pollutant(wastewater);
            Assert.LessOrEqual(wastewater.BOD, 7);
            Assert.LessOrEqual(wastewater.COD, 51);
            Assert.LessOrEqual(wastewater.N_Kj_Field, 6.6);
            Assert.LessOrEqual(wastewater.N_T_Field, 13.5);
            Assert.LessOrEqual(wastewater.P_T, 1.8);
            Assert.LessOrEqual(wastewater.TSS, 10);
        }

        [Test]
        public void Wastewater_Treatment_Effluent_Requirement_Netherlands_2009()
        {
            Wastewater wastewater = WastewaterGenerator.CreateDomesticWastewater("NL");
            WWTP wwtp = WWTP_Builder.CreateWWTP();
            wwtp.Minimize_Discharged_Pollutant(wastewater);
            Assert.LessOrEqual(wastewater.BOD, 20);
            Assert.LessOrEqual(wastewater.COD, 125);
            Assert.LessOrEqual(wastewater.TSS, 30);

            if (wwtp.PopulationEquivalent < 100000)
            {
                Assert.LessOrEqual(wastewater.P_T, 2.0);
            }
            else
            {
                Assert.LessOrEqual(wastewater.P_T, 1.0);
            }

            if (wwtp.PopulationEquivalent < 20000)
            {
                Assert.LessOrEqual(wastewater.N_T_Field, 15);
            }
            else
            {
                Assert.LessOrEqual(wastewater.N_T_Field, 10);
            }
            Assert.LessOrEqual(wastewater.N_Kj_Field, 6.6);
        }

        [Test]
        public void Wastewater_Treatment_Effluent_Requirement_Germany()
        {
            Wastewater wastewater = WastewaterGenerator.CreateDomesticWastewater("DE");
            WWTP wwtp = WWTP_Builder.CreateWWTP();
            wwtp.Minimize_Discharged_Pollutant(wastewater);
            
            if (wwtp.PopulationEquivalent < 1000)
            {
                Assert.LessOrEqual(wastewater.COD, 150);
                Assert.LessOrEqual(wastewater.BOD, 40);
            }
            else if (wwtp.PopulationEquivalent < 5000)
            {
                Assert.LessOrEqual(wastewater.COD, 110);
                Assert.LessOrEqual(wastewater.BOD, 25);
            }
            else if (wwtp.PopulationEquivalent < 10000)
            {
                Assert.LessOrEqual(wastewater.COD, 90);
                Assert.LessOrEqual(wastewater.BOD, 20);
                Assert.LessOrEqual(wastewater.ammonia_nitrogen, 10);
            }
            else if (wwtp.PopulationEquivalent < 100000)
            {
                Assert.LessOrEqual(wastewater.COD, 90);
                Assert.LessOrEqual(wastewater.BOD, 20);
                Assert.LessOrEqual(wastewater.ammonia_nitrogen, 10);
                Assert.LessOrEqual(wastewater.N_T_Field, 18);
                Assert.LessOrEqual(wastewater.P_T, 2.0);
            }
            else // PE >= 100,000
            {
                Assert.LessOrEqual(wastewater.COD, 75);
                Assert.LessOrEqual(wastewater.BOD, 15);
                Assert.LessOrEqual(wastewater.ammonia_nitrogen, 10);
                Assert.LessOrEqual(wastewater.N_T_Field, 13);
                Assert.LessOrEqual(wastewater.P_T, 1.0);
            }            
        }

        [Test]
        public void Wastewater_Treatment_Effluent_Requirement_Switzerland()
        {
            Wastewater wastewater = WastewaterGenerator.CreateDomesticWastewater("CH");
            WWTP wwtp = WWTP_Builder.CreateWWTP();
            wwtp.Minimize_Discharged_Pollutant(wastewater);

            Assert.LessOrEqual(wastewater.TSS, 5);

            //Assert.GreaterOrEqual(wastewater.BOD, 5);
            //Assert.LessOrEqual(wastewater.BOD, 10);
            Assert.LessOrEqual(wastewater.BOD, 5);

            Assert.LessOrEqual(wastewater.DOC, 10);

            //Assert.GreaterOrEqual(wastewater.ammonia_nitrogen, 1);
            //Assert.LessOrEqual(wastewater.ammonia_nitrogen, 2);
            Assert.LessOrEqual(wastewater.ammonia_nitrogen, 1);
            Assert.LessOrEqual(wastewater.nitrite_nitrogen, 0.3);
            Assert.LessOrEqual(wastewater.nitrate_nitrogen, 10);

            //Assert.LessOrEqual(wastewater.Phosphate_P, 1.8);
            //Assert.GreaterOrEqual(wastewater.P_T, 0.2);
            //Assert.LessOrEqual(wastewater.P_T, 0.8);
            Assert.LessOrEqual(wastewater.P_T, 0.2);
        }

        [Test]
        public void Wastewater_Composition_Domestic_US()
        {            
            Wastewater wastewater = WastewaterGenerator.CreateDomesticWastewater("US");
            Assert.GreaterOrEqual(wastewater.BOD, 200);
            Assert.LessOrEqual(wastewater.BOD, 200);
            Assert.GreaterOrEqual(wastewater.COD, 500);
            Assert.LessOrEqual(wastewater.COD, 500);
            Assert.GreaterOrEqual(wastewater.SS, 200);
            Assert.LessOrEqual(wastewater.SS, 200);
            Assert.GreaterOrEqual(wastewater.N_T_Field, 40);
            Assert.LessOrEqual(wastewater.N_T_Field, 40);
            Assert.GreaterOrEqual(wastewater.P_T, 10);
            Assert.LessOrEqual(wastewater.P_T, 10);
        }

        [Test]
        public void Wastewater_Composition_Domestic_TW()
        {
            Wastewater wastewater = WastewaterGenerator.CreateDomesticWastewater("TW");
            Assert.GreaterOrEqual(wastewater.BOD, 100);
            Assert.LessOrEqual(wastewater.BOD, 150);
            Assert.GreaterOrEqual(wastewater.COD, 200);
            Assert.LessOrEqual(wastewater.COD, 400);
            Assert.GreaterOrEqual(wastewater.SS, 200);
            Assert.LessOrEqual(wastewater.SS, 200);
            Assert.GreaterOrEqual(wastewater.N_T_Field, 30);
            Assert.LessOrEqual(wastewater.N_T_Field, 40);
            Assert.GreaterOrEqual(wastewater.P_T, 3);
            Assert.LessOrEqual(wastewater.P_T, 10);
        }

        /*
        More tests
        */
 
        
    }
}