using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Location.Shapes
{
    public static class ShapeExtensions
    {
        public static ShapeRelationState GetState(this Shape shape, Shape otherShape)
        {
            if (shape is Circle)
            {
                return GetCircleRelations((Circle)shape, otherShape);
            }
            if (shape is Rectangle)
            {
                return GetRectangleRelations((Rectangle)shape, otherShape);
            }
            throw new InvalidOperationException(Constants.Messages.NotSupportedShape);            
        }

        private static ShapeRelationState GetCircleRelations(Circle circle, Shape otherShape)
        {
            if (otherShape is Circle)
            {
                Circle otherCircle = (Circle)otherShape;
                double distance = otherCircle.Center.GetDistance(circle.Center);
                double radSum = otherCircle.Radius + circle.Radius;
                if (distance >= radSum)
                {
                    return ShapeRelationState.NoRelation;
                }
                if (distance < radSum)
                {
                    if (otherCircle.Radius + distance < circle.Radius)
                    {
                        return ShapeRelationState.Outside;
                    }
                    if (circle.Radius + distance < otherCircle.Radius)
                    {
                        return ShapeRelationState.Inside;
                    }
                    return ShapeRelationState.Overlapped;
                }
            }
            if (otherShape is Rectangle)
            {
                Rectangle rectangle = (Rectangle)otherShape;
                ShapeRelationState ab = GetLineRelations(rectangle.A, rectangle.B, circle);
                ShapeRelationState bd = GetLineRelations(rectangle.B, rectangle.D, circle);
                ShapeRelationState dc = GetLineRelations(rectangle.D, rectangle.C, circle);
                ShapeRelationState ca = GetLineRelations(rectangle.C, rectangle.A, circle);
                if (ab == bd && bd == dc && dc == ca && ca == ShapeRelationState.Inside)
                {
                    return ShapeRelationState.Outside;
                }
                if (ab == ShapeRelationState.Overlapped 
                    || bd == ShapeRelationState.Overlapped
                    || dc == ShapeRelationState.Overlapped 
                    || ca == ShapeRelationState.Overlapped)
                {
                    return ShapeRelationState.Overlapped;
                }
                if (Geometry.IsPointInsideRectangle(rectangle, circle.Center))
                {
                    return ShapeRelationState.Inside;
                }
                return ShapeRelationState.NoRelation;
            }
            throw new InvalidOperationException(Constants.Messages.NotSupportedShape);
        }

        private static ShapeRelationState GetRectangleRelations(Rectangle rectangle, Shape otherShape)
        {
            if (otherShape is Rectangle)
            {
                Rectangle otherRectangle = (Rectangle)otherShape;
                ShapeRelationState ab = GetLineRelations(otherRectangle.A, otherRectangle.B, rectangle);
                ShapeRelationState bd = GetLineRelations(otherRectangle.B, otherRectangle.D, rectangle);
                ShapeRelationState dc = GetLineRelations(otherRectangle.D, otherRectangle.C, rectangle);
                ShapeRelationState ca = GetLineRelations(otherRectangle.C, otherRectangle.A, rectangle);
                if (ab == bd && bd == dc && dc == ca && ca == ShapeRelationState.Inside)
                {
                    return ShapeRelationState.Outside;
                }
                if (ab == ShapeRelationState.Overlapped
                    || bd == ShapeRelationState.Overlapped
                    || dc == ShapeRelationState.Overlapped
                    || ca == ShapeRelationState.Overlapped)
                {
                    return ShapeRelationState.Overlapped;
                }
                ab = GetLineRelations(rectangle.A, rectangle.B, otherRectangle);
                if (ab == ShapeRelationState.Inside)
                {
                    return ShapeRelationState.Inside;
                }
                return ShapeRelationState.NoRelation;
            }
            if (otherShape is Circle)
            {
                Circle circle = (Circle)otherShape;
                ShapeRelationState ab = GetLineRelations(rectangle.A, rectangle.B, circle);
                ShapeRelationState bd = GetLineRelations(rectangle.B, rectangle.D, circle);
                ShapeRelationState dc = GetLineRelations(rectangle.D, rectangle.C, circle);
                ShapeRelationState ca = GetLineRelations(rectangle.C, rectangle.A, circle);
                if (ab == bd && bd == dc && dc == ca && ca == ShapeRelationState.Inside)
                {
                    return ShapeRelationState.Inside;
                }
                if (ab == ShapeRelationState.Overlapped
                    || bd == ShapeRelationState.Overlapped
                    || dc == ShapeRelationState.Overlapped
                    || ca == ShapeRelationState.Overlapped)
                {
                    return ShapeRelationState.Overlapped;
                }
                if (Geometry.IsPointInsideRectangle(rectangle, circle.Center))
                {
                    return ShapeRelationState.Outside;
                }
                return ShapeRelationState.NoRelation;
            }
            throw new InvalidOperationException(Constants.Messages.NotSupportedShape);
        }

        private static ShapeRelationState GetLineRelations(Point p1, Point p2, Shape otherShape)
        {
            if (otherShape is Circle)
            {
                Circle circle = (Circle)otherShape;
                //line vector
                Point lineVector = new Point()
                {
                    X = p2.X - p1.X,
                    Y = p2.Y - p1.Y
                };
                //center to start
                Point f = new Point()
                {
                    X = p1.X - circle.Center.X,
                    Y = p1.Y - circle.Center.Y
                };
                long a = Geometry.DotProduct(lineVector, lineVector);
                long b = 2 * Geometry.DotProduct(f, lineVector);
                long c = Geometry.DotProduct(f, f) - circle.Radius * circle.Radius;

                long discriminant = b * b - 4 * a * c;
                if (discriminant < 0)
                {
                    return ShapeRelationState.NoRelation;
                }
                double sqrtDiscriminant = Math.Sqrt(discriminant);
                double t1 = (-b - sqrtDiscriminant) / (2 * a);
                double t2 = (-b + sqrtDiscriminant) / (2 * a);

                if (t1 > 1 || t2 < 0)
                {
                    return ShapeRelationState.NoRelation;
                }
                if (t1 < 0 && t2 > 1)
                {
                    return ShapeRelationState.Inside;
                }
                return ShapeRelationState.Overlapped;
                




                /*
                double normal = Math.Abs(
                    circle.Center.Y * (p1.X - p2.X) 
                    + circle.Center.X * (p2.Y - p1.Y)
                    + p2.X * p1.Y - p2.Y * p1.X) / (p1.GetDistance(p2));
                if (normal > circle.Radius)
                {
                    return ShapeRelationState.NoRelation;
                }
                if (Geometry.IsPointInsideCircle(circle, p1) && Geometry.IsPointInsideCircle(circle, p2))
                {
                    return ShapeRelationState.Inside;
                }

                return ShapeRelationState.Overlapped;*/
            }
            if (otherShape is Rectangle)
            {
                Rectangle rectangle = (Rectangle)otherShape;
                if (Geometry.IsPointInsideRectangle(rectangle,p1) 
                    && Geometry.IsPointInsideRectangle(rectangle, p2))
                {
                    return ShapeRelationState.Inside;
                }
                if (Geometry.GetLinesIntersectionPoint(p1,p2,rectangle.A,rectangle.B) != null)
                {
                    return ShapeRelationState.Overlapped;
                }
                if (Geometry.GetLinesIntersectionPoint(p1, p2, rectangle.B, rectangle.D) != null)
                {
                    return ShapeRelationState.Overlapped;
                }
                if (Geometry.GetLinesIntersectionPoint(p1, p2, rectangle.D, rectangle.C) != null)
                {
                    return ShapeRelationState.Overlapped;
                }
                if (Geometry.GetLinesIntersectionPoint(p1, p2, rectangle.C, rectangle.A) != null)
                {
                    return ShapeRelationState.Overlapped;
                }
                return ShapeRelationState.NoRelation;
            }
            throw new InvalidOperationException(Constants.Messages.NotSupportedShape);
        }
        
        public static bool IsShapeInArea(this Shape shape, IList<Shape> shapes)
        {
            var states = shapes.Select((s) => new { State = shape.GetState(s), Shape = s } );
            if (states.Any(s => s.State == ShapeRelationState.Inside))
            {
                return true;
            }
            IList<Shape> overlapped = states
                .Where(s => s.State == ShapeRelationState.Overlapped)
                .Select(s => s.Shape).ToList();
            if (!overlapped.Any())
            {
                return false;
            }
            List<Point> points = new List<Point>();
            if (shape is Circle)
            {
                Circle circle = (Circle)shape;
                int pointCount = (int)(circle.Length / Constants.Map.CheckRange);
                for (int i = 0; i<pointCount; i++)
                {
                    points.Add(new Point()
                    {
                        X = (int)(circle.Center.X + Math.Cos(i * 2 * Math.PI / pointCount) * circle.Radius),
                        Y = (int)(circle.Center.Y + Math.Sin(i * 2 * Math.PI / pointCount) * circle.Radius)
                    });
                }
            }
            if (shape is Rectangle)
            {
                Rectangle rectangle = (Rectangle)shape;
                double distance = Constants.Map.CheckRange;
                while(distance < rectangle.Height)
                {
                    points.Add(new Point() {
                        X = rectangle.A.X + (int)((rectangle.B.X - rectangle.A.X) * distance / rectangle.Height),
                        Y = rectangle.A.Y + (int)((rectangle.B.Y - rectangle.A.Y) * distance / rectangle.Height)
                    });
                    points.Add(new Point()
                    {
                        X = rectangle.C.X + (int)((rectangle.D.X - rectangle.C.X) * distance / rectangle.Height),
                        Y = rectangle.C.Y + (int)((rectangle.D.Y - rectangle.C.Y) * distance / rectangle.Height)
                    });
                    distance += Constants.Map.CheckRange;
                }
                distance = Constants.Map.CheckRange;
                while (distance < rectangle.Width)
                {
                    points.Add(new Point()
                    {
                        X = rectangle.B.X + (int)((rectangle.D.X - rectangle.B.X) * distance / rectangle.Width),
                        Y = rectangle.B.Y + (int)((rectangle.D.Y - rectangle.B.Y) * distance / rectangle.Width)
                    });
                    points.Add(new Point()
                    {
                        X = rectangle.A.X + (int)((rectangle.C.X - rectangle.A.X) * distance / rectangle.Width),
                        Y = rectangle.A.Y + (int)((rectangle.C.Y - rectangle.A.Y) * distance / rectangle.Width)
                    });
                    distance += Constants.Map.CheckRange;
                }
            }
            foreach(Point p in points)
            {
                bool marked = false;
                foreach(Shape s in overlapped)
                {
                    if (Geometry.IsPointInsideShape(s,p))
                    {
                        marked = true;
                        break;
                    }
                }
                if (!marked)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
