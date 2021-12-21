using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    bool colliding = false;
    public GameObject treeObject;
    // public GameObject woodItem;
    public ItemObject woodItemObject;
    bool canChop = true;
    public int itemYOffset = 1;
    public InventoryObject inventory;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Axe")
        {
            // Debug.Log("Collided with axe");
            colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Axe")
        {
            // Debug.Log("Not colliding with axe");
            colliding = false;
        }
    }

    private void Update()
    {
        if(colliding)
        {
            if(ChopTree.chopping && canChop)
            {
                treeObject.GetComponent<Rigidbody>().isKinematic = false;
                // Instantiate(woodItem, this.transform.position + new Vector3(0, itemYOffset, 0), Quaternion.identity);
                inventory.AddItem(new Item(woodItemObject), Mathf.FloorToInt(Random.Range(1f, 7f)));
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
