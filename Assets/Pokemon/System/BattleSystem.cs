using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem :IGameSystem
{
    public IpokeMon NowPokeMon;
    public IpokeMon EnemyPokeMon;

    public BattleSystem(PokeMonFacade PBDGame) : base(PBDGame)
    {
    }

    public void SetNowPokeMon(IpokeMon pokemon)
    {
        NowPokeMon = pokemon;
    }
    public void SetEnemyPokeMon(IpokeMon pokemon)
    {
        EnemyPokeMon = pokemon;
    }

    public void StartBatttle ()
    {

    }
    public void BattleOver(bool flag)
    {
        if (flag==true)
        {
            Debug.Log("玩家胜利！");
        }
        else
        {
            Debug.Log("玩家没有可以战斗的精灵了，眼前一黑！");
        }
    }

    public void PlayerAtterEnemY()
    {

    }

    public void EnemyAttackPlayer()
    {

    }
}
