using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeMonAttrFactory : IPokeMonAttrFactory
{
    private Dictionary<int, PokeMonBaseType> m_PokeMonAttrDB = null;
	private Dictionary<int, PokeMonData> m_PokeMonDB = null;
	private Dictionary<int, PokeMonSkillData> m_PokeMonSkillDB = null;

    

    public override void Init()
    {
		InitPokeMonAttr();
		InitPokeMonSkill();
		InitPokeMon();
	}
	private void InitPokeMon()
    {
		m_PokeMonDB = new Dictionary<int, PokeMonData>();
		m_PokeMonDB.Add(1, new PokeMonData("小火龙", GetTypeData(1)));
		m_PokeMonDB.Add(2, new PokeMonData("杰尼龟", GetTypeData(2)));
		m_PokeMonDB.Add(3, new PokeMonData("妙蛙种子", GetTypeData(3)));
		m_PokeMonDB.Add(4, new PokeMonData("皮卡丘", GetTypeData(4)));
		m_PokeMonDB.Add(5, new PokeMonData("伊布", GetTypeData(5)));
	}
	public override PokeMonData GetPokeMon(int id)
    {
		return m_PokeMonDB[id].Clone();

	}
	private void InitPokeMonSkill()
	{
		m_PokeMonSkillDB = new Dictionary<int, PokeMonSkillData>();
		m_PokeMonSkillDB.Add(1, new PokeMonSkillData("火花", 35 ,25, 25, GetTypeData(1)));
		m_PokeMonSkillDB.Add(2, new PokeMonSkillData("泡沫", 35, 25, 25, GetTypeData(2)));
		m_PokeMonSkillDB.Add(3, new PokeMonSkillData("叶刃", 35, 25, 25, GetTypeData(3)));
		m_PokeMonSkillDB.Add(4, new PokeMonSkillData("电击", 35, 25, 25, GetTypeData(4)));
		m_PokeMonSkillDB.Add(5, new PokeMonSkillData("撞击", 30, 30, 30, GetTypeData(5)));
	}
	private void InitPokeMonAttr()
	{
		m_PokeMonAttrDB = new Dictionary<int, PokeMonBaseType>();
		m_PokeMonAttrDB.Add(1, new PokeMonBaseType(PokeMonType.fire,  "火系"));
		m_PokeMonAttrDB.Add(2, new PokeMonBaseType(PokeMonType.water,  "水系"));
		m_PokeMonAttrDB.Add(3, new PokeMonBaseType(PokeMonType.glass,  "草系"));
		m_PokeMonAttrDB.Add(4, new PokeMonBaseType(PokeMonType.electric, "电系"));
		m_PokeMonAttrDB.Add(5, new PokeMonBaseType(PokeMonType.simple, "普通系"));
	}
	public override PokeMonBaseType GetTypeData(int id)
    {
		return m_PokeMonAttrDB[id];
	}

    public override PokeMonSkillData GetSkillData(int id)
    {
		return m_PokeMonSkillDB[id].Copy();

	}
}
