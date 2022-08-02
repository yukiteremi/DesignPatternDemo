using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CutOffSystem : IGameSystem
{
    public CutOffSystem(PokeMonFacade PBDGame) : base(PBDGame)
    {
    }
    public Material material;
    public float now = 0;
    public Texture image1, image2;
    int nowMessage = 0;
    bool flag = false;

    public override void Initialize()
    {
        material = Resources.Load<Material>("Show");
        image1= Resources.Load<Texture>("Textures/angular_pattern");
        image2 = Resources.Load<Texture>("Textures/topbottom_pattern");
    }
    public void CutUpdate()
    {
        if (flag)
        {
            material.SetFloat("_Cutoff", now);
        }
    }
    
    public void ChangeScreem(ScreenChangeType message)
    {
        switch (message)
        {
            case ScreenChangeType.MeetPokeMon:
                material.SetTexture("_TransitionTex", image1);
                nowMessage = 1001;
                break;
            case ScreenChangeType.BattleOver:
                material.SetTexture("_TransitionTex", image2);
                nowMessage = 1010;
                break;
            default:
                break;
        }
        flag = true;
        Tweener tweener = DOTween.To(() => now, x => now = x, 1,2.5f);
        tweener.OnComplete(ScreenToDark);
    }

    public void ScreenToDark()
    {
        if (nowMessage == 1010)
        {
            m_PBDGame.GetCameraSystem().ChangeSimpleView();
            m_PBDGame.GetCharatorSystem().PokeMon.AsyncHideGameObject(PokeMonAniType.Null);
            m_PBDGame.GetCharatorSystem().player.SelcetPokemon().AsyncHideGameObject(PokeMonAniType.Null);
        }
        else if (nowMessage == 1001)
        {
            MessageCenter.GetSingelton().OnDisPatch(1000);
        }
        material.SetTexture("_TransitionTex", image2);
        Tweener tweener = DOTween.To(() => now, x => now = x, 1, 2f);
        tweener.OnComplete(ScreenKeepDark);
    }
    public void ScreenKeepDark()
    {
        Tweener tweener = DOTween.To(() => now, x => now = x, 0, 2.5f);
        tweener.OnComplete(ScreenToLight);
    }
    public void ScreenToLight()
    {
        if (nowMessage== 1001)
        {
            MessageCenter.GetSingelton().OnDisPatch(1001);
        }
        else if (nowMessage == 1010)
        {
            MessageCenter.GetSingelton().OnDisPatch(1010);
        }
        nowMessage = 0;
        flag = false;
    }
}

public enum ScreenChangeType
{
    MeetPokeMon,
    BattleOver
}