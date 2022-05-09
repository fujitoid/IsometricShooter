using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.UI.HUD
{
    public abstract class HUDBehaviour : MonoBehaviour
    {
        public abstract void Enable();
        public abstract void Disable();
    } 
}
