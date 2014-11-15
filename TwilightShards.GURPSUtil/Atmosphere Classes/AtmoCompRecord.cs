using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightShards.GURPSUtil
{
    /// <summary>
    /// Record of element and amount of the element in the atmosphere
    /// </summary>
    public class AtmoCompRecord {

        /// <summary>
        /// This is the element in the air
        /// </summary>
        public string compound { get; private set; }

        /// <summary>
        /// This is the amount in the air
        /// </summary>
        public ElemAmount amount { get; set; }// type 0 is only, 1 is primary, 2 is some, 3 is trace
    
        public AtmoCompRecord(string c, ElemAmount t)
        {
	        compound = c;
	        amount = t;
        }

        public AtmoCompRecord(string c, int t)
        {
            compound = c;
            amount = (ElemAmount)t;
        }

    }
}
