using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharatorSystem :IGameSystem
{
    public Iplayer player;
    public IpokeMon PokeMon;
    public CharatorSystem(PokeMonFacade PBDGame) : base(PBDGame)
    {
    }

    public override void Initialize()
    {
        SetHostPlayer(m_PBDGame.SetMainPlayer());
        m_PBDGame.SetHostPlayerCamera(player.M_go);
        MessageCenter.GetSingelton().OnAddListen(1000, SceneChange);
        MessageCenter.GetSingelton().OnAddListen(1001, BattleStart);
        MessageCenter.GetSingelton().OnAddListen(1005, BallToEnd);
        MessageCenter.GetSingelton().OnAddListen(1006, Catch);
        MessageCenter.GetSingelton().OnAddListen(1007, CatchIn);
        MessageCenter.GetSingelton().OnAddListen(1009, ChangePokeMon);
        MessageCenter.GetSingelton().OnAddListen(1010, Reset);
        MessageCenter.GetSingelton().OnAddListen(3000, Win);

    }

    private void Win(object obj)
    {
        OnlyChangeData();
        m_PBDGame.ChangeSimpleScreen();
    }

    private void ChangePokeMon(object obj)
    {
        m_PBDGame.BallChange(player.M_go.transform,player.SelcetPokemon().GetGameObject().transform);

    }
    public void OnlyChangeData()
    {
        player.OnlyChangeData();
    }
    private void Reset(object obj)
    {
        player.Reset();
    }

    private void SceneChange(object obj)
    {
        IpokeMon pokeMon = player.SelcetPokemon();
        if (pokeMon!=null)
        {
            pokeMon.GetGameObject().transform.position = player.M_go.transform.position + Vector3.forward + Vector3.right+new Vector3(0,0.5f,0);
            pokeMon.GetGameObject().transform.LookAt(new Vector3(PokeMon.GetGameObject().transform.position.x, pokeMon.GetGameObject().transform.position.y, PokeMon.GetGameObject().transform.position.z));
            m_PBDGame.GetCameraSystem().BattleView.Follow = pokeMon.GetGameObject().transform;
            m_PBDGame.GetCameraSystem().BattleView.LookAt = pokeMon.GetGameObject().transform;
            m_PBDGame.GetCameraSystem().ChangeBattleView();
        }
    }

    private void BattleStart(object obj)
    {
        PokeMon.AsyncShowGameObject(PokeMonAniType.StartBattle);
    }
    public void HideBoth()
    {
        PokeMon.AsyncHideGameObject(PokeMonAniType.Null);
        PokeMonFacade.GetSingelton().GetResSystem().OverGo(PokeMon.GetGameObject());
        player.SelcetPokemon().AsyncHideGameObject(PokeMonAniType.Null);
    }
    public void MeetPokeMon()
    {
        player.SelcetPokemon().SetGameObject(m_PBDGame.GetResSystem().GetNewGameObject());
        player.SelcetPokemon().GetGameObject().transform.position = player.M_go.transform.position + Vector3.forward + Vector3.left;
        PokeMon = m_PBDGame.GetFactorySystem().CreatePokemon(1,5, player.M_go.transform.position+ player.M_go.transform.forward*5+ player.M_go.transform.right*2);
        m_PBDGame.AddLog("野生的"+ PokeMon.GetData().name+"出现了！");
        m_PBDGame.AddLog("去吧" + player.SelcetPokemon().GetData().name + "！");
        m_PBDGame.AddAction(()=> {
            //todo
            m_PBDGame.Ball(player.M_go.transform, player.SelcetPokemon().GetGameObject().transform,false);
        });
    }

    private void BallToEnd(object obj)
    {
        player.SelcetPokemon().AsyncShowGameObject(PokeMonAniType.RoundStart);
        m_PBDGame.GetUISystem().RefreshMyInfo();
    }
    private void Catch(object obj)
    {
        PokeMon.AsyncHideGameObject(PokeMonAniType.CatchIn);
    }
    private void CatchIn(object obj)
    {
        int num = UnityEngine.Random.Range(1,100);
        if (num>20)
        {
            m_PBDGame.AddLog("捕捉成功！");
            player.AddPokeMon(PokeMon);
            m_PBDGame.AddAction(() => { MessageCenter.GetSingelton().OnDisPatch(1011); });
            MessageCenter.GetSingelton().OnDisPatch(1012);
        }
        else
        {
            m_PBDGame.AddLog("捕捉失败！");
            m_PBDGame.AddAction(()=> { MessageCenter.GetSingelton().OnDisPatch(1004); });
            PokeMon.AsyncShowGameObject(PokeMonAniType.Null);
        }
    }
    public Iplayer GetHostPlayer()
    {
        return player;
    }
    public void SetHostPlayer(Iplayer play)
    {
        player = play;
    }
}
