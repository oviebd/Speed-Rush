using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameUIPanel : AnimatorPanel
{
	public void OnClickedLeaderBoardButton()
	{
		GooglePlayServiceManager.instance.ShowLeaderboardUi();
	}

	public void OnStartNewGameButtonClicked()
	{
		GameManager.instance.StartNewGame();
	}	

	public void OnClickedQuitApplication()
	{
		DialogClass actionDialogClass = new DialogBuilder().
						   Title("Quit Game !").
						   Message(" Want to Quit Game ?").
						   PositiveButtonText("OK").
						   NegativeButtonText("Cancel").

						   PositiveButtonAction((IDialog dialog) =>
						   {
							   Debug.Log("Action Dialog posituve Button clicked ");
							   dialog.HideDialog();
							   Application.Quit();
						   }).

						   NegativeButtonAction((IDialog dialog) =>
						   {
							   Debug.Log("Action Dialog Negative Button clicked  ");
							   dialog.HideDialog();
						   }).

						   build();

		DialogManager.instance.SpawnDialogBasedOnDialogType(DialogTypeEnum.DialogType.ActionDialog, actionDialogClass);
	}
}
