using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Iplayer
{
    private GameObject m_go;
    private IpokeMon[] pokelist;
    private IpokeMon SelectPokeMon;
    private Animator ani;
    private bool BattleIng = false;
    public Player()
    {
        PokeList = new IpokeMon[6];
        for (int i = 0; i < PokeList.Length; i++)
        {
            PokeList[i] = null;
        }
    }
    public void SetGameObject(GameObject go)
    {
        M_go = go;
        ani = m_go.GetComponent<Animator>();
    }
    public IpokeMon[] PokeList { get => pokelist; set => pokelist=value; }
    public GameObject M_go { get => m_go; set => m_go=value; }

    public void Check(MonoBehaviour mono)
    {
        mono.StartCoroutine(time());
    }
    IEnumerator time()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Ray ray = new Ray(M_go.transform.position+new Vector3(0,0.1f,0), Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit info))
            {
                Debug.Log(info.collider.tag);
                if (info.collider.tag== "Green")
                {
                    Battle();
                }
            }
            else
            {
                Debug.Log("未检测到脚下物体");
            }
        }
    }
    public IpokeMon SelcetPokemon()
    {
        if (SelectPokeMon==null)
        {
            for (int i = 0; i < PokeList.Length; i++)
            {
                if (PokeList[i] != null)
                {
                    if (PokeList[i].GetData().Attr.hpNow > 0)
                    {
                        SelectPokeMon = PokeList[i];
                        return SelectPokeMon;
                    }
                }
            }
        }
        return SelectPokeMon;
    }
    public void Battle()
    {
        if (!BattleIng)
        {
            int num = Random.Range(0, 100);
            if (num > 80)
            {
                BattleIng = true;
                PokeMonFacade.GetSingelton().MeetPokeMon();

                ani.SetBool("Run", false);
                PokeMonFacade.GetSingelton().ChangeScreen();
                //遇敌
                //开始随机，判断具体遇到哪个敌人
                //PokeMonFacade.GetSingelton().
            }
        }
    }
    public void AddPokeMon(IpokeMon pokemon)
    {
        for (int i = 0; i < pokelist.Length; i++)
        {
            if (pokelist[i] == null)
            {
                pokelist[i] = pokemon;
                return;
            }
        }
        Debug.Log("携带的宝可梦以及满了，将宝可梦传送到了正辉的电脑1");
    }
    public void Move()
    {
        if (!BattleIng)
        {
            M_go.transform.eulerAngles += Vector3.up * Input.GetAxis("Horizontal") * 0.25f;
            if (Input.GetKeyDown(KeyCode.W))
            {
                ani.SetBool("Run", true);
            }
            if (Input.GetKey(KeyCode.W))
            {
                M_go.transform.Translate(Vector3.forward * Time.deltaTime * 3);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                ani.SetBool("Run", false);
            }
            if (Input.GetKey(KeyCode.S))
            {
                M_go.transform.Translate(Vector3.back * Time.deltaTime * 3);
            }
        }
           
    }

    public IpokeMon[] GetList()
    {
        return PokeList;
    }

    public IpokeMon GetBattlePokeMon()
    {
        return SelectPokeMon;
    }

    public void Reset()
    {
        BattleIng = false;
    }
    public void OnlyChangeData()
    {
        for (int i = 0; i < pokelist.Length; i++)
        {
            if (pokelist[i] != null)
            {
                if (pokelist[i].Equals(SelectPokeMon) == false)
                {
                    if (pokelist[i].GetData().Attr.hpNow > 0)
                    {
                        pokelist[i].GetGameObject().transform.position = SelectPokeMon.GetGameObject().transform.position;
                        SelectPokeMon = pokelist[i];
                        return;
                    }
                }
            }
        }
    }
    public void ChangePokeMon()
    {
        for (int i = 0; i < pokelist.Length; i++)
        {
            if (pokelist[i] != null)
            {
                if (pokelist[i].Equals(SelectPokeMon) == false)
                {
                    if (pokelist[i].GetData().Attr.hpNow>0)
                    {
                        PokeMonFacade.GetSingelton().AddLog("回来吧" + SelectPokeMon.GetData().name + "!");
                        SelectPokeMon.AsyncHideGameObject(PokeMonAniType.Null);
                        pokelist[i].GetGameObject().transform.position = SelectPokeMon.GetGameObject().transform.position;
                        SelectPokeMon = pokelist[i];
                        PokeMonFacade.GetSingelton().AddLog("敌人非常虚弱！去吧" + SelectPokeMon.GetData().name + "!");
                        PokeMonFacade.GetSingelton().AddAction(() => { MessageCenter.GetSingelton().OnDisPatch(1009); });
                        PokeMonFacade.GetSingelton().RoundOver();
                        return;
                    }
                }
            }
        }
        Debug.Log("没有其他可以更换精灵！");
    }
}
