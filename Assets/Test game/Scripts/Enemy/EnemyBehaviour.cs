using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, ICollisionEnter
{
	GameManager _gameManeger;

	public void onCollisionEnter(GameObject collidedObj)
	{
		if (collidedObj.tag.Equals(GameTagEnum.TAGS.Player.ToString()))
		{
			PlayerController playerController = collidedObj.GetComponent<PlayerController>();
			if (playerController == null)
				return;

			playerController.Die();
			ItemBehaviourData data = playerController?.GetCurrentConsumedItemData();
			if (data.itemType == ItemTypeEnum.ItemType.None)
			{
				playerController.Die();
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
