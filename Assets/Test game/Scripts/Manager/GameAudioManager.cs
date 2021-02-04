using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
	public static GameAudioManager instance;

	[SerializeField] private AudioPlayerControler backgroundAudioPlayer;
	[SerializeField] private AudioPlayerControler playerAudioPlayer;

	[SerializeField] private List<AudioClip> backgroundSoundClipList;
	[SerializeField] private AudioClip playerDieClip;

	int _currentBackgroundAudioIndex = -1;

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

	public void PlayBackgroundSound()
	{
		AudioClip clip = GetRandomBackgroundAudioClip();
		if(clip != null)
		{
			backgroundAudioPlayer.PlaySound(clip);
		}
	}

	public void StopBackgroundSound()
	{
		backgroundAudioPlayer.StopSound();
	}

	private AudioClip GetRandomBackgroundAudioClip()
	{
		AudioClip clip = null;

		if (backgroundSoundClipList != null && backgroundSoundClipList.Count > 0)
		{
			int index = Random.Range(0, backgroundSoundClipList.Count);
			while (_currentBackgroundAudioIndex != index)
			{
				clip = backgroundSoundClipList[index];
				_currentBackgroundAudioIndex = index;
			}
		}
		return clip;
	}
}
