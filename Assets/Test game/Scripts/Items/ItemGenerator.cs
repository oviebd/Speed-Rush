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
	}

	public void GenerateReward()
	{
		for (int i = 0; i < 2; i++)
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

		float randomXpos = Random.Range(-4f, 4f);
		int randomDistance = Random.Range(10, 20);
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
