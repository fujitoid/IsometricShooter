using Shooter.AI.Context;
using UnityEngine;

public class CloseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    public void Attack()
    {
        if (Vector3.Distance(Player.Instance.gameObject.transform.position, gameObject.transform.position) <= _radius)
        {
            Player.Instance.SetDamage(_damage);
        }
    }
}
