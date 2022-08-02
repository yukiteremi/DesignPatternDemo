using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ICharacter 
{
    protected string C_name;

    protected GameObject C_Go;
    protected NavMeshAgent C_Nav;
    protected AudioSource C_Audio;

    protected string C_Icon = string.Empty;
    protected string C_AssetName = string.Empty;
    protected int C_AttrId=0;

    protected bool C_IsDead=false;

    protected ICharacterAttr C_AttrData;
    private IWeapon C_Weapon;
    protected IAttrStrategy C_Strategy;

    public void SetGameObjectInit(GameObject gameObject)
    {
        C_Go = gameObject;
        C_Nav = gameObject.GetComponent<NavMeshAgent>();
        C_Audio = gameObject.GetComponent<AudioSource>();
    }

    public GameObject GetGameObject()
    {
        return C_Go;
    }
    public string GetName()
    {
        return C_name;
    }
    public string GetAssetName()
    {
        return C_AssetName;
    }
    public int GetAttrID()
    {
        return C_AttrId;
    }
    public AudioSource GetAudio()
    {
        return C_Audio;
    }
    public int GetPower()
    {
        return C_Weapon.GetPower();
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return C_Nav;
    }
    public ICharacterAttr GetCharateData()
    {
        return C_AttrData;
    }
    public void Move(Vector3 Pos)
    {
        C_Nav.SetDestination(Pos);
    }
    public void StopMove()
    {
        C_Nav.isStopped = true;
    }

    public void Attack(ICharacter Target)
    {
        Target.UnderAttack(this);
        /*
         todo
         */
    }

    public void SetWeapon(IWeapon weapon)
    {
        if (C_Weapon!=null)
        {

        }
        C_Weapon = weapon;
    }
    public void SetAttr(ICharacterAttr data)
    {
        C_AttrData = data;
        C_AttrData.InitAttr();


        //C_Nav.speed = C_AttrData.GetMoveSpeed();
        C_name = C_AttrData.GetAttrName();
    }


    public virtual void UnderAttack(ICharacter Attacker)
    {

    }
}

