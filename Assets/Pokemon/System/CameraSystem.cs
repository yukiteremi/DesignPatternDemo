using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraSystem :IGameSystem
{
    public CinemachineVirtualCamera SimpleView;
    public CinemachineVirtualCamera BattleView;

    public CameraSystem(PokeMonFacade PBDGame) : base(PBDGame)
    {
    }
    public override void Initialize()
    {
        Init();
    }
    public void Init()
    {
        SimpleView = GameObject.Find("SimpleView").GetComponent<CinemachineVirtualCamera>();
        BattleView = GameObject.Find("BattleView").GetComponent<CinemachineVirtualCamera>();
    }

    public void SetSimplePoint(GameObject game)
    {
        SimpleView.LookAt=game.transform;
        SimpleView.Follow = game.transform;
    }
    public void SetBattlePoint(GameObject game)
    {
        BattleView.LookAt = game.transform;
        BattleView.Follow = game.transform;
    }
    public void ChangeSimpleView()
    {
        BattleView.gameObject.SetActive(false);
        SimpleView.gameObject.SetActive(true);
    }
    public void ChangeBattleView()
    {
        SimpleView.gameObject.SetActive(false);
        BattleView.gameObject.SetActive(true);
    }
}
