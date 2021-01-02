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
	//private bool _canChangeDirection;

	private Vector3 _moveDirectionVector;

	private void Start()
	{
		_currentMaxSpeed = maxPlayerSpeed;
		_currentPlayerSpeed = playerSpeed;
		ResetMoveData();
		_canMove = true;
		//_canChangeDirection = true;

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
	//	_canChangeDirection = false;
		_previousSpeed = _currentPlayerSpeed;
		_currentPlayerSpeed = _currentPlayerSpeed * 2.0f;
		_currentMaxSpeed = maxPlayerSpeed * 2.0f;
	}

	public void GoNormalSpeed()
	{
	//	_canChangeDirection = true; 
		_currentPlayerSpeed = _previousSpeed;
		_currentMaxSpeed = maxPlayerSpeed;
	}

	public void ResetMoveData()
	{
		_moveDirectionVector = new Vector3(0, 0, 1); // Move to z pos 
		currentDirection = MovingDirection.Forward;
	}

	/*private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ChangeDirection();
		}
	}*/

	private void ChangeDirection()
	{
		/*if (_canChangeDirection == false)
			return;*/

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
		float factor = .01f;
		_currentPlayerSpeed = _currentPlayerSpeed + factor;

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
}
