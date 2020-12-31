using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
	public static GameAudioManager instance;

	[SerializeField] private AudioPlayerControler backgroundAudioPlayer;
	[SerializeField] private AudioPlayerControler playerAudioPlayer;


	[SerializeField] private AudioClip playerDieClip;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	private void Start()
	{
		backgroundAudioPlayer?.PlaySound();
	}

	public void PlayPlayerDieSound()
	{
		playerAudioPlayer.SetAudioClip(playerDieClip);
		playerAudioPlayer.PlaySound();
	}

}
