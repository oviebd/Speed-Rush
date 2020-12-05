using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] GameObject explosionEffect;

	private PlayerMovement _movement;

	private bool _hasConsumedAnyItem;

	private ItemBehaviourData _currentConsumedItemData;

	private bool _isPlayerDamageable;

	private float _cooldownTime;

	void Start()
	{
		_currentConsumedItemData = new ItemBehaviourData();
		_movement = this.gameObject.GetComponent<PlayerMovement>();
		explosionEffect.SetActive(false);
		_isPlayerDamageable = false;

		_cooldownTime = 1.0f;
		Invoke("MakePlayerDamageable", _cooldownTime);
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
		Debug.Log("Die in Player C");
		explosionEffect.SetActive(true);
		Invoke("CallEndGame", 1f);
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
}
