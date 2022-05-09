using System;

namespace Shooter.AI.Context
{
    public interface IEnemy
    {
        public void SetDamage(float damage);
        public void OnDeath(Action<IEnemy> action);
    } 
}
