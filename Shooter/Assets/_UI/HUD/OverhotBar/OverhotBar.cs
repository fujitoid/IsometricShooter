using Shooter.Core.Controllers.Weapon;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UI.HUD
{
    public class OverhotBar : HUDBehaviour
    {
        [SerializeField] private MainWeaponController _weaponController;
        [Space]
        [SerializeField] private Image _fill;

        private void OnEnable()
        {
            _weaponController.TestWeapon.Data.CurrentOverhot.OnValueChanged += UpdateOverhot;
        }

        private void OnDisable()
        {
            _weaponController.TestWeapon.Data.CurrentOverhot.OnValueChanged -= UpdateOverhot;
        }

        public override void Disable()
        {
            gameObject.SetActive(false);

            _weaponController.TestWeapon.Data.CurrentOverhot.OnValueChanged -= UpdateOverhot;
        }

        public override void Enable()
        {
            gameObject.SetActive(true);

            _weaponController.TestWeapon.Data.CurrentOverhot.OnValueChanged += UpdateOverhot;
        }

        private void UpdateOverhot(float currentOverhot)
        {
            _fill.fillAmount = currentOverhot;
        }
    }
}
