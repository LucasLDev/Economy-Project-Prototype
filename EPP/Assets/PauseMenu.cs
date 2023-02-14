using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Currency currency;

    public MainMenu mainMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            } else    
            {

                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        mainMenu.hasSave = true;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        GameIsPaused = false;
        //SceneManager.LoadScene("Menu");
    }

    public void ClearProgress()
    {
        Debug.Log("Cleared Currency");
        PlayerPrefs.DeleteAll();
        DeleteInt();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game ...");
        Application.Quit();
    }

    public void DeleteInt()
    {
        PlayerPrefs.DeleteKey("amount");
    }
}
