using System;
using System.Reflection;
using Newtonsoft.Json;
using Shooter.Core.Model.Player.Weapone;
using Shooter.Core.Model.Misc.CoreReactive;

namespace Shooter.Core.Model.Player.Inventory
{
    [Serializable]
    public class Inventory
    {
        [JsonProperty] private CoreReactiveProperty<int> _money;

        [JsonProperty] private CoreReactiveProperty<MainWeapon> _weapon;
        [JsonProperty] private CoreReactiveList<Granate> _granats;
        [JsonProperty] private CoreReactiveList<Module> _modules;
        
        [JsonIgnore] public CoreReactiveProperty<int> Money => _money;

        [JsonIgnore] public CoreReactiveProperty<MainWeapon> Weapon => _weapon;
        [JsonIgnore] public CoreReactiveList<Granate> Granats => _granats;
        [JsonIgnore] public CoreReactiveList<Module> Modules => _modules;

        [JsonConstructor]
        public Inventory()
        {
            _money = new CoreReactiveProperty<int>();
            
            _weapon = new CoreReactiveProperty<MainWeapon>();
            _granats = new CoreReactiveList<Granate>();
            _modules = new CoreReactiveList<Module>();
        }
    }
}
