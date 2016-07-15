using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.PathAlgorithmAnalyser.ViewEntities;

namespace TD.PathAlgorithmAnalyser.Mappers
{
    public static class Mapper
    {
        public static Location.Shapes.Shape ToShape(this ViewShape shape)
        {
            return shape.DataShape;
        }

        public static ViewShape ToViewShape(this Location.Shapes.Shape shape)
        {
            if (shape is Location.Shapes.Circle)
            {
                return new ViewCircle((Location.Shapes.Circle)shape);
            }
            if (shape is Location.Shapes.Rectangle)
            {
                return new ViewRectangle((Location.Shapes.Rectangle)shape);
            }
            throw new InvalidOperationException("Not such transform implemented");
        }

        public static ViewUnit ToViewUnit(this Units.Unit unit)
        {
            return new ViewUnit(unit);
        }
    }
}
