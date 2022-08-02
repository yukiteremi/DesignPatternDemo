using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : IPlayerFactory
{
    public Iplayer CreateSoldier<T>(Vector3 SpawnPosition) where T : Iplayer, new()
    {
        GameObject PlayerGo = GameObject.Instantiate(Resources.Load<GameObject>("Player/Samural"));
        PlayerGo.transform.position = SpawnPosition;
        T play = new T();
        play.SetGameObject(PlayerGo);
        return play;
    }

   
}
