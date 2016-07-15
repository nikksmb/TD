using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Location.Shapes;

namespace TD.Location
{
    public class Road
    {
        private List<Point> roadPoints;
        private List<Shape> roadShapes;

        public Shape StartArea { get { return roadShapes.First(); } }
        public Shape FinishArea { get { return roadShapes.Last(); } }

        public Road(IList<Point> points)
        {
            roadPoints = new List<Point>(points);
            InitializeShapes();
        }

        public bool IsOnRoad(Point point)
        {
            foreach(Shape s in roadShapes)
            {
                if (Geometry.IsPointInsideShape(s,point))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsOnRoad(ILocatable location)
        {
            return location.Shape.IsShapeInArea(roadShapes);
        }

        public List<Shape> GetFigures()
        {
            return new List<Shape>(roadShapes);
        }

        private void InitializeShapes()
        {
            roadShapes = new List<Shape>();
            for (int i = 0; i < roadPoints.Count - 1; i++)
            {
                roadShapes.Add(new Circle(roadPoints[i], Constants.Map.RoadRadius));
                roadShapes.Add(new Rectangle(roadPoints[i], roadPoints[i + 1], Constants.Map.RoadWidth));
            }
            roadShapes.Add(new Circle(roadPoints.Last(), Constants.Map.RoadRadius));
        }
    }
}
