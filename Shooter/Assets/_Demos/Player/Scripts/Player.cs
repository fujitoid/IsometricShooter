using Shooter.UI.HUD;
using UnityEngine;

public class Player : SingletoneMonoBehavior<Player>, IPlayer
{
    [SerializeField] private float _health;

    protected override void Initialization()
    {
        base.Initialization();
        HUD.Instance.HealthBar.Construct(_health);
    }

    public void SetDamage(float damage)
    {
        _health -= damage;
        HUD.Instance.HealthBar.SetNewHealth(_health);
    }
}
