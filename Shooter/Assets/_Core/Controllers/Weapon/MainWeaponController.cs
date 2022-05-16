using Shooter.Core.Model.Player.Weapone;
using Shooter.Core.Model.Player.Weapone.Bullet;
using UnityEngine;

namespace Shooter.Core.Controllers.Weapon
{
    public class MainWeaponController : ScriptableObject
    {
        private MainWeapon _testWeapom = null;

        public MainWeapon TestWeapon => _testWeapom;

        public void CreateMainWeaponTest()
        {
            var bullet = new Bullet()
                .SetDamage(20)
                .SetSpeed(20)
                .SetDistance(20);

            var data = new MainWeaponData()
                .SetCanShoot(true)
                .SetBullet(bullet)
                .SetScatter(1)
                .SetShootingSpeed(.5f)
                .SetCurrentBulletsCount(999)
                .SetOverhotPercent(.5f)
                .SetOverhotFromShoot(3f)
                .SetCurrentOverhot(0);
            _testWeapom = new MainWeapon()
                .SetId(1)
                .SetType(WeaponeType.Riffle)
                .SetData(data);
        }

        public bool TryShoot(MainWeapon mainWeapon)
        {
            if (mainWeapon == null)
                return false;

            if (mainWeapon.Data.CanShoot == false)
                return mainWeapon.Data.CanShoot;

            if (mainWeapon.Data.CurrentCountOfBullets.Value <= 0)
                return false;

            mainWeapon.Data.SetCurrentBulletsCount(mainWeapon.Data.CurrentCountOfBullets.Value - 1);
            mainWeapon.Data.SetCurrentOverhot(mainWeapon.Data.CurrentOverhot.Value + mainWeapon.Data.OverhotPercent);

            if (mainWeapon.Data.CurrentOverhot.Value >= 1)
                mainWeapon.Data.SetCanShoot(false);

            return true;
        }

        public void UpdateOverhot(MainWeapon mainWeapon, float decriment)
        {
            if (mainWeapon.Data.CurrentOverhot.Value <= 0.0066)
            {
                mainWeapon.Data.SetCanShoot(true);
                return;
            }


            mainWeapon.Data.SetCurrentOverhot(mainWeapon.Data.CurrentOverhot.Value - decriment);
        }
    }
}
