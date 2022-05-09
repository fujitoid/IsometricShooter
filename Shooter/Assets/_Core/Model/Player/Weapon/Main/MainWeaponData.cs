using System;
using Newtonsoft.Json;

namespace Shooter.Core.Model.Player.Weapone
{
    [Serializable]
    public class MainWeaponData
    {
        [JsonProperty] private float _speedOfShooting;
        [JsonProperty] private float _damage;
        [JsonProperty] private float _distance;
        [JsonProperty] private float _scatter;
        [JsonProperty] private float _reloadSpeed;
        
        [JsonProperty] private int _countOfBulletsFull;
        [JsonProperty] private int _currentCountOfBullets;
        
        [JsonIgnore] public float SpeedOfShooting => _speedOfShooting;
        [JsonIgnore] public float Damage => _damage;
        [JsonIgnore] public float Distance => _distance;
        [JsonIgnore] public float Scatter => _scatter;
        [JsonIgnore] public float ReloadSpeed => _reloadSpeed;
        
        [JsonIgnore] public int CountOfBulletsFull => _countOfBulletsFull;
        [JsonIgnore] public int CurrentCountOfBullets => _currentCountOfBullets;

        [JsonConstructor]
        public MainWeaponData()
        {
            
        }
        
        internal MainWeaponData SetShootingSpeed(float speed)
        {
            _speedOfShooting = speed;
            return this;
        }

        internal MainWeaponData SetDamage(float damage)
        {
            _damage = damage;
            return this;
        }
        
        internal MainWeaponData SetDistance(float distance)
        {
            _distance = distance;
            return this;
        }  
        
        internal MainWeaponData SetScatter(float scatter)
        {
            _scatter = scatter;
            return this;
        }  
        
        internal MainWeaponData SetReloadSpeed(float speed)
        {
            _reloadSpeed = speed;
            return this;
        }   
        
        internal MainWeaponData SetFullBulletCount(int count)
        {
            _countOfBulletsFull = count;
            return this;
        }   
        
        internal MainWeaponData SetCurrentBulletsCount(int count)
        {
            _currentCountOfBullets = count;
            return this;
        }
    }
}
