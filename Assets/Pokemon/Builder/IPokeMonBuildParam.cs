using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPokeMonBuildParam 
{
	public IpokeMon NewPokeMon = null;
	public Vector3 SpawnPosition;
	public int AttrID;
	public int lv;
	public string AssetName;
}

public abstract class IPokeMonBuilder
{
	// 設定建立參數
	public abstract void SetBuildParam(IPokeMonBuildParam theParam );
	// 載入Asset中的角色模型
	public abstract void LoadAsset	( int GameObjectID,Vector3 pos );
	// 設定角色能力
	public abstract void SetCharacterAttr();
	
}

