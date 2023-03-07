using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistantObjectsLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Global", LoadSceneMode.Additive);
    }
}
