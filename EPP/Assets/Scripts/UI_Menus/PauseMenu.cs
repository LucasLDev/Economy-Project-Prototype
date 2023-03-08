using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject HUD;
    public GameObject fuelDisplay;
    public GameObject remainingZombiesCounter;
    [Space]

    public MainMenu mainMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.gameManager.inDialogue != true)
        {
            if (GameManager.gameManager.gameIsPaused)
            {
                Resume();
            } else if(GameManager.gameManager.storeEnabled == false && GameManager.gameManager.inDialogue == false)   
            {

                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        remainingZombiesCounter.SetActive(true);
        HUD.SetActive(true);
        fuelDisplay.SetActive(true);
        Time.timeScale = 1f;
        GameManager.gameManager.gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        HUD.SetActive(false);
        fuelDisplay.SetActive(false);
        remainingZombiesCounter.SetActive(false);
        Time.timeScale = 0f;
        GameManager.gameManager.gameIsPaused = true;
    }

    public void LoadMenu()
    {
        //mainMenu.hasSave = true;
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        GameManager.gameManager.gameIsPaused = false;
        //SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game ...");
        Application.Quit();
    }

}
