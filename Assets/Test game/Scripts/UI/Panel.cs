using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
   public virtual void Hide()
	{
		this.gameObject.SetActive(false);
	}

	public virtual void Show()
	{
		this.gameObject.SetActive(true);
	}
}
