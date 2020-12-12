using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItems : MonoBehaviour
{
	public GameItemType.TYPE type = GameItemType.TYPE.OBSTACLE_1;
	public int spawnPercentage = 90;
}

public class GameItemType{
	public enum TYPE
	{
		OBSTACLE_1 = 0,
		OBSTACLE_BREAKER = 1
	}
}
