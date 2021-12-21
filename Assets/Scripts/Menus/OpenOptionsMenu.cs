using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenOptionsMenu : MonoBehaviour
{
    public string OptionsMenuScene = "Options";

    public void Options()
    {
        SceneManager.LoadScene(OptionsMenuScene);
    }
}
