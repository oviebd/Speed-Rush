using SaveSystem;
using UnityEngine;

public class PlayerDataSaver : MonoBehaviour
{
	public static PlayerDataSaver instance;

	public delegate void OnPlayerDataUpdated(PlayerDataModel data);
	public static event OnPlayerDataUpdated onPlayerDataUpdated;

	private string fileName = "PlayerData.json";

	private void Awake()
	{
		if (instance == null)
			instance = this;

		GooglePlayServiceManager.onAuthenticatedSuccessfully += OnPlayServiceSuccessfullyAuthenticated;
	}

	private void OnDestroy()
	{
		GooglePlayServiceManager.onAuthenticatedSuccessfully -= OnPlayServiceSuccessfullyAuthenticated;
	}

	private string GetFilePath()
	{
		return FileHandler.GetPersistantFilePath(fileName);
	}

	public PlayerDataModel GetPlayerData()
	{
		return SaveDataHandler.GetData<PlayerDataModel>(GetFilePath(), new PlayerDataModel());
	}

	public void StorePlayerData(PlayerDataModel data)
	{
		SaveDataHandler.SaveData(data, GetFilePath());
		onPlayerDataUpdated?.Invoke(data);
	}

	public void SetHighScore(long currentScore)
	{
		PlayerDataModel data = GetPlayerData();
		if(data.highScore < currentScore)
		{
			data.highScore = currentScore;
		}
		StorePlayerData(data);
	}

	private void OnPlayServiceSuccessfullyAuthenticated(PlayerDataModel authenticateUserData)
	{
		PlayerDataModel savedPlayerData = PlayerDataSaver.instance.GetPlayerData();

		if(savedPlayerData.playerID == authenticateUserData.playerID)
		{
			//Same User
			if (savedPlayerData.highScore > authenticateUserData.highScore)
				authenticateUserData.highScore = savedPlayerData.highScore;
		}

		StorePlayerData(authenticateUserData);
	}
}
