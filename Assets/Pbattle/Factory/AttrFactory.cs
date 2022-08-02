using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrFactory : IAttrFactory
{
    private Dictionary<int, BaseAttr> m_SoldierAttrDB = null;
    private Dictionary<int, WeaponAttr> m_WeaponAttrDB = null;
    public override SoldierAttr GetSoldierAttr(int AttrID)
    {
        if (m_SoldierAttrDB.ContainsKey(AttrID) == false)
        {
            Debug.Log("对象不存在！");
            return null;
        }
        SoldierAttr NewAttr = new SoldierAttr();
        NewAttr.SetSoldierAttr(m_SoldierAttrDB[AttrID]);
        return NewAttr;
    }

    public override WeaponAttr GetWeaponAttr(int AttrID)
    {
        return m_WeaponAttrDB[AttrID];
    }
    private void InitSoldierAttr()
    {
        m_SoldierAttrDB = new Dictionary<int, BaseAttr>();
        m_SoldierAttrDB.Add(1, new CharacterBaseAttr(10, 3.0f, "小兵"));
        m_SoldierAttrDB.Add(2, new CharacterBaseAttr(20, 3.2f, "中兵"));
    }

    public void WeaponInit()
    {
        m_WeaponAttrDB = new Dictionary<int, WeaponAttr>();
        m_WeaponAttrDB.Add(1, new WeaponAttr(2, 4, "手枪"));
        m_WeaponAttrDB.Add(2, new WeaponAttr(4, 7, "步枪"));
    }
    public void Init()
    {
        WeaponInit();
        InitSoldierAttr();
    }

}
