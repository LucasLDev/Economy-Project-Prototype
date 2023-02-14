using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool hasSave = false;
   public void NewGame()
   {
        hasSave = true;
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("MainScene");
        
   }

   public void LoadGame()
   {
        if (hasSave != false)
        {
            SceneManager.LoadScene("MainScene");
        }
   }

   public void DeleteSave()
   {
        PlayerPrefs.DeleteAll();
   }

   public void Information()
   {
        SceneManager.LoadScene("Information");
   }

   public void Quit()
   {
        Application.Quit();
   }
}
