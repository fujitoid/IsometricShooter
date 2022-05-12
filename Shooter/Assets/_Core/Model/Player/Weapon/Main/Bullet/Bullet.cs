using System;
using Unity.Plastic.Newtonsoft.Json;

namespace Shooter.Core.Model.Player.Weapone.Bullet
{
    [Serializable]
    public class Bullet
    {
        [JsonProperty] private float _damage;
        [JsonProperty] private float _speed;
        [JsonProperty] private float _distance;

        [JsonIgnore] public float Damage => _damage;
        [JsonIgnore] public float Speed => _speed;
        [JsonIgnore] public float Distance => _distance;

        [JsonConstructor]
        public Bullet()
        {

        }

        internal Bullet SetDamage(float damage)
        {
            _damage = damage;
            return this;
        }

        internal Bullet SetSpeed(float speed)
        {
            _speed = speed;
            return this;
        }

        internal Bullet SetDistance(float distance)
        {
            _distance = distance;
            return this;
        }
    } 
}
