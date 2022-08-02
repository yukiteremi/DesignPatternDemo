using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPokeMonAttrStrategy 
{
	public abstract void InitAttr(ICharacterAttr CharacterAttr);

	// 攻擊加乘
	public abstract float GetAtkPlusValue(PokeMonSkillData CharacterAttr);

	// 取得減傷害值
	public abstract float GetDmgDescValue(PokeMonBaseType CharacterAttr);
}
