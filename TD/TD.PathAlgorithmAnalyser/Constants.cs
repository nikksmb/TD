using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TD.PathAlgorithmAnalyser
{
    public static class Constants
    {
        public const int FieldWidth = 500;
        public const int FieldHeight = 500;

        public static class Colors
        {
#if (DEBUG)
            public static Color RoadColor = Color.FromArgb(150, 150, 150, 150);
            public static Color DefaultUnitColor = Color.FromArgb(150, 61, 161, 0);
            public static Color DefaultUnitBorder = Color.FromArgb(255, 0, 0, 0);

            public static Color DefaultTowerLocationColor = Color.FromArgb(150, 50, 50, 100);
            public static Color DefaultTowerColor = Color.FromArgb(150, 50, 50, 100);
            public static Color InvalidTowerPosition = Color.FromArgb(150, 190, 50, 100);
#else
            public static Color RoadColor = Color.FromArgb(255, 150, 150, 150);
            public static Color DefaultUnitColor = Color.FromArgb(255, 61, 161, 0);
            public static Color DefaultUnitBorder = Color.FromArgb(255, 0, 0, 0);

            public static Color DefaultTowerLocationColor = Color.FromArgb(150, 50, 50, 100);
            public static Color DefaultTowerColor = Color.FromArgb(255, 50, 50, 100);
            public static Color InvalidTowerPosition = Color.FromArgb(150, 190, 50, 100);
#endif
        }

    }
}
