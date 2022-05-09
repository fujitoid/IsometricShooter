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
        HUD.Instance.HealthBar.Construct(_health);
    }

    public void SetDamage(float damage)
    {
        _health -= damage;
        HUD.Instance.HealthBar.SetNewHealth(_health);
    }
}
