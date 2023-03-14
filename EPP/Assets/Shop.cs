using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject interact;
    public GameObject interactor;
    private MainUI ui;

    public string weapon;
    
    public bool interactOn;


    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("MainUI").GetComponent<MainUI>();
        interactor.SetActive(true);
        interactOn = false;
        GameManager.gameManager.machinePistolStore = false;
        GameManager.gameManager.subMachineGunStore = false;
        GameManager.gameManager.assaultRifleStore = false;
        GameManager.gameManager.shotgunStore = false;
    }

    void Update()
    {
        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && GameManager.gameManager.storeEnabled == false && weapon == "shotgun")
        {
            ui.Store();
            GameManager.gameManager.shotgunStore = true;
        }
        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && GameManager.gameManager.storeEnabled == false && weapon == "machine pistol")
        {
            ui.Store();
            GameManager.gameManager.machinePistolStore = true;
        }
        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && GameManager.gameManager.storeEnabled == false && weapon == "sub machine gun")
        {
            ui.Store();
            GameManager.gameManager.subMachineGunStore = true;
        }
        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && GameManager.gameManager.storeEnabled == false && weapon == "assault rifle")
        {
            ui.Store();
            GameManager.gameManager.assaultRifleStore = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            interact.SetActive(true);
            interactOn = true;
        }
        
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        interact.SetActive(false);
        interactOn = false;
    }
}
