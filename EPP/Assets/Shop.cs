using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public StoreMenu store;
    public GameManager gameManager;
    public GameObject interact;
    public GameObject interactor;
    public GameObject shopUI;

    public string weapon;
    
    public bool interactOn;


    void Start()
    {
        interactor.SetActive(true);
        interactOn = false;
        gameManager.machinePistolStore = false;
        gameManager.subMachineGunStore = false;
        gameManager.assaultRifleStore = false;
        gameManager.shotgunStore = false;
    }

    void Update()
    {
        shopUI = GameObject.FindGameObjectWithTag("ShopUI");
        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && gameManager.storeEnabled == false && weapon == "shotgun")
        {
            store.Store();
            gameManager.shotgunStore = true;
        }
        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && gameManager.storeEnabled == false && weapon == "machine pistol")
        {
            store.Store();
            gameManager.machinePistolStore = true;
        }
        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && gameManager.storeEnabled == false && weapon == "sub machine gun")
        {
            store.Store();
            gameManager.subMachineGunStore = true;
        }
        if (interactOn == true && Input.GetKeyDown(KeyCode.F) && gameManager.storeEnabled == false && weapon == "assault rifle")
        {
            store.Store();
            gameManager.assaultRifleStore = true;
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
