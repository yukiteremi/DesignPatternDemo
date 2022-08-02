using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeMonFactory : IPokeMonFactory
{
    private PokeMonBuilderSystem m_BuilderDirector = new PokeMonBuilderSystem(PokeMonFacade.GetSingelton());
    public PokeMonFactory(PokeMonFacade PBDGame) : base(PBDGame)
    {
    }

    public override IpokeMon CreatePokeMon(int AttrID,int lv,Vector3 pos)
    {
        PokeMonBuildParam SoldierParam = new PokeMonBuildParam();

        SoldierParam.AttrID = AttrID;
        SoldierParam.lv = lv;
        switch (AttrID)
        {
            case 1:
                SoldierParam.NewPokeMon = new FireDragon();
                break;
            case 2:
                SoldierParam.NewPokeMon = new Squirtle();
                break;
            case 3:
                SoldierParam.NewPokeMon = new Bulbasaur();
                break;
            default:
                break;
        }
       
        IPokeMonBuilder pokeMon = new PokemonBuilder();
        pokeMon.SetBuildParam(SoldierParam);
        m_BuilderDirector.Construct(pokeMon, AttrID,pos);
        Debug.Log(SoldierParam.NewPokeMon.GetGameObject().transform.position);
        return SoldierParam.NewPokeMon;
    }
}
