using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float playerSpeed = 2;
	[SerializeField] private float maxPlayerSpeed = 5.0f;

	private PlayerController _playerController;

	private float _currentPlayerSpeed;
	private float _currentMaxSpeed;
	private float _previousSpeed;

	private enum MovingDirection { Forward = 0, Right = 1, Left = -1 };

	private MovingDirection currentDirection;

	private bool _canMove;
	private bool _canChangeDirection;

	private Vector3 _moveDirectionVector;
	bool isInMaxSpeedMode = false;

	private void Start()
	{
		_currentMaxSpeed = maxPlayerSpeed;
		_currentPlayerSpeed = playerSpeed;
		ResetMoveData();
		_canMove = true;
		_canChangeDirection = true;

		_playerController = this.gameObject.GetComponent<PlayerController>();
		InputClicked.onClickedTouchArea += ChangeDirection;
	}

	private void OnDestroy()
	{
		InputClicked.onClickedTouchArea -= ChangeDirection;
	}

	public void SetExtremeSpeed()
	{
		currentDirection = MovingDirection.Forward;
	    _canChangeDirection = false;
		_previousSpeed = _currentPlayerSpeed;
		_currentPlayerSpeed = _currentPlayerSpeed * 3f;
		_currentMaxSpeed = maxPlayerSpeed * 3f;
		isInMaxSpeedMode = true;
	}

	public void GoNormalSpeed()
	{
		_canChangeDirection = true; 
		_currentPlayerSpeed = _previousSpeed;
		_currentMaxSpeed = maxPlayerSpeed;
		isInMaxSpeedMode = false;
	}

	public void ResetMoveData()
	{
		_moveDirectionVector = new Vector3(0, 0, 1); // Move to z pos 
		currentDirection = MovingDirection.Forward;
	}

	private void ChangeDirection()
	{
		if (_canChangeDirection == false)
			return;

		switch (currentDirection)
		{
			case MovingDirection.Forward:
				/*	if(_playerController.GetPlayerCurrentPosition().x <=0)
					{
						currentDirection = MovingDirection.Right;
					}
					else
					{
						currentDirection = MovingDirection.Left;
					}*/
				currentDirection = MovingDirection.Right;
				break;
			case MovingDirection.Left:
				currentDirection = MovingDirection.Right;
				break;
			case MovingDirection.Right:
				currentDirection = MovingDirection.Left;
				break;
		}
	
	}

	private void FixedUpdate()
	{
		if (_canMove)
			Move();
	}

	private void Move()
	{
		_moveDirectionVector.x = (float)currentDirection;

		Vector3 velocity = _moveDirectionVector * _currentPlayerSpeed * Time.deltaTime;
		transform.Translate(velocity);
		SpeedUpGradually();
	}

	void SpeedUpGradually()
	{
		if (isInMaxSpeedMode == true)
			return;
		//float factor = .01f;
		float factor = .15f;
		_currentPlayerSpeed = _currentPlayerSpeed + (factor * Time.deltaTime);
		if (_currentPlayerSpeed >= _currentMaxSpeed)
			_currentPlayerSpeed = _currentMaxSpeed;
	}

	public void StopMovement()
	{
		_canMove = false;
	}
	public void StartMovement()
	{
		_canMove = true;
	}

    public float GetCurrentSpeed()
    {
		/* if(_currentPlayerSpeed > playerSpeed + 2)
		 {
			 return _currentPlayerSpeed - 2;
		 }*/
		//	return _currentPlayerSpeed;
		return playerSpeed + 1.5f;

	}

    public void SetSpeedData(float speed)
    {
		playerSpeed = speed;
		_currentPlayerSpeed = playerSpeed;
	}

}
