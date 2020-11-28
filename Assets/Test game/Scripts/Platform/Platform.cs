using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	[SerializeField] private GameObject platform;

	[SerializeField] private float platformZaxisSize = 46.0f;

	public Vector3 GetPlatformSize()
	{
		return platform.transform.localScale;
	}

	public float GetPlatformZaxisSize()
	{
		return platformZaxisSize;
	}
}
