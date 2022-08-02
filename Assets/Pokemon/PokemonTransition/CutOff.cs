using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutOff : MonoBehaviour
{
    public Material material;
    public float now = 0;
    public Texture image1, image2;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ChangeScreem()
    {
        material.SetTexture("_TransitionTex", image1);
        Tweener tweener = DOTween.To(() => now, x => now = x, 1, 2.5f);
        tweener.OnComplete(ScreenToDark);
    }
    public void ScreenToDark()
    {
        material.SetTexture("_TransitionTex", image2);
        DOTween.To(() => now, x => now = x, 0, 2.5f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeScreem();
        }
        material.SetFloat("_Cutoff", now);
    }
}
