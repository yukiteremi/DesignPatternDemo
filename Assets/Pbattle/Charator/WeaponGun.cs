using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : IWeapon
{
    public WeaponGun( ) 
    {
        W_Enum = ENUM_Weapon.Gun;
    }
    public static string GetClassName()
    {
        return "WeaponGun";

    }
}
