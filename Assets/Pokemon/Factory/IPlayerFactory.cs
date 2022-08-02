using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerFactory
{
	Iplayer CreateSoldier<T>(Vector3 SpawnPosition) where T : Iplayer, new();

}
