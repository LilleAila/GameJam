using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    public static bool soundPlaying = false;
    bool wasInTrigger = false;
    
    // Update is called once per frame
    void Update()
    {
        bool inTrigger = GetComponent<EnemyScript>().inTrigger;

        if (!soundPlaying && inTrigger)
        {
            FindObjectOfType<AudioManager>().Play("meow");
            soundPlaying = true;
        } else if(!inTrigger && wasInTrigger)
        {
            Stop();
        }

        wasInTrigger = inTrigger;
    }

    public void Stop()
    {
        FindObjectOfType<AudioManager>().Stop("meow");
        soundPlaying = false;
    }
}
