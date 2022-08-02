using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulbasaur : IpokeMon
{
    public Bulbasaur()
    {
        SetStrategy();
    }

    public override void SetStrategy()
    {
        m_AttrStrategy = new GlassStrategy();
    }
}
