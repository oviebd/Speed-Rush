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
			//	Debug.Log("Player hit on Item");
		}

		if (collidedObj.tag.Equals(GameTagEnum.TAGS.Enemy.ToString()))
		{
			Destroy(collidedObj);
		}
	}
}

[System.Serializable]
public class ItemBehaviourData
{
	public ItemTypeEnum.ItemType itemType = ItemTypeEnum.ItemType.None;

	public float activeTime = 0;
}
