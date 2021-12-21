using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTree : MonoBehaviour
{
    public static bool chopping = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Left Mouse"))
        {
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().Play("ChopTree");
            chopping = true;
        }
        else if (Input.GetButtonUp("Left Mouse"))
        {
            GetComponent<Animator>().speed = 0;
            chopping = false;
        }
    }
}
