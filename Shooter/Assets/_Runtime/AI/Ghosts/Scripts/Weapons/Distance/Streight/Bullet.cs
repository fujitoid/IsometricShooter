using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shooter.AI.Ghosts.Weapon.Distance.Streight
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [Space]
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;

        public void OnCreate()
        {
            _rigidbody.AddForce(transform.forward * _speed, ForceMode.VelocityChange);

            StartCoroutine(LifeCycleRoutine());
        }

        private IEnumerator LifeCycleRoutine()
        {
            yield return new WaitForSeconds(1f);
            Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            Player player = other.gameObject.GetComponent<Player>();

            if(player != null)
            {
                player.SetDamage(_damage);
                Destroy(this.gameObject);
            }
        }
    } 
}
