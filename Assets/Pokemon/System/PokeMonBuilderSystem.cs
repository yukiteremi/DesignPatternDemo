using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeMonBuilderSystem : IGameSystem
{
    public PokeMonBuilderSystem(PokeMonFacade PBDGame) : base(PBDGame)
    {
    }
	public void Construct(IPokeMonBuilder theBuilder,int id,Vector3 pos)
	{
		// 利用Builder產生各部份加入Product中
		theBuilder.LoadAsset(id,pos);
		theBuilder.SetCharacterAttr();

		// 加入管理器內
		//theBuilder.AddCharacterSystem(m_PBDGame);
	}

}
