using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Iplayer 
{
    IpokeMon[] PokeList { get; set; }
    GameObject M_go { get; set; }

    void SetGameObject(GameObject go);
    void Move();
    void Check(MonoBehaviour mono);

    void AddPokeMon(IpokeMon pokemon);
    IpokeMon[] GetList();
    IpokeMon SelcetPokemon();
    IpokeMon GetBattlePokeMon();
    void Reset();

    void ChangePokeMon();
    void OnlyChangeData();
}
