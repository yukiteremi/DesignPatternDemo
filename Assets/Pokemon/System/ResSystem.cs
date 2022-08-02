using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResSystem : IGameSystem
{
    public List<GamePool> list = new List<GamePool>();
    public ResSystem(PokeMonFacade PBDGame) : base(PBDGame)
    {
    }

    public GameObject GetNewGameObject()
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].isExist==false)
            {
                list[i].isExist = true;
                list[i].Go.SetActive(true);
                return list[i].Go;
            }
        }
        GamePool newGo = new GamePool();
        newGo.Go = GameObject.Instantiate(Resources.Load<GameObject>("Cube"));
        newGo.isExist = false;
        list.Add(newGo);
        return newGo.Go;
    }

    public void OverGo(GameObject go)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Go.Equals(go))
            {
                list[i].isExist = false;
                list[i].Go.SetActive(false);
                break;
            }
        }
    }
}

public class GamePool
{
    public bool isExist;
    public GameObject Go;
}