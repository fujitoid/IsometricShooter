using Shooter.UI.Core.Inputs;
using UnityEngine;

namespace Shooter.UI.Core
{
    public class UIProvider : SingletoneMonoBehavior<UIProvider>
    {
        [SerializeField] private UIInputsContainer _inputsContainer;
        [Space]
        [SerializeField] private HUD.HUD _hud;

        public HUD.HUD HUD => _hud;
        public UIInputsContainer InputsContainer => _inputsContainer;
    } 
}
