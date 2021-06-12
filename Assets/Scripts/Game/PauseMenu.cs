using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool isPaused;
    public string mainMenuSceneName = "Titlescreen";


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        if (pauseMenu == null)
        {
            //if scene changes, find and hide new pause menu
            pauseMenu = GameObject.Find("PauseMenu");
            pauseMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu == null)
        {
            //if scene changes, find and hide new pause menu
            pauseMenu = GameObject.Find("PauseMenu");
            pauseMenu.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenu()
    {
        ResumeGame();
        SceneManager.LoadScene(mainMenuSceneName);
        Destroy(gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quitting... (Not really, this doesn't work while in the Editor, but will on a build)");
    }
}
