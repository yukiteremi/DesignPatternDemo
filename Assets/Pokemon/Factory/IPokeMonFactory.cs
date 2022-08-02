using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPokeMonFactory
{
	protected PokeMonFacade m_PBDGame = null;
	public IPokeMonFactory(PokeMonFacade PBDGame)
	{
		m_PBDGame = PBDGame;
	}

	public abstract IpokeMon CreatePokeMon(int AttrID,int lv, Vector3 pos);
}
