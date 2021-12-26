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

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAtkRange")
        {
            // Debug.Log("Collideing");
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerAtkRange")
        {
            // Debug.Log("Not colliding");
            inTrigger = false;
        }
    }

    private void Update()
    {
        if (inTrigger && MineRock.mining)
        {
            health -= MineRock.staticAtk * Time.deltaTime;
            if (health <= 0)
            {
                inventory.AddItem(new Item(stoneItemObject), Mathf.FloorToInt(Random.Range((float)minDrop, (float)maxDrop)));
                Destroy(this.gameObject);
            }
        }
    }
}
