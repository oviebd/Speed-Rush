using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementController))]
public class InputController : MonoBehaviour {


    string _horizontalInput = "Horizontal";
    float _horizontalMoveValue;

    string _jumpInput="Jump";

    private PlayerMovementController _playerMoveController;


    void Start () {

        _playerMoveController = GetComponent<PlayerMovementController>();
	}


	void Update () {
        
    }

   
}
