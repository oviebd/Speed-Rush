using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{

    [SerializeField] private float lerpTime;
    [SerializeField] private float leftMostPosition;
    [SerializeField] private float rightMostPosition;

    private float _lerpingStartTime;

    float _learpValueX = 0;
    private enum MoveDirection { MoveLeft, MoveRight }
    private MoveDirection _currentMoveDirection;

    private bool canMove;
    private float lerpRunningTime;

    private void Start()
    {
		canMove = false;
		_currentMoveDirection = MoveDirection.MoveRight;
		Invoke("StartMove", 1.0f);
		/*	canMove = false;
			_currentMoveDirection = MoveDirection.MoveRight;
			Invoke("StartMove",1.0f);*/
	}

    public void StartMove()
    {
        canMove = true;
        _lerpingStartTime = Time.time;
        lerpRunningTime = 0.0f;
    }

    public void PauseMove()
    {
        canMove = false;
        lerpRunningTime = Time.time - _lerpingStartTime;
    }

    public void ResumeMove()
    {
        canMove = true;
        _lerpingStartTime = Time.time - lerpRunningTime;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            StartMove();
        if (Input.GetKeyDown(KeyCode.P))
            PauseMove();
        if (Input.GetKeyDown(KeyCode.R))
            ResumeMove();

        if (canMove == false)
            return;

        if (_currentMoveDirection == MoveDirection.MoveLeft)
        {
            _learpValueX = LearpValue(rightMostPosition, leftMostPosition, _lerpingStartTime, lerpTime);
        }
        if (_currentMoveDirection == MoveDirection.MoveRight)
        {
            _learpValueX = LearpValue(leftMostPosition, rightMostPosition, _lerpingStartTime, lerpTime);
        }

        Vector3 currenrPos = transform.position;
        currenrPos.x = _learpValueX;

        this.transform.position = currenrPos;

    }
    private void LerpingComplete()
    {
        if (_currentMoveDirection == MoveDirection.MoveRight)
        {
            _currentMoveDirection = MoveDirection.MoveLeft;
        }
        else
        {
            _currentMoveDirection = MoveDirection.MoveRight;
        }

        StartMove();
    }

    public float LearpValue(float start, float end, float timeStartedLearping, float learpTime)
    {

        float timeSinceStarted = Time.time - timeStartedLearping;
        float percentageComplete = timeSinceStarted / learpTime;

        float result = 0;

        result = Mathf.SmoothStep(start, end, percentageComplete);
//        Debug.Log("Resiult " + result);

        if (timeSinceStarted >= learpTime)
        {

            LerpingComplete();
        }

        return result;
    }
}
