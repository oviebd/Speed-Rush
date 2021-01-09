using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSpriteData : MonoBehaviour
{
	public static PillarSpriteData instance;
	[SerializeField] private List<PillarSpriteClass> texeureSet;
	private int spriteIndex = 0;

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

	private void OnGameStateChange(GameStateEnum.GAME_STATE gameState)
	{
		switch (gameState)
		{
			case GameStateEnum.GAME_STATE.PREPARE:
				GenerateRandomTextureSet();
				break;
			case GameStateEnum.GAME_STATE.GAME_OVER:
				GenerateRandomTextureSet();
				break;
			default:
				break;
		}
	}

	private void GenerateRandomTextureSet()
	{
		int index = Random.RandomRange(0, texeureSet.Count);
		spriteIndex = index;
	}

	public Texture GetPillarTexture()
	{
		Texture resultedTexture = null;

		PillarSpriteClass pillarSpriteClass = texeureSet[spriteIndex];

		if (pillarSpriteClass != null && pillarSpriteClass.textures != null && pillarSpriteClass.textures.Count > 0)
		{
			int randomIndex = Random.RandomRange(0, pillarSpriteClass.textures.Count);
			resultedTexture = pillarSpriteClass.textures[randomIndex];
		}
		return resultedTexture;
	}
}

[System.Serializable]
public class PillarSpriteClass
{
	public List<Texture> textures;
}
