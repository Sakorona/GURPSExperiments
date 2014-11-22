using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwilightShards.GURPSUtil
{
    public class MoonRecord
    {
        public MoonType moon {get; set;}
        public double orbitalRadius { get; set; }
        public WorldSize moonSize { get; set; }

        public MoonRecord(MoonType m, double d, WorldSize s)
        {
            moon = m;
            orbitalRadius = d;
            moonSize = s;
        }
    }
}
