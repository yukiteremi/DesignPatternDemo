using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class IpokeMon 
{
	protected GameObject m_GameObject = null;   // 顯示的Uniyt模型
	protected PokeMonData Data;
	protected IPokeMonAttrStrategy m_AttrStrategy = null;// 數值的計算策略

	PokeMonAniType type;
	//bool isShow = false;
	//bool isHide = false;

    //策略模式
    public void SetGameObject(GameObject theGameObject)
	{
		m_GameObject = theGameObject;
	}
	public abstract void SetStrategy();

	public void AddNewSkill(PokeMonSkillData skill)
    {
        for (int i = 0; i < Data.skillList.Length; i++)
        {
            if (Data.skillList[i]==null)
            {
				Data.skillList[i] = skill;
				return;
			}
        }
		Debug.Log("技能满了！");
	}
	public void Attack(PokeMonSkillData data,IpokeMon pokemon)
    {
		PokeMonFacade.GetSingelton().AddLog(Data.name+"使用了"+ data.SkillName);
		data.PPNow--;
		float demage = Data.Attr.atk* ((float)data.Power / 100.0f)* m_AttrStrategy.GetAtkPlusValue(data);
		pokemon.Hited(data, demage);

	}
	public void Hited(PokeMonSkillData data, float damage)
    {
		float finallyDemage = damage * m_AttrStrategy.GetDmgDescValue(data.type);
		Data.Attr.hpNow -= (int)finallyDemage;
    }
	public void SetData(PokeMonData data)
    {
		Data = data;
	}
	public PokeMonData GetData()
	{
		return Data;
	}

	// 取得Unity模型
	public GameObject GetGameObject()
	{
		return m_GameObject;
	}
	public void AsyncShowGameObject(PokeMonAniType aniType)
    {
		type = aniType;
		Tweener tweener = m_GameObject.transform.DOScale(Vector3.one,2);
		tweener.OnComplete(ShowComplete);
	}
	public void ShowComplete()
    {
        switch (type)
        {
            case PokeMonAniType.StartBattle:
				MessageCenter.GetSingelton().OnDisPatch(1002);
				break;
            case PokeMonAniType.RoundStart:
				MessageCenter.GetSingelton().OnDisPatch(1004);
				break;
            case PokeMonAniType.CatchIn:
                break;
            case PokeMonAniType.CatchFail:
                break;
            case PokeMonAniType.Win:
                break;
            default:
                break;
        }
    }
	public void AsyncHideGameObject(PokeMonAniType aniType)
	{
		type = aniType;
		Tweener tweener = m_GameObject.transform.DOScale(Vector3.zero, 2);
		tweener.OnComplete(HideComplete);
	}
	public void HideComplete()
	{
		switch (type)
		{
			case PokeMonAniType.CatchIn:
				MessageCenter.GetSingelton().OnDisPatch(1007);
				break;
			case PokeMonAniType.CatchFail:
				AsyncShowGameObject(PokeMonAniType.CatchFail);
				break;
			case PokeMonAniType.Win:
				MessageCenter.GetSingelton().OnDisPatch(3000);
				break;
			default:
				break;
		}
		PokeMonFacade.GetSingelton().GetResSystem().OverGo(m_GameObject);
	}
}

public enum PokeMonAniType
{
	StartBattle,//遇到宝可梦开始战斗模式
	RoundStart,//当前回合开始，展示选择面板
	CatchIn,//捕捉成功！
	CatchFail,//捕捉失败！
	Win,//胜利
	Null
}