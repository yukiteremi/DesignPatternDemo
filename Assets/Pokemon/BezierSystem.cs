using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BezierSystem :IGameSystem
{
    float interpolateAmout;
    public Transform Ball;
    float delta = 0;
    public BezierSystem(PokeMonFacade PBDGame) : base(PBDGame)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void Initialize()
    {
        Ball = GameObject.Find("Ball").transform;
    }
    /*
     三节点贝塞尔曲线
     */
    private Vector3 LinearLerp(Vector3 a, Vector3 b, float t)
    {
        return Vector3.Lerp(a, b, t);
    }
    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = LinearLerp(a, b, t);
        Vector3 bc = LinearLerp(b, c, t);

        return Vector3.Lerp(ab, bc, interpolateAmout);
    }


    //public void shoot(Transform start, Transform end,BallType type)
    //{
    //    delta = 0;
    //    //中点
    //    Vector3 pos = (end.position - start.position) / 2 + start.position;
    //    pos.y = 6;
    //    Vector3 Leppos = QuadraticLerp(start.position, pos, end.position, delta);
    //    Ball.DOLocalMove(Leppos,0.01f);
    //}

    

    public void StartBeizier(Transform start,Transform end,MonoBehaviour mono,bool flag)
    {
        Ball.transform.position = start.transform.position;
        mono.StartCoroutine(Beizier(start,end, Ball.transform, flag));
    }
    public void StartBeizier2(Transform start, Transform end, MonoBehaviour mono)
    {
        Ball.transform.position = start.transform.position;
        mono.StartCoroutine(Beizier2(start, end, Ball.transform));
    }
    IEnumerator Beizier2(Transform start, Transform end, Transform Ball)
    {
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        Vector3 pos = (end.position - start.position) / 2 + start.position;
        pos.y = 6;
        while (Vector3.Distance(Ball.transform.position, end.transform.position) > 0.3f)
        {
            interpolateAmout = (interpolateAmout + Time.deltaTime * 4f) % 1f;
            Ball.position = QuadraticLerp(start.position, pos, end.position, interpolateAmout);
            yield return wait;
        }
        Debug.Log("球到了！");
        MessageCenter.GetSingelton().OnDisPatch(1005);
        Ball.transform.position = new Vector3(1000, 1000, 1000);
    }


    IEnumerator Beizier(Transform start, Transform end, Transform Ball,bool flag)
    {
        WaitForSeconds wait= new WaitForSeconds(0.01f);
        Vector3 pos = (end.position - start.position) / 2 + start.position;
        pos.y = 6;
        while (Vector3.Distance(Ball.transform.position, end.transform.position) > 0.3f)
        {
            interpolateAmout = (interpolateAmout + Time.deltaTime*4f) % 1f;
            Ball.position = QuadraticLerp(start.position, pos, end.position, interpolateAmout);
            yield return wait;
        }
        if (!flag)
        {
            MessageCenter.GetSingelton().OnDisPatch(1005);
        }
        else
        {
            MessageCenter.GetSingelton().OnDisPatch(1006);
        }
        Ball.transform.position = new Vector3(1000,1000,1000);
    }
   
   
 
}

public enum BallType
{
    ShowMyPokeMon,
    CatchPokeMonSuccess,
    CatchPokeMonFail
}