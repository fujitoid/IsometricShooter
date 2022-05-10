using System;
using Newtonsoft.Json;
using Shooter.Core.Model.Player.State;

namespace Shooter.Core.Model.Player
{
    [Serializable]
    public class Player
    {
        [JsonProperty] private string _name;
        [JsonProperty] private PlayerState _state;

        [JsonProperty] private Inventory.Inventory _inventory;

        [JsonIgnore] public PlayerState State => _state;
        [JsonIgnore] public Inventory.Inventory Inventory => _inventory;

        [JsonConstructor]
        public Player()
        {
            _inventory = new Inventory.Inventory();
            _state = new PlayerState();
        }

        internal Player SetName(string name)
        {
            _name = name;
            return this;
        }
    }
}
