using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterFactory
{
	public abstract ISoldier CreateSoldier(ENUM_Soldier emSoldier, ENUM_Weapon emWeapon, int Lv, Vector3 SpawnPosition);


}
