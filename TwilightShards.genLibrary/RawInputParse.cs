using System;
using System.Collections.Generic;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This class contains functions designed to read raw input and return a valid output for options 
    /// or other things that needed limited values.
    /// </summary>
    public static class RawInputParse
    {        
        /// <summary>
        /// This functon verifies the input (i.e makes sure it's a string) from a source then checks against allowed values 
        /// </summary>
        /// <param name="rawInput">The string input (raw) from the file/stream</param>
        /// <param name="procValue">The value passed back to the function.</param>
        /// <param name="defaultValue">The default value for this input</param>
        /// <param name="possibleValues">The possible values for this input</param>
        /// <returns>true if it was valid, false if it was reset to default</returns>
        public static bool VerifyInputWithValues(string rawInput, out string procValue, string defaultValue, List<string> possibleValues)
        {
            //check against the values
            foreach (string s in possibleValues)
            {
                if (rawInput == s)
                {
                    procValue = rawInput;
                    return true;
                }
            }

            //Not found. Pass out default, tell caller it was invalid.
            procValue = defaultValue;
            return false;
        }

        /// <summary>
        /// This functon verifies the input (i.e makes sure it's an unsigned integer) from a source then checks against allowed values 
        /// </summary>
        /// <param name="rawInput">The string input (raw) from the file/stream</param>
        /// <param name="procValue">The value passed back to the function.</param>
        /// <param name="defaultValue">The default value for this input</param>
        /// <param name="possibleValues">The possible values for this input</param>
        /// <returns>true if it was valid, false if it was reset to default</returns>
        public static bool VerifyInputWithValues(string rawInput, out uint procValue, uint defaultValue, List<uint> possibleValues)
        {
            uint testVal;
            if (UInt32.TryParse(rawInput, out testVal))
            {
                foreach (uint s in possibleValues)
                {
                    if (testVal == s)
                    {
                        procValue = testVal;
                        return true;
                    }
                }
            }

           //whether or not it didnt' convert or was invalid, return the default and tell the caller it was invalid.
           procValue = defaultValue;
           return false;
        }

        /// <summary>
        /// This functon verifies the input (i.e makes sure it's an boolean) from a source then checks against allowed values
        /// </summary>
        /// <param name="rawInput">The string input (raw) from the file/stream</param>
        /// <param name="procValue">The value passed back to the function.</param>
        /// <param name="defaultValue">The default value for this input</param>
        /// <returns>true if it was valid, false if it was reset to default</returns>
        public static bool VerifyInputWithValues(string rawInput, out bool procValue, bool defaultValue)
        {
            bool testVal;
            if (Boolean.TryParse(rawInput, out testVal))
            {
                procValue = testVal;
                return true;
            }

            procValue = defaultValue;
            return false;
        }


        /// <summary>
        /// This functon verifies the input (i.e makes sure it's an integer) from a source then checks against allowed values 
        /// </summary>
        /// <param name="rawInput">The string input (raw) from the file/stream</param>
        /// <param name="procValue">The value passed back to the function.</param>
        /// <param name="defaultValue">The default value for this input</param>
        /// <param name="possibleValues">The possible values for this input</param>
        /// <returns>true if it was valid, false if it was reset to default</returns>
        public static bool VerifyInputWithValues(string rawInput, out int procValue, int defaultValue, List<int> possibleValues)
        {
            int testVal;
            if (Int32.TryParse(rawInput, out testVal))
            {
                foreach (int s in possibleValues)
                {
                    if (testVal == s)
                    {
                        procValue = testVal;
                        return true;
                    }
                }
            }

            //whether or not it didnt' convert or was invalid, return the default and tell the caller it was invalid.
            procValue = defaultValue;
            return false;
        }

        /// <summary>
        /// This functon verifies the input (i.e makes sure it's an usigned long) from a source then checks against allowed values 
        /// </summary>
        /// <param name="rawInput">The string input (raw) from the file/stream</param>
        /// <param name="procValue">The value passed back to the function.</param>
        /// <param name="defaultValue">The default value for this input</param>
        /// <param name="possibleValues">The possible values for this input</param>
        /// <returns>true if it was valid, false if it was reset to default</returns>
        public static bool VerifyInputWithValues(string rawInput, out ulong procValue, ulong defaultValue, List<ulong> possibleValues)
        {
            ulong testVal;
            if (UInt64.TryParse(rawInput, out testVal))
            {
                foreach (ulong s in possibleValues)
                {
                    if (testVal == s)
                    {
                        procValue = testVal;
                        return true;
                    }
                }
            }

            //whether or not it didnt' convert or was invalid, return the default and tell the caller it was invalid.
            procValue = defaultValue;
            return false;
        }

        /// <summary>
        /// This functon verifies the input (i.e makes sure it's an usigned long) from a source then checks against allowed values 
        /// </summary>
        /// <param name="rawInput">The string input (raw) from the file/stream</param>
        /// <param name="procValue">The value passed back to the function.</param>
        /// <param name="defaultValue">The default value for this input</param>
        /// <param name="possibleValues">The possible values for this input</param>
        /// <returns>true if it was valid, false if it was reset to default</returns>
        public static bool VerifyInputWithValues(string rawInput, out long procValue, long defaultValue, List<long> possibleValues)
        {
            long testVal;
            if (Int64.TryParse(rawInput, out testVal))
            {
                foreach (long s in possibleValues)
                {
                    if (testVal == s)
                    {
                        procValue = testVal;
                        return true;
                    }
                }
            }

            //whether or not it didnt' convert or was invalid, return the default and tell the caller it was invalid.
            procValue = defaultValue;
            return false;
        }

        /// <summary>
        /// This function verifies the input is an unsigned long and then makes sure it is within specified bounds
        /// </summary>
        /// <param name="rawInput">The raw input from the file/stream</param>
        /// <param name="procValue">The value returned to the function</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="minValue">The minimum valid value</param>
        /// <param name="maxValue">The maxmimum valid value</param>
        /// <returns>True if valid, False if invalid.</returns>
        public static bool VerifyInputWithBounds(string rawInput, out ulong procValue, ulong defaultValue, ulong minValue, ulong maxValue)
        {
            //sanity checking.
            if (defaultValue < UInt64.MinValue || defaultValue > UInt64.MaxValue)
            {
                //Not sure it's POSSIBLE to have this happen, but.
                throw new Exception("Default value out of bounds!");
            }

            ulong testVal;
            if (UInt64.TryParse(rawInput, out testVal))
            {
              //if it's either above max value or the max value for a UInt64, return default/invalid. I don't think it'd actually convert in the case of the latter
              // but sanity checking is advisable.
              if (testVal > maxValue || testVal > UInt64.MaxValue)
              {
                  procValue = defaultValue;
                  return false;
              }

              //if it's either below min value or the min value for a UInt64, return default/invalid. I don't think it'd actually convert in the case of the latter
              // but sanity checking is advisable.
              if (testVal < minValue || testVal < UInt64.MinValue)
              {
                  procValue = defaultValue;
                  return false;
              }

              //it is valid!
              procValue = testVal;
              return true;
            }

            //did not convert.
            procValue = defaultValue;
            return false;
        }

        /// <summary>
        /// This function verifies the input is a long and then makes sure it is within specified bounds
        /// </summary>
        /// <param name="rawInput">The raw input from the file/stream</param>
        /// <param name="procValue">The value returned to the function</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="minValue">The minimum valid value</param>
        /// <param name="maxValue">The maxmimum valid value</param>
        /// <returns>True if valid, False if invalid.</returns>
        public static bool VerifyInputWithBounds(string rawInput, out long procValue, long defaultValue, long minValue, long maxValue)
        {
            //sanity checking.
            if (defaultValue < Int64.MinValue || defaultValue > Int64.MaxValue)
            {
                //Not sure it's POSSIBLE to have this happen, but.
                throw new Exception("Default value out of bounds!");
            }

            long testVal;
            if (Int64.TryParse(rawInput, out testVal))
            {
              //if it's either above max value or the max value for a UInt64, return default/invalid. I don't think it'd actually convert in the case of the latter
              // but sanity checking is advisable.
              if (testVal > maxValue || testVal > Int64.MaxValue)
              {
                  procValue = defaultValue;
                  return false;
              }

              //if it's either below min value or the min value for a UInt64, return default/invalid. I don't think it'd actually convert in the case of the latter
              // but sanity checking is advisable.
              if (testVal < minValue || testVal < Int64.MinValue)
              {
                  procValue = defaultValue;
                  return false;
              }

              //it is valid!
              procValue = testVal;
              return true;
            }

            //did not convert.
            procValue = defaultValue;
            return false;
        }
        

        /// <summary>
        /// This function verifies the input is an unsigned integer and then makes sure it is within specified bounds
        /// </summary>
        /// <param name="rawInput">The raw input from the file/stream</param>
        /// <param name="procValue">The value returned to the function</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="minValue">The minimum valid value</param>
        /// <param name="maxValue">The maxmimum valid value</param>
        /// <returns>True if valid, False if invalid.</returns>
        public static bool VerifyInputWithBounds(string rawInput, out uint procValue, uint defaultValue, uint minValue, uint maxValue)
        {
            //sanity checking.
            if (defaultValue < UInt32.MinValue || defaultValue > UInt32.MaxValue)
            {
                //Not sure it's POSSIBLE to have this happen, but.
                throw new Exception("Default value out of bounds!");
            }

            uint testVal;
            if (UInt32.TryParse(rawInput, out testVal))
            {
              //if it's either above max value or the max value for a UInt64, return default/invalid. I don't think it'd actually convert in the case of the latter
              // but sanity checking is advisable.
              if (testVal > maxValue || testVal > UInt32.MaxValue)
              {
                  procValue = defaultValue;
                  return false;
              }

              //if it's either below min value or the min value for a UInt64, return default/invalid. I don't think it'd actually convert in the case of the latter
              // but sanity checking is advisable.
              if (testVal < minValue || testVal < UInt32.MinValue)
              {
                  procValue = defaultValue;
                  return false;
              }

              //it is valid!
              procValue = testVal;
              return true;
            }

            //did not convert.
            procValue = defaultValue;
            return false;
        }

        /// <summary>
        /// This function verifies the input is an integer and then makes sure it is within specified bounds
        /// </summary>
        /// <param name="rawInput">The raw input from the file/stream</param>
        /// <param name="procValue">The value returned to the function</param>
        /// <param name="defaultValue">The default value</param>
        /// <param name="minValue">The minimum valid value</param>
        /// <param name="maxValue">The maxmimum valid value</param>
        /// <returns>True if valid, False if invalid.</returns>
        public static bool VerifyInputWithBounds(string rawInput, out int procValue, int defaultValue, int minValue, int maxValue)
        {
             //sanity checking.
            if (defaultValue < Int32.MinValue || defaultValue > Int32.MaxValue)
            {
                //Not sure it's POSSIBLE to have this happen, but.
                throw new Exception("Default value out of bounds!");
            }

            int testVal;
            if (Int32.TryParse(rawInput, out testVal))
            {
              //if it's either above max value or the max value for a UInt64, return default/invalid. I don't think it'd actually convert in the case of the latter
              // but sanity checking is advisable.
              if (testVal > maxValue || testVal > Int32.MaxValue)
              {
                  procValue = defaultValue;
                  return false;
              }

              //if it's either below min value or the min value for a UInt64, return default/invalid. I don't think it'd actually convert in the case of the latter
              // but sanity checking is advisable.
              if (testVal < minValue || testVal < Int32.MinValue)
              {
                  procValue = defaultValue;
                  return false;
              }

              //it is valid!
              procValue = testVal;
              return true;
            }

            //did not convert.
            procValue = defaultValue;
            return false;
        }
    }
}
