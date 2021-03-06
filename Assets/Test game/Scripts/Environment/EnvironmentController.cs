﻿using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
	public static EnvironmentController instance;
	[SerializeField] private GameObject playerPrefab;
	[SerializeField] private GameObject environmentParent;
	[SerializeField] private  CinemachineVirtualCamera vCam;
	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	public void PrepareForNewGame()
	{
		PlatformGenerator.instance.ResetData();
		EnemyGenerator.instance.ResetData();
		ItemGenerator.instance.ResetData();
		InstantiatePlayer(Vector3.zero);
	}

	public void RespawnPlayer(Vector3 playerPosition)
	{
		InstantiatePlayer(playerPosition);
	}

	private PlayerController InstantiatePlayer(Vector3 playerPosition)
	{

		PlayerController previousPlayer = FindObjectOfType<PlayerController>();
		if(previousPlayer!= null)
		{
			Destroy(previousPlayer.gameObject);
		}

		GameObject playerObj = Instantiate(playerPrefab, environmentParent.transform);
		playerObj.name = "Player " ;
		playerObj.transform.parent = null;
		playerObj.transform.SetParent(environmentParent.transform, false);

        if(playerPosition != Vector3.zero)
        {
			playerPosition.x = 0;
			playerObj.transform.position = playerPosition;
        }

		PlayerController playerController = playerObj.GetComponent<PlayerController>();
		GameManager.instance.SetPlayerController(playerController);
		vCam.LookAt = (playerObj.transform);
		vCam.Follow = playerObj.transform;

		return playerController;
	}


	void Start()
    {
        
    }
}
