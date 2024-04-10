using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSoundBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioSource _beatSound;

    private readonly string _volumeKey = "Volume";

    public void PlayBeatSound()
    {
        _beatSound.volume = PlayerPrefs.GetFloat(_volumeKey);
        _beatSound.Play();
    }
}
