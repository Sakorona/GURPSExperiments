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
            testPlanet.biomeType = WorldType.Greenhouse;
            testPlanet.worldSize = WorldSize.Standard;
            testPlanet.planetType = PlanetType.TerrestialPlanet;

            PlanetReference.GenerateAtmosphere(new Dice(), testPlanet);

            Assert.AreEqual(true, testPlanet.ContainsCondition(AtmosphericConditions.Corrosive));
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
    }
}
