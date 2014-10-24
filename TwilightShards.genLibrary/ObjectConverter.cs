using System;
using System.Collections.Generic;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This contains functions to verify that objects can be converted.
    /// </summary>
    public static class ObjectConverter
    {
        /// <summary>
        /// This function verifies if it is a boolean
        /// </summary>
        /// <param name="rawInput">The object being verified</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool VerifyBool(object rawInput)
        {
            try{
                bool test = Convert.ToBoolean(rawInput);
                return true;
            }
            catch (Exception){
               return false;
            }
        }
        
        /// <summary>
        /// This function verifies that the object is an integer
        /// </summary>
        /// <param name="rawInput">The object being verified</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool VerifyInteger(object rawInput)
        {
            try{
                if ((rawInput.GetType() == typeof(double)) && (Convert.ToInt32(rawInput) != Convert.ToDouble(rawInput)))
                    return false;
                if (Convert.ToInt32(rawInput) != Convert.ToDouble(rawInput))
                    return false;

                int test = Convert.ToInt32(rawInput);
                return true;
            }
            catch (Exception){
                return false;
            }
        }

        /// <summary>
        /// This function verifies that the object is a double
        /// </summary>
        /// <param name="rawInput">The object being verified</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool VerifyDouble(object rawInput)
        {
            try{
                double test = Convert.ToDouble(rawInput);
                return true;
            }
            catch (Exception){
                return false;
            }
        }

        /// <summary>
        /// This function verifies that the object is a long
        /// </summary>
        /// <param name="rawInput">The object being verified</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool VerifyLong(object rawInput)
        {
            try{
                long test = Convert.ToInt64(rawInput);
                return true;
            }
            catch (Exception){
                return false;
            }
        }

        /// <summary>
        /// This function verifies that the object is a short
        /// </summary>
        /// <param name="rawInput">The object being verified</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool VerifyShort(object rawInput)
        {
            try{
                short test = Convert.ToInt16(rawInput);
                return true;
            }
            catch (Exception){
                return false;
            }
        }

        /// <summary>
        /// This function verifies that the object is a string
        /// </summary>
        /// <param name="rawInput">The object being verified</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool VerifyString(object rawInput)
        {
            try{
                string test = Convert.ToString(rawInput);
                return true;
            }
            catch (Exception){
                return false;
            }
        }
    }
}
