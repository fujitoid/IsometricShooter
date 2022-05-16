using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UI.HUD
{
    public class HealthBar : HUDBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _value;

        private float _currentHelth;
        private float _maxHealth;

        public void Construct(float health)
        {
            _maxHealth = _currentHelth = health;

            _slider.value = _currentHelth / _maxHealth;
            _value.text = GetValuePercent();
        }

        public override void Disable()
        {
            gameObject.SetActive(true);
        }

        public override void Enable()
        {
            gameObject.SetActive(false);
        }

        public void SetNewHealth(float health)
        {
            _currentHelth = health;
            _slider.value = _currentHelth / _maxHealth;
            _value.text = GetValuePercent();
        }

        private string GetValuePercent()
        {
            var value = Mathf.Clamp(_currentHelth / _maxHealth * 100f, 0, 100);
            return $"{value.ToString("N0")}%";
        }
    } 
}
