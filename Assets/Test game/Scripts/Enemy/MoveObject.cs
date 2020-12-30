using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private float lerpTime;
    [SerializeField] private float leftMostPosition;
    [SerializeField] private float rightMostPosition;

	private float topMostPosition;
	 private float bottomMostPosition;

	private float _lerpingStartTime;

    float _learpValueX = 0;
	float _learpValueY = 0;

	private enum MovingAxis { XAxis,YAxis}
	[SerializeField] private MovingAxis movingAxis;

    private enum MoveDirection { MoveLeft, MoveRight,MoveUp,MoveDown }
    private MoveDirection _currentMoveDirection;

    private bool canMove;
    private float lerpRunningTime;

    private void Start()
    {
		canMove = false;
	
		if(movingAxis == MovingAxis.XAxis)
			_currentMoveDirection = MoveDirection.MoveRight;
		else
		{
			if( (Random.Range(0, 10) %2) ==0 )
				_currentMoveDirection = MoveDirection.MoveUp;
			else
				_currentMoveDirection = MoveDirection.MoveDown;

			topMostPosition = Random.Range(-18, -14);
			bottomMostPosition = Random.Range(-20, -24);
			lerpTime = Random.Range(1.0f,3.0f);
		}

		StartMove();
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

		Vector3 currenrPos = transform.position;

		if(movingAxis == MovingAxis.XAxis)
		{
			if (_currentMoveDirection == MoveDirection.MoveLeft)
				_learpValueX = LearpValue(rightMostPosition, leftMostPosition, _lerpingStartTime, lerpTime);

			if (_currentMoveDirection == MoveDirection.MoveRight)
				_learpValueX = LearpValue(leftMostPosition, rightMostPosition, _lerpingStartTime, lerpTime);

			currenrPos.x = _learpValueX;
		}
		else
		{
			if (_currentMoveDirection == MoveDirection.MoveUp)
				_learpValueY = LearpValue(bottomMostPosition, topMostPosition, _lerpingStartTime, lerpTime);

			if (_currentMoveDirection == MoveDirection.MoveDown)
				_learpValueY = LearpValue(topMostPosition, bottomMostPosition, _lerpingStartTime, lerpTime);

			currenrPos.y = _learpValueY;
		}
		
        this.transform.position = currenrPos;

    }
    private void LerpingComplete()
    {
		if(movingAxis == MovingAxis.XAxis)
		{
			if (_currentMoveDirection == MoveDirection.MoveRight)
				_currentMoveDirection = MoveDirection.MoveLeft;
			else
				_currentMoveDirection = MoveDirection.MoveRight;
		}
		else
		{
			if (_currentMoveDirection == MoveDirection.MoveUp)
				_currentMoveDirection = MoveDirection.MoveDown;
			else
				_currentMoveDirection = MoveDirection.MoveUp;
		}

        StartMove();
    }

    public float LearpValue(float start, float end, float timeStartedLearping, float learpTime)
    {
        float timeSinceStarted = Time.time - timeStartedLearping;
        float percentageComplete = timeSinceStarted / learpTime;
        float result = 0;
        result = Mathf.SmoothStep(start, end, percentageComplete);
        if (timeSinceStarted >= learpTime)
            LerpingComplete();
   
        return result;
    }
}
