using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwilightShards.GURPSUtil;
using TwilightShards.genLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GURPSUtilTestProject
{
    [TestClass]
    public class PlanetReferenceTesting
    {
        [TestMethod]
        public void GenerateAtmosphere()
        {
            Planet testPlanet = new Planet();
            Dice ourDice = new Dice();
            testPlanet.biomeType = WorldType.Greenhouse;
            testPlanet.worldSize = WorldSize.Standard;
            testPlanet.planetType = PlanetType.TerrestialPlanet;
            PlanetReference.GenerateAtmosphere(ourDice, testPlanet);
            PlanetReference.GenerateHydrographicCoverage(ourDice, testPlanet);
            testPlanet.CreateAtmosphere(ourDice); //call this to create the atmosphere


            Assert.AreEqual(true, testPlanet.IsAtmosphereCorrosive());
        }

        [TestMethod]
        public void GenerateAtmosphereFailure()
        {
            Planet testPlanet = new Planet();
            testPlanet.biomeType = WorldType.Greenhouse;
            testPlanet.worldSize = WorldSize.Tiny;
            testPlanet.planetType = PlanetType.TerrestialPlanet;

            PlanetReference.GenerateAtmosphere(new Dice(), testPlanet);

            Assert.AreEqual(true, testPlanet.HasNoAtmosphere());
        }

        [TestMethod] 
        public void GenerateProperGreenhouseBiomeType()
        {
            Planet testPlanet = new Planet();
            testPlanet.biomeType = WorldType.Greenhouse;
            testPlanet.worldSize = WorldSize.Standard;
            testPlanet.planetType = PlanetType.TerrestialPlanet;

            PlanetReference.GenerateHydrographicCoverage(new Dice(), testPlanet);

            Assert.AreNotEqual(testPlanet.biomeType, WorldType.Greenhouse);
        }

        [TestMethod]
        public void GenerateGreenhouseProperly()
        {
            Planet testPlanet = new Planet();
            testPlanet.biomeType = WorldType.Greenhouse;
            testPlanet.worldSize = WorldSize.Standard;
            testPlanet.planetType = PlanetType.TerrestialPlanet;

            PlanetReference.GenerateHydrographicCoverage(new Dice(), testPlanet);

            if (testPlanet.biomeType == WorldType.DryGreenhouse)
                Assert.AreEqual(testPlanet.hydroCoverage, 0);
            else
                Assert.AreNotEqual(testPlanet.hydroCoverage, 0);
        }

        [TestMethod]
        public void VerifyHydrographicCoverage()
        {
            double minHydro = 1, maxHydro = 0;
            Planet currPlanet = new Planet();
            Dice testDice = new Dice();
            List<WorldSize> sizes = new List<WorldSize>() { WorldSize.Standard, WorldSize.Small, WorldSize.Tiny, WorldSize.Large };
            bool testValue = false;

            for (int i = 0; i < 1000000; i++)
            {
                currPlanet.planetType = PlanetType.TerrestialPlanet;
                currPlanet.worldSize = sizes.PickRandom(testDice);
                currPlanet.blackbodyTemp = PlanetReference.GetBlackBodyTemp(testDice.rollInRange(.007,20), 
                                                                            testDice.rollInRange(.1, 20));
                currPlanet.biomeType = PlanetReference.GetWorldType(testDice, currPlanet,
                    "Star", testDice.rollInRange(.1,2), testDice.rollInRange(2,13));
                PlanetReference.GenerateAtmosphere(testDice, currPlanet);
                PlanetReference.GenerateHydrographicCoverage(testDice, currPlanet);

                if (currPlanet.hydroCoverage > maxHydro)
                    maxHydro = currPlanet.hydroCoverage;

                if (currPlanet.hydroCoverage < minHydro)
                    minHydro = currPlanet.hydroCoverage;
            }

            if (minHydro < 0 || maxHydro > 1)
                testValue = false;
            else
                testValue = true;

            Assert.AreEqual(true, testValue);

        }
    
        [TestMethod]
        public void VerifyBlackbodyGeneration()
        {
            double testTemp = PlanetReference.GetBlackBodyTemp(.9, .75);
            Assert.AreEqual(313, Math.Round(testTemp));
        }

        [TestMethod]
        public void VerifyTotalBlackbodyGeneration()
        {
            double testTemp = PlanetReference.GetBlackbodyTempTotal(new double[] { PlanetReference.GetBlackBodyTemp(.9, .75), PlanetReference.GetBlackBodyTemp(.6, 1.51) });
            Assert.AreEqual(325, Math.Round(testTemp));
        }

        [TestMethod]
        public void VerifySanityExtremeRangeBlackbody()
        {
            double testTemp = PlanetReference.GetBlackBodyTemp(.001, 440);
            //Assert.AreEqual(testTemp, 0);
            if (testTemp >= 0)
                Assert.AreEqual(1, 1);
            else
                Assert.Fail("Test Temp is below 0");
        }

        [TestMethod]
        public void VerifyRangeBlackbody()
        {
            bool test = false;

            try
            {
                double testTemp = PlanetReference.GetBlackBodyTemp(1, 0);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Orbital Radius cannot be 0 or negative.");
                test = true;
            }

            if (!test) Assert.Fail("Did not fail on 0 range as expected.");
        }

        [TestMethod]
        public void VerifyNegativeRangeBlackbody()
        {
            bool test = false;

            try
            {
                double testTemp = PlanetReference.GetBlackBodyTemp(1, -20);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Orbital Radius cannot be 0 or negative.");
                test = true;
            }
            
            if (!test) Assert.Fail("Did not fail on negative range as expected.");
        }
  
        [TestMethod]
        public void VerifyIdyllicPlanet()
        {
            Planet currP = new Planet();

            currP.atmoPressure = 1;
            currP.surfaceTemp = 298;
            currP.hydroCoverage = .89;
            currP.SetAtmosphereMarginal(false);

            int mod  = PlanetReference.GenerateHabitabilityModifer(currP);

            Assert.AreEqual(8, mod);
        }

        [TestMethod]
        public void VerifyHellPlanet()
        {
            Planet currP = new Planet();

            currP.atmoPressure = 1;
            currP.surfaceTemp = 698;
            currP.hydroCoverage = .64;
            currP.SetAtmosphereCorrosive(true);
            currP.SetAtmosphereSuffocating(true);
            currP.SetAtmosphereLethallyToxic(true);

            int mod = PlanetReference.GenerateHabitabilityModifer(currP);

            Assert.AreEqual(-2, mod);
        }

        [TestMethod]
        public void VerifyNormalPlanet()
        {
            Planet currP = new Planet();

            currP.atmoPressure = .91; 
            currP.surfaceTemp = 298; 
            currP.hydroCoverage = 0; 
            currP.SetAtmosphereMarginal(true);

            int mod = PlanetReference.GenerateHabitabilityModifer(currP);

            Assert.AreEqual(5, mod);
        }

        [TestMethod]
        public void VerifyAffinity()
        {
            double minAffinity = 10, maxAffinity = 0;
            Planet currPlanet = new Planet();
            Dice testDice = new Dice();
            List<WorldSize> sizes = new List<WorldSize>() { WorldSize.Standard, WorldSize.Small, WorldSize.Tiny, WorldSize.Large };
            bool testValue = false;

            int affin = 0;

            for (int i = 0; i < 100000; i++)
            {
                currPlanet.planetType = PlanetType.TerrestialPlanet;
                currPlanet.worldSize = sizes.PickRandom(testDice);
                currPlanet.blackbodyTemp = PlanetReference.GetBlackBodyTemp(testDice.rollInRange(.007, 20),
                                                                            testDice.rollInRange(.1, 20));
                currPlanet.biomeType = PlanetReference.GetWorldType(testDice, currPlanet,
                    "Star", testDice.rollInRange(.1, 2), testDice.rollInRange(2, 13));
                PlanetReference.GenerateAtmosphere(testDice, currPlanet);
                PlanetReference.GenerateHydrographicCoverage(testDice, currPlanet);
                currPlanet.CreateAtmosphere(testDice); //call this to create the atmosphere
                currPlanet.surfaceTemp = PlanetReference.GenerateSurfaceTempFromBlackbody(currPlanet);
                PlanetReference.GenerateResourceValue(testDice, currPlanet);
                affin = PlanetReference.GenerateAffinityModifier(currPlanet);
                currPlanet.PurgeAtmosphere();

                if (affin < minAffinity)
                    minAffinity = affin;
                if (affin > maxAffinity)
                    maxAffinity = affin;
            }

            if (minAffinity < -5 || maxAffinity > 10)
                testValue = false;
            else
                testValue = true;

            Assert.AreEqual(true, testValue);
        }
    }
}
