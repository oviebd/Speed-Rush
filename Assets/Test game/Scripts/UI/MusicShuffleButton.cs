using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicShuffleButton : Panel
{
    public void OnEnable()
    {
        bool isAudioOn = AudioManager.instance.IsGameAudioOn();
        if (isAudioOn)
        {
            Show();
        }
        else
        {
            Hide();
        }

    }
}
