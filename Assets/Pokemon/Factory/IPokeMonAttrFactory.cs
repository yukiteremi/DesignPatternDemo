using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPokeMonAttrFactory 
{
	public abstract void Init();
	public abstract PokeMonData GetPokeMon(int id);

	public abstract PokeMonBaseType GetTypeData(int id);

	public abstract PokeMonSkillData GetSkillData(int id);
}
