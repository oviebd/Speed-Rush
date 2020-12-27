using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameUIPanel : AnimatorPanel
{
	public void OnStartNewGameButtonClicked()
	{
		GameManager.instance.StartNewGame();
		Hide();
	}	
}
