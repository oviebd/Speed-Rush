using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject _environmentPrefab;
    [SerializeField] private GameObject _cameraObj;


    private GameObject initPositionOfCamera; 
    private GameObject _instanceEnvironment;

    private bool _isGameRunning;


    int score ;

	void Start () {
        initPositionOfCamera = _cameraObj;
        StrtGame();
	}

   

    private void Update()
    {
        if (_isGameRunning)
        {
            score++;
            FindObjectOfType<UiManager>().UpdateScoreText(score);
        }
       
    }

    void StrtGame()
    {
        score = 0;
        _instanceEnvironment = Instantiate(_environmentPrefab);
        _instanceEnvironment.transform.position = _environmentPrefab.transform.position;
        _instanceEnvironment.transform.rotation = _environmentPrefab.transform.rotation;

        _cameraObj.transform.position = initPositionOfCamera.transform.position;
        _cameraObj.transform.rotation = initPositionOfCamera.transform.rotation;
        _cameraObj.SetActive(true);

        _isGameRunning = true;
      //  _cameraObj.GetComponent<CameraFollower>().SetCameraTarget();

    }

    public void EndGame()
    {
        _isGameRunning = false;
        _cameraObj.GetComponent<CameraFollower>().MakeCameraTargetLost();
        _cameraObj.SetActive(false);
        Destroy(_instanceEnvironment);
    }

   public void Restart()
    {
        EndGame();
        StrtGame();
    }

	
}
