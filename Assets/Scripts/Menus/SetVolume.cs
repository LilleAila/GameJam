using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    [Range(1, 5)]public int index = 1;
    public float speed = 25f;
    public GameObject head;
    bool cubeClicked = false;

    public AudioMixer audioMixer;

    private void Start()
    {
        float masterVolume = SettingsMenu.masterVolume;
        float cubeIndex = (masterVolume + 100) / 20;
        float xPosition = cubeIndex * 15 - 45;

        head.transform.position = new Vector3(xPosition, head.transform.position.y, head.transform.position.z);
    }

    private void OnMouseDown()
    {
        if (head.transform.position == transform.position) return;

        cubeClicked = true;

        SettingsMenu.masterVolume = index * 20 - 100;
        audioMixer.SetFloat("volume", SettingsMenu.masterVolume);

        PlayerPrefs.SetFloat("masterVolume", SettingsMenu.masterVolume);
    }

    private void Update()
    {
        if (cubeClicked)
            head.transform.position = Vector3.MoveTowards(head.transform.position, transform.position, Time.deltaTime * speed);

        if (head.transform.position == transform.position)
            cubeClicked = false;
    }
}
