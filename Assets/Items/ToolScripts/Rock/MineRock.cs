using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineRock : MonoBehaviour
{
    [Range(0, 10)] public float atkPerSecond = 0;
    public static float staticAtk;
    public static bool mining = false;

    private void Start()
    {
        staticAtk = atkPerSecond;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.GetKeyDown("Attack"))
        {
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().Play("MineRock");
            mining = true;
        }
        else if (InputManager.GetKeyUp("Attack"))
        {
            GetComponent<Animator>().speed = 0;
            mining = false;
            FindObjectOfType<AudioManager>().Stop("mineRock");
        }
    }
}
