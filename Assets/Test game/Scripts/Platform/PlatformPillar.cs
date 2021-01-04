using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPillar : MonoBehaviour
{
	[SerializeField] private MeshRenderer renderer;


	private void Start()
	{
		ChangeMaterialColor();
	}

	private void ChangeMaterialColor()
	{
		Color materialColor = PillarColorData.instance.GetPillarColor();
		renderer.material.color = materialColor;
	}
}
