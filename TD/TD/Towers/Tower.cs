using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Location;
using TD.Location.Shapes;

namespace TD.Towers
{
    public abstract class Tower : ILocatable
    {
        public virtual int Range { get; set; }
        public virtual int Effect { get; set; }
        public virtual int Speed { get; set; }

        public virtual int BaseRange { get; set; }
        public virtual int BaseEffect { get; set; }
        public virtual int BaseSpeed { get; set; }

        public virtual TowerActionState ActionState { get; set; }
        public virtual TowerTargetingStyle TargetingStyle { get; set; }
        public virtual TowerTargetingPriority TargetingPriority { get; set; }

        public virtual TowerLiveState LiveState { get; set; }
        
        public virtual AllowedLocation AllowedLocationState { get; protected set; }

        public LocationCollision LocationCollisionState { get; protected set; }

        public Shape Shape { get; protected set; }

        public void ResetParams()
        {
            Range = BaseRange;
            Effect = BaseEffect;
            Speed = BaseSpeed;
        }
    }
}
