using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlescreenMenu : MonoBehaviour
{
    public string SceneNameToLoad;
    public void playButtonClicked()
    {
        Debug.Log("Play Button Clicked.");
        SceneManager.LoadScene(SceneNameToLoad, LoadSceneMode.Single);
    }
}
