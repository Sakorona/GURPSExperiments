using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TwilightShards.genLibrary;

namespace TwilightShards.GURPSUtil
{
    /// <summary>
    /// This contains the refrence for generating stars.
    /// </summary>
    public static class PlanetReference
    {
        //*******************************************************************************************
        // Constants 
        //*******************************************************************************************
        /// <summary>
        /// The average density of the Earth (in g/cc)
        /// </summary>
        public static double EarthDensity = 5.22;

        /// <summary>
        /// The diameter of the Earth (in km)
        /// </summary>
        public static double EarthDiameter = 12742;

        /// <summary>
        /// Earth Gravity, defined as 1 G. (m/s^2)
        /// </summary>
        public static double EarthGravity = 9.80665;

        //*******************************************************************************************
        // Members
        //*******************************************************************************************

        /// <summary>
        /// The table for world density
        /// </summary>
        private static double[][] worldDensityTable;

        /// <summary>
        /// The table for size constraints
        /// </summary>
        private static double[][] sizeConstraintsTable;

        /// <summary>
        /// This table stores resource value rolls.
        /// </summary>
        private static int[][] resourceValueTable;
        //*******************************************************************************************
        // Static Initalizer
        //*******************************************************************************************

        /// <summary>
        /// Static constructor for tables
        /// </summary>
        static PlanetReference()
        {
            #region World Density Table
            worldDensityTable = new double[5][];

            worldDensityTable[0] = new double[3];
            worldDensityTable[0][0] = .3;
            worldDensityTable[0][1] = .6;
            worldDensityTable[0][2] = .8;

            worldDensityTable[1] = new double[3];
            worldDensityTable[1][0] = .4;
            worldDensityTable[1][1] = .7;
            worldDensityTable[1][2] = .9;

            worldDensityTable[2] = new double[3];
            worldDensityTable[2][0] = .5;
            worldDensityTable[2][1] = .8;
            worldDensityTable[2][2] = 1;

            worldDensityTable[3] = new double[3];
            worldDensityTable[3][0] = .6;
            worldDensityTable[3][1] = .9;
            worldDensityTable[3][2] = 1.1;

            worldDensityTable[4] = new double[3];
            worldDensityTable[4][0] = .7;
            worldDensityTable[4][1] = 1.0;
            worldDensityTable[4][2] = 1.2;
            #endregion

            #region Size Constraints Table
            sizeConstraintsTable = new double[4][];

            sizeConstraintsTable[0] = new double[2];
            sizeConstraintsTable[0][0] = .004;
            sizeConstraintsTable[0][1] = .024;

            sizeConstraintsTable[1] = new double[2];
            sizeConstraintsTable[1][0] = .024;
            sizeConstraintsTable[1][1] = .030;

            sizeConstraintsTable[2] = new double[2];
            sizeConstraintsTable[2][0] = .030;
            sizeConstraintsTable[2][1] = .065;

            sizeConstraintsTable[3] = new double[2];
            sizeConstraintsTable[3][0] = .065;
            sizeConstraintsTable[3][1] = .091;
            #endregion

            #region ResourceValueTable
            resourceValueTable = new int[19][];
            
            resourceValueTable[0] = new int[2];
            resourceValueTable[0][0] = -5;
            resourceValueTable[0][1] = -3;

            resourceValueTable[1] = new int[2];
            resourceValueTable[1][0] = -5;
            resourceValueTable[1][1] = -3;

            resourceValueTable[2] = new int[2];
            resourceValueTable[2][0] = -5;
            resourceValueTable[2][1] = -2;

            resourceValueTable[3] = new int[2];
            resourceValueTable[3][0] = -4;
            resourceValueTable[3][1] = -2;

            resourceValueTable[4] = new int[2];
            resourceValueTable[4][0] = -3;
            resourceValueTable[4][1] = -1;

            resourceValueTable[5] = new int[2];
            resourceValueTable[5][0] = -2;
            resourceValueTable[5][1] = -1;

            resourceValueTable[6] = new int[2];
            resourceValueTable[6][0] = -2;
            resourceValueTable[6][1] = -1;

            resourceValueTable[7] = new int[2];
            resourceValueTable[7][0] = -1;
            resourceValueTable[7][1] = 0;

            resourceValueTable[8] = new int[2];
            resourceValueTable[8][0] = -1;
            resourceValueTable[8][1] = 0;

            resourceValueTable[9] = new int[2];
            resourceValueTable[9][0] = 0;
            resourceValueTable[9][1] = 0;

            resourceValueTable[10] = new int[2];
            resourceValueTable[10][0] = 0;
            resourceValueTable[10][1] = 0;

            resourceValueTable[11] = new int[2];
            resourceValueTable[11][0] = 1;
            resourceValueTable[11][1] = 0;

            resourceValueTable[12] = new int[2];
            resourceValueTable[12][0] = 1;
            resourceValueTable[12][1] = 0;

            resourceValueTable[13] = new int[2];
            resourceValueTable[13][0] = 2;
            resourceValueTable[13][1] = 1;

            resourceValueTable[14] = new int[2];
            resourceValueTable[14][0] = 2;
            resourceValueTable[14][1] = 1;

            resourceValueTable[15] = new int[2];
            resourceValueTable[15][0] = 3;
            resourceValueTable[15][1] = 1;

            resourceValueTable[16] = new int[2];
            resourceValueTable[16][0] = 4;
            resourceValueTable[16][1] = 2;

            resourceValueTable[17] = new int[2];
            resourceValueTable[17][0] = 5;
            resourceValueTable[17][1] = 2;

            resourceValueTable[18] = new int[2];
            resourceValueTable[18][0] = 5;
            resourceValueTable[18][1] = 3;

            #endregion
        }
        
        //*******************************************************************************************
        // Planetary Basic Genereation Methods
        //*******************************************************************************************
        
        /// <summary>
        /// This function generates a blackbody temperature for a planet
        /// </summary>
        /// <param name="lumin">Luminosity of the star</param>
        /// <param name="radius">Radius from the star</param>
        /// <returns>The blackbody temperature</returns>
        public static double GetBlackBodyTemp(double lumin, double radius)
        {
            if (radius <= 0)
                throw new Exception("Orbital Radius cannot be 0 or negative.");

            return (278 * Math.Pow(lumin, .25)) / Math.Sqrt(radius);
        }

        /// <summary>
        /// This function generates a total blackbody temperature for many planets.
        /// </summary>
        /// <param name="temps">The list of blackbody temps for the other stars</param>
        /// <returns>The total blackbody temperature</returns>
        public static double GetBlackbodyTempTotal(double[] temps)
        {
            double blackTemp = 0;

            for (int i = 0; i < temps.Length; i++)
            {
                blackTemp = blackTemp + Math.Pow(temps[i], 4);
            }

            return Math.Pow(blackTemp, .25);
        }

        //*******************************************************************************************
        // Planetary Enum Helper Functions
        //*******************************************************************************************

        /// <summary>
        /// This converts a string to the world type enum
        /// </summary>
        /// <param name="baseVal">The raw string</param>
        /// <returns>The parsed enum</returns>
        public static PlanetType ConvertPlanetType(string baseVal)
        {
            if (baseVal == "Asteroid Belt")
                return PlanetType.AsteroidBelt;
            else if (baseVal == "Terrestial")
                return PlanetType.TerrestialPlanet;
            else if (baseVal == "Terrestial Planet")
                return PlanetType.TerrestialPlanet;
            else if (baseVal == "Moon")
                return PlanetType.Moon;
            else if (baseVal == "Gas Giant Planet")
                return PlanetType.GasGiantPlanet;

            return PlanetType.None;
        }

