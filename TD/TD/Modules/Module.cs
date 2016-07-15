using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.Module
{
    public abstract class Module
    {
        public int Power { get; private set; }
        public int ModifiedPower { get; private set; }

        public abstract void OnInstall();

        public abstract void OnWork();

        public abstract void OnDestroy();
    }
}
