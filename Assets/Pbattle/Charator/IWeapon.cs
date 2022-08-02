using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ENUM_Weapon
{
	Null = 0,
	Gun = 1,
	Rifle = 2,
	Rocket = 3,
	Max,
}
public class IWeapon 
{
	protected ENUM_Weapon W_Enum = ENUM_Weapon.Null;

	// 數值
	protected int W_Power = 0;          
	protected WeaponAttr W_Attr = null;           
	protected GameObject W_Go = null;         
	protected ICharacter W_Charater = null;

    public IWeapon()
    {
	}
	public void SetGameObject(GameObject gameObject)
    {
		W_Go = gameObject;
	}
	public void SetData(WeaponAttr attr)
	{
		W_Attr = attr;
	}
	public GameObject GetWeaponGameObject()
    {
		return W_Go;
    }
	public WeaponAttr GetWeaponAttr()
    {
		return W_Attr;
    }
	public void SetWeaponOwner(ICharacter character)
    {
		W_Charater = character;
	}
	public int GetPower()
    {
		return W_Power+ W_Attr.Atk;
    }
}