        /// <summary>
        /// This converts a string to the world size enum
        /// </summary>
        /// <param name="baseVal">The raw string</param>
        /// <returns>The parsed enum</returns>
        public static WorldSize ConvertWorldSize(string baseVal)
        {
            if (baseVal == "Tiny")
                return WorldSize.Tiny;
            else if (baseVal == "Small")
                return WorldSize.Small;
            else if (baseVal == "Standard")
                return WorldSize.Standard;
            else if (baseVal == "Medium")
                return WorldSize.Medium;
            else if (baseVal == "Large")
                return WorldSize.Large;

            return WorldSize.None;
        }

        /// <summary>
        /// This function will get a size a certain number away from the current planet size.
        /// </summary>
        /// <param name="workingSize">The size of the world</param>
        /// <param name="offset">The offset</param>
        /// <returns>The world size that far off</returns>
        /// <remarks>This is bounded to not go under Tiny.</remarks>
        public static WorldSize GetWorldSizeDifference(WorldSize workingSize, int offset)
        {
            if (offset == 0)
                return workingSize;
            if (offset == 1)
            {
                switch (workingSize)
                {
                    case WorldSize.Tiny:
                    case WorldSize.Small:
                        return WorldSize.Tiny;
                    case WorldSize.Standard:
                        return WorldSize.Small;
                    case WorldSize.Large:
                        return WorldSize.Standard;
                    default:
                        return WorldSize.None;
                }
            }
            if (offset == 2)
            {
                switch (workingSize)
                {
                    case WorldSize.Tiny:
                    case WorldSize.Small:
                    case WorldSize.Standard:
                        return WorldSize.Tiny;
                    case WorldSize.Large:
                        return WorldSize.Small;
                    default:
                        return WorldSize.None;
                }
            }
            
            return WorldSize.Tiny;            
        }

        /// <summary>
        /// This function returns the offset world type
        /// </summary>
        /// <param name="baseVal">This is the base value</param>
        /// <param name="compVal">This is the value you want the comparison of</param>
        /// <returns>The offset of the second value and the first</returns>
        public static int GetSizeOffset(WorldSize baseVal, WorldSize compVal)
        {
            switch (baseVal)
            {
                case WorldSize.Large:
                    if (compVal == WorldSize.Large) return 0;
                    if (compVal == WorldSize.Standard) return 1;
                    if (compVal == WorldSize.Small) return 2;
                    if (compVal == WorldSize.Tiny) return 3;
                    break;
                case WorldSize.Standard:
                    if (compVal == WorldSize.Large) return -1;
                    if (compVal == WorldSize.Standard) return 0;
                    if (compVal == WorldSize.Small) return 1;
                    if (compVal == WorldSize.Tiny) return 2;
                    break;
                case WorldSize.Small:
                    if (compVal == WorldSize.Large) return -2;
                    if (compVal == WorldSize.Standard) return -1;
                    if (compVal == WorldSize.Small) return 0;
                    if (compVal == WorldSize.Tiny) return 1;
                    break;
                case WorldSize.Tiny:
                    if (compVal == WorldSize.Large) return -3;
                    if (compVal == WorldSize.Standard) return -2;
                    if (compVal == WorldSize.Small) return -1;
                    if (compVal == WorldSize.Tiny) return 0;
                    break;
                default:
                    return 999;
            }

            return -1;
        }

        //*******************************************************************************************
        // Planetary Attribute Generation Methods
        //*******************************************************************************************

        /// <summary>
        /// This generates moons and moonlets for a terrestial planet.
        /// </summary>
        /// <param name="ourDice">Dice object</param>
        /// <param name="currPlanet">Current planet</param>
        /// <param name="primaryDistance">Distance from the parent star</param>
        public static void GenerateTerrestialMoons(Dice ourDice, Planet currPlanet, double primaryDistance)
        {
            int mod = -4;

            if (primaryDistance <= 0.5)
                return;
            if (primaryDistance > .5 && primaryDistance <= .75)
                mod = mod - 3;
            if (primaryDistance > .75 && primaryDistance <= 1.5)
                mod = mod - 1;

            if (currPlanet.worldSize == WorldSize.Tiny) mod = mod - 2;
            if (currPlanet.worldSize == WorldSize.Small) mod = mod - 1;
            if (currPlanet.worldSize == WorldSize.Large) mod = mod + 1;

            int moons = ourDice.rngGTZero(1, 6, mod);
            int roll = 0, worldOffset = 0;
            if (moons > 0)
            {
                for (int i = 0; i < moons; i++)
                {
                    //roll for moons
                    roll = ourDice.gurpsRoll();
                    if (roll <= 11) worldOffset = 3;
                    if (roll > 11 && roll <= 14) worldOffset = 2;
                    if (roll >= 15) worldOffset = 1;

                    currPlanet.AddMoon(MoonType.MajorMoon, 0, 
                                           PlanetReference.GetWorldSizeDifference(currPlanet.worldSize, worldOffset));
                }
            }
            if (moons == 0)
            {
                mod = mod + 2;
                int moonlets = ourDice.rngGTZero(1, 6, mod);
                for (int i = 0; i < moons; i++)
                {
                    currPlanet.AddMoon(MoonType.Moonlet, 0, WorldSize.Insignficant);
                }
            }
        }

        /// <summary>
        /// This is
        /// </summary>
        /// <param name="ourDice"></param>
        /// <param name="currPlanet"></param>
        /// <param name="primaryDistance"></param>
        public static void GenerateGasGiantMoons(Dice ourDice, Planet currPlanet, double primaryDistance)
        {
            int mod = 0;
            if (primaryDistance <= .1)
                mod = -10;
            else if (primaryDistance > .1 && primaryDistance <= .5)
                mod = -8;
            else if (primaryDistance > .5 && primaryDistance <= .75)
                mod = -6;
            else if (primaryDistance > .75 && primaryDistance <= 1.5)
                mod = -3;

            int moons = ourDice.rngGTZero(2, 6, mod);
            if (moons > 0)
            {
                for (int i = 0; i < moons; i++)
                {
                    currPlanet.AddMoon(MoonType.Ringlet, 0, WorldSize.Insignficant);
                }
            }

            mod = 0;

            if (primaryDistance <= .1)
                return;
            else if (primaryDistance > .1 && primaryDistance <= .5)
                mod = -5;
            else if (primaryDistance > .5 && primaryDistance <= .75)
                mod = -4;
            else if (primaryDistance > .75 && primaryDistance <= 1.5)
                mod = -1;

            moons = ourDice.rngGTZero(1, 6, mod);
            if (moons > 0)
            {
                for (int i = 0; i < moons; i++)
                {
                    currPlanet.AddMoon(MoonType.OuterMoon, 0, WorldSize.Insignficant);
                }
            }

            mod = 0;

            if (primaryDistance <= .5)
                return;
            else if (primaryDistance > .5 && primaryDistance <= .75)
                mod = -5;
            else if (primaryDistance > .75 && primaryDistance <= 1.5)
                mod = -4;
            else if (primaryDistance > 1.5 && primaryDistance <= 3)
                mod = mod - 1;

            moons = ourDice.rngGTZero(1, 6, mod);
            int roll = 0, worldOffset = 0;
            if (moons > 0)
            {
                for (int i = 0; i < moons; i++)
                {
                    //roll for moons
                    roll = ourDice.gurpsRoll();
                    if (roll <= 11) worldOffset = 3;
                    if (roll > 11 && roll <= 14) worldOffset = 2;
                    if (roll >= 15) worldOffset = 1;

                    currPlanet.AddMoon(MoonType.MajorMoon, 0,
                                           PlanetReference.GetWorldSizeDifference(WorldSize.Large, worldOffset));
                }
            }

        }

        /// <summary>
        /// This function rolls for a basic atmosphere
        /// </summary>
        /// <param name="ourDice">Dice object</param>
        /// <returns>An atmospheric mass</returns>
        public static double RollAtmosphere(Dice ourDice)
        {
            double variance = .05;
            return (ourDice.gurpsRoll() / 10.0 + ourDice.rollInRange(-1 * variance, variance));
        }
       
