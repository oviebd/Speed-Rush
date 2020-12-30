using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
	public static void DestroyALlSameTypeGameObjects(UnityEngine.Object type)
	{
		UnityEngine.Object[] previousItems =  GameObject.FindObjectsOfType<UnityEngine.Object>();
		for(int i = 0; i < previousItems.Length; i++)
		{
			Destroy(previousItems[i]);
		}
	}

	public static bool isNetworkAvilable()
	{
		bool isNetworkAvilable = false;
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			isNetworkAvilable = false;
			// Debug.Log("Error. Check internet connection!");
		}
		else
		{
			isNetworkAvilable = true;
			// Debug.Log("Internet ase!");
		}

		return isNetworkAvilable;
	}
}
