using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonBuilder : IPokeMonBuilder
{
    IPokeMonBuildParam m_BuildParam;
    public override void LoadAsset(int GameObjectID,Vector3 pos)
    {
        GameObject clone = PokeMonFacade.GetSingelton().GetResSystem().GetNewGameObject();
        clone.transform.position = pos + new Vector3(0,0.5f,0);
        clone.transform.localScale = Vector3.zero;
        m_BuildParam.NewPokeMon.SetGameObject(clone);
    }

    public override void SetBuildParam(IPokeMonBuildParam theParam)
    {
        m_BuildParam = theParam as PokeMonBuildParam;
    }

    public override void SetCharacterAttr()
    {
        FactorySystem factory = PokeMonFacade.GetSingelton().GetFactorySystem();
        m_BuildParam.NewPokeMon.SetData(factory.GetNameAndType(m_BuildParam.AttrID));
        m_BuildParam.NewPokeMon.AddNewSkill(factory.GetSkill(m_BuildParam.AttrID));
        PokeMonAttr attr = factory.PokemonData(m_BuildParam.lv);
        m_BuildParam.NewPokeMon.GetData().Attr = attr;
    }
}


public  class PokeMonBuildParam:IPokeMonBuildParam
{
    public PokeMonBuildParam()
    {
    }
}
