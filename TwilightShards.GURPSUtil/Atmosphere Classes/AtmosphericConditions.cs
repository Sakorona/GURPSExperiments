using System;

namespace TwilightShards.GURPSUtil
{
    /// <summary>
    /// This contains the various types of atmospheric conditions
    /// </summary>
    public enum AtmosphericConditions
    {
        /// <summary>
        /// No atmospheric condition
        /// </summary>
        None,

        /// <summary>
        /// High amounts of Chlorine in the air.
        /// </summary>
        Chlorine,

        /// <summary>
        /// High amounts of Flourine in the air.
        /// </summary>
        Flourine,
        
        /// <summary>
        /// There are sulfur compounds in the air
        /// </summary>
        SulfurCompounds,

        /// <summary>
        /// There are nitrogen oxides in the air.
        /// </summary>
        NitrogenCompounds,
        
        /// <summary>
        /// There are organic toxins in the air.
        /// </summary>
        OrganicToxins,

        /// <summary>
        /// There is lower amounts of free oxygen than expected
        /// </summary>
        LowOxygen,

        /// <summary>
        /// There are pollutants in the atmosphere
        /// </summary>
        Pollutants,

        /// <summary>
        /// The atmosphere has more carbon dioxide than expected
        /// </summary>
        HighCarbonDioxide,

        /// <summary>
        /// The atmosphere has more free oxygen than expected
        /// </summary>
        HighOxygen,

        /// <summary>
        /// There is a large amount of inert gases in the atmosphere
        /// </summary>
        InertGases,

        /*
        /// <summary>
        /// This atmosphere is mildly toxic.
        /// </summary>
        MildlyToxic,

        /// <summary>
        /// The atmosphere is corrosive.
        /// </summary>
        Corrosive,

        /// <summary>
        /// The atmosphere is highly toxic.
        /// </summary>
        HighlyToxic,

        /// <summary>
        /// The atmosphere is suffocating
        /// </summary>
        Suffocating,

        /// <summary>
        /// The atmosphere is lethally toxic
        /// </summary>
        LethallyToxic, */
        //HeavyMetalPoisoning,
        //RadioactivePoisoning,

        /* /// <summary>
        /// This notes that the Pressure class is treated as one higher
        /// </summary>
        EffectiveOnePressureClassUp, */

        /// <summary>
        /// This notes that the Pressure class is treated as one lower
        /// </summary>
        EffectiveOnePressureClassDown,

        /// <summary>
        /// This notes that the Pressure class is treated as one higher
        /// </summary>
        FlammabilityOneClassUp,
        //LocallyLethallyToxic,
        //LocallyHighlyToxic
    }
}
