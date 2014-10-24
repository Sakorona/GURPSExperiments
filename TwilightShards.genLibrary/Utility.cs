using System;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// A collection of general helper functions
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Constant for 1 Thousand
        /// </summary>
        public const long Thousand = 1000;

        /// <summary>
        /// Constant for 1 Million
        /// </summary>
        public const long Million  = 1000000;

        /// <summary>
        /// Constant for 1 Billion
        /// </summary>
        public const long Billion  = 1000000000;

        /// <summary>
        /// Constant for 1 Trillion
        /// </summary>
        public const long Trillion = 1000000000000;

        /// <summary>
        /// This function formats for an astronomical year.
        /// </summary>
        /// <param name="year">Number of years</param>
        /// <returns></returns>
        public static string FormatForAstronomicalYear(double year) 
        {
            //Millions are MYa, Billions are GYa, Trillions are TYa
            if (year < 10000)
                return String.Format("{0:F3} Kyr", (year / Thousand));
            if (year >= 100000 && year < Million)
                return String.Format("{0:F3} Myr", (year / Million));
            
            if (year >= Million && year < Billion)
                return String.Format("{0:F3} Myr", (year / Million));

            if (year >= Billion && year < Trillion)
                return String.Format("{0:F3} Gyr", (year / Billion));
            if (year >= Trillion)
                return String.Format("{0:F3} Tyr", (year / Trillion));

            return year.ToString() + " ???";            
        }

        /// <summary>
        /// This function parses an string to an Enum
        /// </summary>
        /// <typeparam name="T">The Enum we are converting to</typeparam>
        /// <param name="value">The string value we are converting from</param>
        /// <returns>The enum value</returns>
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
