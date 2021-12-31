using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenOptionsMenu : MonoBehaviour
{
    public string OptionsMenuScene = "Options";

    public void Options()
    {
        FindObjectOfType<AudioManager>().Play("click");
        PlayerPrefs.Save();
        SceneManager.LoadScene(OptionsMenuScene);
    }
}
