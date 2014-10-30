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

                    //set composition
                    currPlanet.atmoComposition = "Primarily Nitrogen and Methane, with components of Argon, and some trace ammonia and carbon dioxide.";
                }

                if (currPlanet.worldSize == WorldSize.Standard && currPlanet.biomeType == WorldType.Ice)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    if (!(ourDice.rollUnderGurps(12)))
                        currPlanet.AddAtmosphericCondition(AtmosphericConditions.MildlyToxic);

                    //set composition
                    currPlanet.atmoComposition = "Primarily Carbon Dioxide, Nitrogen, with some Methane or Sulfur Dioxide";
                }

                if (currPlanet.worldSize == WorldSize.Large && currPlanet.biomeType == WorldType.Ice)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);

                    //set composition
                    currPlanet.atmoComposition = "Primarily Helium, Nitrogen, with some Sulfur Dioxide, Chlorine and Flourine";
                }

                if (currPlanet.worldSize == WorldSize.Standard && currPlanet.biomeType == WorldType.Ammonia)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.LethallyToxic);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Corrosive);

                    //set composition
                    currPlanet.atmoComposition = "Primarily Nitrogen with a large amount of ammonia and methane.";
                }

                if (currPlanet.worldSize == WorldSize.Large && currPlanet.biomeType == WorldType.Ammonia)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.LethallyToxic);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Corrosive);

                    //set composition
                    currPlanet.atmoComposition = "Primarily Nitrogen with a large amount of ammonia and methane.";
                }

                if (currPlanet.worldSize == WorldSize.Standard && currPlanet.biomeType == WorldType.Ocean)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    if (!(ourDice.rollUnderGurps(12)))
                        currPlanet.AddAtmosphericCondition(AtmosphericConditions.Corrosive);

                    //set composition
                    currPlanet.atmoComposition = "Primarily Carbon Dioxide and Nitrogen.";
                }

                if (currPlanet.worldSize == WorldSize.Large && currPlanet.biomeType == WorldType.Ocean)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.HighlyToxic);

                    //set composition
                    currPlanet.atmoComposition = "A mixture of Helium and Nitrogen.";
                }

                if (currPlanet.worldSize == WorldSize.Standard && currPlanet.biomeType == WorldType.Garden)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);

                    if (!(ourDice.rollUnderGurps(11)))
                        StarReference.addMarginalConditions(currPlanet, ourDice);

                    currPlanet.atmoComposition = "Primarily Nitrogen and Oxygen. Some Argon and other trace gasses.";
                }
                if (currPlanet.worldSize == WorldSize.Large && currPlanet.biomeType == WorldType.Garden)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);

                    if (!(ourDice.rollUnderGurps(11)))
                        StarReference.addMarginalConditions(currPlanet, ourDice);

                    currPlanet.atmoComposition = "Primarily Nitrogen and Oxygen as well as many inert gasses (neon, argon)";
                }
                if ((currPlanet.worldSize == WorldSize.Large || currPlanet.worldSize == WorldSize.Standard) && currPlanet.biomeType == WorldType.Greenhouse)
                {
                    currPlanet.atmoMass = StarReference.rollAtmosphere(ourDice);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Suffocating);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.LethallyToxic);
                    currPlanet.AddAtmosphericCondition(AtmosphericConditions.Corrosive);
                    //this is either dry or wet.
                    if (ourDice.coinFlip() == true)
                    {
                        currPlanet.biomeType = WorldType.DryGreenhouse;
                        currPlanet.atmoComposition = "Primarily Carbon Dioxide and some Sulfur Dioxide.";
                    }
                    else
                    {
                        currPlanet.atmoComposition = "Carbon Dioxide, Nitorgen, and large amounts of Water Vapor, and traces of free oxygen.";
                        currPlanet.biomeType = WorldType.WetGreenhouse;
                    }
                }
            }
        }

        public static void GenerateHydrographicCoverage(Dice ourDice, Planet currPlanet)
        {
            currPlanet.hydroCoverage = 0;
            if ((currPlanet.worldSize == WorldSize.Small) && (currPlanet.biomeType == WorldType.Ice))
            {
                currPlanet.volatileType = HydroCoverageType.Hydrocarbons;
                currPlanet.hydroCoverage = 0;
            }
        }
    }
}
