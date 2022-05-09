using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Shooter.Core.Model.Player.Weapone
{
    [Serializable]
    public class ModuleData
    {
        [JsonProperty] private int _id;
        [JsonProperty] private int _lvl;
        [JsonProperty] private ModuleType _type;
        [JsonProperty] private List<BonusData> _bonuses;

        [JsonIgnore] public int ID => _id;
        [JsonIgnore] public int Lvl => _lvl;
        [JsonIgnore] public ModuleType Type => _type;
        [JsonIgnore] private IReadOnlyList<BonusData> Bonuses => _bonuses;

        [JsonConstructor]
        public ModuleData()
        {
            _bonuses = new List<BonusData>();
        }
    }
}
