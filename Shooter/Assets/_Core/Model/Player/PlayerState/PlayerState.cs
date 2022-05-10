using System;
using Newtonsoft.Json;
using Shooter.Core.Model.Misc.CoreReactive;

namespace Shooter.Core.Model.Player.State
{
    [Serializable]
    public class PlayerState
    {
        [JsonProperty] private float _fullHealth;
        [JsonProperty] private float _fullShield;
        
        [JsonProperty] private CoreReactiveProperty<float> _health;
        [JsonProperty] private CoreReactiveProperty<float> _shield;

        [JsonIgnore] public float FullHealth => _fullHealth;
        [JsonIgnore] public float FullShield => _fullShield;

        [JsonIgnore] public CoreReactiveProperty<float> Health => _health;
        [JsonIgnore] public CoreReactiveProperty<float> Shield => _shield;

        [JsonConstructor]
        public PlayerState()
        {
            _health = new CoreReactiveProperty<float>();
            _shield = new CoreReactiveProperty<float>();
        }
    }
}
