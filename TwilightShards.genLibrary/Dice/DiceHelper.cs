using System;
using System.Collections.Generic;
using System.Linq;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This class contains extension methods that use the Dice object.
    /// </summary>
    public static class DiceHelper
    {
        /// <summary>
        /// This is a extension method for RangePair that rolls within the range
        /// </summary>
        /// <param name="s">The RangePair</param>
        /// <returns></returns>
        public static double RollInRange(this RangePair s, Dice d)
        {
            return d.rollInRange(s.LowerBound, s.HigherBound);
        }
        
        /// <summary>
        /// This extension method for a list picks a random item from the list.
        /// </summary>
        /// <typeparam name="T">The type of the list</typeparam>
        /// <param name="s">The List</param>
        /// <param name="d">The Dice</param>
        /// <returns>A random item from the list</returns>
        public static T PickRandom<T>(this List<T> s, Dice d)
        {
            return s[d.rollInIntRange(0, (s.Count -1))];
        }

        /// <summary>
        /// This extension method picks a random item from a dictionary.
        /// </summary>
        /// <typeparam name="T">The type of the Key</typeparam>
        /// <typeparam name="U">The type of the Value</typeparam>
        /// <param name="s">The Dictionary</param>
        /// <param name="d">The Dice</param>
        /// <returns>A random item from the dictionary</returns>
        public static U PickRandom<T,U>(this Dictionary<T, U> s, Dice d)
        {
            List<U> values = Enumerable.ToList(s.Values);
            return values[d.rollInIntRange(0,values.Count-1)];
        }

        
        /// <summary>
        /// This function picks a random item from an integer array.
        /// </summary>
        /// <param name="a">The integer array</param>
        /// <param name="d">Dice object</param>
        /// <returns></returns>
        public static int PickRandom(this int[] a, Dice d) 
        {
            return a[d.rollInIntRange(0, a.Length - 1)];
        }
        
        /// <summary>
        /// This function picks a random item from a generic array.
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="source">The generic array</param>
        /// <param name="d">Dice object</param>
        /// <returns>A random item from that array.</returns>
        public static T PickRandom<T>(this T[] source, Dice d)
        {
            return source[d.rollInIntRange(0, source.Length - 1)];
        }
    }
}
