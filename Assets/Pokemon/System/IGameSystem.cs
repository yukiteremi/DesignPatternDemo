using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGameSystem
{
	protected PokeMonFacade m_PBDGame = null;
	public IGameSystem(PokeMonFacade PBDGame)
	{
		m_PBDGame = PBDGame;
	}

	public virtual void Initialize() { }
	public virtual void Release() { }
	public virtual void Update() { }
}
