using Shooter.AI.Context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.AI.Ghosts.Weapon.Distance.Streight
{
    public class DistanceStreightWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private Bullet _bullet;
        [Space]
        [SerializeField] private Transform _fromShootPoint;
        [SerializeField] private float _distance;

        public void Attack()
        {
            var toShootPoint = _fromShootPoint.position + _fromShootPoint.forward * _distance;

            if(Physics.Raycast(_fromShootPoint.position, toShootPoint, out var hit, _distance))
            {
                Player player = hit.collider.gameObject.GetComponent<Player>();

                if(player != null)
                {
                    toShootPoint = hit.point;
                }
            }

            var bullet = Instantiate(_bullet, _fromShootPoint.position, Quaternion.LookRotation((toShootPoint - _fromShootPoint.position).normalized));
            bullet.OnCreate();
        }
    }
}
