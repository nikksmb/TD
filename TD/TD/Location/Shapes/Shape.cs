using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Location.Shapes
{
    public abstract class Shape
    {
        private Point _center;
        public Point Center { get { return _center; }
            set
            {
                RecalculateChanges(value);
                _center = value;
                InvokeChanged();
            }
        }
        public event EventHandler Changed = delegate { };
        public virtual void InvokeChanged()
        {
            Changed.Invoke(_center, new EventArgs());
        }

        /// <summary>
        /// Recalculate shape parameters according center changes
        /// </summary>
        public abstract void RecalculateChanges(Point newCenter);
    }
}
