using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSFX : MonoBehaviour
{
    public AudioSource playButtonAudioSource;
    public AudioClip playButtonClickSFX;

    public void playClickSound()
    {
        playButtonAudioSource.PlayOneShot(playButtonClickSFX);
        Debug.Log("inside playClickSound");
    }
}
