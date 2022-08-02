using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Root;
    FsmSystem system;
    void Start()
    {
        system = new FsmSystem();
        system.AddState(Fsm.LoginState, new LoginState(system, gameObject));
        system.AddState(Fsm.LoadingState, new LoadingState(system, gameObject));
        system.AddState(Fsm.GameMainState, new GameMainState(system, gameObject,this, Root));
        system.AddState(Fsm.BattleState, new BattleState(system, gameObject));
        system.SetNowState(Fsm.LoginState);
        Root.gameObject.SetActive(false);
    }
    void Update()
    {
        system.Update();
        //Collider[] c1= Physics.OverlapSphere(transform.position,5);
    }
}
/*
 消息中心消息号
1000    改变场景
1001    战斗加载完毕
1002    敌人加载完毕
1003    对话结束 该继续执行一些事情了
1004    我的宝可梦加载完毕
1005    球到了目标点
1006    球到了目标点  捕捉用！
1007    捕捉失败 敌方宝可梦出来了!
1010    战斗结束恢复正常
3000    战斗胜利
 */