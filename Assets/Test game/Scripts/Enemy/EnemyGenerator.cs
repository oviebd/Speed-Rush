using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
	[HideInInspector]
	public static EnemyGenerator instance;

	[SerializeField] private List<GameObject> enemyPrefabList;
	[SerializeField] private GameObject enemyParent;

	[SerializeField] private List<int> enemyNumberListInPerSession;

	private int enemyNumber ;
	private int lastEnemyZpos;

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
		enemyNumber = 1;
		lastEnemyZpos = 0;

		EnemyBehaviour[] previousItems = FindObjectsOfType<EnemyBehaviour>();
		for (int i = 0; i < previousItems.Length; i++)
		{
			Destroy(previousItems[i].gameObject);
		}

		for (int i = 0; i < 10; i++)
		{
			InstantiateEnemy();
		}
	}

	public void GenerateEnemy()
	{
		for (int i = 0; i < 3; i++)
		{
			GameObject enemyObj = InstantiateEnemy();
		}	
	}
	private GameObject InstantiateEnemy()
	{
		GameObject enemyPrefab = GetRandomEnemyPrefab();
		GameObject enemyObj = Instantiate(enemyPrefab, enemyParent.transform);
		enemyObj.name = "Enemy " + enemyNumber;
		enemyObj.transform.parent = null;

		enemyObj.transform.position = GetRandomEnemyPosition();
		enemyObj.transform.SetParent(enemyParent.transform, false);
		enemyNumber = enemyNumber + 1;
		return enemyObj;
	}

	private Vector3 GetRandomEnemyPosition()
	{
		Vector3 randomPosition = Vector3.zero;

		float randomXpos = Random.Range(-3.5f, 3.5f);
		int randomDistance = Random.Range(10,15);
		int zPos = lastEnemyZpos + randomDistance;

		randomPosition = new Vector3(randomXpos, 0, zPos);

		lastEnemyZpos = (int) randomPosition.z;
		return randomPosition;
	}

	private GameObject GetRandomEnemyPrefab()
	{
		GameObject enemyPrefab;
	    int length = enemyPrefabList.Count;
		if (length <= 0)
			return null;

		int randomPercentage = Random.Range(1, 100);
		
		if(enemyNumber < 3)
			enemyPrefab = enemyPrefabList[0];
		else
		{
			if (randomPercentage >= 40 && length > 1)
			{
				int index = Random.Range(1, length);
				enemyPrefab = enemyPrefabList[index];
			}
			else
				enemyPrefab = enemyPrefabList[0];
		}

	

		return enemyPrefab;
	}
}
