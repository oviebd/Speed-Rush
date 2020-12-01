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

	private int enemyNumber = 1;
	private int lastEnemyZpos = 0;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void Start()
	{
		//GenerateEnemy();
	}

	public void GenerateEnemy()
	{
		Debug.Log("Generate Enemy...");
		for (int i = 0; i < 10; i++)
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
		int randomDistance = Random.Range(2,5);
		int zPos = lastEnemyZpos + randomDistance;

		randomPosition = new Vector3(randomXpos, 0, zPos);

		lastEnemyZpos = (int) randomPosition.z;
		return randomPosition;
	}

	private GameObject GetRandomEnemyPrefab()
	{
		int length = enemyPrefabList.Count;
		int randomIndex = Random.Range(0, length);
		GameObject platformPrefab = enemyPrefabList[randomIndex];
		return platformPrefab;
	}
}
