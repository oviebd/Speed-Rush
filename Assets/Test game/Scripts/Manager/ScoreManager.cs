
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    UiManager _uiManager;
    GameManager _gameManager;

    public int score = 0;
  
    Vector3 playerInitPosition;
    public int distance = 0;
    GameObject playerObj;

    bool isCalculationStarted;
   

    void Start () {

        _uiManager = FindObjectOfType<UiManager>();
        //  _gameManager = FindObjectOfType<GameManager>();
        _gameManager = GameManager.instance;
        ResetScoreManagerData();
	}
	
    public void StartCalculateScoreAndDistance()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerInitPosition = playerObj.transform.position;
       // Debug.Log("player init : " + playerInitPosition);
        isCalculationStarted = true;

    }

    void ResetScoreManagerData()
    {
        isCalculationStarted = false;
        distance = 0;
        score = 0;
        playerInitPosition = Vector3.zero;
        playerObj = null;
    }

	void Update () {

        if (_gameManager._isGameRunning && isCalculationStarted)
        {
            score++;
            _uiManager.UpdateScoreText(score);
          distance = (int) Vector3.Distance(playerObj.transform.position, playerInitPosition);
            // distance = ( playerObj.transform.position - playerInitPosition.transform.position).magnitude;
            _uiManager.UpdateDistanceText(distance);
         //   Debug.Log("Distance : " + distance + " Init pos : " + playerInitPosition + "  Curr Pos : " + playerObj.transform.position);

        }
	}
}
