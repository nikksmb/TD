using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Location;
using TD.Location.Shapes;

namespace TD
{
    public static class Geometry
    {
        public static Point? GetLinesIntersectionPoint(Point p1, Point p2, Point p3, Point p4)
        {
            int denominator = ((p4.Y - p3.Y) * (p2.X - p1.X) - (p4.X - p3.X) * (p2.Y - p1.Y));
            if (denominator == 0)
            {
                return null;
            }
            double multiplier = 1.0 / denominator;
            double ua = ((p4.X - p3.X) * (p1.Y - p3.Y) - (p4.Y - p3.Y) * (p1.X - p3.X))
                * multiplier;
            if (ua < 0 || ua > 1)
            {
                return null;
            }
            double ub = ((p2.X - p1.X) * (p1.Y - p3.Y) - (p2.Y - p1.Y) * (p1.X - p3.X))
                * multiplier;
            if (ub < 0 || ub > 1)
            {
                return null;
            }
            return new Point
            {
                X = (int)(p1.X + ua * (p2.X - p1.X)),
                Y = (int)(p1.Y + ua * (p2.Y - p1.Y))
            };
        }

        public static bool IsPointInsideShape(Shape shape, Point point)
        {
            if (shape is Circle)
            {
                return IsPointInsideCircle((Circle)shape, point);
            }
            if (shape is Rectangle)
            {
                return IsPointInsideRectangle((Rectangle)shape, point);
            }
            throw new InvalidOperationException(Constants.Messages.NotSupportedShape);
        }

        public static bool IsPointInsideCircle(Circle circle, Point point)
        {
            return (point.X - circle.Center.X) * (point.X - circle.Center.X)
                 + (point.Y - circle.Center.Y) * (point.Y - circle.Center.Y)
                 <= circle.Radius * circle.Radius;
        }

        public static bool IsPointInsideRectangle(Rectangle rectangle, Point point)
        {
            int firstSign = Math.Sign(
                (rectangle.A.Y - rectangle.B.Y) * point.X
                + (rectangle.B.X - rectangle.A.X) * point.Y
                - ((rectangle.A.Y - rectangle.B.Y) * rectangle.A.X + (rectangle.B.X - rectangle.A.X) * rectangle.A.Y));
            int secondSign = Math.Sign(
                (rectangle.B.Y - rectangle.D.Y) * point.X
                + (rectangle.D.X - rectangle.B.X) * point.Y
                - ((rectangle.B.Y - rectangle.D.Y) * rectangle.B.X + (rectangle.D.X - rectangle.B.X) * rectangle.B.Y));
            int thirdSign = Math.Sign(
                (rectangle.D.Y - rectangle.C.Y) * point.X
                + (rectangle.C.X - rectangle.D.X) * point.Y
                - ((rectangle.D.Y - rectangle.C.Y) * rectangle.D.X + (rectangle.C.X - rectangle.D.X) * rectangle.D.Y));
            int fourthSign = Math.Sign(
                (rectangle.C.Y - rectangle.A.Y) * point.X
                + (rectangle.A.X - rectangle.C.X) * point.Y
                - ((rectangle.C.Y - rectangle.A.Y) * rectangle.C.X + (rectangle.A.X - rectangle.C.X) * rectangle.C.Y));
            return
                (firstSign >= 0 &&
                secondSign >= 0 &&
                thirdSign >= 0 &&
                fourthSign >= 0)
                ||
                (firstSign <= 0 &&
                secondSign <= 0 &&
                thirdSign <= 0 &&
                fourthSign <= 0);
        }

        public static double TriangleArea(Point a, Point b, Point c)
        {
            return Math.Abs(0.5 * (a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y)));
        }

        public static int DotProduct(Point a, Point b, Point c, Point d)
        {
            return (b.X - a.X) * (d.X - c.X) + (b.Y - a.Y) * (d.Y - c.Y);
        }

        public static int DotProduct(Point a, Point b)
        {
            return a.X * b.X + a.Y * b.Y;
        }
    }
}
