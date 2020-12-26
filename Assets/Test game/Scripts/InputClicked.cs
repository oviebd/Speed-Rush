using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputClicked : MonoBehaviour
{

	public delegate void OnClickedTouchedArea();
	public static event OnClickedTouchedArea onClickedTouchArea;

	public void OnClickedButton()
	{
		Debug.Log("Clicked............");
		onClickedTouchArea?.Invoke();
	}



}
