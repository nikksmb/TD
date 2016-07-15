using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Location.Shapes
{
    public class Rectangle : Shape
    {
        /// <summary>
        /// Point between 9 and 12 o'clock looknig from the middle of rectangle
        /// </summary>
        public Point A { get; private set; }

        /// <summary>
        /// Point between 12 and 3 o'clock looknig from the middle of rectangle
        /// </summary>
        public Point B { get; private set; }

        /// <summary>
        /// Point between 6 and 9 o'clock looknig from the middle of rectangle
        /// </summary>
        public Point C { get; private set; }

        /// <summary>
        /// Point between 3 and 6 o'clock looknig from the middle of rectangle
        /// </summary>
        public Point D { get; private set; }

        private int _width;
        private int _height;

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                RecalculateWidth(value);
                _width = value;
                InvokeChanged();
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                RecalculateHeight(value);
                _height = value;
                InvokeChanged();
            }
        }

        public double Angle {
            get
            {
                if (Width == 0 || Height == 0)
                {
                    return 0;
                }
                return Math.Acos((double)(A.Y - C.Y) / Height);
            }
            set
            {
                RecalculateAngle(value - Angle);
            }
        }

        public Rectangle(Point firstMiddle, Point secondMiddle, int width)
        {
            //Step 1: move rectangle to needed location
            Point center = new Point
            {
                X = (firstMiddle.X + secondMiddle.X) / 2,
                Y = (firstMiddle.Y + secondMiddle.Y) / 2
            };
            Center = center;

            //Step 2: initialize Height and Width
            Width = width;
            double height = firstMiddle.GetDistance(secondMiddle);
            Height = (int)height;

            //Step 3: initialize angle and rotation
            Angle = Math.Acos((secondMiddle.Y - firstMiddle.Y) / height);
/*
            double cosA = (secondMiddle.Y - firstMiddle.Y) / height;
            double sinA = (secondMiddle.X - firstMiddle.X) / height;

            //!!!
            A = new Point
            {
                X = (int)(firstMiddle.X - width / 2.0 * cosA),
                Y = (int)(firstMiddle.Y + width / 2.0 * sinA)
            };
            B = new Point
            {
                X = (int)(firstMiddle.X + width / 2.0 * cosA),
                Y = (int)(firstMiddle.Y - width / 2.0 * sinA)
            };
            C = new Point
            {
                X = (int)(secondMiddle.X - width / 2.0 * cosA),
                Y = (int)(secondMiddle.Y + width / 2.0 * sinA)
            };
            D = new Point
            {
                X = (int)(secondMiddle.X + width / 2.0 * cosA),
                Y = (int)(secondMiddle.Y - width / 2.0 * sinA)
            };*/
        }

        public override void RecalculateChanges(Point newCenter)
        {
            A = new Point()
            {
                X = A.X + newCenter.X - Center.X,
                Y = A.Y + newCenter.Y - Center.Y
            };
            B = new Point()
            {
                X = B.X + newCenter.X - Center.X,
                Y = B.Y + newCenter.Y - Center.Y
            };
            C = new Point()
            {
                X = C.X + newCenter.X - Center.X,
                Y = C.Y + newCenter.Y - Center.Y
            };
            D = new Point()
            {
                X = D.X + newCenter.X - Center.X,
                Y = D.Y + newCenter.Y - Center.Y
            };
        }

        private void RecalculateWidth(int newWidth)
        {
            Point pA = new Point
            {
                X = (int)(A.X - (newWidth - Width) / 2.0 * Math.Cos(Angle)),
                Y = (int)(A.Y - (newWidth - Width) / 2.0 * Math.Sin(Angle))
            };
            Point pB = new Point
            {
                X = (int)(B.X + (newWidth - Width) / 2.0 * Math.Cos(Angle)),
                Y = (int)(B.Y + (newWidth - Width) / 2.0 * Math.Sin(Angle))
            };
            Point pC = new Point
            {
                X = (int)(C.X - (newWidth - Width) / 2.0 * Math.Cos(Angle)),
                Y = (int)(C.Y - (newWidth - Width) / 2.0 * Math.Sin(Angle))
            };
            Point pD = new Point
            {
                X = (int)(D.X + (newWidth - Width) / 2.0 * Math.Cos(Angle)),
                Y = (int)(D.Y + (newWidth - Width) / 2.0 * Math.Sin(Angle))
            };
            A = pA;
            B = pB;
            C = pC;
            D = pD;
        }

        private void RecalculateHeight(int newHeight)
        {
            Point pA = new Point
            {
                X = (int)(A.X - (newHeight - Height) / 2.0 * Math.Sin(Angle)),
                Y = (int)(A.Y + (newHeight - Height) / 2.0 * Math.Cos(Angle))
            };
            Point pB = new Point
            {
                X = (int)(B.X - (newHeight - Height) / 2.0 * Math.Sin(Angle)),
                Y = (int)(B.Y + (newHeight - Height) / 2.0 * Math.Cos(Angle))
            };
            Point pC = new Point
            {
                X = (int)(C.X + (newHeight - Height) / 2.0 * Math.Sin(Angle)),
                Y = (int)(C.Y - (newHeight - Height) / 2.0 * Math.Cos(Angle))
            };
            Point pD = new Point
            {
                X = (int)(D.X + (newHeight - Height) / 2.0 * Math.Sin(Angle)),
                Y = (int)(D.Y - (newHeight - Height) / 2.0 * Math.Cos(Angle))
            };
            A = pA;
            B = pB;
            C = pC;
            D = pD;
        }

        private void RecalculateAngle(double newAngle)
        {
            //just initialize foor now
            //return;

            double cosA = Math.Cos(newAngle);
            double sinA = Math.Sin(newAngle);
            Point pA = new Point()
            {
                X = (int)(cosA*(A.X - Center.X) - sinA*(A.Y - Center.Y) + Center.X),
                Y = (int)(sinA*(A.X - Center.X) + cosA*(A.Y - Center.Y) + Center.Y)
            };

            Point pB = new Point()
            {
                X = (int)(cosA * (B.X - Center.X) - sinA * (B.Y - Center.Y) + Center.X),
                Y = (int)(sinA * (B.X - Center.X) + cosA * (B.Y - Center.Y) + Center.Y)
            };

            Point pC = new Point()
            {
                X = (int)(cosA * (C.X - Center.X) - sinA * (C.Y - Center.Y) + Center.X),
                Y = (int)(sinA * (C.X - Center.X) + cosA * (C.Y - Center.Y) + Center.Y)
            };

            Point pD = new Point()
            {
                X = (int)(cosA * (D.X - Center.X) - sinA * (D.Y - Center.Y) + Center.X),
                Y = (int)(sinA * (D.X - Center.X) + cosA * (D.Y - Center.Y) + Center.Y)
            };

            A = pA;
            B = pB;
            C = pC;
            D = pD;
        }
    }
}
