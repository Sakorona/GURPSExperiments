using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This contains array extension methods.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// This function partially fills an array with a value.
        /// </summary>
        /// <typeparam name="T">The variable type</typeparam>
        /// <param name="originalArray">The array being partially filled</param>
        /// <param name="lowerBound">The lower bound of the fill area</param>
        /// <param name="higherBound">The higher bound of the fill area</param>
        /// <param name="item">The item being added</param>
        public static void PartiallyFill<T>(this T[] originalArray, int lowerBound, int higherBound, T item)
        {
            if (lowerBound == higherBound)
                originalArray[lowerBound] = item;

            if (lowerBound > higherBound)
                return;

            for (int i = lowerBound; i <= higherBound; i++)
            {
                originalArray[i] = item;
            }
        }
    }
}
