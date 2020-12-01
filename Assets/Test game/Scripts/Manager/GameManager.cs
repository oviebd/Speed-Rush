using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance =null;

    [SerializeField] private GameObject _environmentPrefab;
    [SerializeField] private GameObject _cameraObj;

    int levelNo;

    private GameObject initPositionOfCamera; 
    private GameObject _instanceEnvironment;

    public bool _isGameRunning { get; set; }


    int score ;

    private UiManager uiManager;
    private MissonController _missonController;

    //Misson 
    public MissonScriptable currentMisson { get; set; }

    void Awake()
    {
      
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

   

    void Start () {

        uiManager = FindObjectOfType<UiManager>();
        _missonController = FindObjectOfType<MissonController>();

        levelNo = 1;

        initPositionOfCamera = _cameraObj;
        StrtGame();
	}

   


   
    void StrtGame()
    {
        uiManager.ShowInGamePanel();
        ResumeGame();

        score = 0;
        _instanceEnvironment = Instantiate(_environmentPrefab);
        _instanceEnvironment.transform.position = _environmentPrefab.transform.position;
        _instanceEnvironment.transform.rotation = _environmentPrefab.transform.rotation;

        _cameraObj.transform.position = initPositionOfCamera.transform.position;
        _cameraObj.transform.rotation = initPositionOfCamera.transform.rotation;
        _cameraObj.SetActive(true);

        _isGameRunning = true;

        FindObjectOfType<ScoreManager>().StartCalculateScoreAndDistance();

        SetLevel();
        SetMissonMisson();
      //  _cameraObj.GetComponent<CameraFollower>().SetCameraTarget();

    }

    void SetLevel()
    {
       LevelManager levelManager = FindObjectOfType<LevelManager>();
     //  int levelNo = levelManager.levelNo;
       
       LevelInfoClass levelClass = levelManager.GetLevelDificulty(levelNo);
        Debug.Log("Current Level "+ levelNo + " Level Info " + levelClass.playerInitSpeed);
        FindObjectOfType<PlayerMovementController>().SetPlayerSpeed(levelClass);
        FindObjectOfType<GenerateLevel>().SetData(levelClass);

    }

    public void SetMissonMisson()
    {
      currentMisson =  _missonController.SetMissonData(levelNo);
    }

    public void EndGame()
    {
        uiManager.ShowEndGamePanel();
        _isGameRunning = false;
        _cameraObj.GetComponent<CameraFollower>().MakeCameraTargetLost();
      //  _cameraObj.SetActive(false);
        Destroy(_instanceEnvironment);
    }
   

   public void Restart()
    {
        EndGame();
        StrtGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

	
}
