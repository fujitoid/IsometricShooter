using System;
using Newtonsoft.Json;

namespace Shooter.Core.Model.Player.Weapone
{
    [Serializable]
    public class Granate
    {
        [JsonProperty] private GranateType _type;
        [JsonProperty] private int _count;
        [JsonProperty] private float _distance;
        [JsonProperty] private float _damage;

        [JsonIgnore] public GranateType Type => _type;
        [JsonIgnore] public int Count => _count;
        [JsonIgnore] public float Distance => _distance;
        [JsonIgnore] public float Damage => _damage;

        [JsonConstructor]
        public Granate()
        {
            
        }

        internal Granate SetType(GranateType type)
        {
            _type = type;
            return this;
        }

        internal Granate SetCount(int count)
        {
            _count = count;
            return this;
        }

        internal Granate SetDistance(float distance)
        {
            _distance = distance;
            return this;
        }

        internal Granate SetDamage(float damage)
        {
            _damage = damage;
            return this;
        }
    }
}
