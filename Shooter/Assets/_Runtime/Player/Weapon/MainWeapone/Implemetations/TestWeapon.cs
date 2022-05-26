using Shooter.AI.Context;
using Shooter.Core.Model.Player.Weapone;
using System.Collections;
using UnityEngine;

namespace Shooter.Runtime.Weapone.Main
{
    public class TestWeapon : MonoBehaviour, IPlayerWeapon
    {
        [SerializeField] private TestBullet _testBullet;
        [SerializeField] private GameObject _muzzleFire;
        [Space, Header("Settings")]
        [SerializeField] private Transform _fromShootPoint;

        private MainWeapon _mainWeapon;

        public void Construct(MainWeapon mainWeapon)
        {
            _mainWeapon = mainWeapon;
        }

        public void Shoot()
        {

            var toShootPoint = _fromShootPoint.position + _fromShootPoint.forward * _mainWeapon.Data.Bullet.Distance;

            if (Physics.Raycast(_fromShootPoint.position, toShootPoint, out var hitInfo, _mainWeapon.Data.Bullet.Distance))
            {
                IEnemy enemy = hitInfo.collider.gameObject.GetComponent<IEnemy>();

                if (enemy != null)
                {
                    toShootPoint = hitInfo.point;
                }
            }

            var bullet = Instantiate(_testBullet, _fromShootPoint.position, Quaternion.LookRotation((toShootPoint - _fromShootPoint.position).normalized));
            bullet.Construct(_mainWeapon.Data.Bullet, _fromShootPoint.position);
            bullet.OnCreate();
            StartCoroutine(MuzzleFireRoutine());
        }

        private IEnumerator MuzzleFireRoutine()
        {
            var playerRotation = Player.Instance.transform.rotation.eulerAngles;
            var muzzleRotation = new Vector3(playerRotation.x, playerRotation.y - 90, playerRotation.z);

            var muzzleFire = Instantiate(_muzzleFire, _fromShootPoint.position, Quaternion.Euler(muzzleRotation));
            yield return new WaitForSeconds(.1f);
            Destroy(muzzleFire.gameObject);
        }
    } 
}
