using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
    [SerializeField] protected WeaponType _type;

    public WeaponType Type => _type;
}
