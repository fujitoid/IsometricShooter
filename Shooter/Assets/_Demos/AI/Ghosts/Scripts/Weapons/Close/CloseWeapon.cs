using Shooter.AI.Context;
using UnityEngine;

public class CloseWeapon : MonoBehaviour, IWeapon
{
    [SerializeField] private float _damage;
    [SerializeField] private float _radius;
    public void Attack()
    {
        var attackPoint = gameObject.transform.position + gameObject.transform.forward * _radius;

        if (Physics.Raycast(gameObject.transform.position, attackPoint, out var hit, _radius))
        {
            Player player = hit.collider.gameObject.GetComponent<Player>();

            if (player != null)
            {
                player.SetDamage(_damage);
            }
        }
    }
}
