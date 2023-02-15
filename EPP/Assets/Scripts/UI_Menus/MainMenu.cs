using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool hasSave = false;
    public GameObject information;
    public GameObject controlsScreen;
    public GameObject informationScreen;
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
      hasSave = false;
      PlayerPrefs.DeleteAll();
   }


   public void Quit()
   {
     Application.Quit();
   }

   public void InformationMenu()
   {
     information.SetActive(true);
   }

   public void Controls()
   {
     controlsScreen.SetActive(true);
   }

   public void Info()
   {
     informationScreen.SetActive(true);
   }

   public void Back()
   {
     information.SetActive(false);
     informationScreen.SetActive(false);
     controlsScreen.SetActive(false);
   }
}
