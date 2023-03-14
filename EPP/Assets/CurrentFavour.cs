using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentFavour : MonoBehaviour
{
    public static CurrentFavour instance;
    public Favour favour;
    private MainUI ui;
    
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        } else {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("MainUI").GetComponent<MainUI>();
    }

    void Update()
    {
        if(favour.isActive == true)
        {
            GameManager.gameManager.favourActive = true;
        }
        ui.trackerDesc.SetText("" + favour.title);
    }
}
