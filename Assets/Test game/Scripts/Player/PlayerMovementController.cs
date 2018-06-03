using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    [SerializeField] private float playerSpeed = 2;
    private float maxPlayerSpeed;

    private float _forwardSpeed=10f;  //10
   
    public bool isPlayerMoved { get; set; }

    float xDir = 2; //2
    Vector3 movVector;
   

    private void Start()
    {
       // SetPlayerSpeed(playerSpeed);
      //  isPlayerMoved = true;
      
    }

    public void SetPlayerSpeed(LevelInfoClass level)
    {
        isPlayerMoved = true;
        playerSpeed = level.playerInitSpeed;

        Debug.Log("Init speed "+ playerSpeed);

        maxPlayerSpeed = level.playerMaxSpeed;
        movVector = new Vector3(xDir, 0, _forwardSpeed);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            xDir = xDir * (-1);
            movVector.x = xDir;
        }
    }

    void FixedUpdate()
    {
        MoveForward();
    }

    private void MoveForward()
    {
       
        if (isPlayerMoved)
        {
            Vector3 velocity = movVector * playerSpeed * Time.deltaTime;
           // Debug.Log("Player Mov Vector : " + velocity);
            transform.Translate(velocity);
            SpeedUpGradually();
        }
       
    }

    void SpeedUpGradually()
    {
        float factor = .001f;
        playerSpeed = playerSpeed + factor;

        if (playerSpeed >= maxPlayerSpeed )
            playerSpeed = maxPlayerSpeed ;

        Debug.Log("Player speed : "+ playerSpeed);

    }
   
}
