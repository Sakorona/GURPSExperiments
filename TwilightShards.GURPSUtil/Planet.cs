﻿using System;
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
        
        public bool HasNoAtmosphere()
        {
            if (this.atmoMass == 0)
                return true;
            else
                return false;
        }
    }
}
