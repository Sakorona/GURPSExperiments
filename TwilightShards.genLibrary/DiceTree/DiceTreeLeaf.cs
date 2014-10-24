using System;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This is a leaf of the tree. It contains a value, and is the end of a node
    /// </summary>
    public class DiceTreeLeaf : DiceTreeBase
    {
        /// <summary>
        /// Value being stored
        /// </summary>
        object value = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value being stored</param>
        public DiceTreeLeaf(object value)
        {
            this.value = value;
        }

        /// <summary>
        /// Walk function. Returns the value if the cast succeds.
        /// </summary>
        /// <typeparam name="T">Type of the value we are looking for</typeparam>
        /// <param name="values">Values being looked for</param>
        /// <returns>Value if it succedds, default value if it fails</returns>
        public override T Walk<T>(params int[] values)
        {
            if (value is T)
                return (T)value;
            else
                return default(T);
        }
    }
}
