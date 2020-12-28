using UnityEngine;

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager instance;

	public delegate void OnScoreChanged(int newScore);
	public static event OnScoreChanged onSCoreChanged;

	private int distance = 0;
	private Vector3 playerInitPosition;
	private GameObject playerObj;
	private bool isCalculationStarted;

	private void Awake()
	{
		if (instance == null)
			instance = this;

		GameManager.onGameStateChanged += OnGameStateChanged;
	}

	void Start()
	{
		ResetCalculateData();
	}

	private void OnDestroy()
	{
		GameManager.onGameStateChanged -= OnGameStateChanged;
	}

	public void OnGameStateChanged(GameStateEnum.GAME_STATE gameState)
	{
		switch (gameState)
		{
			case GameStateEnum.GAME_STATE.PREPARE:
				ResetCalculateData();
				break;
			case GameStateEnum.GAME_STATE.RUNNING:
				StartCalculate();
				break;
			default:
				break;
		}
	}

	public void StartCalculate()
	{
		playerObj = GameManager.instance.GetPlayerController().gameObject;
		playerInitPosition = playerObj.transform.position;
		// Debug.Log("player init : " + playerInitPosition);
		isCalculationStarted = true;
	}

	void ResetCalculateData()
	{
		isCalculationStarted = false;
	//	distance = 0;
		playerInitPosition = Vector3.zero;
		playerObj = null;
	}

	void Update()
	{

		if (GameManager.instance.GetCurrentGameState() == GameStateEnum.GAME_STATE.RUNNING && isCalculationStarted)
		{
			distance = (int)Vector3.Distance(playerObj.transform.position, playerInitPosition);
			onSCoreChanged?.Invoke(distance);
			// distance = ( playerObj.transform.position - playerInitPosition.transform.position).magnitude;
		//	Debug.Log("Distance : " + distance + " Init pos : " + playerInitPosition + "  Curr Pos : " + playerObj.transform.position);

		}
	}

	public int GetScore()
	{
		return distance;
	}
}
