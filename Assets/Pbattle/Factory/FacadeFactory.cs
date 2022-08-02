using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacadeFactory:Singelton<FacadeFactory>
{
    private IWeaponFactory m_WeaponFactory = null;
    private IAttrFactory m_AttrFactory = null;
    private ICharacterFactory m_CharacterFactory = null;

    public  ICharacterFactory GetCharacterFactory()
    {
        if (m_CharacterFactory == null)
            Debug.Log("?");
            //m_CharacterFactory = new CharacterFactory();
        return m_CharacterFactory;
    }
    public IWeaponFactory GetWeaponFactory()
    {
        if (m_WeaponFactory==null)
        {
            m_WeaponFactory = new WeaponFactory();
        }
        return m_WeaponFactory;
    }
    public IAttrFactory GetAttrFactory()
    {
        if (m_AttrFactory == null)
        {
            AttrFactory attr = new AttrFactory();
            attr.Init();
            m_AttrFactory = attr;
        }
        return m_AttrFactory;
    }
}
