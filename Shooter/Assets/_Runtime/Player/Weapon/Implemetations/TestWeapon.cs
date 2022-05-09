using Shooter.AI.Context;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestWeapon : PlayerWeapon
{
    [SerializeField] private InputAction _onShoot;
    [Space]
    [SerializeField] private TestBullet _testBullet;
    [Space, Header("Settings")]
    [SerializeField] private Transform _fromShootPoint;
    [SerializeField] private float _distance;

    private bool _isHolded;
    private Coroutine _shootRoutine;

    private void OnEnable()
    {
        _onShoot.Enable();
        _onShoot.started += OnHoldBegin;
        _onShoot.canceled += OnHoldFinish;
    }

    private void OnDisable()
    {
        _onShoot.started -= OnHoldBegin;
        _onShoot.canceled -= OnHoldFinish;
        _onShoot.Disable();
    }

    private void Update()
    {
        if(gameObject.activeSelf == false)
            return;
        
        if(_isHolded)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_shootRoutine != null)
            return;

        _shootRoutine = StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine()
    {
        var toShootPoint = _fromShootPoint.position + _fromShootPoint.forward * _distance;

        if(Physics.Raycast(_fromShootPoint.position, toShootPoint, out var hitInfo, _distance))
        {
            IEnemy enemy = hitInfo.collider.gameObject.GetComponent<IEnemy>();

            if(enemy != null)
            {
                toShootPoint = hitInfo.point;
            }
        }

        var bullet = Instantiate(_testBullet, _fromShootPoint.position, Quaternion.LookRotation((toShootPoint - _fromShootPoint.position).normalized));
        bullet.OnCreate();

        yield return new WaitForSeconds(.3f);
        yield return null;

        _shootRoutine = null;
    }

    private void OnDrawGizmos()
    {
        if(gameObject.activeSelf == false)
            return;
        
        var toShootPoint = _fromShootPoint.position + _fromShootPoint.forward * _distance;

        if(Physics.Raycast(_fromShootPoint.position, toShootPoint, out var hitInfo))
        {
            IEnemy enemy = hitInfo.collider.gameObject.GetComponent<IEnemy>();
            if(enemy != null)
            {
                toShootPoint = hitInfo.point;
            }
        }

        Gizmos.DrawLine(_fromShootPoint.position, toShootPoint);
    }

    private void OnHoldBegin(InputAction.CallbackContext context) => _isHolded = true;
    private void OnHoldFinish(InputAction.CallbackContext context) => _isHolded = false;
}
