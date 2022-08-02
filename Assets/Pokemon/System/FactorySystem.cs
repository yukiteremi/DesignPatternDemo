using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactorySystem : IGameSystem
{
    public IPokeMonAttrFactory pokeMonAttrFactory;
    public IPokeMonFactory pokeMonFactory;
    public IPlayerFactory playerFactory;
    public FactorySystem(PokeMonFacade PBDGame) : base(PBDGame)
    {
    }

    public override void Initialize()
    {
        pokeMonAttrFactory = new PokeMonAttrFactory();
        pokeMonFactory = new PokeMonFactory(m_PBDGame);
        playerFactory = new PlayerFactory();
        pokeMonAttrFactory.Init();
    }
    public IpokeMon CreatePokemon(int id,int lv,Vector3 pos)
    {
        return pokeMonFactory.CreatePokeMon(id,lv,pos);
    }

    public PokeMonData GetNameAndType(int id)
    {
        return pokeMonAttrFactory.GetPokeMon(id);
    }
    public PokeMonBaseType GetTypeData(int id)
    {
        return pokeMonAttrFactory.GetTypeData(id);
    }
    public PokeMonSkillData GetSkill(int id)
    {
        return pokeMonAttrFactory.GetSkillData(id); 
    }
    public PokeMonAttr PokemonData(int Lv)
    {
        PokeMonAttr NewAttr = new PokeMonAttr();
        NewAttr.level = Lv;
        if (Lv==0)
        {
            NewAttr.level = Random.Range(1,10);
        }
        NewAttr.hpMax = 17 + 2 * Lv + Random.Range(0,3);
        NewAttr.hpNow = NewAttr.hpMax;
        NewAttr.atk = 7 + 2 * Lv + Random.Range(0,3);
        NewAttr.def = 6 + 2 * Lv + Random.Range(0, 3);
        return NewAttr;
    }
    
}
