using UnityEngine;

namespace Shooter.UI.HUD
{
    public class HUD : SingletoneMonoBehavior<HUD>
    {
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private OverhotBar _overhotBar;

        public HealthBar HealthBar => _healthBar;
        public OverhotBar OverhotBar => _overhotBar;
    } 
}
