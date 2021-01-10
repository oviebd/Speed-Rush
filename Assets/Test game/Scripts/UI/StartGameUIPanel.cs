using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameUIPanel : AnimatorPanel
{
	[SerializeField] private Sprite soundOnSprite;
	[SerializeField] private Sprite soundOffSprite;
	[SerializeField] private Image soundImage;
	[SerializeField] private Panel discoButtonPanel;

	[SerializeField] Text discoModeOnOffText;

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
		SetDiscoButtonUI();
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
		bool isDiscoMode = PlayerDataSaver.instance.IsInDiscoMode();

		if (isDiscoMode == true )
		{
			discoButtonPanel.Hide();
			DialogClass alertDialogClass = new DialogBuilder().
						 Title("Disco Mode!").
						 Message(" Can not Turn off sound in Disco Mode").
						 PositiveButtonText("Ok").
						   PositiveButtonAction((IDialog dialog) =>
						   {
							   dialog.HideDialog();
							   discoButtonPanel.Show();
						   }).
						 build();

			DialogManager.instance.SpawnDialogBasedOnDialogType(DialogTypeEnum.DialogType.AlertDialog, alertDialogClass);
		}
		else
		{
			ToggleAudioState();
		}
	}

	private void ToggleAudioState()
	{
		AudioManager.instance.ChangeGameAudioStatus();
		SetSoundButtonGraphics();
	}

	public void OnDiscoModeButtonClicked()
	{
		bool isDiscoMode =  PlayerDataSaver.instance.IsInDiscoMode();
		PlayerDataModel data = PlayerDataSaver.instance.GetPlayerData();

		if (isDiscoMode)
			data.GameMode = GameModeEnum.GAME_MODE.MODE_NORMAL;
		else
		{
			data.GameMode = GameModeEnum.GAME_MODE.MODE_DISCO;
			if(AudioManager.instance.IsGameAudioOn() == false)
			{
				ToggleAudioState();
			}
		}
		PlayerDataSaver.instance.StorePlayerData(data);
		SetDiscoButtonUI();
	}

	private void SetDiscoButtonUI()
	{
		bool isDiscoMode = PlayerDataSaver.instance.IsInDiscoMode();
		if (isDiscoMode)
			discoModeOnOffText.text = "On";
		else
			discoModeOnOffText.text = "Off";
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
