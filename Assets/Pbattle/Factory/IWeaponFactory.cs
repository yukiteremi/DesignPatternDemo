using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWeaponFactory 
{
    public abstract IWeapon CreateWeapon(ENUM_Weapon emWeapon);
}
