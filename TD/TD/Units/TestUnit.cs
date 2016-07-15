using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Location;
using TD.Location.Shapes;

namespace TD.Units
{
    public class TestUnit : Unit
    {
        public TestUnit(Point spawnPoint) : base(new Circle(spawnPoint, Constants.Units.DefaultMonsterSize))
        {
            AllowedLocationState = AllowedLocation.OnRoad;
            LocationCollisionState = LocationCollision.ShapeCollision;
        }
    }
}
