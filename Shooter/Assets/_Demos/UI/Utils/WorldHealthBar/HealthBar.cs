using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.UI.Utils
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;

        private float _currentHealth;
        private float _maxHealth;

        public void Construct(float health)
        {
            _currentHealth = _maxHealth = health;

            _healthBar.fillAmount = _currentHealth / _maxHealth;
        }

        public void SetNewHealth(float health)
        {
            _currentHealth = health;
            _healthBar.fillAmount = _currentHealth / _maxHealth;
        }

        private void Update()
        {
            transform.LookAt(Camera.main.transform.position);
        }
    } 
}
