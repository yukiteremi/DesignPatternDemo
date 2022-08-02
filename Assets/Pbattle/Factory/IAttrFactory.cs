using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttrFactory 
{
	public abstract SoldierAttr GetSoldierAttr(int AttrID);
	public abstract WeaponAttr GetWeaponAttr(int AttrID);
}
