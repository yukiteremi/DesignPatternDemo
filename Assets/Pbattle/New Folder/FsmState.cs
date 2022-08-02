using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmState
{
    public FsmSystem system;
    public GameObject go;
    public FsmState(FsmSystem system, GameObject game)
    {
        this.system = system;
        go = game;
    }

    public virtual void Do(GameObject go)
    {

    }

    public virtual void Check(GameObject go)
    {

    }
    public virtual void come()
    {

    }
    public virtual void goout()
    {

    }
}
public class LoginState : FsmState
{

    public LoginState(FsmSystem system, GameObject game) : base(system, game)
    {

    }

    public override void come()
    {
        Debug.Log("进入登录页面");
    }
    float time = 0;
    public override void Do(GameObject go)
    {
        time += Time.deltaTime;
    }
    public override void Check(GameObject go)
    {
        if (time > 3)
        {
            system.tarGet = Fsm.GameMainState;
            system.SetNowState(Fsm.LoadingState);
        }
    }
    public override void goout()
    {
        Debug.Log("离开登录页面");
    }
}
public class LoadingState : FsmState
{

    public LoadingState(FsmSystem system, GameObject game) : base(system, game)
    {

    }
    public override void come()
    {
        Debug.Log("进入加载页面 目标：" + system.tarGet.ToString());
        time = 0;
    }
    float time = 0;
    public override void Do(GameObject go)
    {
        time += Time.deltaTime;
    }

    public override void Check(GameObject go)
    {
        if (time > 3)
        {
            system.SetNowState(system.tarGet);
        }
    }
    public override void goout()
    {
        Debug.Log("离开加载页面！");
    }
}
public class GameMainState : FsmState
{
    PokeMonFacade facade;
    GameManager manager;
    GameObject root;

    public GameMainState(FsmSystem system, GameObject game,GameManager mana,GameObject Root) : base(system, game)
    {
        manager = mana;
        root = Root;
    }

    public override void come()
    {
        Debug.Log("进入主场景页面,按键交互允许");
        if (facade == null)
        {
            facade = PokeMonFacade.GetSingelton();
            facade.Init(manager, root);
            facade.GameInit();
            facade.CharatorCheck();
            Debug.Log("初次进入主场景,初始化facade");
        }
        time = 0;
    }
    float time = 0;
    public override void Do(GameObject go)
    {
        time += Time.deltaTime;
        if (facade!=null)
        {
            facade.Move();
            facade.Update();
        }
    }
    public override void Check(GameObject go)
    {
       
    }
    public override void goout()
    {
        Debug.Log("离开主场景页面！ 关闭用户交互");
    }
}
public class BattleState : FsmState
{
    
    public BattleState(FsmSystem system, GameObject game) : base(system, game)
    {

    }
    public override void come()
    {
        
        Debug.Log("进入战斗页面");
        time = 0;
    }
    float time = 0;
    public override void Do(GameObject go)
    {
        time += Time.deltaTime;

    }

    public override void Check(GameObject go)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //sir.Attack(sir);
        }
        if (time > 20)
        {
            system.tarGet = Fsm.GameMainState;
            system.SetNowState(Fsm.LoadingState);
        }
    }
    public override void goout()
    {
        Debug.Log("离开战斗场景");
    }
}
public class FsmSystem
{
    public Dictionary<Fsm, FsmState> dic = new Dictionary<Fsm, FsmState>();
    public FsmState nowState;
    public Fsm tarGet;

    public void AddState(Fsm fsm, FsmState state)
    {
        if (!dic.ContainsKey(fsm))
        {
            dic.Add(fsm, state);
        }
    }
    public void Update()
    {
        if (nowState != null)
        {
            nowState.Do(nowState.go);
            nowState.Check(nowState.go);
        }
    }
    public void SetNowState(Fsm fsm)
    {
        if (nowState != null)
        {
            nowState.goout();
        }
        nowState = dic[fsm];
        nowState.come();
    }
}
public enum Fsm
{
    LoginState,
    LoadingState,
    GameMainState,
    BattleState
}