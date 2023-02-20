using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Space]
    public static bool GameIsPaused = false;
    [Space]
    public GameObject pauseMenuUI;
    public GameObject healthBar;
    public GameObject currencyDisplay;
    [Space]
    public Currency currency;
    public Health _health;
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
        healthBar.SetActive(true);
        currencyDisplay.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        healthBar.SetActive(false);
        currencyDisplay.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        mainMenu.hasSave = true;
        PlayerPrefs.Save();
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
