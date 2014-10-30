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
    }
}
