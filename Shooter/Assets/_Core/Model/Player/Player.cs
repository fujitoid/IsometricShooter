using System;
using Newtonsoft.Json;
using Shooter.Core.Model.Player.Weapone;
using Shooter.Core.Model.ReactiveProperty;

namespace Shooter.Core.Model.Player
{
    [Serializable]
    public class Player
    {
        [JsonProperty] private string _name;
        [JsonProperty] private CoreReactiveProperty<float> _health;
        [JsonProperty] private CoreReactiveProperty<int> _money;

        [JsonProperty] private CoreReactiveProperty<MainWeapon> _weapon;
        
        [JsonIgnore] private string Name => _name;
        [JsonIgnore] private CoreReactiveProperty<float> Health => _health;
        [JsonIgnore] private CoreReactiveProperty<int> Money => _money;

        [JsonIgnore] private CoreReactiveProperty<MainWeapon> Weapon => _weapon;

        [JsonConstructor]
        public Player()
        {
            _health = new CoreReactiveProperty<float>(0);
            _money = new CoreReactiveProperty<int>(0);
            _weapon = new CoreReactiveProperty<MainWeapon>(new MainWeapon());
        }

        internal Player SetName(string name)
        {
            _name = name;
            return this;
        }
    }
}
