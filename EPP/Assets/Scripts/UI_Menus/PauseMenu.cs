using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Space]
    public GameManager gameManager;
    [Space]
    public GameObject pauseMenuUI;
    public GameObject healthBar;
    public GameObject fuelDisplay;
    public GameObject remainingZombiesCounter;
    [Space]

    public MainMenu mainMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameManager.inDialogue != true)
        {
            if (gameManager.gameIsPaused)
            {
                Resume();
            } else if(gameManager.storeEnabled == false && gameManager.inDialogue == false)   
            {

                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        remainingZombiesCounter.SetActive(true);
        healthBar.SetActive(true);
        fuelDisplay.SetActive(true);
        Time.timeScale = 1f;
        gameManager.gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        healthBar.SetActive(false);
        fuelDisplay.SetActive(false);
        remainingZombiesCounter.SetActive(false);
        Time.timeScale = 0f;
        gameManager.gameIsPaused = true;
    }

    public void LoadMenu()
    {
        //mainMenu.hasSave = true;
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        gameManager.gameIsPaused = false;
        //SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game ...");
        Application.Quit();
    }

}
