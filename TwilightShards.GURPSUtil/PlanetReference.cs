using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwilightShards.genLibrary;

namespace TwilightShards.GURPSUtil
{
    /// <summary>
    /// This contains the refrence for generating stars.
    /// </summary>
    public static class PlanetReference
    {

        //*******************************************************************************************
        // Planetary Attribute Generation Methods
        //*******************************************************************************************
        
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
            //apply the rule that the diff is split.
            //Now for tiny worlds.
            if (currPlanet.worldSize == WorldSize.Tiny)
            {
                if (currPlanet.blackbodyTemp >= 140.5)
                    return WorldType.Rock;
                else
                {
                    if (parentType == "Gas Giant Planet")
                        return WorldType.IceSulfur;
                    else
                        return WorldType.Ice;
                }
            }

            //Now for small worlds.
            if (currPlanet.worldSize == WorldSize.Small)
            {
                if (currPlanet.blackbodyTemp < 80.5)
                    return WorldType.Hadean;
                else if (currPlanet.blackbodyTemp >= 80.5 && currPlanet.blackbodyTemp < 140.5)
                    return WorldType.Ice;
                else if (currPlanet.blackbodyTemp > 140.5)
                    return WorldType.Rock;
            }

            //now for standard worlds
            if (currPlanet.worldSize == WorldSize.Standard)
            {
                if (currPlanet.blackbodyTemp < 80.5)
                    return WorldType.Hadean;

                else if (currPlanet.blackbodyTemp >= 80.5 && currPlanet.blackbodyTemp < 150.5)
                    return WorldType.Ice;

                else if (currPlanet.blackbodyTemp >= 150.5 && currPlanet.blackbodyTemp < 230.5)
                {
                    if (primaryMass <= .65)
                        return WorldType.Ammonia;
                    else
                        return WorldType.Ice;
                }

                else if (currPlanet.blackbodyTemp > 230.5 && currPlanet.blackbodyTemp < 240)
                    return WorldType.Ice;

                else if (currPlanet.blackbodyTemp >= 240 && currPlanet.blackbodyTemp < 321)
                {
                    int roll = ourDice.gurpsRollWithCappedMod(10, (systemAge / .5));
                    if (roll >= 18)
                        return WorldType.Garden;
                    else
                        return WorldType.Ocean;
                }

                else if (currPlanet.blackbodyTemp >= 321 && currPlanet.blackbodyTemp < 500.5)
                    return WorldType.Greenhouse;
                else if (currPlanet.blackbodyTemp > 500.5)
                    return WorldType.Chthonian;
            }

            if (currPlanet.worldSize == WorldSize.Large)
            {
                if (currPlanet.blackbodyTemp < 150.5)
                    return WorldType.Ice;

                else if (currPlanet.blackbodyTemp >= 150.5 && currPlanet.blackbodyTemp < 230.5)
                {
                    if (primaryMass <= .65)
                        return WorldType.Ammonia;
                    else
                        return WorldType.Ice;
                }

                else if (currPlanet.blackbodyTemp > -230.5 && currPlanet.blackbodyTemp < 240)
                    return WorldType.Ice;

                else if (currPlanet.blackbodyTemp >= 240 && currPlanet.blackbodyTemp < 321)
                {
                    int roll = ourDice.gurpsRollWithCappedMod(5, (systemAge / .5));
                    if (roll >= 18)
                        return WorldType.Garden;
                    else
                        return WorldType.Ocean;
                }

                else if (currPlanet.blackbodyTemp >= 321 && currPlanet.blackbodyTemp < 500.5)
                    return WorldType.Greenhouse;
                else if (currPlanet.blackbodyTemp > 500.5)
                    return WorldType.Chthonian;
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

                if (currPlanet.worldSize == WorldSize.Small && currPlanet.biomeType == WorldType.Ice)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    if (ourDice.rollUnderGurps(15))
                        currPlanet.AddAtmosphericCondition(AtmosphericConditions.MildlyToxic);
                    else
                        currPlanet.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);
                }

                if (currPlanet.worldSize == WorldSize.Standard && currPlanet.biomeType == WorldType.Ice)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    if (!(ourDice.rollUnderGurps(12)))
                        currPlanet.AddAtmosphericCondition(AtmosphericConditions.MildlyToxic);
                }

                if (currPlanet.worldSize == WorldSize.Large && currPlanet.biomeType == WorldType.Ice)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);
                }

                if ((currPlanet.worldSize == WorldSize.Standard || currPlanet.worldSize == WorldSize.Large) && currPlanet.biomeType == WorldType.Ammonia)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.LethallyToxic);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Corrosive);
                }

                if (currPlanet.worldSize == WorldSize.Standard && currPlanet.biomeType == WorldType.Ocean)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    if (!(ourDice.rollUnderGurps(12)))
                        currPlanet.AddAtmosphericCondition(AtmosphericConditions.Corrosive);
                }

                if (currPlanet.worldSize == WorldSize.Large && currPlanet.biomeType == WorldType.Ocean)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);
                }

                if (currPlanet.worldSize == WorldSize.Standard && currPlanet.biomeType == WorldType.Garden)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);

                    if (!(ourDice.rollUnderGurps(11)))
                        StarReference.addMarginalConditions(currPlanet, ourDice);
                }

                if (currPlanet.worldSize == WorldSize.Large && currPlanet.biomeType == WorldType.Garden)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);

                    if (!(ourDice.rollUnderGurps(11)))
                        StarReference.addMarginalConditions(currPlanet, ourDice);
                }

                if ((currPlanet.worldSize == WorldSize.Large || currPlanet.worldSize == WorldSize.Standard) 
                    && currPlanet.biomeType == WorldType.Greenhouse)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.LethallyToxic);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Corrosive);
                }
            }
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
    }
}
