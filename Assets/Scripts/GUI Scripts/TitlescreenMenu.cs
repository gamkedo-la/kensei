using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenMenu : MonoBehaviour
{
    public AudioSource playButtonAudioSource;
    public AudioClip playButtonClickSFX;
    public string SceneNameToLoad;
    public void playButtonClicked()
    {
        Debug.Log("Play Button Clicked.");
        //playButtonAudioSource.PlayOneShot(playButtonClickSFX);
        SceneManager.LoadScene(SceneNameToLoad, LoadSceneMode.Single);
    }
}
