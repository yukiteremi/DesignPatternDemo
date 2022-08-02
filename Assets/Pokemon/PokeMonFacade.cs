using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeMonFacade :Singelton<PokeMonFacade>
{
    public MonoBehaviour manager;

    BattleSystem battle;
    UISystem UI;
    CharatorSystem Charator;
    BezierSystem Bezier;
    CameraSystem camrea;
    FactorySystem Factory;
    ResSystem Res;
    PokeMonBuilderSystem builder;
    CutOffSystem cutOff;
    public void Init(MonoBehaviour mana,GameObject Root)
    {
        manager = mana;
        Res = new ResSystem(this);
        battle = new BattleSystem(this);
        UI = new UISystem(this, Root);
        Charator = new CharatorSystem(this);
        Bezier = new BezierSystem(this);
        camrea = new CameraSystem(this);
        Factory = new FactorySystem(this);
        builder = new PokeMonBuilderSystem(this);
        cutOff = new CutOffSystem(this);
    }
    public void GameInit()
    {
        UI.Initialize();
        Bezier.Initialize();
        camrea.Initialize();
        Factory.Initialize();
        Charator.Initialize();
        cutOff.Initialize();
    }

    public void Update()
    {
        cutOff.CutUpdate();
    }
    public void ChangeBattleView()
    {

    }
    public void AddLog(string str)
    {
        UI.LogQueAdd(str);
    }
    public void AddAction(Action act)
    {
        UI.ActQueAdd(act);
    }
    
    public void ChangeScreen()
    {
        cutOff.ChangeScreem(ScreenChangeType.MeetPokeMon);
    }
    public void ChangeSimpleScreen()
    {
        cutOff.ChangeScreem(ScreenChangeType.BattleOver);
    }
    public void Move()
    {
        Charator.GetHostPlayer().Move();
    }
    //public
    public void SetHostPlayerCamera(GameObject game)
    {
        camrea.SetSimplePoint(game);
    }
    public void MeetPokeMon()
    {
        Charator.MeetPokeMon();
    }
    public void CharatorCheck()
    {
        Charator.GetHostPlayer().Check(manager);
    }
    public void Ball(Transform start,Transform end,bool flag)
    {
        Bezier.StartBeizier(start, end, manager,flag);
    }
    public void RoundOver()
    {
        UI.RoundOver();
    }
    public void BallChange(Transform start, Transform end)
    {
        Bezier.StartBeizier2(start, end, manager);
    }
    public Iplayer SetMainPlayer()
    {
        Iplayer player= Factory.playerFactory.CreateSoldier<Player>(new Vector3(-7,0,-11));
        player.AddPokeMon(Factory.CreatePokemon(2, 5, player.M_go.transform.position));
        return player;
    }
    public FactorySystem GetFactorySystem()
    {
        return Factory;
    }
    public CameraSystem GetCameraSystem()
    {
        return camrea;
    }
    public UISystem GetUISystem()
    {
        return UI;
    }
    public CharatorSystem GetCharatorSystem()
    {
        return Charator;
    }
    public ResSystem GetResSystem()
    {
        return Res;
    }
}
