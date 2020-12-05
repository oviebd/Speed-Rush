using UnityEngine;

public class ItemBehaviour : MonoBehaviour, ICollisionEnter
{
	[SerializeField] private ItemBehaviourData behaviourData;

	public void onCollisionEnter(GameObject collidedObj)
	{
		if (collidedObj.tag.Equals(GameTagEnum.TAGS.Player.ToString()))
		{
			PlayerController playerController = collidedObj.GetComponent<PlayerController>();
			playerController?.ActivateConsumedItem(behaviourData);
			//	Debug.Log("Player hit on Item");
		}
	}
}

[System.Serializable]
public class ItemBehaviourData
{
	public ItemTypeEnum.ItemType itemType = ItemTypeEnum.ItemType.None;

	public float activeTime = 0;
}
