using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightShards.GURPSUtil
{
    public struct StarRecord{
        public int starID;
        public bool isPrimary;
        public double orbitalDist;

        public StarRecord(int id, bool primary, double dist)
        {
            starID = id;
            isPrimary = primary;
            orbitalDist = dist;
        }
    }

    public class Planet
    {
        public string planetName { get; set; }
        public PlanetType planetType { get; set; }
        public WorldSize worldSize { get; set; }
        public List<StarRecord> parentStars { get; set; }
        public int majorMoons { get; set; }
        public int moonlets { get; set; }
        public int ringMoons { get; set; }
        public int capturedMoons { get; set; }
        public double blackbodyTemp { get; set; }
        public double surfaceTemp { get; set; }
        public WorldType biomeType { get; set; }
        public string atmoComposition { get; set; }
        public double atmoPressure { get; set; }
        public double atmoMass { get; set; }
        private List<AtmosphericConditions> atmoConditions { get; set; }

        /// <summary>
        /// This is the hydrographic coverage of the world, expressed as a percentage [0-1]
        /// </summary>
        public double hydroCoverage { get; set; }
        public HydroCoverageType volatileType { get; set; }
        public double worldDiameter { get; set; }
        public double worldDensity { get; set; }
        public double worldMass { get; set; }
        public double worldGravity { get; set; }
        public int resourceValue { get; set; }
        public double eccentricity { get; set; }
        public double orbitalPeriod { get; set; }
        public double moonOrbitalLen { get; set; }
        public int axialTilt { get; set; }
        public int dayLength { get; set; }
        public string geologicActivity { get; set; }
        public string volcanicActivity { get; set; }

        public Planet()
        {
            parentStars = new List<StarRecord>();
            atmoConditions = new List<AtmosphericConditions>();
        }

        //*******************************************************************************************
        // Atmospheric Conditions Function
        //*******************************************************************************************

        /// <summary>
        /// Adds a condition to the atmospheric condition list
        /// </summary>
        /// <param name="item">The condition being added.</param>
        public void AddAtmosphericCondition(AtmosphericConditions item)
        {
            this.atmoConditions.Add(item);
        }

        /// <summary>
        /// Checks to see if the planet has a certain atmospheric condition.
        /// </summary>
        /// <param name="item">The atmospheric condition</param>
        /// <returns>True if present, false if not.</returns>
        public bool ContainsCondition(AtmosphericConditions item)
        {
            return this.atmoConditions.Contains(item);
        }
        
        /// <summary>
        /// This is a simple function that determines if it has an atmosphere
        /// </summary>
        /// <returns>True if it has no, false if it does</returns>
        public bool HasNoAtmosphere()
        {
            if (this.atmoMass == 0)
                return true;
            else
                return false;
        }

        //*******************************************************************************************
        // Planetary Helper Functions
        //*******************************************************************************************

        /// <summary>
        /// This is a function that determines if it is a gas giant.
        /// </summary>
        /// <returns>True if a gas giant, false if not.</returns>
        public bool IsGasGiant()
        {
            if (this.planetType == PlanetType.GasGiantPlanet)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This is a function that determines if it is a terrestial planet.
        /// </summary>
        /// <returns>True if a terrestial planet, false if not.</returns>
        public bool IsTerrestialPlanet()
        {
            if (this.planetType == PlanetType.TerrestialPlanet)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This is a function that determines if it is a terrestial planet.
        /// </summary>
        /// <returns>True if a terrestial planet, false if not.</returns>
        public bool IsAsteroidBelt()
        {
            if (this.planetType == PlanetType.AsteroidBelt)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This function determines if this is a greenhouse planet.
        /// </summary>
        /// <returns>True if a greenhouse planet</returns>
        public bool IsAGreenhousePlanet()
        {
            if (this.biomeType == WorldType.Greenhouse || this.biomeType == WorldType.DryGreenhouse || this.biomeType == WorldType.WetGreenhouse)
                return true;
            else
                return false;
        }

        //*******************************************************************************************
        // Planetary Description Functions
        //*******************************************************************************************

        /// <summary>
        /// This function will produce a string description of the planetary atmosphere
        /// </summary>
        /// <returns></returns>
        public string DescribeAtmosphere()
        {
            string desc = "";

            //first is atmosphere composition
            desc = desc + "Atmosphere Composition: ";

            if (this.worldSize == WorldSize.Small && this.biomeType == WorldType.Ice)
                desc = desc + "Primarily Nitrogen and Methane, with components of Argon, and some trace ammonia and carbon dioxide."
                       + Environment.NewLine;

            if (this.worldSize == WorldSize.Standard && this.biomeType == WorldType.Ice)
                desc = desc + "Primarily Carbon Dioxide and Nitrogen, with some Methane or Sulfur Dioxide" + Environment.NewLine;

            if (this.worldSize == WorldSize.Large && this.biomeType == WorldType.Ice)
                desc = desc + "Primarily Helium and Nitrogen, with some Sulfur Dioxide, Chlorine and Flourine" + Environment.NewLine;

            if ((this.worldSize == WorldSize.Standard || this.worldSize == WorldSize.Large) && this.biomeType == WorldType.Ammonia)
                desc = desc + "Primarily Nitrogen with a large amount of ammonia and methane." + Environment.NewLine;

            if (this.worldSize == WorldSize.Standard && this.biomeType == WorldType.Ocean)
                desc = desc + "Primarily Carbon Dioxide and Nitrogen." + Environment.NewLine;

            if (this.worldSize == WorldSize.Large && this.biomeType == WorldType.Ocean)
                desc = desc + "Primarily Helium and Nitrogen." + Environment.NewLine;

            if (this.worldSize == WorldSize.Standard && this.biomeType == WorldType.Garden)
                desc = desc + "Primarily Nitrogen and Oxygen with some Argon and other trace gasses." + Environment.NewLine;

            if (this.worldSize == WorldSize.Large && this.biomeType == WorldType.Garden)
                desc = desc + "Primarily Nitrogen and Oxygen, with some Argon, Neon, and some Krypton." + Environment.NewLine;

            if (this.biomeType == WorldType.DryGreenhouse)
                desc = desc + "Primarily Carbon Dioxide and some Sulfur Dioxide." + Environment.NewLine;

            if (this.biomeType == WorldType.DryGreenhouse)
                desc = desc + "Primarily Carbon Dioxide, Nitrogen, with large amounts of Water Vapor, and traces of free oxygen.";

            //TODO ATMOSPHERE CONDITIONS

            return desc;
        }
    }
}
