using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string GameScene = "Game";
    public string OptionsScene = "Options";

    public void PlayGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(GameScene);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void Options()
    {
        SceneManager.LoadScene(OptionsScene);
    }
}
