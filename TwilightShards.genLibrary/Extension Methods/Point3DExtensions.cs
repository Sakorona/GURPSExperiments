using System;
using System.Windows.Media.Media3D;

namespace TwilightShards.genLibrary
{
    /// <summary>
    /// This stores Point3D extension methods
    /// </summary>
    public static class Point3DExtensions
    {
        /// <summary>
        /// This function determines the distance between two points.
        /// </summary>
        /// <param name="p">Point 1</param>
        /// <param name="q">Point 2</param>
        /// <returns>The distance between the two points</returns>
        public static double GetDistance(this Point3D p, Point3D q) 
        {
            double distX = Math.Pow(p.X - q.X, 2);
            double distY = Math.Pow(p.Y - q.Y, 2);
            double distZ = Math.Pow(p.Z - q.Z, 2);

            return Math.Sqrt(distX + distY + distZ);
        }
    }
}
