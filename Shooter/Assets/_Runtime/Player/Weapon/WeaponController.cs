using System.Collections;
using Shooter.Core.Controllers.Weapon;
using Shooter.Core.Model.Player.Weapone;
using Shooter.Runtime.Weapone.Granate;
using Shooter.Runtime.Weapone.Main;
using Shooter.UI.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter.Runtime.Weapone
{
    public class WeaponController : MonoBehaviour, IInputReciver
    {
        [SerializeField] private MainWeaponController _weaponController;
        [Space]
        [SerializeField] private TestWeapon _testWeapon;
        [Space]
        [SerializeField] private GranateAim _granateAim;
        [SerializeField] private GranateDroper _granateDroper;

        private Coroutine _shootRoutine;
        private Coroutine _reloadRoutine;
        private Coroutine _overhotRoutine;

        private bool _isShootHolded = false;
        private bool _isGranageHolded = false;

        private void Start()
        {
            _testWeapon.Construct(_weaponController.TestWeapon);
            _granateAim.SetContext(this);

            Player.Instance.PlayerInput.Shoot.Enable();
            Player.Instance.PlayerInput.DropGranate.Enable();

            Player.Instance.PlayerInput.Shoot.performed += Shoot;
            Player.Instance.PlayerInput.Shoot.started += x => _isShootHolded = true;
            Player.Instance.PlayerInput.Shoot.canceled += x => _isShootHolded = false;

            Player.Instance.PlayerInput.DropGranate.performed += x => _granateAim.DrawAim();
            Player.Instance.PlayerInput.DropGranate.started += x => _isGranageHolded = true;
            Player.Instance.PlayerInput.DropGranate.canceled += x => _isGranageHolded = false;
        }

        private void OnDestroy()
        {
            Player.Instance.PlayerInput.Shoot.Disable();
            Player.Instance.PlayerInput.DropGranate.Disable();

            Player.Instance.PlayerInput.Shoot.performed -= Shoot;
            Player.Instance.PlayerInput.Shoot.started -= x => _isShootHolded = true;
            Player.Instance.PlayerInput.Shoot.canceled -= x => _isShootHolded = false;

            Player.Instance.PlayerInput.DropGranate.performed -= x => _granateAim.DrawAim();
            Player.Instance.PlayerInput.DropGranate.started -= x => _isGranageHolded = true;
            Player.Instance.PlayerInput.DropGranate.canceled -= x => _isGranageHolded = false;
        }

        private void Shoot(InputAction.CallbackContext context)
        {
            if (_shootRoutine != null)
            {
                StopCoroutine(_shootRoutine);
                _shootRoutine = null;
            }

            _shootRoutine = StartCoroutine(ShootRoutine());
        }

        private IEnumerator ShootRoutine()
        {
            while (_isGranageHolded == false && _isShootHolded)
            {
                if (_weaponController.TryShoot(_weaponController.TestWeapon))
                {
                    _testWeapon.Shoot();

                    if (_weaponController.TestWeapon.Data.CurrentOverhot.Value > 0)
                        UpdateOverhot(_weaponController.TestWeapon.Data.CurrentOverhot.Value);

                    _reloadRoutine = StartCoroutine(RealoadRoutine(_weaponController.TestWeapon));
                }

                if (_reloadRoutine != null)
                    yield return _reloadRoutine;

                yield return null;
            }

            if(_isGranageHolded)
            {
                _granateDroper.Drop();
                yield return null;
            }
        }

        private IEnumerator RealoadRoutine(MainWeapon mainWeapon)
        {
            yield return new WaitForSeconds(_weaponController.TestWeapon.Data.SpeedOfShooting);
            _reloadRoutine = null;
        }

        private void UpdateOverhot(float currentOverhot)
        {
            if (_overhotRoutine != null)
            {
                StopCoroutine(_overhotRoutine);
                _overhotRoutine = null;
            }

            _overhotRoutine = StartCoroutine(UpdateoverhotRoutine(currentOverhot));
        }

        private IEnumerator UpdateoverhotRoutine(float currentOverhot)
        {
            if (currentOverhot <= 0)
                yield break;

            var coldTime = currentOverhot / _weaponController.TestWeapon.Data.OverhotPercent * _weaponController.TestWeapon.Data.OverhotFromShoot;
            var decriment = currentOverhot / coldTime * Time.fixedDeltaTime;

            while (coldTime > 0)
            {
                coldTime -= Time.fixedDeltaTime;

                _weaponController.UpdateOverhot(_weaponController.TestWeapon, decriment);
                yield return new WaitForSeconds(Time.fixedDeltaTime);
            }
        }

        public bool IsHolded() => _isGranageHolded;
    } 
}
