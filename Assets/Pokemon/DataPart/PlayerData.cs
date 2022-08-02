using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerData 
{
    
}
public enum PokeMonType
{
    fire,
    water,
    electric,
    glass,
    simple
}
public class PokeMonData 
{
    public string name;
    public PokeMonBaseType type;
    public PokeMonSkillData[] skillList = new PokeMonSkillData[4];
    public PokeMonAttr Attr;

    public PokeMonData(string name, PokeMonBaseType type)
    {
        this.name = name;
        this.type = type;
        for (int i = 0; i < skillList.Length; i++)
        {
            skillList[i] = null;
        }
    }

    public PokeMonData Clone()
    {
        return new PokeMonData(name,type);
    }

   
}
public class PokeMonBaseType
{
    public PokeMonType type;
    public string Sub;

    public PokeMonBaseType(PokeMonType type, string sub)
    {
        this.type = type;
        Sub = sub;
    }

    public override string ToString()
    {
        return Sub;
    }
}
public class PokeMonAttr 
{
    public int level;
    public int hpMax;
    public int hpNow;
    public int atk;
    public int def;
}
public class PokeMonSkillData
{
    public string SkillName;
    public int Power;
    public int PPMax;
    public int PPNow;
    public PokeMonBaseType type;

    public PokeMonSkillData(string skillName, int power, int pPMax, int pPNow, PokeMonBaseType type)
    {
        SkillName = skillName;
        Power = power;
        PPMax = pPMax;
        PPNow = pPNow;
        this.type = type;
    }

    public PokeMonSkillData Copy()
    {
        return new PokeMonSkillData(SkillName, Power, PPMax,PPNow,type);
    }
}