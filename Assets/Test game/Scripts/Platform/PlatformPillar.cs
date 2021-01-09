using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPillar : MonoBehaviour
{
	[SerializeField] private MeshRenderer renderer;


	private void Start()
	{
		ChangeMaterial();
	}

	private void ChangeMaterial()
	{
		Texture materialTexture = PillarSpriteData.instance.GetPillarTexture();
		renderer.material.SetTexture("_MainTex", materialTexture); 
		//Color materialColor = PillarColorData.instance.GetPillarColor();
		//renderer.material.color = materialColor;
	}

	
}
