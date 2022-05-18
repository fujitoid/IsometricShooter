using Shooter.Core.Model.Player.Weapone;
using UnityEngine;

namespace Shooter.Core.Controllers.Weapon
{
    public class GranateController : ScriptableObject
    {
        private SimpleGranate _simpleGranate;

        public SimpleGranate SimpleGranate => _simpleGranate;

        public void CreateSimpleGranate()
        {
            _simpleGranate = (SimpleGranate) new SimpleGranate()
                .SetExplodeTime(3)
                .SetCount(999)
                .SetDamage(100)
                .SetDistance(15)
                .SetRadius(50)
                .SetReloadTime(1);
        }

        public bool TryDropGranate(GranateBase granate)
        {
            if (granate.Count <= 0)
                return false;

            granate.SetCount(_simpleGranate.Count - 1);

            return true;
        }
    } 
}
