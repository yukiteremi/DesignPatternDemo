using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassStrategy : IPokeMonAttrStrategy
{
    public override float GetAtkPlusValue(PokeMonSkillData type)
    {
        if (type.type.type == PokeMonType.glass)
        {
            return 1.5f;
        }

        else
        {
            return 1;
        }
    }

    public override float GetDmgDescValue(PokeMonBaseType type)
    {
        if (type.type == PokeMonType.fire)
        {
            PokeMonFacade.GetSingelton().AddLog("效果绝佳");
            return 2f;
        }
        else if (type.type == PokeMonType.water)
        {
            PokeMonFacade.GetSingelton().AddLog("效果不太是很好");
            return 0.5f;
        }
        else
        {
            return 1;
        }
    }

    public override void InitAttr(ICharacterAttr CharacterAttr)
    {
    }
}

