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

	private void Awake()
	{
		if (instance == null)
			instance = this;

		ResetData();
	}

	private void Update()
	{
		if (GameManager.instance.GetPlayerController() == null)
			return;
		int playerZPos = (int)GameManager.instance.GetPlayerController()?.GetPlayerCurrentPosition().z;
		if ((playerZPos - 10) > this.gameObject.transform.position.z)
		{
			Destroy(this.gameObject);
		}
	}


	public void ResetData()
	{
		itemNumber = 1;
		lastItemZPos = 0;

		ItemBehaviour[] previousItems = FindObjectsOfType<ItemBehaviour>();
		for (int i = 0; i < previousItems.Length; i++)
		{
			Destroy(previousItems[i].gameObject);
		}
		GenerateReward();
	}

	public void GenerateReward()
	{
		for (int i = 0; i < 4; i++)
		{
			GameObject enemyObj = InstantiateReward();
		}
	}
	private GameObject InstantiateReward()
	{
		GameObject enemyPrefab = GetRandomRewardPrefab();
		GameObject enemyObj = Instantiate(enemyPrefab, rewardParent.transform);
		enemyObj.name = "Reward " + itemNumber;
		enemyObj.transform.parent = null;

		enemyObj.transform.position = GetRandomEnemyPosition();
		enemyObj.transform.SetParent(rewardParent.transform, false);
		itemNumber = itemNumber + 1;
		return enemyObj;
	}

	private Vector3 GetRandomEnemyPosition()
	{
		Vector3 randomPosition = Vector3.zero;

		float randomXpos = Random.Range(-3f, 3f);
		int randomDistance = Random.Range(100, 200);
		//int randomDistance = Random.Range(100, 300);
		int zPos = lastItemZPos + randomDistance;

		randomPosition = new Vector3(randomXpos, 0, zPos);

		lastItemZPos = (int)randomPosition.z;
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
