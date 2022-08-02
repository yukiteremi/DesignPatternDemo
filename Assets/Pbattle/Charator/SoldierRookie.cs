using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRookie : ISoldier
{
	public SoldierRookie()
	{
		m_emSoldier = ENUM_Soldier.Rookie;
		C_AssetName = "Soldier1";
		C_Icon = "RookieIcon";
		C_AttrId = 1;
	}

	public override void DoPlayKilledSound()
	{
		Debug.Log("播放死亡音效");
	}

	public override void DoShowKilledEffect()
	{
		Debug.Log("播放死亡特效");
	}

	

}