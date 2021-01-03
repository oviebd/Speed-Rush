using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, ICollisionEnter
{
	[SerializeField] private GameObject breakEffectObj;
	[SerializeField] private GameObject graphicsObject;
	private GameManager _gameManeger;

	private void Awake()
	{
		if(breakEffectObj != null)
			breakEffectObj.SetActive(false);
	}

	public void onCollisionEnter(GameObject collidedObj)
	{
		if (collidedObj.tag.Equals(GameTagEnum.TAGS.Player.ToString()))
		{
			PlayerController playerController = collidedObj.GetComponent<PlayerController>();
			if (playerController == null)
				return;

			ItemBehaviourData data = playerController?.GetCurrentConsumedItemData();
			if (data.itemType == ItemTypeEnum.ItemType.None)
			{
				playerController.Die();
			}
			if (data.itemType == ItemTypeEnum.ItemType.Breaker)
			{
				graphicsObject.SetActive(false);
				breakEffectObj.SetActive(true);
			}
		}
	}

	void Start()
	{
		_gameManeger = GameManager.instance;
	}

	void Update()
	{
		if (_gameManeger.GetPlayerController()?.GetPlayerCurrentPosition().z > (this.transform.position.z + 10.0f))
		{
			Destroy(this.gameObject);
		}
	}
}
