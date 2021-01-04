using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarColorData : MonoBehaviour
{
	public static PillarColorData instance;
	[SerializeField] private List <PillorColorClass> colorSet;
	private int colorSetIndex = 0;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		GameManager.onGameStateChanged += OnGameStateChange;
	}

	private void OnDestroy()
	{
		GameManager.onGameStateChanged -= OnGameStateChange;
	}

	private void OnGameStateChange(GameStateEnum.GAME_STATE  gameState)
	{
		switch (gameState)
		{
			case GameStateEnum.GAME_STATE.PREPARE:
				GenerateRandomColorSet();
				break;
			case GameStateEnum.GAME_STATE.GAME_OVER:
				GenerateRandomColorSet();
				break;
			default:
				break;
		}
	}

	private void GenerateRandomColorSet()
	{
		int index = Random.RandomRange(0, colorSet.Count);
		colorSetIndex = index;
	}

	public Color GetPillarColor()
	{
		Color resultedColor = Color.white;

		PillorColorClass pillarColorClassData = colorSet[colorSetIndex];

		if (pillarColorClassData !=null && pillarColorClassData.colors != null && pillarColorClassData.colors.Count > 0)
		{
			int randomIndex = Random.RandomRange(0, pillarColorClassData.colors.Count);
			resultedColor = pillarColorClassData.colors[randomIndex];
		}
		return resultedColor;
	}
}

[System.Serializable]
public class PillorColorClass
{
	public List<Color> colors;
}
