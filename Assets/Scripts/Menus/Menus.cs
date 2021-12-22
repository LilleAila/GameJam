using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour {
    public string MenuScene = "Menu";

    public GameObject PauseMenuUI;
    public GameObject InvUI;
    public GameObject CraftingUI;

    public InventoryObject inventory;

    private bool menuOpen = false;

    public bool GameIsPaused = false;
    public bool invOpen = false;
    public bool craftingOpen = false;

    public GameObject inventoryGameObject;

    // Update is called once per frame
    void Update()
    {
        if(InputManager.GetKeyDown("Pause"))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                if(!menuOpen) Pause();
            }
            if(invOpen)
            {
                closeInv();
            }
            if(craftingOpen)
            {
                closeCrafting();
            }
        }

        if(InputManager.GetKeyDown("Inventory"))
        {
            if (invOpen)
            {
                closeInv();
            } else
            {
                if (!menuOpen) openInv();
            }
            if (craftingOpen)
            {
                closeCrafting();
            }
        }
    }

    // Lock and unlock game
    public void Lock()
    {
        menuOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        inventory.Save();
    }

    public void Unlock()
    {
        menuOpen = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        inventory.Save();
    }

    // Pause Menu
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Lock();
    }

    public void Pause()
    {
        PauseMenuUI.SetActive(true);
        GameIsPaused = true;
        Unlock();
    }

    // Inventory
    public void closeInv()
    {
        InvUI.SetActive(false);
        invOpen = false;
        Lock();
    }

    public void openInv()
    {
        InvUI.SetActive(true);
        invOpen = true;
        Unlock();
    }

    // Crafting
    public void openCrafting()
    {
        if (menuOpen) return;
        CraftingUI.SetActive(true);
        craftingOpen = true;
        Unlock();
    }

    public void closeCrafting()
    {
        CraftingUI.SetActive(false);
        craftingOpen = false;
        Lock();
    }

    // Load menu scene and set time back to 1
    public void LoadMenu()
    {
        foreach(Transform child in inventoryGameObject.transform)
        {
            Destroy(child.gameObject);
        }
        inventory.Save();
        Time.timeScale = 1f;
        SceneManager.LoadScene(MenuScene);
    }

    // Close All
    public void CloseAll() {
        closeCrafting();
        closeInv();
        Resume();
    }

    // Quit game
    public void QuitGame()
    {
        inventory.Save();
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
