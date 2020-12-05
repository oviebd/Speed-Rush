using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour,ICollisionEnter {

	[SerializeField] private ItemTypeEnum.ItemType itemType;

	public void onCollisionEnter(GameObject collidedObj)
	{
		if (collidedObj.tag.Equals(GameTagEnum.TAGS.Player.ToString()))
		{
			Debug.Log("Player hit on Item");
		}
	}
}
