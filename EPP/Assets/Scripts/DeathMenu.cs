using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public MainMenu _mainmenu;
  public void ToMenu()
  {
    _mainmenu.hasSave = false;
    PlayerPrefs.DeleteAll();
    SceneManager.LoadScene("MainMenu");
  }

  public void Quit()
  {
    Application.Quit();
  }
}
