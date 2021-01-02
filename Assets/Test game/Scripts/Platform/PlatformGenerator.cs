using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
	public static PlatformGenerator instance;

	[SerializeField] private List<GameObject> platformPrefabList;

	[SerializeField] private GameObject platformParent;

	private Vector3 _startPosition = Vector3.zero;

	private Vector3 _lastPlatformPosition;

	private int _platformNumber = 1;
	private float lastGenerateTime;
	private Queue<GameObject> platformQueue = new Queue<GameObject>();

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
		ResetData();
	}

	public void ResetData()
	{
		_lastPlatformPosition = _startPosition;
		lastGenerateTime = Time.time;

		Platform[] previousItems =  FindObjectsOfType<Platform>();
		for(int i = 0; i < previousItems.Length; i++)
		{
			Destroy(previousItems[i].gameObject);
		}

		platformQueue = new Queue<GameObject>();
		platformQueue.Clear();
		_platformNumber = 1;

		for (int i = 0; i < 6; i++)
		{
			InstantiatePlatform();
		}
	}

	private void Update()
	{
		if (GameManager.instance.GetPlayerController() == null)
			return;
		int playerZPos = (int)GameManager.instance.GetPlayerController()?.GetPlayerCurrentPosition().z;
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

		EnemyGenerator.instance.GenerateEnemy();
		ItemGenerator.instance.GenerateReward();

		DestroyPlatform();
	}

	private void InstantiatePlatform()
	{
		GameObject platformPrefab = GetRandomPlatformPrefab();
		Platform platformScript = platformPrefab.GetComponent<Platform>();
		if (_platformNumber > 1)
		{
			_lastPlatformPosition.z = platformScript.GetPlatformZaxisSize() + _lastPlatformPosition.z;
		}
		GameObject platformObj = Instantiate(platformPrefab, platformParent.transform);
		platformObj.name = "Platform " + _platformNumber;
		platformObj.transform.parent = null;

		platformObj.transform.position = _lastPlatformPosition;
		platformObj.transform.SetParent(platformParent.transform, false);
		_platformNumber = _platformNumber + 1;

		platformQueue.Enqueue(platformObj);

		
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
		if ( GameManager.instance.GetCurrentGameState() == GameStateEnum.GAME_STATE.RUNNING &&  platformQueue.Count > 6)
		{
			GameObject obj = platformQueue.Dequeue();
			Destroy(obj);
		}
	}
}
