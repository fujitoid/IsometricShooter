using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shooter.Core.Model.Player.Weapone
{
    [Serializable]
    public class MainWeapon
    {
        [JsonProperty] private int _id;
        [JsonProperty] private WeaponeType _type;
        [JsonProperty] private List<ModuleData> _modules;
        [JsonProperty] private MainWeaponData _data;

        [JsonIgnore] public int ID => _id;
        [JsonIgnore] public WeaponeType Type => _type;
        [JsonIgnore] public IReadOnlyList<ModuleData> Modules => _modules;
        [JsonIgnore] public MainWeaponData Data => _data;

        [JsonConstructor]
        public MainWeapon()
        {
            _modules = new List<ModuleData>();
            _data = new MainWeaponData();
        }
    }
}
