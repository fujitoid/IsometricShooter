using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter.UI.Core.Inputs
{
    public class UIInputsContainer : ScriptableObject
    {
        [SerializeField] private InputAction _mousePosition;
        [SerializeField] private InputAction _callGranateAim;

        public InputAction MousePosition => _mousePosition;
        public InputAction CallGranateAim => _callGranateAim;
    } 
}
