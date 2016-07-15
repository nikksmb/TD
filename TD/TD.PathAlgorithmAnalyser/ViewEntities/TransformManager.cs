using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.PathAlgorithmAnalyser.ViewEntities
{
    public static class TransformManager
    {
        private static int Width = Constants.FieldWidth;
        private static int Height = Constants.FieldHeight;

        public static double TransformWidth(double item)
        {
            return item / TD.Constants.Map.Width * Width;
        }

        public static double TransformHeight(double item)
        {
            return item / TD.Constants.Map.Length * Height;
        }
    }
}
