using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameUiPanel : AnimatorPanel
{
	public void OnPauseGameButtonClicked()
	{
		GameManager.instance.PauseGame();
	}
}