        /// <summary>
        /// This determines the biome type of the planet. Requires the blackbody and size to be set
        /// </summary>
        /// <param name="ourDice">The dice object</param>
        /// <param name="currPlanet">The current planet</param>
        /// <param name="parentType">The parent planet type</param>
        /// <param name="primaryMass">The parent mass</param>
        /// <param name="systemAge">The age of the system</param>
        /// <returns>A biome type for the planet.</returns>
        public static WorldType GetWorldType(Dice ourDice, Planet currPlanet, string parentType, 
            double primaryMass, double systemAge)
        {
            switch (currPlanet.worldSize)
            {
                case WorldSize.None:
                    break;
                case WorldSize.Tiny:
                    if (currPlanet.blackbodyTemp >= 140.5) //140.5 +
                        return WorldType.Rock;
                    else // 0 to 140.5
                    {
                        if (parentType == "Gas Giant Planet")
                            return WorldType.IceSulfur;
                        else
                            return WorldType.Ice;
                    }
                case WorldSize.Small:
                    if (currPlanet.blackbodyTemp < 80.5) //0 to 80.5
                        return WorldType.Hadean;
                    else if (currPlanet.blackbodyTemp < 140.5) //80.5 to 140.5
                        return WorldType.Ice;
                    else //140.5+
                        return WorldType.Rock;
                case WorldSize.Standard:
                    if (currPlanet.blackbodyTemp < 80.5) //0 to 80.5
                        return WorldType.Hadean;

                    else if (currPlanet.blackbodyTemp < 150.5) //80.5 to 150.5
                        return WorldType.Ice;

                    else if (currPlanet.blackbodyTemp < 230.5) //150.5 to 230.5
                    {
                        if (primaryMass <= .65)
                            return WorldType.Ammonia;
                        else
                            return WorldType.Ice;
                    }

                    else if (currPlanet.blackbodyTemp < 240) //230.5 to 240
                        return WorldType.Ice;

                    else if (currPlanet.blackbodyTemp < 321) //240 to 321
                    {
                        int roll = ourDice.gurpsRollWithCappedMod(10, (systemAge / .5));
                        if (roll >= 18)
                            return WorldType.Garden;
                        else
                            return WorldType.Ocean;
                    }

                    else if (currPlanet.blackbodyTemp < 500.5) //321 to 500.5
                        return WorldType.Greenhouse;
                    else //500.5+
                        return WorldType.Chthonian;
                case WorldSize.Medium:
                    break;
                case WorldSize.Large:
                    if (currPlanet.blackbodyTemp < 150.5) //0 to 150.5
                        return WorldType.Ice;

                    else if (currPlanet.blackbodyTemp < 230.5) //150.5 to 230.5
                    {
                        if (primaryMass <= .65)
                            return WorldType.Ammonia;
                        else
                            return WorldType.Ice;
                    }

                    else if (currPlanet.blackbodyTemp < 240) //230.5 to 240
                        return WorldType.Ice;

                    else if (currPlanet.blackbodyTemp < 321) //240 to 321
                    {
                        int roll = ourDice.gurpsRollWithCappedMod(5, (systemAge / .5));
                        if (roll >= 18)
                            return WorldType.Garden;
                        else
                            return WorldType.Ocean;
                    }

                    else if (currPlanet.blackbodyTemp < 500.5) //321 to 500.5
                        return WorldType.Greenhouse;
                    else //500.5+
                        return WorldType.Chthonian;
                default:
                    break;
            }

            return WorldType.None;
        }

        /// <summary>
        /// This function generates an atmosphere.
        /// </summary>
        /// <param name="ourDice">The dice object</param>
        /// <param name="currPlanet">The current planet</param>
        public static void GenerateAtmosphere(Dice ourDice, Planet currPlanet)
        {
            currPlanet.atmoMass = 0; // set default mass.
            if (currPlanet.planetType == PlanetType.TerrestialPlanet || currPlanet.planetType == PlanetType.Moon)
            {

                if ((currPlanet.worldSize == WorldSize.Small || currPlanet.worldSize == WorldSize.Standard || currPlanet.worldSize == WorldSize.Large) && currPlanet.biomeType == WorldType.Ice)
                    currPlanet.atmoMass = PlanetReference.RollAtmosphere(ourDice);             
                
                if ((currPlanet.worldSize == WorldSize.Standard || currPlanet.worldSize == WorldSize.Large) && currPlanet.biomeType == WorldType.Ammonia)
                    currPlanet.atmoMass = PlanetReference.RollAtmosphere(ourDice);

                if ((currPlanet.worldSize == WorldSize.Standard || currPlanet.worldSize == WorldSize.Large) && currPlanet.biomeType == WorldType.Ocean)
                    currPlanet.atmoMass = PlanetReference.RollAtmosphere(ourDice);

                if ((currPlanet.worldSize == WorldSize.Large || currPlanet.worldSize == WorldSize.Standard)
                    && currPlanet.biomeType == WorldType.Greenhouse)
                    currPlanet.atmoMass = PlanetReference.RollAtmosphere(ourDice);

                
                if (currPlanet.worldSize == WorldSize.Standard && currPlanet.biomeType == WorldType.Garden)
                {
                    currPlanet.atmoMass = PlanetReference.RollAtmosphere(ourDice);

                    if (!(ourDice.rollUnderGurps(11)))
                        PlanetReference.AddMarginalConditions(currPlanet, ourDice);
                }

                if (currPlanet.worldSize == WorldSize.Large && currPlanet.biomeType == WorldType.Garden)
                {
                    currPlanet.atmoMass = PlanetReference.RollAtmosphere(ourDice);

                    if (!(ourDice.rollUnderGurps(11)))
                        PlanetReference.AddMarginalConditions(currPlanet, ourDice);
                }
            }
        }

        /// <summary>
        /// This generates a marginal atmosphere.
        /// </summary>
        /// <param name="curr">The planet being generated for</param>
        /// <param name="ourDice">The dice object</param>
        public static void AddMarginalConditions(Planet curr, Dice ourDice)
        {
            curr.SetAtmosphereMarginal(true);

            int roll = ourDice.gurpsRoll();
            if (roll == 3 || roll == 4)
            {
                if (ourDice.dblProb() < .99)
                    curr.AddAtmosphericCondition(AtmosphericConditions.Chlorine);
                else
                   curr.AddAtmosphericCondition(AtmosphericConditions.Flourine);
            }

            else if (roll == 5 || roll == 6)
                curr.AddAtmosphericCondition(AtmosphericConditions.SulfurCompounds);
            else if (roll == 7)
                curr.AddAtmosphericCondition(AtmosphericConditions.NitrogenCompounds);
            else if (roll == 8 || roll == 9)
                curr.AddAtmosphericCondition(AtmosphericConditions.OrganicToxins);
            
            else if (roll == 10 || roll == 11)
            {
                curr.AddAtmosphericCondition(AtmosphericConditions.LowOxygen);
                curr.AddAtmosphericCondition(AtmosphericConditions.EffectiveOnePressureClassDown);
            }

            else if (roll == 12 || roll == 13)
                curr.AddAtmosphericCondition(AtmosphericConditions.Pollutants);
            else if (roll == 14)
                curr.AddAtmosphericCondition(AtmosphericConditions.HighCarbonDioxide);

            else if (roll == 15 || roll == 16)
            {
                curr.AddAtmosphericCondition(AtmosphericConditions.HighOxygen);           
                if (ourDice.dblProb() > .81)
                    curr.AddAtmosphericCondition(AtmosphericConditions.FlammabilityOneClassUp);
            }

            else if (roll == 17 || roll == 18)
                curr.AddAtmosphericCondition(AtmosphericConditions.InertGases);
        }

