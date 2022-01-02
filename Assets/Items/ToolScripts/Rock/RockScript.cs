using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    [Range(0.1f, 10)] public float maxHealth;
    float health;
    bool inTrigger = false;
    public int minDrop = 1;
    public int maxDrop = 4;

    public ItemObject stoneItemObject;
    public InventoryObject inventory;

    bool soundPlaying = false;

    // bool wasInTrigger = false;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerMiningRange")
        {
            // Debug.Log("Collideing");
            inTrigger = true;
            FindObjectOfType<AudioManager>().Stop("mineRock");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerMiningRange")
        {
            // Debug.Log("Not colliding");
            inTrigger = false;
            // wasInTrigger = false;
            FindObjectOfType<AudioManager>().Stop("mineRock");
        }
    }

    private void Update()
    {
        /* if (inTrigger && !FindObjectOfType<AudioManager>().getSound("mineRock").playingLoop && MineRock.mining)
            StartCoroutine(FindObjectOfType<AudioManager>().PlayLoop("mineRock", 0.5f, 0.1f)); */

        if(inTrigger)
        {
            // if (inTrigger && MineRock.mining && !FindObjectOfType<AudioManager>().getSound("mineRock").coroutineRunning) StartCoroutine(FindObjectOfType<AudioManager>().PlayLoop("mineRock", 0.5f, 0.1f));
            // else FindObjectOfType<AudioManager>().Stop("mineRock");

            if (MineRock.mining && !soundPlaying)
            {
                FindObjectOfType<AudioManager>().Play("mineRock");
                soundPlaying = true;
            }
            else if (!MineRock.mining)
            {
                FindObjectOfType<AudioManager>().Stop("mineRock");
                soundPlaying = false;
            }
        }

        if (inTrigger && MineRock.mining)
        {
            health -= MineRock.staticAtk * Time.deltaTime;
            if (health <= 0)
            {
                inventory.AddItem(new Item(stoneItemObject), Mathf.FloorToInt(Random.Range((float)minDrop, (float)maxDrop)));
                FindObjectOfType<AudioManager>().Stop("mineRock");
                FindObjectOfType<AudioManager>().Play("lastRockMine");
                Destroy(this.gameObject);
            }
        }
    }
}
