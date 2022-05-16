using UnityEngine;

namespace Shooter.UI.HUD
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private WeaponScreen _overhotBar;

        public HealthBar HealthBar => _healthBar;
        public WeaponScreen OverhotBar => _overhotBar;
    } 
}
