using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Shooter.Core.Model.Player.Weapone
{
    [Serializable]
    public class BonusData : MonoBehaviour
    {
        [JsonProperty] private BonusType _bonusType;
        [JsonProperty] private float _value;

        [JsonIgnore] public BonusType BonusType => _bonusType;
        [JsonIgnore] public float Value => _value;

        [JsonConstructor]
        public BonusData()
        {
            
        }
    }
}
