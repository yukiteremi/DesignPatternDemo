using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facade 
{
    AttrFactory attrFactory;
    ISoldier NowSoldier;
    ISoldier Target;
    public void init() {
        //数据工厂舒初始化
        attrFactory = new AttrFactory();
        attrFactory.Init();
    }

    public void AddSolder()
    {
        ICharacterFactory Factory = FacadeFactory.GetSingelton().GetCharacterFactory();
        NowSoldier = Factory.CreateSoldier(ENUM_Soldier.Rookie, ENUM_Weapon.Gun, 1, Vector3.zero);
    }
    public void AddEnemy()
    {
        Debug.Log("创造一个敌人");
        ICharacterFactory Factory = FacadeFactory.GetSingelton().GetCharacterFactory();
        Target = Factory.CreateSoldier(ENUM_Soldier.Rookie, ENUM_Weapon.Gun, 1, Vector3.zero);
    }
    public void Fight()
    {
        if (NowSoldier!=null  && Target!=null)
        {
            NowSoldier.Attack(Target);
        }
        else
        {
            Debug.Log("缺少敌人或士兵");
        }
    }
}
