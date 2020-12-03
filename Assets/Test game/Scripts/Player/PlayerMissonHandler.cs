using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissonHandler : MonoBehaviour {

    MissonScriptable currentMisson;
    GameManager _gameManager;
    ScoreManager _scoreManager;

    int currentTarget = 0;

    bool isItScoreMisson,isItDistanceMisson;

    void Start () {
        _gameManager = FindObjectOfType<GameManager>();
        _scoreManager = FindObjectOfType<ScoreManager>();

        ResetData();

        GetCurrentMisson();
	}
	
    public void GetCurrentMisson()
    {
   //   currentMisson = _gameManager.currentMisson;

        Debug.Log("Player Controller Name :" + currentMisson.name + "  target : " + currentMisson.targetedValue);
        if (currentMisson.id == 1)
        {
            //It is Score Misson
            isItScoreMisson = true;
        }

        if(currentMisson.id == 2)
        {
            // Distance Misson
        }
    }

    private void ResetData()
    {
        isItDistanceMisson = false;
        isItScoreMisson = false;
        currentMisson = null;
    }

    private void Update()
    {
        if (isItScoreMisson)
        {
            if(currentMisson.targetedValue<= _scoreManager.score)
            {
               // Debug.Log("Score Misson Complete");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(currentMisson !=null && currentMisson.tag!= null){

            if (other.tag == currentMisson.tag)
            {
                currentTarget++;

                if(currentTarget>= currentMisson.targetedValue)
                {
                    Debug.Log("Heart Misson Complete");
                }
            }
        }
        
    }
}
