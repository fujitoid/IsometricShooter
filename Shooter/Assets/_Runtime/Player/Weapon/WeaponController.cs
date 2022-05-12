using System.Collections;
using Shooter.Core.Controllers.Weapon;
using Shooter.Core.Model.Player.Weapone;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private MainWeaponController _weaponController;
    [Space]
    [SerializeField] private TestWeapon _testWeapon;

    private Coroutine _shootRoutine;
    private Coroutine _reloadRoutine;
    private Coroutine _overhotRoutine;

    private bool _isShootHolded = false;
    private bool _isGranageHolded = false;

    private void Start()
    {
        _testWeapon.Construct(_weaponController.TestWeapon);

        Player.Instance.PlayerInput.Shoot.Enable();
        Player.Instance.PlayerInput.DropGranate.Enable();

        Player.Instance.PlayerInput.Shoot.performed += Shoot;
        Player.Instance.PlayerInput.Shoot.started += x => _isShootHolded = true;
        Player.Instance.PlayerInput.Shoot.canceled += x => _isShootHolded = false;

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
            if (_reloadRoutine != null)
                yield return _reloadRoutine;

            if (_weaponController.TryShoot(_weaponController.TestWeapon))
            {
                _testWeapon.Shoot();

                if (_weaponController.TestWeapon.Data.CurrentOverhot.Value > 0)
                    UpdateOverhot(_weaponController.TestWeapon.Data.CurrentOverhot.Value);

                _reloadRoutine = StartCoroutine(RealoadRoutine(_weaponController.TestWeapon));
            }

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
        var dicrement = currentOverhot * Time.deltaTime;

        while(coldTime > 0)
        {
            coldTime -= Time.deltaTime;
            _weaponController.UpdateOverhot(_weaponController.TestWeapon, dicrement);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
