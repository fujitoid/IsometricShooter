using Shooter.Core.Model.Player.Weapone;
using UnityEngine;

public class MainWeaponController : ScriptableObject
{
    public MainWeapon GetMainWeaponTest()
    {
        var data = new MainWeaponData()
            .SetDamage(10)
            .SetDistance(20)
            .SetScatter(1)
            .SetReloadSpeed(1)
            .SetShootingSpeed(.2f)
            .SetCurrentBulletsCount(999)
            .SetFullBulletCount(999);
        return new MainWeapon()
            .SetId(1)
            .SetType(WeaponeType.Riffle)
            .SetModules(null)
            .SetData(data);
    }
}
