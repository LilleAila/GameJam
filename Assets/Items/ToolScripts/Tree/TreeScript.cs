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

    // bool wasInTrigger = false;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerMiningRange")
        {
            // Debug.Log("Collided with axe");
            inTrigger = true;
            FindObjectOfType<AudioManager>().Stop("chopTree");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PlayerMiningRange")
        {
            // Debug.Log("Not colliding with axe");
            inTrigger = false;
            // wasInTrigger = false;
            FindObjectOfType<AudioManager>().Stop("chopTree");
        }
    }

    private void Update()
    {
        if(inTrigger)
        {
            if (inTrigger && ChopTree.chopping) StartCoroutine(FindObjectOfType<AudioManager>().PlayLoop("chopTree", 0.5f, 0.1f));
            else FindObjectOfType<AudioManager>().Stop("chopTree");

            if (ChopTree.chopping && canChop)
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

                Destroy(gameObject.GetComponent<TreeScript>());
                treeObject.layer = 12;

                // Invoke("DisableGravity", 5);
                Destroy(this.gameObject, 60.0f);
            }
        }
    }

    void DisableGravity()
    {
        treeObject.GetComponent<Rigidbody>().useGravity = false;
    }
}
