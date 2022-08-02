using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirtle : IpokeMon
{
    public Squirtle()
    {
        SetStrategy();
    }

    public override void SetStrategy()
    {
        m_AttrStrategy = new WaterStrategy();
    }
}
