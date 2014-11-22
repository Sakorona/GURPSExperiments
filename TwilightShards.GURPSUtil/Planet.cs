using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwilightShards.genLibrary;

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

        /// <summary>
        /// This contains the moons of the planet. 
        /// </summary>
        /// <remarks>The value in this is in planetary diameters.</remarks>
        public List<MoonRecord> PlanetaryMoons { get; set; }
        public double blackbodyTemp { get; set; }
        public double surfaceTemp { get; set; }
        public WorldType biomeType { get; set; }

        /// <summary>
        /// This is the description of the atmosphere.
        /// </summary>
        private AtmoComp atmoBreakdown { get; set; }

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
        
        /// <summary>
        /// This is the period the world takes to complete one rotation to a distant fixed point
        /// </summary>
        public double siderealPeriod { get; set;}
        
        //public double moonOrbitalLen { get; set; }
        public int axialTilt { get; set; }
        public double dayLength { get; set; }
        public GeologicActivity tectonicActivity { get; set; }
        public GeologicActivity volcanicActivity { get; set; }

        /// <summary>
        /// This checks if the world is tidal locked
        /// </summary>
        public bool IsTidalLocked { get; set; }

        /// <summary>
        /// This checks if the world is orbiting in a retrograde manner
        /// </summary>
        public bool IsRetrograde { get; set; }

        public Planet()
        {
            atmoBreakdown = new AtmoComp();
            parentStars = new List<StarRecord>();
            atmoConditions = new List<AtmosphericConditions>();
            PlanetaryMoons = new List<MoonRecord>();
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
        /// This adds a moon to the dictionary.
        /// </summary>
        /// <param name="type">The type of the Moon</param>
        /// <param name="dist">The distance from the parent planet</param>
        public void AddMoon(MoonType type, double dist, WorldSize size)
        {
            this.PlanetaryMoons.Add(new MoonRecord(type, dist,size));
        }

        /// <summary>
        /// This checks to see if the orbital is present or within a variance
        /// </summary>
        /// <param name="dist">Distance</param>
        /// <param name="allowedDist">Allowed variance</param>
        /// <returns>True if present or within variance, false otherwise</returns>
        public bool CheckLunarOrbitalPresent(double dist, double allowedDist)
        {
            foreach (MoonRecord md in this.PlanetaryMoons)
            {
                if ((md.orbitalRadius != 0) &&
                    (md.orbitalRadius >= (dist - allowedDist)) && (md.orbitalRadius <= (dist + allowedDist))){
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This returns a count of how many major moons this world has.
        /// </summary>
        /// <returns>A count of major moons</returns>
        public int MajorMoonCount()
        {
            int moons = 0;
            foreach (MoonRecord mr in this.PlanetaryMoons)
            {
                if (mr.moon == MoonType.MajorMoon) moons++;
            }

            return moons;
        }

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

        /// <summary>
        /// Sets the atmosphere marginal status.
        /// </summary>
        /// <param name="val">The value to set it to marginal</param>
        public void SetAtmosphereMarginal(bool val)
        {
            this.atmoBreakdown.SetMarginal(val);
        }

        public void SetAtmosphereCorrosive(bool val)
        {
            this.atmoBreakdown.SetCorrosive(true);
        }

        public void SetAtmosphereLethallyToxic(bool val)
        {
            this.atmoBreakdown.SetLethallyToxic(true);
        }

        public void SetAtmosphereSuffocating(bool val)
        {
            this.atmoBreakdown.SetSuffocating(true);
        }

        /// <summary>
        /// This function purges the atmosphere.
        /// </summary>
        public void PurgeAtmosphere()
        {
            this.atmoBreakdown.Purge();
        }

        //*******************************************************************************************
        // Planetary Description Functions
        //*******************************************************************************************

        /// <summary>
        /// This function will create the atmosphere record.
        /// </summary>
        public void CreateAtmosphere(Dice ourDice)
        {
            if (this.worldSize == WorldSize.Small)
            {
                if (this.biomeType == WorldType.Ice)
                {
                    atmoBreakdown.AddAtmoElement("Nitrogen", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Methane", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Argon", ElemAmount.Some);
                    atmoBreakdown.AddAtmoElement("Ammonia", ElemAmount.Trace);
                    atmoBreakdown.AddAtmoElement("Carbon Dioxide", ElemAmount.Trace);
                    atmoBreakdown.SetSuffocating(true);

                    if (ourDice.rollUnderGurps(15))
                        atmoBreakdown.SetMildlyToxic(true);
                    else
                        atmoBreakdown.SetHighlyToxic(true);
                }
            }


            if (this.worldSize == WorldSize.Standard)
            { 
                if (this.biomeType == WorldType.Ice)
                {
                    atmoBreakdown.AddAtmoElement("Carbon Dioxide", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Nitrogen", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Methane", ElemAmount.Some);
                    atmoBreakdown.AddAtmoElement("Sulfur Dioxide", ElemAmount.Some);
                    atmoBreakdown.SetSuffocating(true);

                    if (!(ourDice.rollUnderGurps(12)))
                        atmoBreakdown.SetMildlyToxic(true);
                }

                if (this.biomeType == WorldType.Ocean)
                {
                    atmoBreakdown.AddAtmoElement("Nitrogen", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Carbon Dioxide", ElemAmount.Primary);
                    atmoBreakdown.SetSuffocating(true);
                    if (!(ourDice.rollUnderGurps(12)))
                        atmoBreakdown.SetCorrosive(true);
                }

                if (this.biomeType == WorldType.Garden)
                {
                    atmoBreakdown.AddAtmoElement("Nitrogen", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Oxygen", ElemAmount.Some);
                }
            }

            if (this.worldSize == WorldSize.Large)
            {
                if (this.biomeType == WorldType.Ice)
                {
                    atmoBreakdown.AddAtmoElement("Helium", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Nitrogen", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Chlorine", ElemAmount.Some);
                    atmoBreakdown.AddAtmoElement("Flourine", ElemAmount.Some);
                    atmoBreakdown.AddAtmoElement("Sulfur Dioxide", ElemAmount.Some);
                    atmoBreakdown.SetSuffocating(true);
                    atmoBreakdown.SetHighlyToxic(true);
                }

                if (this.biomeType == WorldType.Ocean)
                {
                    atmoBreakdown.AddAtmoElement("Helium", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Nitrogen", ElemAmount.Primary);
                    atmoBreakdown.SetSuffocating(true);
                    atmoBreakdown.SetHighlyToxic(true);
                }

                if (this.biomeType == WorldType.Garden)
                {
                    atmoBreakdown.AddAtmoElement("Nitrogen", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Oxygen", ElemAmount.Primary);
                    atmoBreakdown.AddAtmoElement("Noble Gasses (Argon, Krypton, Neon)", ElemAmount.Some);
                }
            }

            if (this.biomeType == WorldType.DryGreenhouse)
            {
                atmoBreakdown.AddAtmoElement("Carbon Dioxide", ElemAmount.Primary);
                atmoBreakdown.AddAtmoElement("Sulfur Dioxide", ElemAmount.Primary);
                atmoBreakdown.SetSuffocating(true);
                atmoBreakdown.SetLethallyToxic(true);
                atmoBreakdown.SetCorrosive(true);
            }

            if (this.biomeType == WorldType.WetGreenhouse)
            {
                atmoBreakdown.AddAtmoElement("Carbon Dioxide", ElemAmount.Primary);
                atmoBreakdown.AddAtmoElement("Nitrogen", ElemAmount.Primary);
                atmoBreakdown.AddAtmoElement("Water Vapor", ElemAmount.Some);
                atmoBreakdown.AddAtmoElement("Oxygen", ElemAmount.Trace);
                atmoBreakdown.SetSuffocating(true);
                atmoBreakdown.SetLethallyToxic(true);
                atmoBreakdown.SetCorrosive(true);
            }

            if ((this.worldSize == WorldSize.Standard || this.worldSize == WorldSize.Large) && this.biomeType == WorldType.Ammonia)
            {
                atmoBreakdown.AddAtmoElement("Nitrogen", ElemAmount.Primary);
                atmoBreakdown.AddAtmoElement("Ammonia", ElemAmount.Some);
                atmoBreakdown.AddAtmoElement("Methane", ElemAmount.Some);
                atmoBreakdown.SetSuffocating(true);
                atmoBreakdown.SetLethallyToxic(true);
                atmoBreakdown.SetCorrosive(true);
            }
                
            //add atmosphere conditions
            foreach (AtmosphericConditions ac in this.atmoConditions)
            {
                if (ac == AtmosphericConditions.Chlorine){
                    if (ourDice.dblProb() < .78)
                    {
                        atmoBreakdown.AddOrIncreaseAtmoElement("Chlorine", ElemAmount.Trace);
                        atmoBreakdown.AddSpecialCondition("Unfiltered air is corrosive and poisonous");
                    }
                    else
                    {
                        atmoBreakdown.AddOrIncreaseAtmoElement("Chlorine", ElemAmount.Some);
                        atmoBreakdown.AddSpecialCondition("The air is faintly colored and views are faintly distorted");
                        atmoBreakdown.AddSpecialCondition("Rainfall is a weak hydrochloric solution");
                        atmoBreakdown.AddSpecialCondition("Certain areas are extremely lethal, with the general atmosphere being highly toxic.");
                        atmoBreakdown.AddSpecialCondition("The atmosphere is corrosive.");
                    }
                }

                //if (ac == AtmosphericConditions.Corrosive)
                //    atmoBreakdown.AddSpecialCondition("The atmosphere is corrosive.");

                // AtmosphericConditions.EffectiveOnePressureClassDown is ignored. It's mainly
                // for pressure classes. 
                // So is AtmosphericConditions.EffectiveOnePressureClassUp for the same reasons.
                // It has now been removed.

                if (ac == AtmosphericConditions.FlammabilityOneClassUp)
                    atmoBreakdown.AddSpecialCondition("The high oxygen concentration in the atmosphere has made everything more flammable.");

                if (ac == AtmosphericConditions.Flourine)
                {
                    if (ourDice.dblProb() < .78)
                    {
                        atmoBreakdown.AddOrIncreaseAtmoElement("Flourine", ElemAmount.Trace);
                        atmoBreakdown.AddSpecialCondition("Unfiltered air is corrosive and poisonous");
                    }
                    else
                    {
                        atmoBreakdown.AddOrIncreaseAtmoElement("Flourine", ElemAmount.Some);
                        atmoBreakdown.AddSpecialCondition("The air is faintly colored and views are faintly distorted");
                        atmoBreakdown.AddSpecialCondition("Rainfall is a weak hydroflouric solution");
                        atmoBreakdown.AddSpecialCondition("Certain areas are extremely lethal, with the general atmosphere being highly poisonous and toxic.");
                        atmoBreakdown.AddSpecialCondition("The atmosphere is corrosive.");
                    }
                }
         
                if (ac == AtmosphericConditions.HighCarbonDioxide)
                {
                    if (ourDice.dblProb() > .6 && ourDice.dblProb() < .9)
                    {
                        atmoBreakdown.AddOrIncreaseAtmoElement("Carbon Dioxide", ElemAmount.Some);
                        atmoBreakdown.AddSpecialCondition("The high amount of carbon dioxide causes hyperventiliation");
                        atmoBreakdown.AddSpecialCondition("The amount of carbon dioxide can be acclimated to.");
                    }
                    if (ourDice.dblProb() > .9)
                    {
                        atmoBreakdown.AddOrIncreaseAtmoElement("Carbon Dioxide", ElemAmount.Primary);
                        atmoBreakdown.AddSpecialCondition("The atmosphere is mildly toxic, and cannot be acclimated to.");
                    }
                }

                //if (ac == AtmosphericConditions.HighlyToxic)
                //    atmoBreakdown.AddSpecialCondition("The atmosphere is highly toxic.");

                if (ac == AtmosphericConditions.HighOxygen)
                    atmoBreakdown.AddSpecialCondition("All ill effects are treated as if the atmosphere was more dense");

                if (ac == AtmosphericConditions.InertGases)
                    atmoBreakdown.AddSpecialCondition("Long term exposure may cause inert gas narcosis.");

                //if (ac == AtmosphericConditions.LethallyToxic)
                //    atmoBreakdown.AddSpecialCondition("The atmosphere is lethally toxic.");
                
                //LowOxygen is ignored for now. 

                //if (ac == AtmosphericConditions.MildlyToxic)
                //    atmoBreakdown.AddSpecialCondition("The atmosphere is mildly toxic");

                if (ac == AtmosphericConditions.NitrogenCompounds)
                {
                    atmoBreakdown.AddAtmoElement("Nitrogen Oxides", ElemAmount.Trace);
                    atmoBreakdown.AddSpecialCondition("This atmosphere is mildly toxic, with some areas highly toxic.");
                    atmoBreakdown.AddSpecialCondition("Any open water will be tainted by acid.");
                    this.volatileType = HydroCoverageType.AcidTainted;
                }

                //Obviously, None is ignored. For now.

                if (ac == AtmosphericConditions.OrganicToxins)
                {
                    atmoBreakdown.AddSpecialCondition("There are organic toxins in the atmosphere, causing it to be mildly toxic.");
                    atmoBreakdown.AddSpecialCondition("This may inflict a disease.");
                }
                
                if (ac == AtmosphericConditions.Pollutants)
                {
                    atmoBreakdown.AddSpecialCondition("Pollutants like heavy metal or radioactive dust are in the air.");
                    atmoBreakdown.AddSpecialCondition("This atmosphere is mildly toxic. It may be heavily toxic in certain areas.");
                }

                //if (ac == AtmosphericConditions.Suffocating)
                //    atmoBreakdown.AddSpecialCondition("The atmosphere is suffocating.");
                
                if (ac == AtmosphericConditions.SulfurCompounds)
                {
                    atmoBreakdown.AddSpecialCondition("There are compounds such as Hydrogen sulfide, Sulfur Dioxide and Sulfur Trioxide in the atmosphere.");
                    atmoBreakdown.AddSpecialCondition("Rainfall and standing water are weak solutions of sulfuric acid.");
                    atmoBreakdown.AddSpecialCondition("The atmosphere is mildly toxic, but highly toxic near a source of sulfur compounds");
                    atmoBreakdown.AddAtmoElement("Sulfur Oxides", ElemAmount.Trace);
                    this.volatileType = HydroCoverageType.SulfuricAcid;
                }
            }
        }

        /// <summary>
        /// This function sees if the atmosphere is corrosive
        /// </summary>
        /// <returns></returns>
        public bool IsAtmosphereCorrosive()
        {
            return this.atmoBreakdown.IsCorrosive();
        }

        /// <summary>
        /// This function sees if the atmosphere is suffocating
        /// </summary>
        /// <returns></returns>
        public bool IsAtmosphereSuffocating()
        {
            return this.atmoBreakdown.IsSuffocating();
        }

        /// <summary>
        /// This function sees if the atmosphere is toxic
        /// </summary>
        /// <returns></returns>
        public bool IsAtmosphereToxic()
        {
            return this.atmoBreakdown.IsToxic();
        }

        /// <summary>
        /// Is this atmosphere mildly toxic?
        /// </summary>
        /// <returns>true if mildly toxic, false if not.</returns>
        public bool IsAtmosphereMildlyToxic()
        {
            return this.atmoBreakdown.IsMildlyToxic();
        }

        /// <summary>
        /// Is this atmosphere highly toxic?
        /// </summary>
        /// <returns>true if highly toxic, false if not.</returns>
        public bool IsAtmosphereHighlyToxic()
        {
            return this.atmoBreakdown.IsHighlyToxic();
        }

        /// <summary>
        /// Is this atmosphere lethally toxic?
        /// </summary>
        /// <returns>true if lethally toxic, false if not.</returns>
        public bool IsAtmosphereLethallyToxic()
        {
            return this.atmoBreakdown.IsLethallyToxic();
        }

        /// <summary>
        /// This determines if the atmosphere is breathable.
        /// </summary>
        /// <returns>True if breathable, false if not.</returns>
        public bool IsBreathableAtmosphere()
        {
            bool atmoVal = true;
            if (IsAtmosphereSuffocating()) atmoVal = false;
            if (IsAtmosphereCorrosive()) atmoVal = false;
            if (IsAtmosphereToxic()) atmoVal = false;
            if (this.atmoPressure < .01) atmoVal = false;
            return atmoVal;
        }

        /// <summary>
        /// This checks if the atmosphere is present
        /// </summary>
        /// <returns>True if the atmosphere is present, false if not</returns>
        public bool IsAtmospherePresent()
        {
            if (this.atmoPressure > .01)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Is this atmosphere marginal
        /// </summary>
        /// <returns>true if marginal, false if not.</returns>
        public bool IsMarginal()
        {
            return this.atmoBreakdown.IsMarginal();
        }

        /// <summary>
        /// This returns the orbital distance from the primary
        /// </summary>
        /// <returns>The orbital distance from the primary</returns>
        public double GetOrbitalDistanceToPrimary()
        {
            foreach (StarRecord s in this.parentStars)
            {
                if (s.isPrimary == true)
                {
                    return s.orbitalDist;
                }
            }

            return 0;
        }

        /// <summary>
        /// This function converts the planetary diameter into AUs.
        /// </summary>
        /// <returns></returns>
        public double ConvertPlanetaryDiameterToAU()
        {
            //planetary diameters are stored in KM.
            return (this.worldDiameter * (PlanetReference.EarthDiameter * .00000000668458712));
        }

        /// <summary>
        /// This function returns the atmosphere description
        /// </summary>
        /// <returns>A string describing the atmosphere</returns>
        public string DescribeAtmosphere()
        {
            return this.atmoBreakdown.DescribeAtmosphere();
        }

        public string DescribePlanet()
        {
            string desc = "";
            bool warning = true;

            if (warning)
            {
                desc = desc + "Be advised that this does not calculate tidal locking, and a slightly lower (5) modifier for ";
                desc = desc + " volcanic activity.";
                desc = desc + Environment.NewLine;
            }

            desc = desc + "Planet: " + this.worldSize + " (" + this.biomeType + ") orbits at ";
            desc = desc + this.GetOrbitalDistanceToPrimary() + " AU. ";
            desc = desc + Environment.NewLine;
            desc = desc + "Blackbody Temperature: " + this.blackbodyTemp + " K" + Environment.NewLine;
            
            //physical properties
            desc = desc + "Diameter: " + Math.Round(this.worldDiameter* PlanetReference.EarthDiameter,2)  + " KM ";
            desc = desc + "Mass: " + Math.Round(this.worldMass, 2) + " earth masses. Density: " + Math.Round(this.worldDensity,2) * PlanetReference.EarthDensity + " g/cc ";
            desc = desc + "Gravity: " + Math.Round(this.worldGravity * PlanetReference.EarthGravity,2) + " m/s^2";
            desc = desc + Environment.NewLine;
            desc = desc + "Volcanic Activity is " + this.volcanicActivity + " and tectonic activity is " + this.tectonicActivity + Environment.NewLine;
            
            //atmospheric properties
            desc = desc + "Atmospheric Pressure: " + Math.Round(this.atmoPressure,2) + " atm, with mass: " + Math.Round(this.atmoMass,2);
            desc = desc + Environment.NewLine;
            desc = desc + this.DescribeAtmosphere();
            desc = desc + Environment.NewLine;

            //hydrographic coverage
            desc = desc + "Hydrographic Coverage: " + Math.Round((this.hydroCoverage * 100), 2) + "%. Type: ";
            desc = desc + PlanetReference.DescribeHydrographicCoverage(this.volatileType);
            desc = desc + Environment.NewLine;

            //Climate properties
            desc = desc + "Climate: ";
            desc = desc + PlanetReference.GetClimateName(this.surfaceTemp, true, true);
            desc = desc + Environment.NewLine;

            //moons?
            desc = desc + "Moons: " + this.PlanetaryMoons.Count() + Environment.NewLine;
            foreach (MoonRecord mr in this.PlanetaryMoons)
            {
                desc = desc + "Moon Type: " + mr.moon + " at orbital " + mr.orbitalRadius + " and size: ";
                desc = desc + mr.moonSize;
                desc = desc + Environment.NewLine;
            }

            //FINALLY, orbital characteristics.
            desc = desc + Environment.NewLine;
            desc = desc + "Orbital Characteristics" + Environment.NewLine;
            if (IsTidalLocked)
                desc = desc + "[This planet is tidally locked!]" + Environment.NewLine;
            if (IsRetrograde)
                desc = desc + "This planet orbits in a retrograde motion." + Environment.NewLine;
            if (eccentricity != 0)
            {
                desc = desc + "Eccentricity: " + Math.Round(this.eccentricity,2) + " with Periastron of ";
                desc = desc + PlanetReference.GetPeriastron(this.GetOrbitalDistanceToPrimary(),
                             this.eccentricity) + " AU and apastron of " +
                             PlanetReference.GetApastron(this.GetOrbitalDistanceToPrimary(), this.eccentricity) +
                             " AU";
                desc = desc + Environment.NewLine;
                desc = desc + "Axial Tilt of " + this.axialTilt + Environment.NewLine + "";
            }
            else
            {
                desc = desc + "Eccentricity of 0 and an axial tilt of " + this.axialTilt + Environment.NewLine;
            }
            desc = desc + "Sidereal Period: " + Math.Round(this.siderealPeriod,2) + " days. Rotation Period: " + Math.Round(this.dayLength,2) + " days.";
            desc = desc + Environment.NewLine;
            desc = desc + "Year Length is " + Math.Round(this.orbitalPeriod,2) + " T-years.";

            return desc;
        }

        public override string ToString()
        {
            return DescribePlanet();
        }

    }
}
