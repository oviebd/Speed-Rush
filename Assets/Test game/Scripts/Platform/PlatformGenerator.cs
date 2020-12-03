using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
	[SerializeField] private List<GameObject> platformPrefabList;

	[SerializeField] private GameObject platformParent;

	private Vector3 _startPosition = Vector3.zero;

	private Vector3 _lastPlatformPosition;

	private int platformNumber = 1;

	//public EnemyGenerator enemyGenerator;
	private float lastGenerateTime;

	private Queue<GameObject> platformQueue = new Queue<GameObject>();

	private PlayerController _pllayerController;

	private void Start()
	{
		_pllayerController = FindObjectOfType<PlayerController>();

		_lastPlatformPosition = _startPosition;
		lastGenerateTime = Time.time;

		for (int i = 0; i < 6; i++)
		{
			InstantiatePlatform();
		}
	}

	private void Update()
	{
		int playerZPos = (int)_pllayerController.GetPlayerCurrentPosition().z;
		if ( playerZPos > 0 &&  (playerZPos % 30) == 0)
		{
			GeneratePlatforms();
		}
	}

	public void GeneratePlatforms()
	{

		if ( (Time.time - lastGenerateTime) < 1.0f)
			return;
		

		lastGenerateTime = Time.time;
		InstantiatePlatform();
		//enemyGenerator.GenerateEnemy();
		DestroyPlatform();
	}

	private void InstantiatePlatform()
	{
		GameObject platformPrefab = GetRandomPlatformPrefab();
		Platform platformScript = platformPrefab.GetComponent<Platform>();
		//	Vector3 platformSize = platformScript.GetPlatformSize();
		if (platformNumber > 1)
		{
			//_lastPlatformPosition.z = platformSize.y + _lastPlatformPosition.z;
			_lastPlatformPosition.z = platformScript.GetPlatformZaxisSize() + _lastPlatformPosition.z;
		}
		GameObject platformObj = Instantiate(platformPrefab, platformParent.transform);
		platformObj.name = "Platform " + platformNumber;
		platformObj.transform.parent = null;

		platformObj.transform.position = _lastPlatformPosition;
		platformObj.transform.SetParent(platformParent.transform, false);
		platformNumber = platformNumber + 1;

		platformQueue.Enqueue(platformObj);

		EnemyGenerator.instance.GenerateEnemy();
	}

	private GameObject GetRandomPlatformPrefab()
	{
		int length = platformPrefabList.Count;
		int randomIndex = Random.Range(0, length);
		GameObject platformPrefab = platformPrefabList[randomIndex];
		return platformPrefab;
	}

	private void DestroyPlatform()
	{
		if (platformQueue.Count > 6)
		{
			GameObject obj = platformQueue.Dequeue();
			Destroy(obj);
		}
	}
}
