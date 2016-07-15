using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Location.Shapes
{
    public class Circle : Shape
    {
        private int _radius;
        public int Radius {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
                InvokeChanged();
            }
        }

        public double Length {
            get
            {
                return 2 * Math.PI * Radius;
            }
        }

        public Circle(Point center, int rad)
        {
            Center = center;
            Radius = rad;
        }

        public override void RecalculateChanges(Point newCenter)
        {

        }
    }
}
