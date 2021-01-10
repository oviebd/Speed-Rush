using UnityEngine;

public class ItemBehaviour : MonoBehaviour, ICollisionEnter
{
	[SerializeField] private ItemBehaviourData behaviourData;

	public void onCollisionEnter(GameObject collidedObj)
	{
		if (collidedObj.tag.Equals(GameTagEnum.TAGS.Player.ToString()))
		{
			//PlayerController playerController = collidedObj.GetComponent<PlayerController>();
			GameManager.instance.GetPlayerController()?.ActivateConsumedItem(behaviourData);
			CountdownController.instance.SHowCountDown(behaviourData.activeTime);
			//	Debug.Log("Player hit on Item");
		}

		if (collidedObj.tag.Equals(GameTagEnum.TAGS.Enemy.ToString()))
		{
			Destroy(collidedObj);
		}
	}

	void Update()
	{
		if (GameManager.instance.GetPlayerController()?.GetPlayerCurrentPosition().z > (this.transform.position.z + 10.0f))
		{
			Destroy(this.gameObject);
		}
	}
}

[System.Serializable]
public class ItemBehaviourData
{
	public ItemTypeEnum.ItemType itemType = ItemTypeEnum.ItemType.None;

	public float activeTime = 0;
}
