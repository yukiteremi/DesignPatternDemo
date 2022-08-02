using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : IWeaponFactory
{
    public WeaponFactory()
    {
    }

    public override IWeapon CreateWeapon(ENUM_Weapon emWeapon)
    {
        IWeapon Weapon = null;
        string AssetName = ""; 
        int AttrID = 0; 	
        switch (emWeapon)
        {
            case ENUM_Weapon.Null:
                break;
            case ENUM_Weapon.Gun:
                Weapon = new WeaponGun();
                AssetName = WeaponGun.GetClassName();
                AttrID = (int)emWeapon;
                break;
            case ENUM_Weapon.Rifle:
                break;
            case ENUM_Weapon.Rocket:
                break;
            case ENUM_Weapon.Max:
                break;
            default:
                break;
        }
        GameObject clone = null;
        Weapon.SetGameObject(clone);
        WeaponAttr theWeaponAttr = FacadeFactory.GetSingelton().GetAttrFactory().GetWeaponAttr(AttrID);
        Weapon.SetData(theWeaponAttr);
        return Weapon;
    }
}
