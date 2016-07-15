using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace TD.Location
{
    public struct Point
    {
        public int X;
        public int Y;
    }

    public static class PointExtensions
    {
        public static double GetDistance(this Point firstPoint, Point secondPoint)
        {
            return Sqrt(
                (firstPoint.X - secondPoint.X) * (firstPoint.X - secondPoint.X)
                +
                (firstPoint.Y - secondPoint.Y) * (firstPoint.Y - secondPoint.Y));
        }
    }
}
