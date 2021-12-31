using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string GameScene = "Game";
    public string OptionsScene = "Options";

    private void Start()
    {
        // FindObjectOfType<AudioManager>().Play("music");
    }

    public void PlayGame()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(GameScene);
        Menus.menuOpen = false;
        Menus.GameIsPaused = false;
        Menus.invOpen = false;
        Menus.craftingOpen = false;
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("click");
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void Options()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene(OptionsScene);
    }
}
