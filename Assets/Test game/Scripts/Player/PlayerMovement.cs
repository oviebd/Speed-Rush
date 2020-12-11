using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float playerSpeed = 2;
	[SerializeField] private float maxPlayerSpeed = 5.0f;

	private float _currentPlayerSpeed;
	private float _currentMaxSpeed;
	private float _previousSpeed;

	private enum MovingDirection { Forward = 0, Right = 1, Left = -1 };

	private MovingDirection currentDirection;

	private bool _canMove;

	private Vector3 _moveDirectionVector;

	private void Start()
	{
		_currentMaxSpeed = maxPlayerSpeed;
		_currentPlayerSpeed = playerSpeed;
		//  SetPlayerSpeed(5.0f);
		ResetMoveData();
		_canMove = true;
	}

	public void SetExtremeSpeed()
	{
		_previousSpeed = _currentPlayerSpeed;
		_currentPlayerSpeed = _currentPlayerSpeed * 2.0f;
		_currentMaxSpeed = maxPlayerSpeed * 2.0f;
	}

	public void GoNormalSpeed()
	{
		_currentPlayerSpeed = _previousSpeed;
		_currentMaxSpeed = maxPlayerSpeed;
	}

	public void ResetMoveData()
	{
		_moveDirectionVector = new Vector3(0, 0, 1); // Move to z pos 
		currentDirection = MovingDirection.Forward;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ChangeDirection();
		}
	}

	private void ChangeDirection()
	{
		switch (currentDirection)
		{
			case MovingDirection.Forward:
				currentDirection = MovingDirection.Right;
				break;
			case MovingDirection.Left:
				currentDirection = MovingDirection.Right;
				break;
			case MovingDirection.Right:
				currentDirection = MovingDirection.Left;
				break;
		}

		_moveDirectionVector.x = (float)currentDirection;
	}

	private void FixedUpdate()
	{
		if (_canMove)
			Move();
	}

	private void Move()
	{
		Vector3 velocity = _moveDirectionVector * _currentPlayerSpeed * Time.deltaTime;
		transform.Translate(velocity);
		SpeedUpGradually();
	}

	void SpeedUpGradually()
	{
		float factor = .01f;
		_currentPlayerSpeed = _currentPlayerSpeed + factor;

		if (_currentPlayerSpeed >= _currentMaxSpeed)
			_currentPlayerSpeed = _currentMaxSpeed;
	}
}
