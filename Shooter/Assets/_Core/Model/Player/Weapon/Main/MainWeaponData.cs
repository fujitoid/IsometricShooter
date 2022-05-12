using System;
using Newtonsoft.Json;
using Shooter.Core.Model.Misc.CoreReactive;

namespace Shooter.Core.Model.Player.Weapone
{
    [Serializable]
    public class MainWeaponData
    {
        [JsonProperty] private Bullet.Bullet _bullet;
        [JsonProperty] private float _speedOfShooting;
        [JsonProperty] private float _scatter;

        [JsonProperty] private float _overhotPercent;
        [JsonProperty] private float _overhotFromShoot;
        [JsonIgnore] private CoreReactiveProperty<float> _currentOverhotValue;
        
        [JsonProperty] private CoreReactiveProperty<int> _currentCountOfBullets;

        [JsonIgnore] private bool _canShoot;
        
        [JsonIgnore] public float SpeedOfShooting => _speedOfShooting;
        [JsonIgnore] public Bullet.Bullet Bullet => _bullet;        
        [JsonIgnore] public float Scatter => _scatter;

        [JsonIgnore] public float OverhotPercent => _overhotPercent;
        [JsonIgnore] public float OverhotFromShoot => _overhotFromShoot;
        [JsonIgnore] public CoreReactiveProperty<float> CurrentOverhot => _currentOverhotValue;
        
        [JsonIgnore] public CoreReactiveProperty<int> CurrentCountOfBullets => _currentCountOfBullets;

        [JsonIgnore] public bool CanShoot => _canShoot;

        [JsonConstructor]
        public MainWeaponData()
        {
            _currentCountOfBullets = new CoreReactiveProperty<int>();
            _currentOverhotValue = new CoreReactiveProperty<float>();
            _bullet = new Bullet.Bullet();
        }

        internal MainWeaponData SetCanShoot(bool canShoot)
        {
            _canShoot = canShoot;
            return this;
        }

        internal MainWeaponData SetBullet(Bullet.Bullet bullet)
        {
            _bullet = bullet;
            return this;
        }
        
        internal MainWeaponData SetShootingSpeed(float speed)
        {
            _speedOfShooting = speed;
            return this;
        }
        
        internal MainWeaponData SetScatter(float scatter)
        {
            _scatter = scatter;
            return this;
        }   
        
        internal MainWeaponData SetCurrentBulletsCount(int count)
        {
            _currentCountOfBullets.Value = count;
            return this;
        }

        internal MainWeaponData SetOverhotPercent(float percent)
        {
            _overhotPercent = percent;
            return this;
        }

        internal MainWeaponData SetOverhotFromShoot(float value)
        {
            _overhotFromShoot = value;
            return this;
        }

        internal MainWeaponData SetCurrentOverhot(float value)
        {
            _currentOverhotValue.Value = value;
            return this;
        }
    }
}