        /// <summary>
        /// This function determines the hydrographic coverage and type for each planet.
        /// </summary>
        /// <param name="ourDice">The dice object</param>
        /// <param name="currPlanet">The current planet</param>
        public static void GenerateHydrographicCoverage(Dice ourDice, Planet currPlanet)
        {
            currPlanet.hydroCoverage = 0;

            if ((currPlanet.worldSize == WorldSize.Small) && (currPlanet.biomeType == WorldType.Ice))
            {
                currPlanet.volatileType = HydroCoverageType.Hydrocarbons;
                currPlanet.hydroCoverage = ourDice.VaryResult(.05, ourDice.MultiplyRNG(1, 6, 2, .1));
            }

            if ((currPlanet.worldSize == WorldSize.Standard || currPlanet.worldSize == WorldSize.Large) &&
                (currPlanet.biomeType == WorldType.Ammonia))
            {
                currPlanet.volatileType = HydroCoverageType.AmmoniaEutectic;
                currPlanet.hydroCoverage = ourDice.VaryResultMax(.05, ourDice.MultiplyRNG(2,6,0,.1),1);
            }

            if ((currPlanet.worldSize == WorldSize.Standard || currPlanet.worldSize == WorldSize.Large) &&
                (currPlanet.biomeType == WorldType.Ice))
            {
                currPlanet.volatileType = HydroCoverageType.WaterIces;
                currPlanet.hydroCoverage = ourDice.VaryResultMin(.05, ourDice.MultiplyRNG(2, 6, -10, .1),0);
            }

            if ((currPlanet.worldSize == WorldSize.Standard || currPlanet.worldSize == WorldSize.Large) &&
                (currPlanet.biomeType == WorldType.Garden || currPlanet.biomeType == WorldType.Ocean))
            {
                currPlanet.volatileType = HydroCoverageType.WaterIces;
                if (currPlanet.worldSize == WorldSize.Standard)
                    currPlanet.hydroCoverage = ourDice.VaryResultMax(.05, ourDice.MultiplyRNG(1, 6, 4, .1), 1);
                else if (currPlanet.worldSize == WorldSize.Large)
                    currPlanet.hydroCoverage = ourDice.VaryResultMax(.05, ourDice.MultiplyRNG(1, 6, 6, .1), 1);

            }

            //this will differentate between a Dry or Wet Greenhouse
            if ((currPlanet.worldSize == WorldSize.Standard || currPlanet.worldSize == WorldSize.Large) &&
                currPlanet.biomeType == WorldType.Greenhouse)
            {
                currPlanet.hydroCoverage = ourDice.VaryResultMin(.05, ourDice.MultiplyRNG(2, 6, -7, .1), 0);
                if (currPlanet.hydroCoverage > 0)
                {
                    if (ourDice.dblProb() > .95)
                        currPlanet.volatileType = HydroCoverageType.ExtremeGreenhouseOcean;
                    else
                        currPlanet.volatileType = HydroCoverageType.GreenhouseOcean;

                    currPlanet.biomeType = WorldType.WetGreenhouse;
                }
                else
                    currPlanet.biomeType = WorldType.DryGreenhouse;
            }

        }
        
        /// <summary>
        /// This function generates the surface temperature.
        /// </summary>
        /// <param name="curr">The current planet</param>
        /// <returns>Surface temperature of the planet (Average)</returns>
        public static double GenerateSurfaceTempFromBlackbody(Planet curr)
        {
            double greenHouseFctr = 0;
            double absorbFctr = 0;

            if (curr.planetType == PlanetType.AsteroidBelt)
                absorbFctr = .97;

            if (curr.worldSize == WorldSize.Tiny)
            {
                if (curr.biomeType == WorldType.Ice) absorbFctr = .86;
                if (curr.biomeType == WorldType.Rock) absorbFctr = .97;
                if (curr.biomeType == WorldType.Sulfur) absorbFctr = .77;
            }

            if (curr.worldSize == WorldSize.Small)
            {
                if (curr.biomeType == WorldType.Hadean) absorbFctr = .67;
                if (curr.biomeType == WorldType.Rock) absorbFctr = .96;
                if (curr.biomeType == WorldType.Ice)
                {
                    absorbFctr = .93;
                    greenHouseFctr = .1;
                }                
            }

            if (curr.worldSize == WorldSize.Standard || curr.worldSize == WorldSize.Large) 
            {
                if (curr.biomeType == WorldType.Hadean) absorbFctr = .67;
                if (curr.biomeType == WorldType.Ammonia)
                {
                    absorbFctr = .84;
                    greenHouseFctr = .2;
                }

                if (curr.biomeType == WorldType.Ice)
                {
                    absorbFctr = .86;
                    greenHouseFctr = .2;
                }

                if (curr.biomeType == WorldType.Ocean || curr.biomeType == WorldType.Garden)
                {
                    greenHouseFctr = .16;
                    
                    if (curr.hydroCoverage <= .2) absorbFctr = .95;
                    if (curr.hydroCoverage > .2 && curr.hydroCoverage <= .5) absorbFctr = .92;
                    if (curr.hydroCoverage > .5 && curr.hydroCoverage <= .9) absorbFctr = .88;
                    if (curr.hydroCoverage > .9) absorbFctr = .84;
                }

                if (curr.IsAGreenhousePlanet())
                {
                    absorbFctr = .77;
                    greenHouseFctr = 2;
                }

                if (curr.biomeType == WorldType.Chthonian)
                {
                    absorbFctr = .97;
                }
            }

            double corrFctr = absorbFctr * (1 + (curr.atmoMass * greenHouseFctr));

            return (curr.blackbodyTemp * corrFctr);
        }

        /// <summary>
        /// This function generates physical properties (gravity, density, mass)
        /// </summary>
        /// <param name="ourDice">Dice object</param>
        /// <param name="currPlanet">Planet being generated for</param>
        public static void GeneratePhysicalProperties(Dice ourDice, Planet currPlanet)
        {
            int densityRow = -1, densityCol = -1;
            
            //Let's get the density.
            if (currPlanet.worldSize == WorldSize.Tiny)
            {
                if (currPlanet.biomeType == WorldType.Ice) densityCol = 0;
                else if (currPlanet.biomeType == WorldType.Rock) densityCol = 1;
                else if (currPlanet.biomeType == WorldType.Sulfur) densityCol = 0;
            }

            if (currPlanet.worldSize == WorldSize.Small)
            {
                if (currPlanet.biomeType == WorldType.Hadean) densityCol = 0;
                if (currPlanet.biomeType == WorldType.Ice) densityCol = 0;
                if (currPlanet.biomeType == WorldType.Rock) densityCol = 1;
            }
            if (currPlanet.worldSize == WorldSize.Standard)
            {
                if (currPlanet.biomeType == WorldType.Hadean) densityCol = 0;
                if (currPlanet.biomeType == WorldType.Ammonia) densityCol = 0;
            }

            if (currPlanet.worldSize == WorldSize.Large && currPlanet.biomeType == WorldType.Ammonia)
                densityCol = 0;

            //If this hasn't been caught before, it's 2.
            if (densityCol == -1)
                densityCol = 2;

            switch (ourDice.gurpsRoll())
            {
                case 3:
                case 4:
                case 5:
                case 6:
                    densityRow = 0;
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                    densityRow = 1;
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                    densityRow = 2;
                    break;
                case 15:
                case 16:
                case 17:
                    densityRow = 3;
                    break;
                case 18:
                    densityRow = 4;
                    break;
            }

            currPlanet.worldDensity = ourDice.VaryResult(.05, worldDensityTable[densityRow][densityCol]);

            //Now it's time to get the diameter.
            double rawDiam = Math.Sqrt(currPlanet.blackbodyTemp / currPlanet.worldDensity);

            int sizeRow = -1;
            switch (currPlanet.worldSize)
            {
                case WorldSize.Tiny:
                    sizeRow = 0;
                    break;
                case WorldSize.Small:
                    sizeRow = 1;
                    break;
                case WorldSize.Standard:
                    sizeRow = 2;
                    break;
                case WorldSize.Large:
                    sizeRow = 3;
                    break;
            }

            currPlanet.worldDiameter = ourDice.rollInRange(rawDiam * sizeConstraintsTable[sizeRow][0],
                                                           rawDiam * sizeConstraintsTable[sizeRow][1]);
            currPlanet.worldGravity = currPlanet.worldDensity * currPlanet.worldDiameter;

            currPlanet.worldMass = currPlanet.worldDensity * Math.Pow(currPlanet.worldDiameter, 3);
        }

