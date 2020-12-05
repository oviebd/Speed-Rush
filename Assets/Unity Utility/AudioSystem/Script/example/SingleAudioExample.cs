using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAudioExample : MonoBehaviour
{
    [SerializeField] private AudioPlayerControler _gameAudio;

    public void PlaySound()
    {
        _gameAudio.PlaySound();
    }
}
