using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPillar : MonoBehaviour
{
	[SerializeField] private MeshRenderer renderer;
	[SerializeField] private MoveObject moveObject;
	[SerializeField] private AudioSyncScale audioSyncScale;

	private void Awake()
	{
		PlayerDataSaver.onGameModeChanged += OnGameModeChange;
	}
	private void OnDestroy()
	{
		PlayerDataSaver.onGameModeChanged -= OnGameModeChange;
	}

	private void Start()
	{
		ChangeMaterial();
		SetPillar(PlayerDataSaver.instance.IsInDiscoMode());
	}

	private void ChangeMaterial()
	{
		Texture materialTexture = PillarSpriteData.instance.GetPillarTexture();
		renderer.material.SetTexture("_MainTex", materialTexture); 
		//Color materialColor = PillarColorData.instance.GetPillarColor();
		//renderer.material.color = materialColor;
	}

	private void SetPillar(bool isDiscoMode)
	{
		moveObject.enabled = !isDiscoMode;
		audioSyncScale.enabled = isDiscoMode;
	}

	private void OnGameModeChange(GameModeEnum.GAME_MODE gameMode)
	{
		if(gameMode == GameModeEnum.GAME_MODE.MODE_DISCO)
		{
			SetPillar(true);
		}
		else
		{
			SetPillar(false);
		}
	}
}
