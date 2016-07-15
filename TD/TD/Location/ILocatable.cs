using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Location.Shapes;

namespace TD.Location
{
    public interface ILocatable
    {
        AllowedLocation AllowedLocationState { get; }
        LocationCollision LocationCollisionState { get; }
        Shape Shape { get; }
    }

    public enum AllowedLocation
    {
        OnRoad,
        OnHighground,
        Any
    }

    public enum LocationCollision
    {
        None,
        ShapeCollision,
        InversedCollision
    }
}
