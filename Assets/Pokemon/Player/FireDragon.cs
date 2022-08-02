using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDragon : IpokeMon
{
    public FireDragon()
    {
        SetStrategy();
    }

    public override void SetStrategy()
    {
        m_AttrStrategy = new FireStrategy();
        //设置初始策略
    }
}
