using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_Soldier
{
	Null = 0,
	Rookie = 1, 
	Sergeant = 2,  
	Captain = 3,   
	Max,
}

public abstract class ISoldier : ICharacter
{
	protected ENUM_Soldier m_emSoldier = ENUM_Soldier.Null;
	protected int m_MedalCount = 0;                
	protected const int MAX_MEDAL = 3;               
	protected const int MEDAL_VALUE_ID = 20;             

	public ISoldier()
	{
	}

	public ENUM_Soldier GetSoldierType()
	{
		return m_emSoldier;
	}

	public SoldierAttr GetSoldierValue()
	{
		return C_AttrData as SoldierAttr;
	}

	public override void UnderAttack(ICharacter Attacker)
	{
		C_AttrData.CalDmgValue(Attacker);

		if (C_AttrData.GetNowHP() <= 0)
		{
			DoPlayKilledSound();    
			DoShowKilledEffect();  
			//Killed();          
		}
	}

	public abstract void DoPlayKilledSound();

	public abstract void DoShowKilledEffect();

}