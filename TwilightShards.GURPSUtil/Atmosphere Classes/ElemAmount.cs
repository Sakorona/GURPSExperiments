using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightShards.GURPSUtil
{
    /// <summary>
    /// This explains the amount of the element in the air.
    /// </summary>
    public enum ElemAmount
    {
        /// <summary>
        /// This means there's only this in the atmosphere
        /// </summary>
        Only = 0,

        /// <summary>
        /// This means this is a primary element in the atmosphere
        /// </summary>
        Primary = 1,

        /// <summary>
        /// This means there is a significant amount in the atmosphere
        /// </summary>
        Some = 2,

        /// <summary>
        /// This means there are trace amounts in the atmosphere
        /// </summary>
        Trace = 3
    }
}
