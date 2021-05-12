using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool isMuted;

    private void Start() {
        isMuted = false;
    }
    public void ToggleSound()
    {
        isMuted = !isMuted;
        if (!isMuted)
        {
            AudioListener.volume = 1f;
        }
        else
        {
            AudioListener.volume = 0f;
        }
    }

    public void SetVolume(float value) {
        AudioListener.volume = value;
    }
}
