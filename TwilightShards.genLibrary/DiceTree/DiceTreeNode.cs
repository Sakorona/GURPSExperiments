using System;
using System.Collections.Generic;
using System.Linq;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// Node object of the tree.
    /// </summary>
    public class DiceTreeNode : DiceTreeBase
    {
        /// <summary>
        /// List of the possible children of this node. 
        /// </summary>
        DiceTreeBase[] Children = new DiceTreeBase[16];

        /// <summary>
        /// Constructor
        /// </summary>
        public DiceTreeNode()
        {

        }

        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="Params">Paramters for the children</param>
        public DiceTreeNode(params KeyValuePair<int, DiceTreeBase>[] Params)
        {
            if (Params.Length == 0)
                return;

            //Move the results from 3-18 to 0-15.
            foreach (KeyValuePair<int, DiceTreeBase> kvp in Params)
                Children[kvp.Key - 3] = kvp.Value;

            //add the children.
            DiceTreeBase Last = null;
            for (int n = 0; n < 16; n++)
            {
                if (Children[n] != null)
                    Last = Children[n];
                else
                    Children[n] = Last;
            }
        }

        /// <summary>
        /// Set one node in the children array from point low to high
        /// </summary>
        /// <param name="low">The low point of the range being set</param>
        /// <param name="high"The high point of the range being set></param>
        /// <param name="node">The node being set.</param>
        public void SetRange(int low, int high, DiceTreeBase node)
        {
            for (int n = low - 3; n < high - 3; n++)
            {
                Children[n] = node;
            }
        }

        /// <summary>
        /// Set one node in the children array from point low to high
        /// </summary>
        /// <param name="low">The low point of the range being set</param>
        /// <param name="high"The high point of the range being set></param>
        /// <param name="Params">A set of Key-Value pairs to be set in a node.</param>
        public void SetRange(int low, int high, params KeyValuePair<int, DiceTreeBase>[] Params)
        {
            if (low - 3 < 0 || low - 3 > Children.Length)
                return;
            if (high - 3 < 0 || high - 3 > Children.Length)
                return;

            DiceTreeBase node = new DiceTreeNode(Params);
            for (int n = low - 3; n <= high - 3; n++)
            {
                Children[n] = node;
            }
        }

        /// <summary>
        /// Update a node by key. 
        /// </summary>
        /// <param name="key">The key to set the node for. (needs to be between 3 and 18)</param>
        /// <param name="node">The node to be set.</param>
        public void SetNodeByKey(int key, DiceTreeNode node)
        {
            if (key - 3 < 0 || key - 3 > Children.Length)
               return;

            Children[key - 3] = node;
        }

        /// <summary>
        /// Update a node by key. 
        /// </summary>
        /// <typeparam name="T">Type for the generic value of a leaf</typeparam>
        /// <param name="low">Low end to be set</param>
        /// <param name="high">High end to be set</param>
        /// <param name="value">The value to be set</param>
        public void SetRange<T>(int low, int high, T value)
        {
            if (low - 3 < 0 || low - 3 > Children.Length)
                return;
            if (high - 3 < 0 || high - 3 > Children.Length)
                return;

            for (int n = low - 3; n < high - 3; n++)
            {
                Children[n] = new DiceTreeLeaf(value);
            }
        }

        /// <summary>
        /// Function that walks the children of the node
        /// </summary>
        /// <typeparam name="T">Type of value we are looking for</typeparam>
        /// <param name="values">Values we aer looking for. First is the children</param>
        /// <returns>Value in Children matching values[0]</returns>
        public override T Walk<T>(params int[] values)
        {
            return Children[values[0]].Walk<T>(values.Skip(1).ToArray());
        }
    }

}
