using Shooter.AI.Context;
using Shooter.Core.Model.Player.Weapone.Bullet;
using System.Collections;
using UnityEngine;

namespace Shooter.Runtime.Weapone.Main
{
    public class TestBullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _startPoint;
        private Bullet _bullet;

        public void Construct(Bullet bullet, Vector3 startPoint)
        {
            _bullet = bullet;
            _startPoint = startPoint;
        }

        public void OnCreate()
        {
            _rigidbody.AddForce(transform.forward * _bullet.Speed, ForceMode.VelocityChange);

            StartCoroutine(LifeCycleRoutine());
        }

        private IEnumerator LifeCycleRoutine()
        {
            yield return new WaitUntil(() => Vector3.Distance(_startPoint, transform.position) >= _bullet.Distance);
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            IEnemy enemy = other.gameObject.GetComponent<IEnemy>();

            if (enemy != null)
            {
                enemy.SetDamage(_bullet.Damage);
                Destroy(this.gameObject);
            }
        }
    } 
}
