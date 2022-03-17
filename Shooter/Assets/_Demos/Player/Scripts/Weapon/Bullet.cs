using Shooter.AI.Context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        IEnemy enemy = other.gameObject.GetComponent<IEnemy>();

        if(enemy != null)
        {
            enemy.SetDamage(_damage);
            Destroy(this.gameObject);
        }
    }
}
