using System;
using System.Collections.Generic;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This class is for leaves and nodes of the DiceTree structure
    /// </summary>
    public abstract class DiceTreeBase
    {
        /// <summary>
        /// This function creates a key-value pair from a key and value. Alias function.
        /// </summary>
        /// <param name="Key">The key being set</param>
        /// <param name="Value">The value being set</param>
        /// <returns>key-value pair with a key and value</returns>
        public static KeyValuePair<int, DiceTreeBase> Init(int Key, DiceTreeBase Value)
        {
            return new KeyValuePair<int, DiceTreeBase>(Key, Value);
        }

        /// <summary>
        /// This function creates a key-value pair from a key and value with generic type. Alias function.
        /// </summary>
        /// <typeparam name="T">The value being set</typeparam>
        /// <param name="Key">The key being set</param>
        /// <param name="Value">The value being set</param>
        /// <returns>key-value pair with a key and value</returns>
        public static KeyValuePair<int, DiceTreeBase> Init<T>(int Key, T Value)
        {
            return new KeyValuePair<int, DiceTreeBase>(Key, new DiceTreeLeaf(Value));
        }

        /// <summary>
        /// Abstract function to walk a node or leaf
        /// </summary>
        /// <typeparam name="T">Type of the object being looked for</typeparam>
        /// <param name="values">The values being looked into</param>
        /// <returns>The value</returns>
        public abstract T Walk<T>(params int[] values);
    }
}