using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
	[HideInInspector]
	public static ItemGenerator instance;

	[SerializeField] private List<GameObject> rewardPrefabList;
	[SerializeField] private GameObject rewardParent;

	private int itemNumber = 1;
	private int lastItemZPos = 0;
	private float lastGenerateTime;
	private bool isFirstTimeCreated;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		ResetData();
	}


	public void ResetData()
	{
		itemNumber = 1;
		lastItemZPos = 0;

		lastGenerateTime = Time.time;
		isFirstTimeCreated = true;
		Debug.Log("Reset Data in itm Last time - " + lastGenerateTime);
		ItemBehaviour[] previousItems = FindObjectsOfType<ItemBehaviour>();
		for (int i = 0; i < previousItems.Length; i++)
		{
			Destroy(previousItems[i].gameObject);
		}

		InstantiateReward();
		for (int i = 0; i < 1; i++)
		{
			
		}
		//GenerateReward();
	}

	public void GenerateReward()
	{
		if ( (Time.time - lastGenerateTime) < 1.0f)
			return;
	
		lastGenerateTime = Time.time;

		GameObject enemyObj = InstantiateReward();

	}
	private GameObject InstantiateReward()
	{
		Vector3 randomPos = GetRandomRewardItemPosition(); ;
		if (randomPos.z == 0)
		{
			return null;
		}

		GameObject enemyPrefab = GetRandomRewardPrefab();
		GameObject enemyObj = Instantiate(enemyPrefab, rewardParent.transform);
		enemyObj.name = "Reward " + itemNumber;
		enemyObj.transform.parent = null;

	
		enemyObj.transform.position = randomPos;

		enemyObj.transform.SetParent(rewardParent.transform, false);
		itemNumber = itemNumber + 1;
		return enemyObj;
	}

	private Vector3 GetRandomRewardItemPosition()
	{
		Vector3 randomPosition = Vector3.zero;

		float randomXpos = Random.Range(-3f, 3f);
		int randomDistance = Random.Range(100, 200);
		//int randomDistance = Random.Range(100, 300);
		int zPos = lastItemZPos + randomDistance;

		if (GameManager.instance.GetPlayerController() != null && zPos <= GameManager.instance.GetPlayerController()?.GetPlayerCurrentPosition().z + 100.0f)
		{
			randomPosition = new Vector3(randomXpos, 0, zPos);
			lastItemZPos = (int)randomPosition.z;
		}
		return randomPosition;
	}

	private GameObject GetRandomRewardPrefab()
	{
		int length = rewardPrefabList.Count;
		int randomIndex = Random.Range(0, length);
		GameObject platformPrefab = rewardPrefabList[randomIndex];
		return platformPrefab;
	}
}
