using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	[SerializeField] private float platformZaxisSize = 46.0f;

	public float GetPlatformZaxisSize()
	{
		return platformZaxisSize;
	}
}
