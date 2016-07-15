using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Buffs;
using TD.Location;
using TD.Location.Shapes;

namespace TD.Units
{
    public abstract class Unit : ILocatable
    {
        public AllowedLocation AllowedLocationState { get; protected set; }

        public LocationCollision LocationCollisionState { get; protected set; }

        public Shape Shape { get; protected set; }

        private bool _isValid;
        public virtual bool IsValid
        {
            get
            {
                return _isValid;
            }
            internal set
            {
                if (value == _isValid) return;
                _isValid = value;
                var handler = OnValidStateChanged;
                handler(this, new EventArgs());
            }
        }

        public event EventHandler OnUnitMove = delegate { };
        public event EventHandler OnValidStateChanged = delegate { };

        protected Unit(Shape shape)
        {
            Shape = shape;
            Shape.Changed += Shape_Changed;
        }

        private void Shape_Changed(object sender, EventArgs e)
        {
            var handler = OnUnitMove;
            handler(this, new EventArgs());
        }

        public virtual List<Buff> Buffs { get; } = new List<Buff>();
    }
}
