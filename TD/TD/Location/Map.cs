using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Towers;
using TD.Units;
using TD.Location.Shapes;

namespace TD.Location
{
    public class Map
    {
        private List<ILocatable> objects;

        private List<Tower> towers;
        private List<Unit> units;

        public event EventHandler OnObjectAdded = delegate { };
        public event EventHandler OnObjectDeleted = delegate { };

        private Road _road;
        public Road Road {
            get
            {
                return _road;
            }
            set
            {
                _road = value;
            }
        }

        public Map()
        {
            objects = new List<ILocatable>();
            units = new List<Unit>();
            towers = new List<Tower>();
        }

        public void StartWave()
        {
            Unit unit = new TestUnit(Road.StartArea.Center);
            unit.OnUnitMove += Unit_OnUnitMove;
            objects.Add(unit);
            units.Add(unit);
            var handler = OnObjectAdded;
            handler(unit, new EventArgs());
        }

        private void Unit_OnUnitMove(object sender, EventArgs e)
        {
            Unit unit = (Unit)sender;
            bool result = true;
            //! switch/case
            //! transform into ILocatable event
            //! create own eventArgs if reasonable
            if (unit.AllowedLocationState == AllowedLocation.OnRoad)
            {
               result &= Road.IsOnRoad(unit);
            }
            
            if (unit.LocationCollisionState == LocationCollision.ShapeCollision)
            {
                //! need to rework
                result &= !unit.Shape.IsShapeInArea(objects.Select(o=>o.Shape)
                    .Except(new[] { unit.Shape }).ToList());
            }
            unit.IsValid = result;
        }
    }
}
