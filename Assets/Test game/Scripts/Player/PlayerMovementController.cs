using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {

    [SerializeField] private float _forwardSpeed=1.0f;
    private float _moveX;


    int xDir = 2;
    Vector3 movVector;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            xDir = xDir * (-1);
            movVector.x = xDir;
        }
    }

    private Rigidbody _rb;

    private void Start()
    {
        movVector = new Vector3(xDir, 0, 10.0f);
        _rb = GetComponent<Rigidbody>();
    }




    void FixedUpdate()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        Vector3 velocity =movVector * _forwardSpeed * Time.deltaTime;
        transform.Translate(velocity);
    }


   
}
