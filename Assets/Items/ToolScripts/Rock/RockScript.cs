using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    bool colliding = false;
    // public GameObject rockObject;
    // public GameObject stoneItem;
    public ItemObject stoneItemObject;
    bool canMine = true;
    public InventoryObject inventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Pickaxe")
        {
            // Debug.Log("Collideing");
            colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Pickaxe")
        {
            // Debug.Log("Not colliding");
            colliding = false;
        }
    }

    private void Update()
    {
        if (colliding)
        {
            if (MineRock.mining && canMine)
            {
                // rockObject.GetComponent<Rigidbody>().isKinematic = false;
                // Instantiate(stoneItem, this.transform.position, Quaternion.identity);
                inventory.AddItem(new Item(stoneItemObject), Mathf.FloorToInt(Random.Range(1f, 4f)));
                canMine = false;
                Destroy(this.gameObject);
            }
        }
    }
}
