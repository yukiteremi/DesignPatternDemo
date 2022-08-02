using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseAttr
{
	public abstract int GetMaxHP();
	public abstract float GetMoveSpeed();
	public abstract string GetAttrName();
}
public class CharacterBaseAttr : BaseAttr
{
	private int m_MaxHP;        
	private float m_MoveSpeed; 
	private string m_AttrName;     
    public CharacterBaseAttr(int maxHP, float moveSpeed, string attrName)
    {
        m_MaxHP = maxHP;
        m_MoveSpeed = moveSpeed;
        m_AttrName = attrName;
    }
    public override string GetAttrName()
    {
		return m_AttrName;
	}
    public override int GetMaxHP()
    {
		return m_MaxHP;
	}
    public override float GetMoveSpeed()
    {
		return m_MoveSpeed;
	}
}
public abstract class ICharacterAttr
{
	protected BaseAttr m_BaseAttr = null;
	protected int m_NowHP = 0;
	protected IAttrStrategy m_AttrStrategy = null;
	public ICharacterAttr() { }

	public void SetBaseAttr(BaseAttr BaseAttr)
	{
		m_BaseAttr = BaseAttr;
	}
	public BaseAttr GetBaseAttr()
	{
		return m_BaseAttr;
	}
	public int GetNowHP()
	{
		return m_NowHP;
	}
	public virtual int GetMaxHP()
	{
		return m_BaseAttr.GetMaxHP();
	}
	public void FullNowHP()
	{
		m_NowHP = GetMaxHP();
	}
	public virtual float GetMoveSpeed()
	{
		return m_BaseAttr.GetMoveSpeed();
	}
	public virtual string GetAttrName()
	{
		return m_BaseAttr.GetAttrName();
	}

	public void SetAttStrategy(IAttrStrategy theAttrStrategy)
	{
		m_AttrStrategy = theAttrStrategy;
	}
	public void InitAttr()
    {
		m_AttrStrategy.InitAttr(this);
		m_NowHP = GetMaxHP();
	}

	public virtual void CalDmgValue(ICharacter attacker)
    {
		int AtkValue = attacker.GetPower();
		AtkValue -= m_AttrStrategy.GetDmgDescValue(this);
		m_NowHP -= AtkValue;
		Debug.Log("敌人受到了"+ AtkValue+"点伤害，还剩"+ m_NowHP+"血量");
	}
}
public class SoldierAttr : ICharacterAttr
{
	protected int m_SoldierLv;
	protected int m_AddMaxHP; 

	public SoldierAttr()
	{ }

	public void SetSoldierAttr(BaseAttr BaseAttr)
	{
		base.SetBaseAttr(BaseAttr);
		m_SoldierLv = 1;
		m_AddMaxHP = 0;
	}
	public void SetSoldierLv(int Lv)
	{
		m_SoldierLv = Lv;
	}
	public int GetSoldierLv()
	{
		return m_SoldierLv;
	}
	public override int GetMaxHP()
	{
		return base.GetMaxHP() + m_AddMaxHP;
	}
	public void AddMaxHP(int AddMaxHP)
	{
		m_AddMaxHP = AddMaxHP;
	}

	public override void CalDmgValue(ICharacter Attacker)
	{
		base.CalDmgValue(Attacker);
	}


}
