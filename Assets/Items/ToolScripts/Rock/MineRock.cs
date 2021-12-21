using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineRock : MonoBehaviour
{
    public static bool mining = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Left Mouse"))
        {
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().Play("MineRock");
            mining = true;
        }
        else if (Input.GetButtonUp("Left Mouse"))
        {
            GetComponent<Animator>().speed = 0;
            mining = false;
        }
    }
}
