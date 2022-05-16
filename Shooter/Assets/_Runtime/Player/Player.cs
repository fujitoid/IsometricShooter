using Shooter.UI.Core;
using Shooter.UI.HUD;
using UnityEngine;

public class Player : SingletoneMonoBehavior<Player>
{
    [SerializeField] private PlayerInputContainer _playerInput;
    
    [SerializeField] private float _health;

    public PlayerInputContainer PlayerInput => _playerInput;

    protected override void Initialization()
    {
        base.Initialization();
        UIProvider.Instance.HUD.HealthBar.SetNewHealth(_health);
    }

    public void SetDamage(float damage)
    {
        _health -= damage;
        UIProvider.Instance.HUD.HealthBar.SetNewHealth(_health);
    }
}
