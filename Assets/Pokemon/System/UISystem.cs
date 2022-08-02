using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISystem:IGameSystem
{
    protected GameObject m_RootUI = null;

    public Image MyHUD, EnemyHUD;
    public Image SkillImg, SkillSubImg;
    public Image BattleUI;
    public Image FightView, FightSubView;
    public Image LogUI, NemuUI;

    public Button BattleBtn, NemuBtn, ItemBtn, RunBtn;
    public Button SkillOne, SkillTwo, SkillThree, SkillFour;
    public Queue<string> LogQue = new Queue<string>();
    public Queue<Action> ActQue = new Queue<Action>();

    public Text LogText;

    public Text PPtext, SkillTypeText;

    public Text EnemyNameText, EnemyLevelText, EnemyHpText;
    public Slider EnemySlide;
    public bool CatchIng = false;
    public Text MyNameText, MyLevelText, MyHpText;
    public Slider MySlide;
    public UISystem(PokeMonFacade PBDGame,GameObject root) : base(PBDGame)
    {
        m_RootUI = root;
    }
    public override void Initialize()
    {
        Init();
        MessageCenter.GetSingelton().OnAddListen(1002,StartBattle);
    }
    public void LogQueAdd(string str)
    {
        LogQue.Enqueue(str);
    }
    public void ActQueAdd(Action act)
    {
        ActQue.Enqueue(act);
    }


    public void Init()
    {
        MessageCenter.GetSingelton().OnAddListen(1004,BattleStart);
        MessageCenter.GetSingelton().OnAddListen(1011, BattleOver);
        MessageCenter.GetSingelton().OnAddListen(1012, CatchSuccess);
        MessageCenter.GetSingelton().OnAddListen(3000, Win);
        BattleUI = UITool.GetUIComponent<Image>(m_RootUI, "BattleUI");

        SkillOne = UITool.GetUIComponent<Button>(m_RootUI, "SkillOneBtn");
        SkillTwo = UITool.GetUIComponent<Button>(m_RootUI, "SkillTwoBtn");
        SkillThree = UITool.GetUIComponent<Button>(m_RootUI, "SkillThreeBtn");
        SkillFour = UITool.GetUIComponent<Button>(m_RootUI, "SkillFourBtn");
        SkillOne.gameObject.AddComponent<SkillItemView>();
        SkillTwo.gameObject.AddComponent<SkillItemView>();
        SkillThree.gameObject.AddComponent<SkillItemView>();
        SkillFour.gameObject.AddComponent<SkillItemView>();


        BattleBtn = UITool.GetUIComponent<Button>(m_RootUI, "FightBtn");
        BattleBtn.onClick.AddListener(FightViewShow);
        NemuBtn = UITool.GetUIComponent<Button>(m_RootUI, "BagBtn");
        NemuBtn.onClick.AddListener(Catch);
        ItemBtn = UITool.GetUIComponent<Button>(m_RootUI, "PokeMonBtn");
        ItemBtn.onClick.AddListener(()=> {
            m_PBDGame.GetCharatorSystem().player.ChangePokeMon();
        });
        RunBtn = UITool.GetUIComponent<Button>(m_RootUI, "RunAwayBtn");
        RunBtn.onClick.AddListener(RunAway);

        MyHUD = UITool.GetUIComponent<Image>(m_RootUI, "MyHUD");
        MyNameText = UITool.GetUIComponent<Text>(MyHUD.gameObject, "NameText");
        MyLevelText = UITool.GetUIComponent<Text>(MyHUD.gameObject, "LevelText");
        MyHpText = UITool.GetUIComponent<Text>(MyHUD.gameObject, "HPText");
        MySlide = UITool.GetUIComponent<Slider>(MyHUD.gameObject, "Slider");

        EnemyHUD = UITool.GetUIComponent<Image>(m_RootUI, "EnemyHUD");
        EnemyNameText = UITool.GetUIComponent<Text>(EnemyHUD.gameObject, "NameText");
        EnemyLevelText = UITool.GetUIComponent<Text>(EnemyHUD.gameObject, "LevelText");
        EnemyHpText = UITool.GetUIComponent<Text>(EnemyHUD.gameObject, "HPText");
        EnemySlide = UITool.GetUIComponent<Slider>(EnemyHUD.gameObject, "Slider");

        NemuUI = UITool.GetUIComponent<Image>(m_RootUI, "NemuPart");
        LogUI = UITool.GetUIComponent<Image>(m_RootUI, "LogPart");
        FightView = UITool.GetUIComponent<Image>(m_RootUI, "SkillPart");
        FightSubView = UITool.GetUIComponent<Image>(m_RootUI, "SkillSubPart");


        PPtext = UITool.GetUIComponent<Text>(FightSubView.gameObject, "PPtext");
        SkillTypeText = UITool.GetUIComponent<Text>(FightSubView.gameObject, "SkillTypeText");

        LogUI.GetComponent<Button>().onClick.AddListener(()=> {
            if (LogQue.Count>0)
            {
                LogText.text = LogQue.Dequeue();

            }
            else
            {
                if (ActQue.Count>0)
                {
                    Action act= ActQue.Dequeue();
                    act();
                }
            }
        });
        LogText = UITool.GetUIComponent<Text>(LogUI.gameObject, "LogText");

    }

    private void Win(object obj)
    {
        BattleOvers();
    }

    private void CatchSuccess(object obj)
    {
        RoundOver();
    }

    public void Catch()
    {
        if (CatchIng == false)
        {
            CatchIng = true;
            m_PBDGame.Ball(m_PBDGame.GetCharatorSystem().player.M_go.transform, m_PBDGame.GetCharatorSystem().PokeMon.GetGameObject().transform, true);
        }
    }
    public void RoundOver()
    {
        HideAll();
        MyHUD.gameObject.SetActive(true);
        RefreshMyInfo();
        EnemyHUD.gameObject.SetActive(true);
        RefreshEnemyInfo();
        LogUI.gameObject.SetActive(true);
        if (LogQue.Count > 0)
        {
            LogText.text = LogQue.Dequeue();
        }
    }
    public void RunAway()
    {
        BattleOver(null);
    }
    private void BattleStart(object obj)
    {
        BattleUI.gameObject.SetActive(true);
        MyHUD.gameObject.SetActive(true);
        RefreshMyInfo();
        //我的HUD初始化
        NemuUI.gameObject.SetActive(true);
        LogText.text = m_PBDGame.GetCharatorSystem().GetHostPlayer().SelcetPokemon().GetData().name + "要做什么";
        CatchIng = false;
    }

    public void SkillBtnClick(int num)
    {
        PokeMonSkillData data = m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[num - 1];
        if (data == null)
        {
            Debug.Log("没有该技能");
        }
        else
        {
            if (data.PPNow>0)
            {
                m_PBDGame.GetCharatorSystem().player.SelcetPokemon().Attack(data,m_PBDGame.GetCharatorSystem().PokeMon);
                if (m_PBDGame.GetCharatorSystem().PokeMon.GetData().Attr.hpNow<=0)
                {
                    LogQue.Enqueue(m_PBDGame.GetCharatorSystem().PokeMon.GetData().name+ "倒下了");
                    ActQue.Enqueue(() => { m_PBDGame.GetCharatorSystem().PokeMon.AsyncHideGameObject(PokeMonAniType.Win); });
                    RoundOver();
                    return;
                }
                RefreshEnemyInfo();
                m_PBDGame.GetCharatorSystem().PokeMon.Attack(m_PBDGame.GetCharatorSystem().PokeMon.GetData().skillList[0], m_PBDGame.GetCharatorSystem().player.SelcetPokemon());
                if (m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().Attr.hpNow <= 0)
                {
                    LogQue.Enqueue(m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().name + "倒下了");
                    ActQue.Enqueue(() => { m_PBDGame.GetCharatorSystem().player.SelcetPokemon().AsyncHideGameObject(PokeMonAniType.Win); });
                    RoundOver();
                    return;
                }
                RefreshMyInfo();
                Debug.Log(LogQue.Count);
                ActQue.Enqueue(()=> { BattleStart(null); });
                RoundOver();
            }
            else
            {
                Debug.Log("技能没了，无法使用");
            }
            
        }
    }
    public void SkillSubChange(int num)
    {
        PokeMonSkillData data = m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[num-1];
        if (data==null)
        {
            PPtext.text = "无";
            SkillTypeText.text = "无";
        }
        else
        {
            PPtext.text = data.PPNow+"/"+ data.PPMax;
            SkillTypeText.text = data.type.ToString();
        }
        

    }
    public void HideAll()
    {
        NemuUI.gameObject.SetActive(false);
        LogUI.gameObject.SetActive(false);
        FightView.gameObject.SetActive(false);
        FightSubView.gameObject.SetActive(false);
        MyHUD.gameObject.SetActive(false);
        EnemyHUD.gameObject.SetActive(false);
    }
    private void StartBattle(object obj)
    {
        m_RootUI.SetActive(true);
        SkillOne.gameObject.GetComponent<SkillItemView>().Init(() => { SkillBtnClick(1); }, () => { SkillSubChange(1); }, m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[0]);
        SkillTwo.gameObject.GetComponent<SkillItemView>().Init(() => { SkillBtnClick(2); }, () => { SkillSubChange(2); }, m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[1]); ;
        SkillThree.gameObject.GetComponent<SkillItemView>().Init(() => { SkillBtnClick(3); }, () => { SkillSubChange(3); }, m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[2]); ;
        SkillFour.gameObject.GetComponent<SkillItemView>().Init(() => { SkillBtnClick(4); }, () => { SkillSubChange(4); }, m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[3]); ;
        HideAll();
        EnemyHUD.gameObject.SetActive(true);
        RefreshEnemyInfo();
        LogUI.gameObject.SetActive(true);
        if (LogQue.Count > 0)
        {
            LogText.text = LogQue.Dequeue();
        }
    }
    public void FightViewShow()
    {
        FightView.gameObject.SetActive(true);
        FightSubView.gameObject.SetActive(true);
    }
    public void StartBattle(PokeMonData My,PokeMonData Enemy)
    {
        HideAll();
        MyHUD.gameObject.SetActive(true);
        EnemyHUD.gameObject.SetActive(true);
        //敌人HUD初始化
        BattleUI.gameObject.SetActive(true);
    }
    public void RefreshEnemyInfo()
    {
        IpokeMon pokemon= m_PBDGame.GetCharatorSystem().PokeMon;
        SkillOne.gameObject.GetComponent<SkillItemView>().Init(() => { SkillBtnClick(1); }, () => { SkillSubChange(1); }, m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[0]);
        SkillTwo.gameObject.GetComponent<SkillItemView>().Init(() => { SkillBtnClick(2); }, () => { SkillSubChange(2); }, m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[1]); ;
        SkillThree.gameObject.GetComponent<SkillItemView>().Init(() => { SkillBtnClick(3); }, () => { SkillSubChange(3); }, m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[2]); ;
        SkillFour.gameObject.GetComponent<SkillItemView>().Init(() => { SkillBtnClick(4); }, () => { SkillSubChange(4); }, m_PBDGame.GetCharatorSystem().player.SelcetPokemon().GetData().skillList[3]); ;
        EnemyNameText.text = pokemon.GetData().name;
        EnemyLevelText.text = "LV:"+pokemon.GetData().Attr.level;
        EnemyHpText.text = pokemon.GetData().Attr.hpNow + "/" + pokemon.GetData().Attr.hpMax;
        EnemySlide.value= (float)pokemon.GetData().Attr.hpNow/(float)pokemon.GetData().Attr.hpMax;
    }
    public void RefreshMyInfo()
    {
        IpokeMon pokemon = m_PBDGame.GetCharatorSystem().player.SelcetPokemon();
        MyNameText.text = pokemon.GetData().name;
        MyLevelText.text = "LV:" + pokemon.GetData().Attr.level;
        MyHpText.text = pokemon.GetData().Attr.hpNow + "/" + pokemon.GetData().Attr.hpMax;
        MySlide.value = (float)pokemon.GetData().Attr.hpNow / (float)pokemon.GetData().Attr.hpMax;
    }
    public void BattleOvers()
    {
        MyHUD.gameObject.SetActive(false);
        EnemyHUD.gameObject.SetActive(false);
        m_RootUI.gameObject.SetActive(false);
    }
    private void BattleOver(object obj)
    {
        BattleOvers();
        m_PBDGame.ChangeSimpleScreen();
    }
}
