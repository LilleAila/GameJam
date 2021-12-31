using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    // public GameObject volumeSlider;
    // public GameObject FOVSlider;

    public string MainMenuScene = "Menu";
    public string VolumeScene = "Volume";
    public string FOVScene = "FOV";
    public string SensitivityScene = "Sensitivity";
    public string ControlsScene = "Controls";

    public static float FOV = 60;
    public static float masterVolume = 0;
    // public static int difficulty = 3;
    public static int Sensitivity = 500;

    private void Awake()
    {
        // FOV = PlayerPrefs.GetFloat("FOV");
        // masterVolume = PlayerPrefs.GetFloat("masterVolume");
        // Sensitivity = PlayerPrefs.GetInt("sensitivity");

        if (PlayerPrefs.HasKey("FOV")) FOV = PlayerPrefs.GetFloat("FOV");
        if (PlayerPrefs.HasKey("masterVolume")) masterVolume = PlayerPrefs.GetFloat("masterVolume");
        if (PlayerPrefs.HasKey("sensitivity")) Sensitivity = PlayerPrefs.GetInt("sensitivity");
    }

    private void Start()
    {
        audioMixer.SetFloat("volume", SettingsMenu.masterVolume);
    }

    /*private void OnEnable()
    {
        volumeSlider.GetComponent<Slider>().value = masterVolume;

        FOVSlider.GetComponent<Slider>().value = FOV;
    }*/

    // Volume
    public void openVolume() {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene(VolumeScene);
    }

    /*public void SetVolume(float volume)
    {
        masterVolume = volume;
        audioMixer.SetFloat("volume", masterVolume);
    }*/

    // FOV
    public void openFOV()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene(FOVScene);
    }

    /*public void SetFOV(float fov)
    {
        FOV = fov;
    }*/

    public void openSensitivity()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene(SensitivityScene);
    }

    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene(MainMenuScene);
    }

    public void ControlsMenu()
    {
        FindObjectOfType<AudioManager>().Play("click");
        SceneManager.LoadScene(ControlsScene);
    }
}
