using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] GameObject explosionEffect;
	[SerializeField] GameObject playerGraphics;

	private PlayerMovement _movement;

	private bool _hasConsumedAnyItem;

	private ItemBehaviourData _currentConsumedItemData;

	private bool _isPlayerDamageable;

	private float _cooldownTime;
	bool _isDie;

	void Start()
	{
		_currentConsumedItemData = new ItemBehaviourData();
		_movement = this.gameObject.GetComponent<PlayerMovement>();
		explosionEffect.SetActive(false);
		_isPlayerDamageable = false;

		_cooldownTime = 1.0f;
		Invoke("MakePlayerDamageable", _cooldownTime);
	}


	private void Update()
	{
		if(GetPlayerCurrentPosition().x >= 4.8 || GetPlayerCurrentPosition().x <= -4.8)
		{
			if(_isDie == false)
				Die();

		}
	}

	void MakePlayerDamageable()
	{
		_isPlayerDamageable = true;
	}

	public Vector3 GetPlayerCurrentPosition()
	{
		return transform.position;
	}

	public void Die()
	{
		_isDie = true;
		_movement.StopMovement();
		playerGraphics.SetActive(false);
		//Debug.Log("Die in Player C");
		GameAudioManager.instance.PlayPlayerDieSound();
		explosionEffect.SetActive(true);

		Invoke("CallEndGame", 1f);
	}

	private void CallEndGame()
	{
		GameManager.instance.EndGame();
	//	Destroy(this.gameObject);
	}

	public void ActivateConsumedItem(ItemBehaviourData data)
	{
		_currentConsumedItemData = data;
		_hasConsumedAnyItem = true;

		if (_currentConsumedItemData.itemType == ItemTypeEnum.ItemType.Breaker)
		{
			_movement.SetExtremeSpeed();
		}
		CancelInvoke();
		Invoke("DeactivateConsumedItem", data.activeTime);
	}

	private void DeactivateConsumedItem()
	{
		if (_currentConsumedItemData.itemType == ItemTypeEnum.ItemType.Breaker)
		{
			_movement.GoNormalSpeed();
		}
		_currentConsumedItemData = new ItemBehaviourData();
		_hasConsumedAnyItem = false;
	}

	public ItemBehaviourData GetCurrentConsumedItemData()
	{
		return _currentConsumedItemData;
	}

	public PlayerMovement GetPlayerMovement()
	{
		if(_movement == null)
			_movement = this.gameObject.GetComponent<PlayerMovement>();
		return _movement;
	}
}
