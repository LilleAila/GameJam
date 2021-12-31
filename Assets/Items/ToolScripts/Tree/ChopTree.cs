using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTree : MonoBehaviour
{
    [Range(0, 10)] public float atkPerSecond = 0;
    public static float staticAtk;
    public static bool chopping = false;

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
            GetComponent<Animator>().Play("ChopTree");
            chopping = true;
        }
        else if (InputManager.GetKeyUp("Attack"))
        {
            GetComponent<Animator>().speed = 0;
            chopping = false;
            FindObjectOfType<AudioManager>().Stop("chopTree");
        }
    }
}
