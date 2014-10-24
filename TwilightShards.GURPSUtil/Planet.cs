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
        public WorldType biomeType { get; set; }
        public string atmoComposition { get; set; }
        public double atmoPressure { get; set; }
        public double atmoMass { get; set; }
        private List<AtmosphericConditions> atmoConditions { get; set; }
        public double hydroCoverage { get; set; }
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

        public void AddAtmosphericCondition(AtmosphericConditions item)
        {
            this.atmoConditions.Add(item);
        }
    }
}
