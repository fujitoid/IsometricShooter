using UnityEngine;

namespace Shooter.UI.HUD
{
    public class HUD : SingletoneMonoBehavior<HUD>
    {
        [SerializeField] private HealthBar _healthBar;

        public HealthBar HealthBar => _healthBar;
    } 
}
