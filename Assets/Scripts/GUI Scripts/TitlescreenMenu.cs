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
        SceneNameToLoad = PlayerPrefs.GetString("Scene");
        Debug.Log("Play Button Clicked.");
        //playButtonAudioSource.PlayOneShot(playButtonClickSFX);
        SceneManager.LoadScene(SceneNameToLoad, LoadSceneMode.Single);
    }

    public void newGameButtonClicked()
    {
        Debug.Log("New Game Button Clicked.");
        PlayerPrefs.DeleteAll();
        //playButtonAudioSource.PlayOneShot(playButtonClickSFX);
        SceneManager.LoadScene(SceneNameToLoad, LoadSceneMode.Single);
    }
}
