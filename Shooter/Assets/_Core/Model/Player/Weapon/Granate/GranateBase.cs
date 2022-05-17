using System;
using Newtonsoft.Json;

namespace Shooter.Core.Model.Player.Weapone
{
    [Serializable]
    public abstract class GranateBase
    {
        [JsonProperty] protected int _count;
        [JsonProperty] protected float _distance;
        [JsonProperty] protected float _damage;
        [JsonProperty] protected float _radius;
        [JsonProperty] protected float _reloadTime;

        [JsonIgnore] public int Count => _count;
        [JsonIgnore] public float Distance => _distance;
        [JsonIgnore] public float Damage => _damage;
        [JsonIgnore] public float Radius => _radius;
        [JsonIgnore] public float ReloadTime => _reloadTime;

        internal GranateBase SetReloadTime(float time)
        {
            _reloadTime = time;
            return this;
        }

        internal GranateBase SetRadius(float radius)
        {
            _radius = radius;
            return this;
        }

        internal GranateBase SetCount(int count)
        {
            _count = count;
            return this;
        }

        internal GranateBase SetDistance(float distance)
        {
            _distance = distance;
            return this;
        }

        internal GranateBase SetDamage(float damage)
        {
            _damage = damage;
            return this;
        }
    }
}