        /// <summary>
        /// This function generates the gas giant properties, since there are only three
        /// </summary>
        /// <param name="ourDice">Dice object</param>
        /// <param name="currPlanet">Current planet</param>
        public static void GenerateGasGiantProperties(Dice ourDice, Planet currPlanet)
        {
            if (currPlanet.worldSize == WorldSize.Standard)
                currPlanet.worldSize = WorldSize.Medium;
            if (currPlanet.worldSize == WorldSize.Tiny)
                currPlanet.worldSize = WorldSize.Small;
            int roll = ourDice.gurpsRoll();

            switch (currPlanet.worldSize)
            {
                case WorldSize.Small:
                    switch (roll)
                    {
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            currPlanet.worldMass = 10;
                            currPlanet.worldDensity = .42;
                            break;
                        case 9:
                        case 10:
                            currPlanet.worldMass = 15;
                            currPlanet.worldDensity = .26;
                            break;
                        case 11:
                            currPlanet.worldMass = 20;
                            currPlanet.worldDensity = .22;
                            break;
                        case 12:
                            currPlanet.worldMass = 30;
                            currPlanet.worldDensity = .19;
                            break;
                        case 13:
                            currPlanet.worldMass = 40;
                            currPlanet.worldDensity = .17;
                            break;
                        case 14:
                            currPlanet.worldMass = 50;
                            currPlanet.worldDensity = .17;
                            break;
                        case 15:
                            currPlanet.worldMass = 60;
                            currPlanet.worldDensity = .17;
                            break;
                        case 16:
                            currPlanet.worldMass = 70;
                            currPlanet.worldDensity = .17;
                            break;
                        case 17:
                        case 18:
                            currPlanet.worldMass = 80;
                            currPlanet.worldDensity = .17;
                            break;
                    }
                    break;
                case WorldSize.Medium:
                    switch (roll)
                    {
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            currPlanet.worldMass = 100;
                            currPlanet.worldDensity = .18;
                            break;
                        case 9:
                        case 10:
                            currPlanet.worldMass = 150;
                            currPlanet.worldDensity = .19;
                            break;
                        case 11:
                            currPlanet.worldMass = 200;
                            currPlanet.worldDensity = .20;
                            break;
                        case 12:
                            currPlanet.worldMass = 250;
                            currPlanet.worldDensity = .22;
                            break;
                        case 13:
                            currPlanet.worldMass = 300;
                            currPlanet.worldDensity = .24;
                            break;
                        case 14:
                            currPlanet.worldMass = 350;
                            currPlanet.worldDensity = .25;
                            break;
                        case 15:
                            currPlanet.worldMass = 400;
                            currPlanet.worldDensity = .26;
                            break;
                        case 16:
                            currPlanet.worldMass = 450;
                            currPlanet.worldDensity = .27;
                            break;
                        case 17:
                        case 18:
                            currPlanet.worldMass = 500;
                            currPlanet.worldDensity = .29;
                            break;
                    }
                    break;
                case WorldSize.Large:
                    switch (roll)
                    {
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            currPlanet.worldMass = 600;
                            currPlanet.worldDensity = .31;
                            break;
                        case 9:
                        case 10:
                            currPlanet.worldMass = 800;
                            currPlanet.worldDensity = .35;
                            break;
                        case 11:
                            currPlanet.worldMass = 1000;
                            currPlanet.worldDensity = .40;
                            break;
                        case 12:
                            currPlanet.worldMass = 1500;
                            currPlanet.worldDensity = .60;
                            break;
                        case 13:
                            currPlanet.worldMass = 2000;
                            currPlanet.worldDensity = .8;
                            break;
                        case 14:
                            currPlanet.worldMass = 2500;
                            currPlanet.worldDensity = 1.0;
                            break;
                        case 15:
                            currPlanet.worldMass = 3000;
                            currPlanet.worldDensity = 1.2;
                            break;
                        case 16:
                            currPlanet.worldMass = 3500;
                            currPlanet.worldDensity = 1.4;
                            break;
                        case 17:
                        case 18:
                            currPlanet.worldMass = 4000;
                            currPlanet.worldDensity = 1.6;
                            break;
                    }
                    break;
            }

            currPlanet.worldDiameter = Math.Pow((currPlanet.worldMass / currPlanet.worldDensity), (1 / 3));
        }
        
        /// <summary>
        /// This generates the dynamic parameters
        /// </summary>
        /// <param name="ourDice">Dice object</param>
        /// <param name="currPlanet">Current planet</param>
        /// <param name="parentMass">The mass of the parent star</param>
        /// <param name="genMoons">Flag if we are generating moons</param>
        /// <param name="systemAge">The age of the system</param>
        public static void GenerateDynamicParameters(Dice ourDice, Planet currPlanet, double parentMass, bool genMoons,
                                                     double systemAge)
        {
            currPlanet.orbitalPeriod = Math.Sqrt(Math.Pow(currPlanet.GetOrbitalDistanceToPrimary(), 3.0) / 
                (parentMass + (.000003 * currPlanet.worldMass)));

            ///add eccentricity
            int roll = ourDice.gurpsRoll();
            switch (roll)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                    currPlanet.eccentricity = 0;
                    break;
                case 4:
                case 5:
                case 6:
                    currPlanet.eccentricity = 0.05;
                    break;
                case 7:
                case 8:
                case 9:
                    currPlanet.eccentricity = 0.1;
                    break;
                case 10:
                case 11:
                    currPlanet.eccentricity = 0.15;
                    break;
                case 12:
                    currPlanet.eccentricity = 0.2;
                    break;
                case 13:
                    currPlanet.eccentricity = 0.3;
                    break;
                case 14:
                    currPlanet.eccentricity = 0.4;
                    break;
                case 15:
                    currPlanet.eccentricity = 0.5;
                    break;
                case 16:
                    currPlanet.eccentricity = 0.6;
                    break;
                case 17:
                    currPlanet.eccentricity = 0.7;
                    break;
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                    currPlanet.eccentricity = 0.8;
                    break;
            }
            

            //tidal forces SKIPPED.
            roll = ourDice.gurpsRoll();
            bool slowRot = false, days = false;

            if (roll >= 16) slowRot = true;
            if ((currPlanet.planetType == PlanetType.GasGiantPlanet && currPlanet.worldSize == WorldSize.Small) ||
               (currPlanet.planetType == PlanetType.TerrestialPlanet && currPlanet.worldSize == WorldSize.Large))
                roll = roll + 6;
            
            if (currPlanet.planetType == PlanetType.GasGiantPlanet && currPlanet.worldSize == WorldSize.Standard)
                roll = roll + 10;

            if (currPlanet.planetType == PlanetType.GasGiantPlanet && currPlanet.worldSize == WorldSize.Small)
                roll = roll + 14;

            if (currPlanet.planetType == PlanetType.GasGiantPlanet && currPlanet.worldSize == WorldSize.Tiny)
                roll = roll + 18;

            if (roll > 36) slowRot = true;

