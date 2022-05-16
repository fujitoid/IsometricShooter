using DG.Tweening;
using Shooter.Core.Controllers.Weapon;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UI.HUD
{
    public class WeaponScreen : HUDBehaviour
    {
        [SerializeField] private MainWeaponController _weaponController;
        [Space]
        [SerializeField] private Slider _slider;
        [Space]
        [SerializeField] private TextMeshProUGUI _bulletsValue;
        [SerializeField] private Image _reloadFill;

        private Sequence _sequence;

        private void OnEnable()
        {
            _weaponController.TestWeapon.Data.CurrentOverhot.OnValueChanged += UpdateOverhot;
            _weaponController.TestWeapon.Data.CurrentCountOfBullets.OnValueChanged += OnBulletCountChanged;
        }

        private void OnDisable()
        {
            _weaponController.TestWeapon.Data.CurrentOverhot.OnValueChanged -= UpdateOverhot;
            _weaponController.TestWeapon.Data.CurrentCountOfBullets.OnValueChanged -= OnBulletCountChanged;
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

        private void OnBulletCountChanged(int bullets)
        {
            _bulletsValue.text = $"Bullets: {bullets}";

            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _reloadFill.fillAmount = 0;

            _sequence
                .Append(_reloadFill.DOFillAmount(1, _weaponController.TestWeapon.Data.SpeedOfShooting));
        }

        private void UpdateOverhot(float currentOverhot)
        {
            _slider.value = currentOverhot;
        }
    }
}
