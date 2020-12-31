using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameUIPanel : AnimatorPanel
{
	[SerializeField] private Sprite soundOnSprite;
	[SerializeField] private Sprite soundOffSprite;
	[SerializeField] private Image soundImage;

	private void Awake()
	{
		AudioManager.onAudioStateChange += onSOundStateChange;
	}

	private void OnDestroy()
	{
		AudioManager.onAudioStateChange -= onSOundStateChange;
	}

	private void Start()
	{
		SetSoundButtonGraphics();
	}

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


	public void OnSoundButtonClicked()
	{
		AudioManager.instance.ChangeGameAudioStatus();
		SetSoundButtonGraphics();
	}



	#region Sound
	void onSOundStateChange(bool isSoundOn)
	{
		SetSoundButtonGraphics();
	}
	private void SetSoundButtonGraphics()
	{
		bool isAudioOn = AudioManager.instance.IsGameAudioOn();
		if (isAudioOn)
			soundImage.sprite = soundOnSprite;
		else
			soundImage.sprite = soundOffSprite;
	}

	#endregion Sound
}
