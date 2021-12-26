using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [Range(0.1f, 10)] public float maxHealth;
    float health;
    bool inTrigger = false;

    public GameObject treeObject;
    // public GameObject woodItem;
    public ItemObject woodItemObject;
    public ItemObject squirrelItemObject;
    bool canChop = true;
    public int itemYOffset = 1;
    public InventoryObject inventory;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerAtkRange")
        {
            // Debug.Log("Collided with axe");
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PlayerAtkRange")
        {
            // Debug.Log("Not colliding with axe");
            inTrigger = false;
        }
    }

    private void Update()
    {
        if(inTrigger)
        {
            if(ChopTree.chopping && canChop)
            {
                health -= ChopTree.staticAtk * Time.deltaTime;
            }
            if(health <= 0 && canChop)
            {
                treeObject.GetComponent<Rigidbody>().isKinematic = false;
                // Instantiate(woodItem, this.transform.position + new Vector3(0, itemYOffset, 0), Quaternion.identity);
                inventory.AddItem(new Item(woodItemObject), Mathf.FloorToInt(Random.Range(1f, 7f)));
                if(Mathf.FloorToInt(Random.Range(1f, 10f)) == 5)
                {
                    inventory.AddItem(new Item(squirrelItemObject), 1);
                }
                canChop = false;

                Invoke("DisableGravity", 5);
                Destroy(this.gameObject, 60.0f);
            }
        }
    }

    void DisableGravity()
    {
        treeObject.GetComponent<Rigidbody>().useGravity = false;
    }
}