            if (slowRot)
            {
                switch (ourDice.rng(2,6,0))
                {
                    //no changes if below 6. Probablity range 2-12
                    case 7:
                        days = true;
                        roll = roll * 2;
                        break;
                    case 8:
                        days = true;
                        roll = roll * 5;
                        break;
                    case 9:
                        days = true;
                        roll = roll * 10;
                        break;
                    case 10:
                        days = true;
                        roll = roll * 20;
                        break;
                    case 11:
                        days = true;
                        roll = roll * 50;
                        break;
                    case 12:
                        days = true;
                        roll = roll * 100;
                        break;
                }
            }

            if ((PlanetReference.GetPeriodInDays(currPlanet.orbitalPeriod) < roll) && days)
            {
                currPlanet.IsTidalLocked = true;
            }

            if (days)
                currPlanet.siderealPeriod = roll;
            else
                currPlanet.siderealPeriod = roll / 24.0;

            if (ourDice.gurpsRoll() >= 13)
                currPlanet.IsRetrograde = true;

            if (currPlanet.orbitalPeriod != currPlanet.siderealPeriod)
                currPlanet.dayLength = (currPlanet.orbitalPeriod * currPlanet.siderealPeriod) / (currPlanet.orbitalPeriod - currPlanet.siderealPeriod);
            else
                currPlanet.dayLength = Double.PositiveInfinity;
            //not displaying moon orbital cycle

            //axial tilt
            PlanetReference.GenerateAxialTilt(ourDice, currPlanet);
            
            //geologic activity. THE END IS IN SIGHT! 
            //volcanic activity
            roll = ourDice.gurpsRoll();
            roll = roll + (int)Math.Round((currPlanet.worldGravity / systemAge) * 40.0);
            if (currPlanet.IsTerrestialPlanet() && (currPlanet.MajorMoonCount() > 1))
                roll = roll + 10;
            
            if (currPlanet.biomeType == WorldType.Sulfur) // || currPlanet.biomeType == WorldType.IceSulfur)
                roll = roll + 60;

            //skpping +5 for major moon of gas giant. No way to track now, also because moon code is sparse
            //this should be noted in the output as well.

            if (roll <= 16) currPlanet.volcanicActivity = GeologicActivity.None;
            if (roll >= 17 && roll <= 20) currPlanet.volcanicActivity = GeologicActivity.Light;
            if (roll >= 21 && roll <= 26) currPlanet.volcanicActivity = GeologicActivity.Moderate;
            if (roll >= 27 && roll <= 70) currPlanet.volcanicActivity = GeologicActivity.Heavy;
            if (roll >= 71) currPlanet.volcanicActivity = GeologicActivity.Extreme;

            //tectonic activity
            roll = ourDice.gurpsRoll();
            if (currPlanet.volcanicActivity == GeologicActivity.None)
                roll = roll - 8;
            if (currPlanet.volcanicActivity == GeologicActivity.Light)
                roll = roll - 4;
            if (currPlanet.volcanicActivity == GeologicActivity.Heavy)
                roll = roll + 4;
            if (currPlanet.volcanicActivity == GeologicActivity.Extreme)
                roll = roll + 8;

            if (currPlanet.hydroCoverage == 0)
                roll = roll - 4;
            else if (currPlanet.hydroCoverage > 0 && currPlanet.hydroCoverage <= .5)
                roll = roll - 2;

            if (currPlanet.IsTerrestialPlanet() && (currPlanet.MajorMoonCount() == 1))
                roll = roll + 2;
            if (currPlanet.IsTerrestialPlanet() && (currPlanet.MajorMoonCount() > 1))
                roll = roll + 4;

            if (roll <= 6) currPlanet.tectonicActivity = GeologicActivity.None;
            if (roll >= 7 && roll <= 10) currPlanet.tectonicActivity = GeologicActivity.Light;
            if (roll >= 11 && roll <= 14) currPlanet.tectonicActivity = GeologicActivity.Moderate;
            if (roll >= 15 && roll <= 18) currPlanet.tectonicActivity = GeologicActivity.Heavy;
            if (roll >= 19) currPlanet.tectonicActivity = GeologicActivity.Extreme;

            if (currPlanet.worldSize == WorldSize.Tiny || currPlanet.worldSize == WorldSize.Small)
                currPlanet.tectonicActivity = GeologicActivity.None;

            //now generate RVM, Habitability, Affinity
            PlanetReference.GenerateResourceValue(ourDice, currPlanet);
        }
        
        /// <summary>
        /// This generates the axial tilt for a world.
        /// </summary>
        /// <param name="ourDice">Dice object</param>
        /// <param name="currPlanet">The current planet we are working with</param>
        public static void GenerateAxialTilt(Dice ourDice, Planet currPlanet)
        {
            int roll = ourDice.gurpsRoll();
            switch (roll)
            {
                case 3:
                case 4:
                case 5:
                case 6:
                    currPlanet.axialTilt = ourDice.rngGTZero(2, 6, -2);
                    return;
                case 7:
                case 8:
                case 9:
                    currPlanet.axialTilt = 10 + ourDice.rngGTZero(2, 6, -2);
                    return;
                case 10:
                case 11:
                case 12:
                    currPlanet.axialTilt = 20 + ourDice.rngGTZero(2, 6, -2);
                    return;
                case 13:
                case 14:
                    currPlanet.axialTilt = 30 + ourDice.rngGTZero(2, 6, -2);
                    return;
                case 15:
                case 16:
                    currPlanet.axialTilt = 40 + ourDice.rngGTZero(2, 6, -2);
                    return;
                case 17:
                case 18:
                    switch (ourDice.rng(6))
                    {
                        case 1:
                        case 2:
                            currPlanet.axialTilt = 50 + ourDice.rngGTZero(2, 6, -2);
                            return;
                        case 3:
                        case 4:
                            currPlanet.axialTilt = 60 + ourDice.rngGTZero(2, 6, -2);
                            return;
                        case 5:
                            currPlanet.axialTilt = 70 + ourDice.rngGTZero(2, 6, -2);
                            return;
                        case 6:
                            currPlanet.axialTilt = 80 + ourDice.rngGTZero(2, 6, -2);
                            return;
                    }
                    return;
            }
        }
        
        /// <summary>
        /// This function calculates the orbits of moons around planets.
        /// </summary>
        /// <param name="ourDice">The dice object</param>
        /// <param name="currPlanet">The current planet we're working with</param>
        public static void PlaceMoons(Dice ourDice, Planet currPlanet)
        {
            double temp = 0;
            if (currPlanet.IsGasGiant())
            {
                for (int i = 0; i < currPlanet.PlanetaryMoons.Count(); i++)
                {
                    if (currPlanet.PlanetaryMoons[i].moon == MoonType.Ringlet)
                        currPlanet.PlanetaryMoons[i].orbitalRadius = ourDice.rng(1, 6, 4) / 4;

                    if (currPlanet.PlanetaryMoons[i].moon == MoonType.MajorMoon){
                        do
                        {
                            temp = ourDice.rng(3, 6, 3);
                            if (temp >= 15) temp = temp + ourDice.rng(2, 6, 0);
                            temp = temp / 2;
                        } while (currPlanet.CheckLunarOrbitalPresent(temp, 1));

                        currPlanet.PlanetaryMoons[i].orbitalRadius = temp;
                    }

                    if (currPlanet.PlanetaryMoons[i].moon == MoonType.OuterMoon)
                        do {
                            temp = ourDice.rollInRange(25, 300);
                        } while (currPlanet.CheckLunarOrbitalPresent(temp, 1));

                    currPlanet.PlanetaryMoons[i].orbitalRadius = temp;
                }
            }
            if (currPlanet.IsTerrestialPlanet())
            {
                int mods = 0;
                for (int i = 0; i < currPlanet.PlanetaryMoons.Count(); i++)
                {
                    if (currPlanet.PlanetaryMoons[i].moon == MoonType.MajorMoon)
                    {
                        if (PlanetReference.GetSizeOffset(currPlanet.worldSize,
                                                         currPlanet.PlanetaryMoons[i].moonSize) == 2)
                            mods = 2;
                        if (PlanetReference.GetSizeOffset(currPlanet.worldSize,
                                                         currPlanet.PlanetaryMoons[i].moonSize) == 1)
                            mods = 4;
                        do
                        {
                            temp = ourDice.rng(2, 6, mods);
                        } while (currPlanet.CheckLunarOrbitalPresent(temp, 5));

                        currPlanet.PlanetaryMoons[i].orbitalRadius = temp;
                    }

                    if (currPlanet.PlanetaryMoons[i].moon == MoonType.Moonlet)
                        currPlanet.PlanetaryMoons[i].orbitalRadius = ourDice.rng(1, 6, 4) / 4;

                }
            }

        }

