using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public GameObject HealthBar;
    public GameObject GameOver;
    public static float hp = 1;
    public Menus menus;
    bool dead = false;
    public GameObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        // hp = 1;
        DisplayHealth();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
    }

    public void Respawn()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        menus.CloseAll();
        menus.Lock();
        GameOver.SetActive(false);
        dead = false;
        foreach(Transform child in inventory.transform)
        {
            Destroy(child.gameObject);
        }
        inventory.GetComponent<DisplayInventory>().CreateDisplay();
        hp = 1;
        SceneManager.LoadScene(0);
        SceneManager.LoadScene(buildIndex);
    }

    void DisplayHealth()
    {
        if (dead) return;
        if(hp <= 0)
        {
            menus.CloseAll();
            menus.Unlock();
            GameOver.SetActive(true);
            dead = true;
        }
        HealthBar.GetComponent<Slider>().value = hp;
    }
}
