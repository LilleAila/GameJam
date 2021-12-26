using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemy : MonoBehaviour
{
    [Range(0, 10)]public float atkPerSecond = 0;
    public static float staticAtk;
    public static bool attacking = false;

    private void Start()
    {
        staticAtk = atkPerSecond;
    }

    private void Update()
    {
        if(InputManager.GetKeyDown("Attack"))
        {
            GetComponent<Animator>().speed = 1;
            GetComponent<Animator>().Play("Sword");
            attacking = true;
        }

        if (InputManager.GetKeyUp("Attack")) {
            GetComponent<Animator>().speed = 0;
            attacking = false;
        }
    }
}
