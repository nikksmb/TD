using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD
{
    public static class Constants
    {
        /// <summary>
        /// Speed coefficient means, that speed value equals this coefficient
        /// will be 1 Hz.
        /// </summary>
        public const int SpeedCoefficient = 1000;

        public static class Map
        {
            public const int Length = 10000;
            public const int Width = 10000;
            public const int RoadWidth = 1000;
            public const int RoadRadius = RoadWidth / 2;
            public const int CheckRange = Units.MinMonsterSize;
        }

        public static class Messages
        {
            public const string NotSupportedShape = "Not supported shape";
        }

        public static class Units
        {
            /// <summary>
            /// Tower radius.
            /// </summary>
            public const int DefaultTowerSize = 500;
            /// <summary>
            /// Monster radius
            /// </summary>
            public const int DefaultMonsterSize = 200;

            public const int MinMonsterSize = 125;
        }
    }
}