        /// <summary>
        /// This function generates the atmospheric pressure
        /// </summary>
        /// <param name="currPlanet">The planet we're working with</param>
        public static void GenerateAtmosphericPressure(Planet currPlanet)
        {
            double pressureFactor = 0;
            if (currPlanet.worldSize == WorldSize.Small && currPlanet.biomeType == WorldType.Ice)
                pressureFactor = 10;
            if (currPlanet.worldSize == WorldSize.Standard && (
                (currPlanet.biomeType == WorldType.Ice) || (currPlanet.biomeType == WorldType.Ocean) ||
                (currPlanet.biomeType == WorldType.Ammonia) || (currPlanet.biomeType == WorldType.Garden)))
                pressureFactor = 1;
            if (currPlanet.IsAGreenhousePlanet() && currPlanet.worldSize == WorldSize.Standard)
                pressureFactor = 100;
            if (currPlanet.worldSize == WorldSize.Large && (
                (currPlanet.biomeType == WorldType.Ice) || (currPlanet.biomeType == WorldType.Ocean) ||
                (currPlanet.biomeType == WorldType.Ammonia) || (currPlanet.biomeType == WorldType.Garden)))
                pressureFactor = 5;
            if (currPlanet.IsAGreenhousePlanet() && currPlanet.worldSize == WorldSize.Standard)
                pressureFactor = 500;

            currPlanet.atmoPressure = currPlanet.atmoMass * currPlanet.worldGravity * pressureFactor;
            
            if ((currPlanet.worldSize == WorldSize.Small && currPlanet.biomeType == WorldType.Rock) ||
               ((currPlanet.worldSize == WorldSize.Standard || currPlanet.worldSize == WorldSize.Large) &&
                (currPlanet.biomeType == WorldType.Chthonian)))
                currPlanet.atmoPressure = .005;


        }

        //TODO: COME BACK AND MOD ROLL
        
        /// <summary>
        /// This generates a resource value for a planet or asteroid belt
        /// </summary>
        /// <param name="ourDice">Dice object</param>
        /// <param name="currPlanet">Current planet</param>
        public static void GenerateResourceValue(Dice ourDice, Planet currPlanet)
        {
            int mod = 0;
            if (currPlanet.volcanicActivity == GeologicActivity.None)
                mod = -2;
            else if (currPlanet.volcanicActivity == GeologicActivity.Light)
                mod = -1;
            else if (currPlanet.volcanicActivity == GeologicActivity.Heavy)
                mod = 1;
            else if (currPlanet.volcanicActivity == GeologicActivity.Extreme)
                mod = 2;

            int roll = ourDice.gurpsRoll(mod);
            if (roll > 19) roll = 19;
            if (roll < 1) roll = 1;

            if (currPlanet.planetType == PlanetType.GasGiantPlanet)
                currPlanet.resourceValue = 0;
            if (currPlanet.planetType == PlanetType.Moon || currPlanet.planetType == PlanetType.TerrestialPlanet)
                currPlanet.resourceValue = resourceValueTable[roll - 1][1];
            else
                currPlanet.resourceValue = resourceValueTable[roll - 1][0];

        }

        /// <summary>
        /// This function generates the habitability modifier for a planet.
        /// </summary>
        /// <param name="currPlanet">Planet generating for.</param>
        /// <returns>The habitability modifier</returns>
        public static int GenerateHabitabilityModifer(Planet currPlanet)
        {
            int mod = 0;
            if (!currPlanet.IsGasGiant()) 
            {
                if (currPlanet.IsAtmospherePresent())
                {
                    if (currPlanet.IsAtmosphereSuffocating())
                    {
                        if (currPlanet.IsAtmosphereToxic())
                        {
                            if (currPlanet.IsAtmosphereCorrosive())
                                mod = mod - 2;
                            else
                                mod = mod - 1;
                        }
                    }
                    else
                    {
                        //breathable!
                        if (!currPlanet.IsMarginal())
                            mod = mod + 1;

                        if (PlanetReference.GetPressureCategory(currPlanet) == PressureCategory.VeryThin ||
                            PlanetReference.GetPressureCategory(currPlanet) == PressureCategory.VeryDense ||
                            PlanetReference.GetPressureCategory(currPlanet) == PressureCategory.Superdense)
                            mod = mod + 1;
                        if (PlanetReference.GetPressureCategory(currPlanet) == PressureCategory.Thin)
                            mod = mod + 2;
                        if (PlanetReference.GetPressureCategory(currPlanet) == PressureCategory.Standard ||
                            PlanetReference.GetPressureCategory(currPlanet) == PressureCategory.Dense)
                            mod = mod + 3;

                        //now we look at hydrographic coverage.
                        if (currPlanet.hydroCoverage > 0 && currPlanet.hydroCoverage < .6)
                            mod = mod + 1;

                        if (currPlanet.hydroCoverage >= .6 && currPlanet.hydroCoverage < .9)
                            mod = mod + 2;

                        if (currPlanet.hydroCoverage >= .9 && currPlanet.hydroCoverage < 1)
                            mod = mod + 1;

                        //climate!
                        if (currPlanet.surfaceTemp >= 267 && currPlanet.surfaceTemp < 278)
                            mod = mod + 1;

                        if (currPlanet.surfaceTemp >= 278 && currPlanet.surfaceTemp < 322)
                            mod = mod + 2;

                        if (currPlanet.surfaceTemp >= 322 && currPlanet.surfaceTemp < 333)
                            mod = mod + 1;
                    }
                }
            }

            //Activity modifiers.
            if (currPlanet.volcanicActivity == GeologicActivity.Heavy)
                mod = mod - 1;
            if (currPlanet.tectonicActivity == GeologicActivity.Heavy)
                mod = mod - 1;
            if (currPlanet.tectonicActivity == GeologicActivity.Extreme)
                mod = mod - 2;
            if (currPlanet.volcanicActivity == GeologicActivity.Extreme)
                mod = mod - 2;

            if (mod <= -2) return -2;
            else if (mod >= 8) return 8;
            
            return mod;
        }

        /// <summary>
        /// This generates the affinity modifier.
        /// </summary>
        /// <param name="currPlanet">The planet we are generating for</param>
        /// <returns>The affinity modifier</returns>
        public static int GenerateAffinityModifier(Planet currPlanet)
        {
            int rawAffinity = currPlanet.resourceValue + PlanetReference.GenerateHabitabilityModifer(currPlanet);
            if (rawAffinity > 10) return 10;
            if (rawAffinity < -5) return -5;
            return rawAffinity;
        }

        //*******************************************************************************************
        // Planetary Description Methods
        //*******************************************************************************************

        /// <summary>
        /// This function gets the minimum seperation (the point of Periastron)
        /// </summary>
        /// <param name="radius">The radius of the orbit</param>
        /// <param name="eccentricity">The eccentricity of the orbit</param>
        /// <returns>The distance to point of periastron</returns>
        public static double GetPeriastron(double radius, double eccentricity)
        {
            return (1 - eccentricity) * radius;
        }
    
        /// <summary>
        /// This function gets the maximum seperation (the point of Apastron)
        /// </summary>
        /// <param name="radius">The radius of the orbit</param>
        /// <param name="eccentricity">The eccentricity of the orbit</param>
        /// <returns>The distance to point of apastrion</returns>
        public static double GetApastron(double radius, double eccentricity)
        {
            return (1 + eccentricity) * radius;
        }

        /// <summary>
        /// This converts a period into days from years
        /// </summary>
        /// <param name="period">Period in years</param>
        /// <returns>Period in days</returns>
        public static double GetPeriodInDays(double period)
        {
            return period * 365.26;
        }

        /// <summary>
        /// This function returns a description of the hydrographic coverage.
        /// </summary>
        /// <param name="volatileType">The volatile type of the world</param>
        /// <returns>The hydrographic coverage</returns>
        public static string DescribeHydrographicCoverage(HydroCoverageType volatileType)
        {
            string desc = "";

            if (volatileType == HydroCoverageType.AcidTainted)
                desc = "Water tainted with acid forming a weak acidic solution in seas and standing water.";
            else if (volatileType == HydroCoverageType.AmmoniaEutectic)
                desc = "Liquid volatiles on this world are ammonia and water mix with other compounds, lowered beyond the normal freezing point for either water or ammonia.";
            else if (volatileType == HydroCoverageType.ExtremeGreenhouseOcean)
                desc = "Any oceans or seas are composed of sulfuric acid.";
            else if (volatileType == HydroCoverageType.GreenhouseOcean)
                desc = "Very heavy water, with a mass of dissolved carbon and sulfur compounds.";
            else if (volatileType == HydroCoverageType.Hydrocarbons)
                desc = "Any liquid volatiles on this world are liquid hydrocarbons due to the low blackbody temperature.";
            else if (volatileType == HydroCoverageType.None)
                desc = "No liquid volatiles on this world";
            else if (volatileType == HydroCoverageType.SulfuricAcid)
                desc = "Any oceans or seas are composed of sulfuric acid.";
            else if (volatileType == HydroCoverageType.WaterIces)
                desc = "Liquid volatiles on this world are water or ice";
            return desc;
        }

        /// <summary>
        /// This function returns the climate info
        /// </summary>
        /// <param name="surfaceTemp">The surface temperature of the world.</param>
        /// <param name="returnTemp">Return the temperature as well. (default: false)</param>
        /// <param name="farenheit">A bool describing if it's in farenheit (default: true)</param>
        /// <returns>A string naming the climate</returns>
        public static string GetClimateName(double surfaceTemp, bool returnTemp = false, bool farenheit = true)
        {
            //get temperature. Surface Temp is stored in Kelvin.
            double temp = 0;
            string climate = "";
            if (farenheit)
                temp = (1.8 * surfaceTemp) - 457.87;
            else
                temp = surfaceTemp - 272.15;

            if (surfaceTemp < 244)
                climate = "Frozen";
            else if (surfaceTemp >= 244 && surfaceTemp < 255)
                climate = "Very Cold";
            else if (surfaceTemp >= 255 && surfaceTemp < 266)
                climate = "Cold";
            else if (surfaceTemp >= 266 && surfaceTemp < 278)
                climate = "Cool";
            else if (surfaceTemp >= 278 && surfaceTemp < 289)
                climate = "Chilly";
            else if (surfaceTemp >= 289 && surfaceTemp < 300)
                climate = "Normal";
            else if (surfaceTemp >= 300 && surfaceTemp < 311)
                climate = "Warm";
            else if (surfaceTemp >= 311 && surfaceTemp < 322)
                climate = "Tropical";
            else if (surfaceTemp >= 322 && surfaceTemp < 333)
                climate = "Hot";
            else if (surfaceTemp >= 333 && surfaceTemp < 344)
                climate = "Very Hot";
            else if (surfaceTemp >= 344)
                climate = "Infernal";


            if (returnTemp)
            {
                climate = climate + " (" + temp.ToString("F2"); //String.Format("F2", temp);
                if (farenheit)
                    climate = climate + "F)";
                else
                    climate = climate + "C)";
            }

            return climate;
        }

        /// <summary>
        /// This will get the atmospheric category. 
        /// </summary>
        /// <param name="currPlanet">The planet for the atmosphere</param>
        /// <returns>String describing the atmospheric category</returns>
        public static string DescribeAtmosphereCategory(Planet currPlanet)
        { 
            string desc = "";
            int mod = 0, category = -1;
            string[] atmoCategories = new string[] {"Trace", "Very Thin", "Thin", "Standard", "Dense", 
                                                    "Very Dense", "Superdense"};

            if (currPlanet.ContainsCondition(AtmosphericConditions.EffectiveOnePressureClassDown))
                mod = -1;

            if (currPlanet.atmoPressure < 0.01)
                category = 0;
            else if (currPlanet.atmoPressure >= .01 && currPlanet.atmoPressure <= .5)
                category = 1;
            else if (currPlanet.atmoPressure > .5 && currPlanet.atmoPressure <= .8)
                category = 2;
            else if (currPlanet.atmoPressure > .8 && currPlanet.atmoPressure <= 1.2)
                category = 3;
            else if (currPlanet.atmoPressure > 1.2 && currPlanet.atmoPressure <= 1.5)
                category = 4;
            else if (currPlanet.atmoPressure > 1.5 && currPlanet.atmoPressure <= 10)
                category = 5;
            else if (currPlanet.atmoPressure > 10)
                category = 6;

            if (mod != 0)
            {
                category = category + mod;
                if (category < 0) category = 0;
                desc = "Effective ";
            }

            desc = desc + atmoCategories[category] + " (" + Math.Round(currPlanet.atmoPressure, 3) + " atm)";
            
            return desc;
        }

        /// <summary>
        /// Returns the pressure category.
        /// </summary>
        /// <param name="currPlanet">Planet containing the atmosphere</param>
        /// <returns>The pressure category</returns>
        public static PressureCategory GetPressureCategory(Planet currPlanet)
        {
            if (currPlanet.atmoPressure < 0.01)
                return PressureCategory.Trace;
            else if (currPlanet.atmoPressure >= .01 && currPlanet.atmoPressure <= .5)
                return PressureCategory.VeryThin;
            else if (currPlanet.atmoPressure > .5 && currPlanet.atmoPressure <= .8)
                return PressureCategory.Thin;
            else if (currPlanet.atmoPressure > .8 && currPlanet.atmoPressure <= 1.2)
                return PressureCategory.Standard;
            else if (currPlanet.atmoPressure > 1.2 && currPlanet.atmoPressure <= 1.5)
                return PressureCategory.Dense;
            else if (currPlanet.atmoPressure > 1.5 && currPlanet.atmoPressure <= 10)
                return PressureCategory.VeryDense;
            else if (currPlanet.atmoPressure > 10)
                return PressureCategory.Superdense;

            return PressureCategory.None;

        }

        /// <summary>
        /// This function describes the resource modifier given the planet.
        /// </summary>
        /// <param name="currPlanet">The planet being used</param>
        /// <returns>A string describing the resource modifier</returns>
        public static string DescribeResourceModifier(Planet currPlanet)
        {
            if (currPlanet.resourceValue == -5)
                return "Worthless";
            else if (currPlanet.resourceValue == -4)
                return "Very Scant";
            else if (currPlanet.resourceValue == -3)
                return "Scant";
            else if (currPlanet.resourceValue == -2)
                return "Very Poor";
            else if (currPlanet.resourceValue == -1)
                return "Poor";
            else if (currPlanet.resourceValue == 0)
                return "Average";
            else if (currPlanet.resourceValue == 1)
                return "Abundant";
            else if (currPlanet.resourceValue == 2)
                return "Very Abundant";
            else if (currPlanet.resourceValue == 3)
                return "Rich";
            else if (currPlanet.resourceValue == 4)
                return "Very Rich";
            else if (currPlanet.resourceValue == 5)
                return "Motherlode";

            return "Conundrum";
        }
    }
}
